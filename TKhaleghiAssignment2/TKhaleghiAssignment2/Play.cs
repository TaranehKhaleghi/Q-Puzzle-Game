using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * Assignment 2
 * Revision History:
 *     Taraneh Khaleghi, 2018-10-18: Created
 *     Taraneh Khaleghi, 2018-10-18: UI Designed
 *     Taraneh Khaleghi, 2018-10-23: Bug Fixed 
 */

namespace TKhaleghiAssignment2
{
    public partial class Play : Form
    {
        public Play()
        {
            InitializeComponent();
        }
        private const int INIT_TOP = 20;
        private const int INIT_LEFT = 20;
        private const int WIDTH = 48;
        private const int HEIGHT = 48;

        Grides[,] tile;
        Grides.ObstacleType currentTool;
        int currentType;
        Grides currentTile;
        int currentRow = 0;
        int currentCol = 0;

        int numOfRow = 0; 
        int numOfColumn = 0;
        int boxNumber = 0;
        int numOfMov = 0; //it counts number of movement whenever user clicks on the arrow keys

        //open the dialog box to select the level of the game
        private void loadGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult r = openFileDialog1.ShowDialog();

            switch (r)
            {
                case DialogResult.None:
                    break;
                case DialogResult.OK:
                    string fileName = openFileDialog1.FileName;
                    load(fileName);
                    MessageBox.Show("The level is loaded");
                    break;
                case DialogResult.Cancel:
                    break;
                case DialogResult.Abort:
                    break;
                case DialogResult.Retry:
                    break;
                case DialogResult.Ignore:
                    break;
                case DialogResult.Yes:
                    break;
                case DialogResult.No:
                    break;
                default:
                    break;
            }
        }
        // load the game by reading the text file and initialize each line
        //(row, column, and the image type) to the array in the nested loop
        public void load(string fileName)
        {
            this.panel1.Controls.Clear();
            try
            {
                StreamReader reader = new StreamReader(fileName);
                 numOfRow = int.Parse(reader.ReadLine());
                 numOfColumn = int.Parse(reader.ReadLine());
                 tile = new Grides[numOfRow, numOfColumn];

                int x = INIT_LEFT;
                int y = INIT_TOP;

                for (int i = 0; i < numOfRow; i++)
                {
                    for (int j = 0; j < numOfColumn; j++)
                    {
                        tile[i, j] = new Grides();
                        tile[i,j].row = int.Parse(reader.ReadLine());
                        tile[i,j].column = int.Parse(reader.ReadLine());
                        tile[i,j].type = int.Parse(reader.ReadLine());
                        
                        switch (tile[i,j].type)
                        {
                            case 0:
                                tile[i, j].Image = TKhaleghiAssignment2.Properties.Resources.None;
                                break;
                            case 1:
                                tile[i, j].Image = TKhaleghiAssignment2.Properties.Resources.Wall;
                                break;
                            case 2:
                                tile[i, j].Image =   TKhaleghiAssignment2.Properties.Resources.BlueDoor;
                                break;
                            case 3:
                                tile[i, j].Image = TKhaleghiAssignment2.Properties.Resources.RedDoor;
                                break;
                            case 4:
                                tile[i, j].Image = TKhaleghiAssignment2.Properties.Resources.GreenDoor;
                                break;
                            case 5:
                                tile[i, j].Image = TKhaleghiAssignment2.Properties.Resources.YellowDoor;
                                break;
                            case 6:
                                tile[i, j].Image = TKhaleghiAssignment2.Properties.Resources.BlueBox;
                                break;
                            case 7:
                                tile[i, j].Image = TKhaleghiAssignment2.Properties.Resources.RedBox;
                                break;
                            case 8:
                                tile[i, j].Image = TKhaleghiAssignment2.Properties.Resources.GreenBox;
                                break;
                            case 9:
                                tile[i, j].Image = TKhaleghiAssignment2.Properties.Resources.YellowBox;
                                break;
                        }
                        tile[i, j].Left = x;
                        tile[i, j].Top = y;
                        tile[i, j].Width = WIDTH;
                        tile[i, j].Height = HEIGHT;
                        tile[i, j].Visible = true;
                        tile[i, j].SizeMode = PictureBoxSizeMode.StretchImage;
                        this.panel1.Controls.Add(tile[i, j]);
                        tile[i, j].Click += Tile_Click;
                      
                        x += WIDTH;
                    }
                    x = INIT_LEFT;
                    y += HEIGHT;
                }
                reader.Close();
            }
            catch (Exception error) 
            {
                MessageBox.Show(error.Message);
            }
        }

        //by clicking on each tile take the number of row, column, and type of the tile
        // if it is a box. Then, keep related image in the currentTool
        private void Tile_Click(object sender, EventArgs e)
        {
            Grides mytile = sender as Grides;
            currentTile = mytile;
            currentRow = currentTile.row;
            currentCol = currentTile.column;
            currentType = tile[currentRow,currentCol].type;
            switch (currentType)
            {
                case 6:
                    currentTool = Grides.ObstacleType.blueBox;
                    break;
                case 7:
                    currentTool = Grides.ObstacleType.redBox;
                    break;
                case 8:
                    currentTool = Grides.ObstacleType.greenBox;
                    break;
                case 9:
                    currentTool = Grides.ObstacleType.yellowBox;
                    break;
                default:
                    
                    MessageBox.Show("You should select just a box");
                    break;
            }
        }

        //by clicking on up button, if the selected tile is a box, it moves
        // the box if the top tile is empty. If next tile is a door with the same color, it calls the remove method
        private void buttonUp_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentType==6||currentType==7||currentType==8||currentType==9)
                {
                    numOfMov += 1;
                    textBoxNumMov.Text = numOfMov.ToString();
                    while (tile[currentRow - 1, currentCol].type == 0)
                    {
                        currentTile.Top -= HEIGHT;
                        currentTile.BringToFront();
                        tile[currentRow - 1, currentCol].type = tile[currentRow, currentCol].type;
                        tile[currentRow, currentCol].type = 0;
                        currentTile.row = currentTile.row - 1;
                        currentRow = currentRow - 1;
                    }
                }
                              
                switch (currentTool)
                {
                    case Grides.ObstacleType.blueBox:
                        if (tile[currentRow - 1, currentCol].type == 2)
                        {
                            Remove(currentTile);
                        }
                        break;
                    case Grides.ObstacleType.redBox:
                        if (tile[currentRow - 1, currentCol].type == 3)
                        {
                            Remove(currentTile);
                        }
                        break;
                    case Grides.ObstacleType.greenBox:
                        if (tile[currentRow - 1, currentCol].type == 4)
                        {
                            Remove(currentTile);
                        }
                        break;
                    case Grides.ObstacleType.yellowBox:
                        if (tile[currentRow - 1, currentCol].type == 5)
                        {
                            Remove(currentTile);
                        }
                        break;
                }              
            }
            catch (Exception)
            {
                MessageBox.Show("Select a box");
            }
        }
        //by clicking on down button, if the selected tile is a box, it moves
        // the box if the down tile is empty. If next tile is a door with the same color, it calls the remove method
        private void buttonDown_Click(object sender, EventArgs e)
         {
            try {
                if (currentType == 6 || currentType == 7 || currentType == 8 || currentType == 9)
                {
                    numOfMov += 1;
                    textBoxNumMov.Text = numOfMov.ToString();

                    while (tile[currentRow + 1, currentCol].type == 0)
                    {
                        currentTile.Top += HEIGHT;
                        currentTile.BringToFront();
                        tile[currentRow + 1, currentCol].type = tile[currentRow, currentCol].type;
                        tile[currentRow, currentCol].type = 0;
                        currentTile.row = currentTile.row + 1;
                        currentRow = currentRow + 1;
                    }
                }              
                switch (currentTool)
                {
                    case Grides.ObstacleType.blueBox:
                        if (tile[currentRow + 1, currentCol].type == 2)
                        {
                            Remove(currentTile);
                        }
                        break;
                    case Grides.ObstacleType.redBox:
                        if (tile[currentRow + 1, currentCol].type == 3)
                        {
                            Remove(currentTile);
                        }
                        break;
                    case Grides.ObstacleType.greenBox:
                        if (tile[currentRow + 1, currentCol].type == 4)
                        {
                            Remove(currentTile);
                        }
                        break;
                    case Grides.ObstacleType.yellowBox:
                        if (tile[currentRow + 1, currentCol].type == 5)
                        {
                            Remove(currentTile);
                        }
                        break;
                } 
            }
            catch (Exception)
            {
                MessageBox.Show("Select a box");
            }
        }
        //by clicking on left button, if the selected tile is a box, it moves
        // the box if the left tile is empty. If next tile is a door with the same color, it calls the remove method
        private void buttonLeft_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentType == 6 || currentType == 7 || currentType == 8 || currentType == 9)
                {
                    numOfMov += 1;
                    textBoxNumMov.Text = numOfMov.ToString();

                    while (tile[currentRow, currentCol - 1].type == 0)
                    {
                        currentTile.Left -= WIDTH;
                        currentTile.BringToFront();
                        tile[currentRow, currentCol - 1].type = tile[currentRow, currentCol].type;
                        tile[currentRow, currentCol].type = 0;
                        currentTile.column = currentTile.column - 1;
                        currentCol = currentCol - 1;

                    }
                }               
                switch (currentTool)
                {
                    case Grides.ObstacleType.blueBox:
                        if (tile[currentRow, currentCol - 1].type == 2)
                        {
                            Remove(currentTile);
                        }
                        break;
                    case Grides.ObstacleType.redBox:
                        if (tile[currentRow, currentCol - 1].type == 3)
                        {
                            Remove(currentTile);
                        }
                        break;
                    case Grides.ObstacleType.greenBox:
                        if (tile[currentRow, currentCol - 1].type == 4)
                        {
                            Remove(currentTile);
                        }
                        break;
                    case Grides.ObstacleType.yellowBox:
                        if (tile[currentRow, currentCol - 1].type == 5)
                        {
                            Remove(currentTile);
                        }
                        break;
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Select a box");
            }
        }
        //by clicking on right button, if the selected tile is a box, it moves
        // the box if the right tile is empty. If next tile is a door with the same color, it calls the remove method
        private void buttonRight_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentType == 6 || currentType == 7 || currentType == 8 || currentType == 9)
                {
                    numOfMov += 1;
                    textBoxNumMov.Text = numOfMov.ToString();

                    while (tile[currentRow, currentCol + 1].type == 0)
                    {
                        currentTile.Left += WIDTH;
                        currentTile.BringToFront();
                        tile[currentRow, currentCol + 1].type = tile[currentRow, currentCol].type;
                        tile[currentRow, currentCol].type = 0;
                        currentTile.column = currentTile.column + 1;
                        currentCol = currentCol + 1;

                    }
                }
                switch (currentTool)
                {
                    case Grides.ObstacleType.blueBox:
                        if (tile[currentRow, currentCol + 1].type == 2)
                        {
                            Remove(currentTile);
                        }
                        break;
                    case Grides.ObstacleType.redBox:
                        if (tile[currentRow, currentCol + 1].type == 3)
                        {
                            Remove(currentTile);
                        }
                        break;
                    case Grides.ObstacleType.greenBox:
                        if (tile[currentRow, currentCol + 1].type == 4)
                        {
                            Remove(currentTile);
                        }
                        break; 
                    case Grides.ObstacleType.yellowBox:
                        if (tile[currentRow, currentCol + 1].type == 5)
                        {
                            Remove(currentTile);
                        }
                        break;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Select a box");
            }        
         }

        // if next tile is a door withe same color of the box, 
        //it removes the tile from the panel, and it calls the BoxNumbers method 
        public void Remove (Grides currentTile)
        {
            this.panel1.Controls.Remove(currentTile);
            tile[currentRow,currentCol].type = 0;
            boxNumber = BoxNumbers(numOfRow, numOfColumn);
            if (boxNumber == 0)
            {
                MessageBox.Show("Congratulation!");
                this.panel1.Controls.Clear();
                textBoxNumMov.Text = "";
            }
        }

        //it counts the number of boxes
        //by going through the tiles, if there is no box, game is finished
        public int BoxNumbers (int r, int col)
         {
            int boxNum = 0;
            for (int i = 0; i < r; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (tile[i, j].type == 6|| tile[i, j].type == 7
                        || tile[i, j].type == 8 || tile[i, j].type == 9)
                    {
                        boxNum++;
                    }                   
                }
            }
            return boxNum;
         }
       
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

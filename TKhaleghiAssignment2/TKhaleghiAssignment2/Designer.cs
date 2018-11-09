using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

/*
 * Assignment 2
 * Revision History:
 *     Taraneh Khaleghi, 2018-10-18: Created
 *     Taraneh Khaleghi, 2018-: UI Designed
 *     Taraneh Khaleghi, 2018-: Bug Fixed 
 */

namespace TKhaleghiAssignment2
{
    /// <summary>
    /// A class that creats an aaray of pictureboxes, fill
    /// them with the images and save them
    /// </summary>
    public partial class Designer : Form
    {

        public Designer()
        {
            InitializeComponent();
        }
        private const int TOP = 100;
        private const int LEFT = 150;
        private const int WIDTH = 48;
        private const int HEIGHT = 48;
        Grides[,] tile;
        Grides.ObstacleType currentTool;
        /// <summary>
        /// The click event handler to generate buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                int row = Convert.ToInt16(textBoxRow.Text);
                int column = Convert.ToInt16(textBoxColumn.Text);
                tile = new Grides[row, column];

                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < column; j++)
                    {
                        tile[i, j] = new Grides();
                        tile[i, j].Location = new Point(j * WIDTH + LEFT, i * HEIGHT + TOP);
                        tile[i, j].Width = WIDTH;
                        tile[i, j].Height = HEIGHT;
                        tile[i, j].Visible = true;
                        tile[i, j].BorderStyle = BorderStyle.Fixed3D;
                        tile[i, j].SizeMode = PictureBoxSizeMode.StretchImage;
                        tile[i, j].BringToFront();
                        this.Controls.Add(tile[i, j]);
                        tile[i, j].Click += Tile_Click;
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }
        /// <summary>
        /// especify images(enum) and the number of them to each grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tile_Click(object sender, EventArgs e)
        {
            Grides myImage = sender as Grides;

            switch (currentTool)
            {
                case Grides.ObstacleType.None:
                    myImage.Image = TKhaleghiAssignment2.Properties.Resources.None;
                    myImage.Tag = 0;
                    break;
                case Grides.ObstacleType.Wall:
                    myImage.Image = TKhaleghiAssignment2.Properties.Resources.Wall;
                    myImage.Tag = 1;
                    break;
                case Grides.ObstacleType.blueDoor:
                    myImage.Image = TKhaleghiAssignment2.Properties.Resources.BlueDoor;
                    myImage.Tag = 2;
                    break;
                case Grides.ObstacleType.redDoor:
                    myImage.Image = TKhaleghiAssignment2.Properties.Resources.RedDoor;
                    myImage.Tag = 3;
                    break;
                case Grides.ObstacleType.greenDoor:
                    myImage.Image = TKhaleghiAssignment2.Properties.Resources.GreenDoor;
                    myImage.Tag = 4;
                    break;
                case Grides.ObstacleType.yellowDoor:
                    myImage.Image = TKhaleghiAssignment2.Properties.Resources.YellowDoor;
                    myImage.Tag = 5;
                    break;
                case Grides.ObstacleType.blueBox:
                    myImage.Image = TKhaleghiAssignment2.Properties.Resources.BlueBox;
                    myImage.Tag = 6;
                    break;
                case Grides.ObstacleType.redBox:
                    myImage.Image = TKhaleghiAssignment2.Properties.Resources.RedBox;
                    myImage.Tag = 7;
                    break;
                case Grides.ObstacleType.greenBox:
                    myImage.Image = TKhaleghiAssignment2.Properties.Resources.GreenBox;
                    myImage.Tag = 8;
                    break;
                case Grides.ObstacleType.yellowBox:
                    myImage.Image = TKhaleghiAssignment2.Properties.Resources.YellowBox;
                    myImage.Tag = 9;
                    break;
            }
        }
        /// <summary>
        /// Saves the file
        /// </summary>
        /// <param name="fileName">The name of the file</param>
        private void save(string fileName)
        {
            int row = Convert.ToInt16(textBoxRow.Text);
            int column = Convert.ToInt16(textBoxColumn.Text);
            try
            {
                StreamWriter writer = new StreamWriter(fileName);
                writer.WriteLine(row);
                writer.WriteLine(column);

                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < column; j++)
                    {
                        writer.WriteLine(i);
                        writer.WriteLine(j);
                        if (tile[i, j].Tag == null)
                        {
                            writer.WriteLine("0");
                        }
                        else
                        {
                            writer.WriteLine(tile[i, j].Tag);
                        }
                    }
                }
                writer.Close();
                MessageBox.Show("Your file is saved");
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }
        /// <summary>
        /// Oben the dialogue box to save the file 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult r = saveFileDialog1.ShowDialog();

            switch (r)
            {
                case DialogResult.None:
                    break;
                case DialogResult.OK:
                    string fileName = saveFileDialog1.FileName;
                    save(fileName);
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
        /// <summary>
        /// add the clicked image (from the images list) to the currentTool
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonNone_Click(object sender, EventArgs e)
        {
            currentTool = Grides.ObstacleType.None;
        }

        private void buttonWall_Click(object sender, EventArgs e)
        {
            currentTool = Grides.ObstacleType.Wall;
        }

        private void buttonBlueDoor_Click(object sender, EventArgs e)
        {
            currentTool = Grides.ObstacleType.blueDoor;
        }

        private void buttonRedDoor_Click(object sender, EventArgs e)
        {
            currentTool = Grides.ObstacleType.redDoor;
        }

        private void buttonGreenDoor_Click(object sender, EventArgs e)
        {
            currentTool = Grides.ObstacleType.greenDoor;
        }

        private void buttonYellowDoor_Click(object sender, EventArgs e)
        {
            currentTool = Grides.ObstacleType.yellowDoor;
        }

        private void buttonBlueBox_Click(object sender, EventArgs e)
        {
            currentTool = Grides.ObstacleType.blueBox;
        }

        private void buttonRedBox_Click(object sender, EventArgs e)
        {
            currentTool = Grides.ObstacleType.redBox;
        }

        private void buttonGreenBox_Click(object sender, EventArgs e)
        {
            currentTool = Grides.ObstacleType.greenBox;
        }

        private void buttonYellowBox_Click(object sender, EventArgs e)
        {
            currentTool = Grides.ObstacleType.yellowBox;
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

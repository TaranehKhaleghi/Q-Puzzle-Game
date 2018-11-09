using System;
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
   
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// By clicking the Design button it Goes to the game Design window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void designButton_Click(object sender, EventArgs e)
        {
            Designer designForm = new Designer();
            designForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        //by clicking on the play button, the new form will show
        private void playButton_Click(object sender, EventArgs e)
        {
            Play playForm = new Play();
            playForm.Show();
        }
    }
}

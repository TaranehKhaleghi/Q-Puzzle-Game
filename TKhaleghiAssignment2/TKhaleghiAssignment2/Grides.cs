using System;
using System.Collections.Generic;
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
    /// <summary>
    /// Create inherited class from picture box
    /// </summary>
    public class Grides : PictureBox
    {
        public int row = 0;
        public int column = 0;
        public int type;
        /// <summary>
        /// list of images name that will be used as an abstacle
        /// </summary>
        public enum ObstacleType
        {
            None,
            Wall,
            blueDoor,
            redDoor,
            greenDoor,
            yellowDoor,
            blueBox,
            redBox,
            greenBox,
            yellowBox
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessApp_3._0
{
    public partial class Board : UserControl
    {
        public int xPos;
        public int yPos;

        public Board()
        {
            InitializeComponent();
        }

        private void Board_Load(object sender, EventArgs e)
        { }

        private void Board_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void Board_DragDrop(object sender, DragEventArgs e)
        {

        }


        private void Board_MouseClick(object sender, MouseEventArgs e)
        {
            if (Global.boardCod[yPos, xPos] != 0 || Global.clicked)
            {
                if (!Global.clicked)
                {
                    Global.clicked = true;
                    Global.clickStr = yPos.ToString() + xPos.ToString();
                }
                else
                {
                    Global.clicked = false;
                    Global.clickStr = yPos.ToString() + xPos.ToString();
                }
                Form1.RenderPiceOnboard();
            }
        }

        private void Board_Load_1(object sender, EventArgs e)
        {

        }
    }
}

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
        public bool clicked = false;
        ChessEngine.MoveCode mossaGrafic = new ChessEngine.MoveCode();

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
            if (Global.boardCod[yPos, xPos] != 0 || clicked)
            {
                if (!clicked)
                {
                    clicked = true;
                    mossaGrafic.xPartenza = (byte)xPos;
                    mossaGrafic.yPartenza = (byte)yPos;
                }
                else
                {
                    clicked = false;
                    mossaGrafic.xArrivo = (byte)xPos;
                    mossaGrafic.yArrivo = (byte)yPos;
                }
                Form1.RenderPiceOnboard();
            }
           // Convalidea_move(mossaGrafic);
        }


    }
}

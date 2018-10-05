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
            if (Global.boardCod[yPos, xPos] != 0 || BoardShared.clicked)
            {
                if (!BoardShared.clicked)
                {
                    BoardShared.clicked = true;
                    mossaGrafic.xPartenza = (byte)xPos;
                    mossaGrafic.yPartenza = (byte)yPos;
                    BoardShared.moveX = xPos;
                    BoardShared.moveY = yPos;

                }
                else
                {
                    BoardShared.clicked = false;
                    mossaGrafic.xArrivo = (byte)xPos;
                    mossaGrafic.yArrivo = (byte)yPos;
                    Global.boardCod[yPos,xPos]= Global.boardCod[BoardShared.moveY,BoardShared.moveX];
                    Global.boardCod[BoardShared.moveY,BoardShared.moveX]= 0;
                }
                Form1.RenderPiceOnboard();
            }

           // Convalidea_move(mossaGrafic);
        }

    }
    public static class BoardShared
    {
        public static bool clicked = false;
        public static int moveY;
        public static int moveX;
    }

}

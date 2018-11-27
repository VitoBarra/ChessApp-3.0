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
        MoveCode move;

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
            if (Global.engine.boardcode[yPos, xPos] != 0 || BoardShared.clicked)
            {
                if (!BoardShared.clicked)
                {
                    BoardShared.clicked = true;
                    BoardShared.movedX = xPos;
                    BoardShared.movedY = yPos;

                }
                else
                {
                  
                    if (yPos != BoardShared.movedY || xPos != BoardShared.movedX)
                    {
                        BoardShared.clicked = false;
                        move = new MoveCode();
                        move.xArrivo = (byte)xPos;
                        move.yArrivo = (byte)yPos;
                        move.xPartenza = (byte)BoardShared.movedX;
                        move.yPartenza = (byte)BoardShared.movedY;

                        Global.engine.Make_move(move);
                        Form1.RenderPiceOnboard();


                        int a = 0;

                        int if_checkmate = Global.engine.MinMaxTree(false, ref a, 0, 1);

                        if (if_checkmate == 10001)
                        {
                            MessageBox.Show("Checkmate! \n White wins!");
                            Global.engine.StampaBitBoard();
                        }
                        else if (if_checkmate == -10001)
                        {
                            MessageBox.Show("Checkmate! \n Black wins!");
                            Global.engine.StampaBitBoard();
                        }





                        int eval = Global.engine.MinMaxTree(false, ref a, 0, 4);

                        MessageBox.Show(eval.ToString());

                        Global.engine.Make_move(Global.engine.mosse_pos[a]);

                        Form1.RenderPiceOnboard();

                        if_checkmate = Global.engine.MinMaxTree(true, ref a, 0, 2);

                        if (if_checkmate == 10001) MessageBox.Show("Checkmate! \n White wins!");
                        else if (if_checkmate == -10001) MessageBox.Show("Checkmate! \n Black wins!");

                       
                    }
                    else
                    {
                        
                    }
                    //Global.engine.ConvalidateMove(mossaGrafic);
                }
                
            }

        }

    }
    public static class BoardShared
    {
        public static bool clicked = false;
        public static int movedY;
        public static int movedX;
    }

}

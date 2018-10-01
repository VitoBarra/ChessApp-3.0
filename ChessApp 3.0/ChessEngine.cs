using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessApp_3._0
{
    public class ChessEngine
    {
        int[,] boardCode;
        int[,] bitBoard =
          { { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 }};


        public ChessEngine(int[,] _boardCode)
        {
            boardCode = _boardCode;
            loadDebug();
        }


        void loadDebug()
        {
            BitBoardGenerator();
            StampaBitBoard();
        }





        void CanMove()
        {

        }
        void StampaBitBoard()
        {
            string bitBoardStr = "";
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    bitBoardStr += bitBoard[y, x].ToString() + " ";
                }
                bitBoardStr += "\n";
            }

            MessageBox.Show(bitBoardStr);

        }
        public void BitBoardGenerator()
        {
            for (int y = 0; y < 8; y++)
                for (int x = 0; x < 8; x++)
                {
                    switch (boardCode[y, x])
                    {
                        case 1: break;
                        case -1: break;
                        case 4: ToweMove(y, x); break;
                        case -4: break;
                        case 3: KnightMove(y, x); break;
                        case -3: break;
                        case 2: BishopMove(y, x); break;
                        case -2: break;
                        case 5: QueenMove(y, x); break;
                        case -5: break;
                        case 6: break;
                        case -6: break;
                    }
                }
        }



        #region ---------------------------------------PiceMove---------------------------------------
        public void ToweMove(int yBoard, int xBoard)
        {
            for (int y = yBoard + 1; y < 8; y++)
                if (boardCode[y, xBoard] != 0)
                {
                    bitBoard[y, xBoard]++;
                    break;
                }

            for (int y = yBoard - 1; y >= 0; y--)
                if (boardCode[y, xBoard] != 0)
                {
                    bitBoard[y, xBoard]++;
                    break;
                }

            for (int x = xBoard + 1; x < 8; x++)
                if (boardCode[yBoard, x] != 0)
                {
                    bitBoard[yBoard, x]++;
                    break;
                }

            for (int x = xBoard - 1; x >= 0; x--)
                if (boardCode[yBoard, x] != 0)
                {
                    bitBoard[yBoard, x]++;
                    break;
                }
        }

        public void BishopMove(int yBoard, int xBoard)
        {
            for (int y = yBoard + 1, x = xBoard + 1; y < 8 && x < 8; y++, x++)
                if (boardCode[y, x] != 0) 
                {
                    bitBoard[y, x]++;
                    break;
                }
            for (int y = yBoard - 1, x = xBoard - 1; y >= 0 && x >= 0; y--, x--)
                if (boardCode[y, x] != 0)
                {
                    bitBoard[y, x]++;
                    break;
                }

            for (int y = yBoard - 1, x = xBoard + 1; y >= 0 && x < 8; y--, x++)
                if (boardCode[y, x] != 0)
                {
                    bitBoard[y, x]++;
                    break;
                }
            for (int y = yBoard + 1, x = xBoard - 1; y < 8 && x >= 0; y++, x--)
                if (boardCode[y, x] != 0)
                {
                    bitBoard[y, x]++;
                    break;
                }
        }

        public void QueenMove(int yBoard, int xBoard) 
        {
            ToweMove(yBoard,xBoard);
            BishopMove(yBoard, xBoard);
        }

        public void KnightMove(int yBoard, int xBoard)
        {
            if (yBoard < 7)
            {
                if (xBoard < 6)
                    bitBoard[yBoard + 1, xBoard + 2]++;
                if (xBoard > 1)
                    bitBoard[yBoard + 1, xBoard - 2]++;
            }
            if (yBoard < 6)
            {
                if (xBoard < 7)
                    bitBoard[yBoard + 2, xBoard + 1]++;
                if (xBoard > 0)
                    bitBoard[yBoard + 2, xBoard - 1]++;
            }
            if (yBoard > 0)
            {
                if (xBoard < 6)
                    bitBoard[yBoard - 1, xBoard + 2]++;
                if (xBoard > 1)
                    bitBoard[yBoard - 1, xBoard - 2]++;
            }
            if (yBoard > 1)
            {
                if (xBoard < 7)
                    bitBoard[yBoard - 2, xBoard + 1]++;
                if (xBoard > 0)
                    bitBoard[yBoard - 2, xBoard - 1]++;
            }

        }
        #endregion




        }
}

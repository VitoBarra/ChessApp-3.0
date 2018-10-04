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
        class MoveCode
        {
            public byte xPartenza = 8;
            public byte yPartenza = 8;
            public byte xArrivo = 8;
            public byte yArrivo = 8;

            public bool cattura = false;
            public byte promozione = 0;
            public byte arrocco = 0;
        } 

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


        public ChessEngine(int[,] _boardcode)
        {
            boardcode = _boardcode;
            loadDebug();
        }


        void loadDebug()
        {
            // BitBoardGenerator(true);
            RookMove(4, 5);
            StampaBitBoard();
        }


        void LoadDebug()
        {
            MoveCode culo = new MoveCode { };

           

            MessageBox.Show(culo.arrocco.ToString());
            //BitBoardGenerator();
            //StampaBitBoard();
        }





        void CanMove()
        {

        }
        void StampaBitBoard()
        {
            string bitboardStr = "";
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    bitboardStr += bitboard[y, x].ToString() + " ";
                }
                bitboardStr += "\n";
            }

            MessageBox.Show(bitboardStr);

        }


        public void BitBoardGenerator(bool iswhite)
        {
            for (int y = 0; y < 8; y++)
                for (int x = 0; x < 8; x++)
                {
                    switch (boardcode[y, x])
                    {
                        case 0: break;
                        case 1: break;
                        case -1: break;
                        case 4: RookMove(y, x); break;
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



        #region ---------------------------------------PieceMove---------------------------------------
        public void RookMove(int yBoard, int xBoard)
        {
            for (int y = yBoard + 1; y < 8; y++)
            {
                if (boardcode[y, xBoard] != 0)
                {
                    bitboard[y, xBoard]++;
                    break;
                }
                bitboard[y, xBoard]++;
            }



        #region ---------------------------------------PiceMove---------------------------------------
        public void RookMove(int yBoard, int xBoard)
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

            for (int x = xBoard + 1; x < 8; x++) { 
                if (boardcode[yBoard, x] != 0)
                {
                        bitboard[yBoard, x]++;
                        break;
                    }
                    bitboard[yBoard, x]++;
                }

            for (int x = xBoard - 1; x >= 0; x--)
            {
                if (boardcode[yBoard, x] != 0)

                {
                    bitboard[yBoard, x]++;
                    break;
                }
                bitboard[yBoard, x]++;
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
            RookMove(yBoard, xBoard);
            BishopMove(yBoard, xBoard);
        }

        public void KnightMove(int yBoard, int xBoard)
        {
            if (yBoard < 7)
            {
                if (xBoard < 6)
                    bitboard[yBoard + 1, xBoard + 2]++;
                if (xBoard > 1)
                    bitboard[yBoard + 1, xBoard - 2]++;
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
                    bitboard[yBoard - 1, xBoard + 2]++;
                if (xBoard > 1)
                    bitboard[yBoard - 1, xBoard - 2]++;
            }
            if (yBoard > 1)
            {
                if (xBoard < 7)
                    bitboard[yBoard - 2, xBoard + 1]++;
                if (xBoard > 0)
                    bitboard[yBoard - 2, xBoard - 1]++;
            }

        }

        public void PawnMoveWhite(int yBoard, int xBoard)
        {
            if (xBoard != 0)
            {
                bitboard[yBoard - 1, xBoard - 1]++;
            }

            if (xBoard != 7)
            {
                bitboard[yBoard - 1, xBoard + 1]++;
            }
        }

        public void PawnMoveBlack(int yBoard, int xBoard)
        {
            if (xBoard != 0)
            {
                bitboard[yBoard + 1, xBoard - 1]++;
            }

            if (xBoard != 7)
            {
                bitboard[yBoard + 1, xBoard + 1]++;
            }
        }

        public void KingMove(int yBoard, int xBoard)
        {
            if (yBoard != 0)
            {
                if (xBoard != 0)
                {
                    bitboard[yBoard - 1, xBoard - 1]++;
                }
                if (xBoard != 7)
                {
                    bitboard[yBoard - 1, xBoard + 1]++;

                }
                bitboard[yBoard - 1, xBoard]++;
            }
            if (yBoard != 7)
            {

                if (xBoard != 0)
                {
                    bitboard[yBoard + 1, xBoard - 1]++;
                }
                if (xBoard != 7)
                {
                    bitboard[yBoard + 1, xBoard + 1]++;

                }
                bitboard[yBoard + 1, xBoard]++;
            }

            if (xBoard != 0) bitboard[yBoard, xBoard - 1]++;
            if (xBoard != 7) bitboard[yBoard, xBoard + 1]++;



        }
        #endregion


    }
}

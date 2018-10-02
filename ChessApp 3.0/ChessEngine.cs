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
        int[,] boardcode =
            { { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 }};
        int[,] bitboard =
            { { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 }};

        //int[,] bitboardforwblack =
        //    { { 0,0,0,0,0,0,0,0 },
        //    { 0,0,0,0,0,0,0,0 },
        //    { 0,0,0,0,0,0,0,0 },
        //    { 0,0,0,0,0,0,0,0 },
        //    { 0,0,0,0,0,0,0,0 },
        //    { 0,0,0,0,0,0,0,0 },
        //    { 0,0,0,0,0,0,0,0 },
        //    { 0,0,0,0,0,0,0,0 }};




        public ChessEngine(int[,] _boardcode)
        {
            boardcode = _boardcode;
            RookMove(4, 5);
            StampaBitBoard();
        }

        void StampaBitBoard()
        {
            string bitboardStr = "";
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    bitboardStr += bitboard[x, y].ToString() + " ";
                }
                bitboardStr += "\n";
            }

            MessageBox.Show(bitboardStr);

        }




        public void bit_board_generation(bool isblack)
        {

            int piece_for_bit;
            if (!isblack)
            {
                for (int index1 = 0, index2 = 0; index1 < 8 && index2 < 8; index1++, index2++)
                {
                        piece_for_bit = bitboard[index1, index2];
                        if (piece_for_bit > 0)
                        {
                            // fai lo switch qui
                        }
                    }
            }
            
            





        }


        public void bitboardUpdate()
        {

        }


        #region ---------------------------------------PieceMove---------------------------------------
        //public void RookMove(int yBoard, int xBoard)
        //{
        //    for (int y = yBoard; y < 8; y++)
        //    {
        //        if (bitboard[y, xBoard] == 0)
        //            bitboard[y, xBoard]++;
        //    }
        //    for (int y = yBoard; y > 0; y--)
        //    {
        //        if (bitboard[y, xBoard] == 0)
        //            bitboard[y, xBoard]++;
        //    }
        //    for (int x = xBoard; x < 8; x++)
        //    {
        //        if (bitboard[yBoard, x] == 0)
        //            bitboard[yBoard, x]++;
        //    }
        //    for (int x = xBoard; x > 0; x--)
        //    {
        //        if (bitboard[yBoard, x] == 0)
        //            bitboard[yBoard, x]++;
        //    }

        //}

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

        public void RookMove(int yBoard, int xBoard)
        {
            for (int y = yBoard + 1; y < 8; y++)
            {
                if (boardcode[y, xBoard] != 0)
                {
                    bitboard[y, xBoard]++;
                    break;
                }
                if (y == 5)
                {
                    MessageBox.Show("HEy I'm here");
                }
                bitboard[y, xBoard]++;
            }
            for (int y = yBoard - 1; y >= 0; y--)
            {
                if (boardcode[y, xBoard] != 0)
                {
                    bitboard[y, xBoard]++;
                    break;
                }
                bitboard[y, xBoard]++;
            }

            for (int x = xBoard + 1; x < 8; x++)
            {
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
                if (boardcode[y, x] != 0)
                {
                    bitboard[y, x]++;
                    break;
                }
            for (int y = yBoard - 1, x = xBoard - 1; y >= 0 && x >= 0; y--, x--)
                if (boardcode[y, x] != 0)
                {
                    bitboard[y, x]++;
                    break;
                }

            for (int y = yBoard - 1, x = xBoard + 1; y >= 0 && x < 8; y--, x++)
                if (boardcode[y, x] != 0)
                {
                    bitboard[y, x]++;
                    break;
                }
            for (int y = yBoard + 1, x = xBoard - 1; y < 8 && x >= 0; y++, x--)
                if (boardcode[y, x] != 0)
                {
                    bitboard[y, x]++;
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
                    bitboard[yBoard + 2, xBoard + 1]++;
                if (xBoard > 0)
                    bitboard[yBoard + 2, xBoard - 1]++;
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
        #endregion






        public void MoveGenerator()
        {
            for (int y = 0, x = 0; y< 8 && x < 8; y++, x++)
                
                {
                    switch (boardcode[y, x])
                    {
                        case 1: break;
                        case -1: break;
                        case 4: RookMove(y, x); break;
                        case -4:  break;
                        case 3: break;
                        case -3: break;
                        case 2: break;
                        case -2:  break;
                        case 5: break;
                        case -5:  break;
                        case 6:  break;
                        case -6:  break;
                    }





                }
        }
        public  void GenerateTree()
        {

        }
        


    }
}

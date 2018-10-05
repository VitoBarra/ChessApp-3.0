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
        public class MoveCode
        {
            public byte xPartenza = 8;
            public byte yPartenza = 8;
            public byte xArrivo = 8;
            public byte yArrivo = 8;

            public bool cattura = false;
            public SByte promozione = 0;
            public byte arrocco = 0;
        }


        int[,] boardcode;
        int[,] bitboard =
          { { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 }};

        MoveCode[] mosse;
        int indexmossa = new int();


        public ChessEngine(int[,] _boardcode)
        {
            boardcode = _boardcode;
            LoadDebug();

        }





        void LoadDebug()
        {
            int kingx = 0;
            int kingy = 0;
            BitBoardGenerator(true, ref kingx, ref kingy);
            StampaBitBoard();
        }


        public void GenerazioneMosse(bool iswhite)
        {
            if (iswhite)
            {
                mosse = new MoveCode[100];
                indexmossa = 0;
                int kingx = 0;
                int kingy = 0;
                BitBoardGenerator(false, ref kingx, ref kingy);

                if (bitboard[kingy, kingx] > 1)
                {
                    KingMoveReal((byte)(kingy), (byte)(kingx), true);
                    return;
                }
                else
                {
                    KingMoveReal((byte)(kingy), (byte)(kingx), true);



                }
            }
        }


        void CanMove() { }


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


        public void BitBoardGenerator(bool iswhite, ref int kingex, ref int kingey)
        {
            bitboard = new int[,]
            { { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 }};

            if (iswhite)
            {
                for (int y = 0; y < 8; y++)
                    for (int x = 0; x < 8; x++)
                    {
                        switch (boardcode[y, x])
                        {
                            case 0: break;
                            case 1: PawnMoveWhite(y, x); break;
                            case 4: RookMove(y, x); break;
                            case 3: KnightMove(y, x); break;
                            case 2: BishopMove(y, x); break;
                            case 5: QueenMove(y, x); break;
                            case 6: KingMove(y, x); kingex = x; kingey = y; break;
                        }
                    }
            }
            else
            {
                for (int y = 0; y < 8; y++)
                    for (int x = 0; x < 8; x++)
                    {
                        switch (boardcode[y, x])
                        {
                            case 0: break;
                            case -1: PawnMoveBlack(y, x); break;
                            case -4: RookMove(y, x); break;
                            case -3: KnightMove(y, x); break;
                            case -2: BishopMove(y, x); break;
                            case -5: QueenMove(y, x); break;
                            case -6: KingMove(y, x); kingex = x; kingey = y; break;
                        }
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

            for (int y = yBoard - 1; y >= 0; y--)
            {
                if (boardcode[y, xBoard] != 0)
                {
                    bitboard[y, xBoard]++;
                    break;
                }
                bitboard[y, xBoard]++;
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
            {
                if (boardcode[y, x] != 0)
                {
                    bitboard[y, x]++;
                    break;
                }
                bitboard[y, x]++;
            }
            for (int y = yBoard - 1, x = xBoard - 1; y >= 0 && x >= 0; y--, x--)
            {
                if (boardcode[y, x] != 0)
                {
                    bitboard[y, x]++;
                    break;
                }
                bitboard[y, x]++;
            }

            for (int y = yBoard - 1, x = xBoard + 1; y >= 0 && x < 8; y--, x++)
            {
                if (boardcode[y, x] != 0)
                {
                    bitboard[y, x]++;
                    break;
                }
                bitboard[y, x]++;
            }
            for (int y = yBoard + 1, x = xBoard - 1; y < 8 && x >= 0; y++, x--)
            {
                if (boardcode[y, x] != 0)
                {
                    bitboard[y, x]++;
                    break;
                }
                bitboard[y, x]++;
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


        #region-------------------------------------MOSSE PER GENERAZIONE REALE------------------------------------------

        public void KingMoveReal(byte yBoard, byte xBoard, bool iswhite)
        {
            if (iswhite)
            {
                MoveCode mossa;
                if (yBoard != 0)
                {
                    if (bitboard[yBoard - 1, xBoard] == 0 && boardcode[yBoard - 1, xBoard] <= 0)
                    {

                        mossa = new MoveCode();
                        if (boardcode[yBoard - 1, xBoard] != 0) mossa.cattura = true;
                        mossa.xPartenza = xBoard;
                        mossa.yPartenza = yBoard;
                        mossa.xArrivo = xBoard;
                        mossa.yArrivo = (byte)(yBoard - 1);
                        mosse[indexmossa] = mossa;
                        indexmossa++;
                    }
                    if (xBoard != 0)
                    {
                        if (bitboard[yBoard - 1, xBoard - 1] == 0 && boardcode[yBoard - 1, xBoard - 1] <= 0)
                        {
                            mossa = new MoveCode();
                            if (boardcode[yBoard - 1, xBoard - 1] != 0) mossa.cattura = true;
                            mossa.xPartenza = xBoard;
                            mossa.yPartenza = yBoard;
                            mossa.xArrivo = (byte)(xBoard - 1);
                            mossa.yArrivo = (byte)(yBoard - 1);
                            mosse[indexmossa] = mossa;
                            indexmossa++;
                        }
                    }
                    if (xBoard != 7)
                    {
                        if (bitboard[yBoard - 1, xBoard + 1] == 0 && boardcode[yBoard - 1, xBoard + 1] <= 0)
                        {
                            mossa = new MoveCode();
                            if (boardcode[yBoard - 1, xBoard + 1] != 0) mossa.cattura = true;
                            mossa.xPartenza = xBoard;
                            mossa.yPartenza = yBoard;
                            mossa.xArrivo = (byte)(xBoard + 1);
                            mossa.yArrivo = (byte)(yBoard - 1);
                            mosse[indexmossa] = mossa;
                            indexmossa++;
                        }
                    }
                }

                if (yBoard != 7)
                {
                    if (bitboard[yBoard + 1, xBoard] == 0 && boardcode[yBoard + 1, xBoard] <= 0)
                    {
                        mossa = new MoveCode();
                        if (boardcode[yBoard + 1, xBoard] != 0) mossa.cattura = true;
                        mossa.xPartenza = xBoard;
                        mossa.yPartenza = yBoard;
                        mossa.xArrivo = xBoard;
                        mossa.yArrivo = (byte)(yBoard + 1);
                        mosse[indexmossa] = mossa;
                        indexmossa++;
                    }
                    if (xBoard != 0)
                    {
                        if (bitboard[yBoard + 1, xBoard - 1] == 0 && boardcode[yBoard + 1, xBoard - 1] <= 0)
                        {
                            mossa = new MoveCode();
                            if (boardcode[yBoard + 1, xBoard - 1] != 0) mossa.cattura = true;
                            mossa.xPartenza = xBoard;
                            mossa.yPartenza = yBoard;
                            mossa.xArrivo = (byte)(xBoard - 1);
                            mossa.yArrivo = (byte)(yBoard + 1);
                            mosse[indexmossa] = mossa;
                            indexmossa++;
                        }
                    }
                    if (xBoard != 7)
                    {
                        if (bitboard[yBoard + 1, xBoard + 1] == 0 && boardcode[yBoard + 1, xBoard + 1] <= 0)
                        {
                            mossa = new MoveCode();
                            if (boardcode[yBoard + 1, xBoard + 1] != 0) mossa.cattura = true;
                            mossa.xPartenza = xBoard;
                            mossa.yPartenza = yBoard;
                            mossa.xArrivo = (byte)(xBoard + 1);
                            mossa.yArrivo = (byte)(yBoard + 1);
                            mosse[indexmossa] = mossa;
                            indexmossa++;
                        }
                    }





                }

                if (xBoard != 0)
                {
                    if (bitboard[yBoard, xBoard - 1] == 0 && boardcode[yBoard, xBoard - 1] <= 0)
                    {
                        mossa = new MoveCode();
                        if (boardcode[yBoard, xBoard - 1] != 0) mossa.cattura = true;
                        mossa.xPartenza = xBoard;
                        mossa.yPartenza = yBoard;
                        mossa.xArrivo = (byte)(xBoard - 1);
                        mossa.yArrivo = yBoard;
                        mosse[indexmossa] = mossa;
                        indexmossa++;
                    }
                }

                if (xBoard != 7)
                {
                    if (bitboard[yBoard, xBoard + 1] == 0 && boardcode[yBoard, xBoard + 1] <= 0)
                    {
                        mossa = new MoveCode();
                        if (boardcode[yBoard, xBoard + 1] != 0) mossa.cattura = true;
                        mossa.xPartenza = xBoard;
                        mossa.yPartenza = yBoard;
                        mossa.xArrivo = (byte)(xBoard + 1);
                        mossa.yArrivo = yBoard;
                        mosse[indexmossa] = mossa;
                        indexmossa++;
                    }
                }
            }
            else
            {
                MoveCode mossa;
                if (yBoard != 0)
                {
                    if (bitboard[yBoard - 1, xBoard] == 0 && boardcode[yBoard - 1, xBoard] >= 0)
                    {

                        mossa = new MoveCode();
                        if (boardcode[yBoard - 1, xBoard] != 0) mossa.cattura = true;
                        mossa.xPartenza = xBoard;
                        mossa.yPartenza = yBoard;
                        mossa.xArrivo = xBoard;
                        mossa.yArrivo = (byte)(yBoard - 1);
                        mosse[indexmossa] = mossa;
                        indexmossa++;
                    }
                    if (xBoard != 0)
                    {
                        if (bitboard[yBoard - 1, xBoard - 1] == 0 && boardcode[yBoard - 1, xBoard - 1] >= 0)
                        {
                            mossa = new MoveCode();
                            if (boardcode[yBoard - 1, xBoard - 1] != 0) mossa.cattura = true;
                            mossa.xPartenza = xBoard;
                            mossa.yPartenza = yBoard;
                            mossa.xArrivo = (byte)(xBoard - 1);
                            mossa.yArrivo = (byte)(yBoard - 1);
                            mosse[indexmossa] = mossa;
                            indexmossa++;
                        }
                    }
                    if (xBoard != 7)
                    {
                        if (bitboard[yBoard - 1, xBoard + 1] == 0 && boardcode[yBoard - 1, xBoard + 1] >= 0)
                        {
                            mossa = new MoveCode();
                            if (boardcode[yBoard - 1, xBoard + 1] != 0) mossa.cattura = true;
                            mossa.xPartenza = xBoard;
                            mossa.yPartenza = yBoard;
                            mossa.xArrivo = (byte)(xBoard + 1);
                            mossa.yArrivo = (byte)(yBoard - 1);
                            mosse[indexmossa] = mossa;
                            indexmossa++;
                        }
                    }
                }

                if (yBoard != 7)
                {
                    if (bitboard[yBoard + 1, xBoard] == 0 && boardcode[yBoard + 1, xBoard] >= 0)
                    {
                        mossa = new MoveCode();
                        if (boardcode[yBoard + 1, xBoard] != 0) mossa.cattura = true;
                        mossa.xPartenza = xBoard;
                        mossa.yPartenza = yBoard;
                        mossa.xArrivo = xBoard;
                        mossa.yArrivo = (byte)(yBoard + 1);
                        mosse[indexmossa] = mossa;
                        indexmossa++;
                    }
                    if (xBoard != 0)
                    {
                        if (bitboard[yBoard + 1, xBoard - 1] == 0 && boardcode[yBoard + 1, xBoard - 1] >= 0)
                        {
                            mossa = new MoveCode();
                            if (boardcode[yBoard + 1, xBoard - 1] != 0) mossa.cattura = true;
                            mossa.xPartenza = xBoard;
                            mossa.yPartenza = yBoard;
                            mossa.xArrivo = (byte)(xBoard - 1);
                            mossa.yArrivo = (byte)(yBoard + 1);
                            mosse[indexmossa] = mossa;
                            indexmossa++;
                        }
                    }
                    if (xBoard != 7)
                    {
                        if (bitboard[yBoard + 1, xBoard + 1] == 0 && boardcode[yBoard + 1, xBoard + 1] >= 0)
                        {
                            mossa = new MoveCode();
                            if (boardcode[yBoard + 1, xBoard + 1] != 0) mossa.cattura = true;
                            mossa.xPartenza = xBoard;
                            mossa.yPartenza = yBoard;
                            mossa.xArrivo = (byte)(xBoard + 1);
                            mossa.yArrivo = (byte)(yBoard + 1);
                            mosse[indexmossa] = mossa;
                            indexmossa++;
                        }
                    }





                }

                if (xBoard != 0)
                {
                    if (bitboard[yBoard, xBoard - 1] == 0 && boardcode[yBoard, xBoard - 1] >= 0)
                    {
                        mossa = new MoveCode();
                        if (boardcode[yBoard, xBoard - 1] != 0) mossa.cattura = true;
                        mossa.xPartenza = xBoard;
                        mossa.yPartenza = yBoard;
                        mossa.xArrivo = (byte)(xBoard - 1);
                        mossa.yArrivo = yBoard;
                        mosse[indexmossa] = mossa;
                        indexmossa++;
                    }
                }

                if (xBoard != 7)
                {
                    if (bitboard[yBoard, xBoard + 1] == 0 && boardcode[yBoard, xBoard + 1] >= 0)
                    {
                        mossa = new MoveCode();
                        if (boardcode[yBoard, xBoard + 1] != 0) mossa.cattura = true;
                        mossa.xPartenza = xBoard;
                        mossa.yPartenza = yBoard;
                        mossa.xArrivo = (byte)(xBoard + 1);
                        mossa.yArrivo = yBoard;
                        mosse[indexmossa] = mossa;
                        indexmossa++;
                    }
                }

            }
        }
    


        #endregion

        public void Make_move(MoveCode sasso)
        {
            if (sasso.arrocco == 0)
            {
                boardcode[sasso.yArrivo, sasso.xArrivo] = boardcode[sasso.yPartenza, sasso.xPartenza] + sasso.promozione;
                boardcode[sasso.yPartenza, sasso.xPartenza] = 0;
            }


        }
    }
}

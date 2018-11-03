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


        int[,] boardcode=
        {
         { -4,-3,-2,-6,-5,-2,-3,-4 },
         { -1,-1,-1,-1,-1,-1,-1,-1 },
         { 0,0,0,0,0,0,0,0 },
         { 0,0,0,0,0,0,0,0 },
         { 0,0,0,0,0,0,0,0 },
         { 0,0,0,0,0,0,0,0 },
         { 1,1,1,1,1,1,1,1 },
         { 4,3,2,6,5,2,3,4 }
        };         


        int[,] bitboard =             
          { { 0,0,0,0,0,0,0,0 },      
            { 0,0,0,0,0,0,0,0 },      
            { 0,0,0,0,0,0,0,0 },      
            { 0,0,0,0,0,0,0,0 },      
            { 0,0,0,0,0,0,0,0 },      
            { 0,0,0,0,0,0,0,0 },      
            { 0,0,0,0,0,0,0,0 },      
            { 0,0,0,0,0,0,0,0 }};

        MoveCode[] mosse_pos;
        int indexmossa = 0;



        public ChessEngine()
        {
            loadDebug();
        }
        public ChessEngine(int[,] _boardcode)
        {
            //  boardcode = _boardcode;
            loadDebug();
        }


        


        void loadDebug()
        {
            int kingx = 0;
            int kingy = 0;
            BitBoardGenerator(true, ref kingx, ref kingy);
            //StampaBitBoard();
            indexmossa = 0;
            mosse_pos = new MoveCode[100];
            PawnMoveReal(1, 4, !(true));
            Countmosse();

        }


        public void GenerazioneMosse(bool iswhite)
        {
            if (iswhite)
            {
                mosse_pos = new MoveCode[100];
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

        public void Countmosse()
        {
            int j = 0;
            for (j = 0; j < 100; j++)
            {
                if (mosse_pos[j] == null) break;
            }
            MessageBox.Show((j).ToString());
        }




        void StampaBitBoard() //debug
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
                            case 1: PawnMoveWhiteBitBoard(y, x); break;
                            case 4: RookmMoveBitBoard(y, x); break;
                            case 3: KnightMoveBitBoard(y, x); break;
                            case 2: BishopMoveBitBoard(y, x); break;
                            case 5: QueenMoveBitBoard(y, x); break;
                            case 6: KingMoveBitBoard(y, x); kingex = x; kingey = y; break;
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
                            case -1: PawnMoveBlackBitBoard(y, x); break;
                            case -4: RookmMoveBitBoard(y, x); break;
                            case -3: KnightMoveBitBoard(y, x); break;
                            case -2: BishopMoveBitBoard(y, x); break;
                            case -5: QueenMoveBitBoard(y, x); break;
                            case -6: KingMoveBitBoard(y, x); kingex = x; kingey = y; break;
                        }
                    }
            }
        }



        #region ---------------------------------------PieceMove BitBoard---------------------------------------
        public void RookmMoveBitBoard(int yBoard, int xBoard)
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

        public void BishopMoveBitBoard(int yBoard, int xBoard)
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

        public void QueenMoveBitBoard(int yBoard, int xBoard)
        {
            RookmMoveBitBoard(yBoard, xBoard);
            BishopMoveBitBoard(yBoard, xBoard);
        }

        public void KnightMoveBitBoard(int yBoard, int xBoard)
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

        public void PawnMoveWhiteBitBoard(int yBoard, int xBoard)
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

        public void PawnMoveBlackBitBoard(int yBoard, int xBoard)
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

        public void KingMoveBitBoard(int yBoard, int xBoard)
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
                        mosse_pos[indexmossa] = mossa;
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
                            mosse_pos[indexmossa] = mossa;
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
                            mosse_pos[indexmossa] = mossa;
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
                        mosse_pos[indexmossa] = mossa;
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
                            mosse_pos[indexmossa] = mossa;
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
                            mosse_pos[indexmossa] = mossa;
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
                        mosse_pos[indexmossa] = mossa;
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
                        mosse_pos[indexmossa] = mossa;
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
                        mosse_pos[indexmossa] = mossa;
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
                            mosse_pos[indexmossa] = mossa;
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
                            mosse_pos[indexmossa] = mossa;
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
                        mosse_pos[indexmossa] = mossa;
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
                            mosse_pos[indexmossa] = mossa;
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
                            mosse_pos[indexmossa] = mossa;
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
                        mosse_pos[indexmossa] = mossa;
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
                        mosse_pos[indexmossa] = mossa;
                        indexmossa++;
                    }
                }

            }
        }

        public void RookMoveReal(byte yBoard, byte xBoard, bool iswhite)
        {
            MoveCode mossa;
            if (iswhite)
            {
                for (int a = yBoard + 1; a < 8; a++)
                {
                    if (boardcode[a, xBoard] > 0) break;
                    mossa = new MoveCode();
                    if (boardcode[a, xBoard] < 0)
                    {
                        mossa.cattura = true;
                        mossa.xPartenza = xBoard;
                        mossa.yPartenza = yBoard;
                        mossa.xArrivo = xBoard;
                        mossa.yArrivo = (byte)(a);
                        mosse_pos[indexmossa] = mossa;
                        indexmossa++;
                        break;
                    }
                    mossa.xPartenza = xBoard;
                    mossa.yPartenza = yBoard;
                    mossa.xArrivo = xBoard;
                    mossa.yArrivo = (byte)(a);
                    mosse_pos[indexmossa] = mossa;
                    indexmossa++;
                }
                for (int a = yBoard - 1; a >= 0; a--)
                {
                    if (boardcode[a, xBoard] > 0) break;
                    mossa = new MoveCode();
                    if (boardcode[a, xBoard] < 0)
                    {
                        mossa.cattura = true;
                        mossa.xPartenza = xBoard;
                        mossa.yPartenza = yBoard;
                        mossa.xArrivo = xBoard;
                        mossa.yArrivo = (byte)(a);
                        mosse_pos[indexmossa] = mossa;
                        indexmossa++;
                        break;
                    }
                    mossa.xPartenza = xBoard;
                    mossa.yPartenza = yBoard;
                    mossa.xArrivo = xBoard;
                    mossa.yArrivo = (byte)(a);
                    mosse_pos[indexmossa] = mossa;
                    indexmossa++;
                }
                for (int a = xBoard - 1; a >= 0; a--)
                {
                    if (boardcode[yBoard, a] > 0) break;
                    mossa = new MoveCode();
                    if (boardcode[yBoard, a] < 0)
                    {
                        mossa.cattura = true;
                        mossa.xPartenza = xBoard;
                        mossa.yPartenza = yBoard;
                        mossa.xArrivo = (byte)(a);
                        mossa.yArrivo = xBoard;
                        mosse_pos[indexmossa] = mossa;
                        indexmossa++;
                        break;
                    }
                    mossa.xPartenza = xBoard;
                    mossa.yPartenza = yBoard;
                    mossa.xArrivo = (byte)(a);
                    mossa.yArrivo = xBoard;
                    mosse_pos[indexmossa] = mossa;
                    indexmossa++;
                }
                for (int a = xBoard + 1; a < 8; a++)
                {
                    if (boardcode[yBoard, a] > 0) break;
                    mossa = new MoveCode();
                    if (boardcode[yBoard, a] < 0)
                    {
                        mossa.cattura = true;
                        mossa.xPartenza = xBoard;
                        mossa.yPartenza = yBoard;
                        mossa.xArrivo = (byte)(a);
                        mossa.yArrivo = xBoard;
                        mosse_pos[indexmossa] = mossa;
                        indexmossa++;
                        break;
                    }
                    mossa.xPartenza = xBoard;
                    mossa.yPartenza = yBoard;
                    mossa.xArrivo = (byte)(a);
                    mossa.yArrivo = xBoard;
                    mosse_pos[indexmossa] = mossa;
                    indexmossa++;
                }
            }
            else
            {
                for (int a = yBoard + 1; a < 8; a++)
                {
                    if (boardcode[a, xBoard] < 0) break;
                    mossa = new MoveCode();
                    if (boardcode[a, xBoard] > 0)
                    {
                        mossa.cattura = true;
                        mossa.xPartenza = xBoard;
                        mossa.yPartenza = yBoard;
                        mossa.xArrivo = xBoard;
                        mossa.yArrivo = (byte)(a);
                        mosse_pos[indexmossa] = mossa;
                        indexmossa++;
                        break;
                    }
                    mossa.xPartenza = xBoard;
                    mossa.yPartenza = yBoard;
                    mossa.xArrivo = xBoard;
                    mossa.yArrivo = (byte)(a);
                    mosse_pos[indexmossa] = mossa;
                    indexmossa++;
                }
                for (int a = yBoard - 1; a >= 0; a--)
                {
                    if (boardcode[a, xBoard] < 0) break;
                    mossa = new MoveCode();
                    if (boardcode[a, xBoard] > 0)
                    {
                        mossa.cattura = true;
                        mossa.xPartenza = xBoard;
                        mossa.yPartenza = yBoard;
                        mossa.xArrivo = xBoard;
                        mossa.yArrivo = (byte)(a);
                        mosse_pos[indexmossa] = mossa;
                        indexmossa++;
                        break;
                    }
                    mossa.xPartenza = xBoard;
                    mossa.yPartenza = yBoard;
                    mossa.xArrivo = xBoard;
                    mossa.yArrivo = (byte)(a);
                    mosse_pos[indexmossa] = mossa;
                    indexmossa++;
                }
                for (int a = xBoard - 1; a >= 0; a--)
                {
                    if (boardcode[yBoard, a] < 0) break;
                    mossa = new MoveCode();
                    if (boardcode[yBoard, a] > 0)
                    {
                        mossa.cattura = true;
                        mossa.xPartenza = xBoard;
                        mossa.yPartenza = yBoard;
                        mossa.xArrivo = (byte)(a);
                        mossa.yArrivo = xBoard;
                        mosse_pos[indexmossa] = mossa;
                        indexmossa++;
                        break;
                    }
                    mossa.xPartenza = xBoard;
                    mossa.yPartenza = yBoard;
                    mossa.xArrivo = (byte)(a);
                    mossa.yArrivo = xBoard;
                    mosse_pos[indexmossa] = mossa;
                    indexmossa++;
                }
                for (int a = xBoard + 1; a < 8; a++)
                {
                    if (boardcode[yBoard, a] < 0) break;
                    mossa = new MoveCode();
                    if (boardcode[yBoard, a] > 0)
                    {
                        mossa.cattura = true;
                        mossa.xPartenza = xBoard;
                        mossa.yPartenza = yBoard;
                        mossa.xArrivo = (byte)(a);
                        mossa.yArrivo = xBoard;
                        mosse_pos[indexmossa] = mossa;
                        indexmossa++;
                        break;
                    }
                    mossa.xPartenza = xBoard;
                    mossa.yPartenza = yBoard;
                    mossa.xArrivo = (byte)(a);
                    mossa.yArrivo = xBoard;
                    mosse_pos[indexmossa] = mossa;
                    indexmossa++;
                }
            }
        }

        public void BishopMoveReal(byte yBoard, byte xBoard, bool iswhite)
        {
            if (iswhite)
            {
                MoveCode mossa;
                for (byte y = (byte)(yBoard + 1), x = (byte)(xBoard + 1); y < 8 && x < 8; y++, x++)
                {
                    if (boardcode[y, x] > 0) break;
                    mossa = new MoveCode();
                    mossa.xPartenza = xBoard;
                    mossa.xArrivo = x;
                    mossa.yPartenza = yBoard;
                    mossa.yArrivo = y;
                    if (boardcode[y, x] < 0)
                    {
                        mossa.cattura = true;
                        mosse_pos[indexmossa] = mossa;
                        indexmossa++;
                        break;
                    }
                    mosse_pos[indexmossa] = mossa;
                    indexmossa++;
                }
                for (byte y = (byte)(yBoard - 1), x = (byte)(xBoard - 1); y >= 0 && x >= 0; y--, x--)
                {
                    if (boardcode[y, x] > 0) break;
                    mossa = new MoveCode();
                    mossa.xPartenza = xBoard;
                    mossa.xArrivo = x;
                    mossa.yPartenza = yBoard;
                    mossa.yArrivo = y;
                    if (boardcode[y, x] < 0)
                    {
                        mossa.cattura = true;
                        mosse_pos[indexmossa] = mossa;
                        indexmossa++;
                        break;
                    }
                    mosse_pos[indexmossa] = mossa;
                    indexmossa++;
                }

                for (byte y = (byte)(yBoard - 1), x = (byte)(xBoard + 1); y >= 0 && x < 8; y--, x++)
                {
                    if (boardcode[y, x] > 0) break;
                    mossa = new MoveCode();
                    mossa.xPartenza = xBoard;
                    mossa.xArrivo = x;
                    mossa.yPartenza = yBoard;
                    mossa.yArrivo = y;
                    if (boardcode[y, x] < 0)
                    {
                        mossa.cattura = true;
                        mosse_pos[indexmossa] = mossa;
                        indexmossa++;
                        break;
                    }
                    mosse_pos[indexmossa] = mossa;
                    indexmossa++;
                }
                for (byte y = (byte)(yBoard + 1), x = (byte)(xBoard - 1); y < 8 && x >= 0; y++, x--)
                {
                    if (boardcode[y, x] > 0) break;
                    mossa = new MoveCode();
                    mossa.xPartenza = xBoard;
                    mossa.xArrivo = x;
                    mossa.yPartenza = yBoard;
                    mossa.yArrivo = y;
                    if (boardcode[y, x] < 0)
                    {
                        mossa.cattura = true;
                        mosse_pos[indexmossa] = mossa;
                        indexmossa++;
                        break;
                    }
                    mosse_pos[indexmossa] = mossa;
                    indexmossa++;
                }
            }
            else
            {
                MoveCode mossa;
                for (byte y = (byte)(yBoard + 1), x = (byte)(xBoard + 1); y < 8 && x < 8; y++, x++)
                {
                    if (boardcode[y, x] < 0) break;
                    mossa = new MoveCode();
                    mossa.xPartenza = xBoard;
                    mossa.xArrivo = x;
                    mossa.yPartenza = yBoard;
                    mossa.yArrivo = y;
                    if (boardcode[y, x] > 0)
                    {
                        mossa.cattura = true;
                        mosse_pos[indexmossa] = mossa;
                        indexmossa++;
                        break;
                    }
                    mosse_pos[indexmossa] = mossa;
                    indexmossa++;
                }
                for (byte y = (byte)(yBoard - 1), x = (byte)(xBoard - 1); y >= 0 && x >= 0; y--, x--)
                {
                    if (boardcode[y, x] < 0) break;
                    mossa = new MoveCode();
                    mossa.xPartenza = xBoard;
                    mossa.xArrivo = x;
                    mossa.yPartenza = yBoard;
                    mossa.yArrivo = y;
                    if (boardcode[y, x] > 0)
                    {
                        mossa.cattura = true;
                        mosse_pos[indexmossa] = mossa;
                        indexmossa++;
                        break;
                    }
                    mosse_pos[indexmossa] = mossa;
                    indexmossa++;
                }

                for (byte y = (byte)(yBoard - 1), x = (byte)(xBoard + 1); y >= 0 && x < 8; y--, x++)
                {
                    if (boardcode[y, x] < 0) break;
                    mossa = new MoveCode();
                    mossa.xPartenza = xBoard;
                    mossa.xArrivo = x;
                    mossa.yPartenza = yBoard;
                    mossa.yArrivo = y;
                    if (boardcode[y, x] > 0)
                    {
                        mossa.cattura = true;
                        mosse_pos[indexmossa] = mossa;
                        indexmossa++;
                        break;
                    }
                    mosse_pos[indexmossa] = mossa;
                    indexmossa++;
                }
                for (byte y = (byte)(yBoard + 1), x = (byte)(xBoard - 1); y < 8 && x >= 0; y++, x--)
                {
                    if (boardcode[y, x] < 0) break;
                    mossa = new MoveCode();
                    mossa.xPartenza = xBoard;
                    mossa.xArrivo = x;
                    mossa.yPartenza = yBoard;
                    mossa.yArrivo = y;
                    if (boardcode[y, x] > 0)
                    {
                        mossa.cattura = true;
                        mosse_pos[indexmossa] = mossa;
                        indexmossa++;
                        break;
                    }
                    mosse_pos[indexmossa] = mossa;
                    indexmossa++;
                }
            }
        }

        public void QueenMoveReal(byte yBoard, byte xBoard, bool iswhite)
        {
            BishopMoveReal(yBoard,xBoard,iswhite);
            RookMoveReal(yBoard,xBoard,iswhite);
        }
    
        public void KnightMoveReal(byte yBoard, byte xBoard, bool iswhite)
        {
            MoveCode mossa;
            if (iswhite)
            {
                if (yBoard < 7)
                {
                    if (xBoard < 6)
                    {
                        if (boardcode[yBoard + 1, xBoard + 2] <= 0)
                        {
                            mossa = new MoveCode();
                            mossa.yPartenza = yBoard;
                            mossa.xPartenza = xBoard;
                            mossa.yArrivo = (byte)(yBoard + 1);
                            mossa.xArrivo = (byte)(xBoard + 2);
                            if (boardcode[yBoard + 1, xBoard + 2] != 0) mossa.cattura = true;
                            mosse_pos[indexmossa] = mossa;
                            indexmossa++;
                        }
                    }
                    if (xBoard > 1)
                    {
                        if (boardcode[yBoard + 1, xBoard - 2] <= 0)
                        {
                            mossa = new MoveCode();
                            mossa.yPartenza = yBoard;
                            mossa.xPartenza = xBoard;
                            mossa.yArrivo = (byte)(yBoard + 1);
                            mossa.xArrivo = (byte)(xBoard - 2);
                            if (boardcode[yBoard + 1, xBoard - 2] != 0) mossa.cattura = true;
                            mosse_pos[indexmossa] = mossa;
                            indexmossa++;
                        }
                    }
                }
                if (yBoard < 6)
                {
                    if (xBoard < 7)
                    {
                        if (boardcode[yBoard + 2, xBoard + 1] <= 0)
                        {
                            mossa = new MoveCode();
                            mossa.yPartenza = yBoard;
                            mossa.xPartenza = xBoard;
                            mossa.yArrivo = (byte)(yBoard + 2);
                            mossa.xArrivo = (byte)(xBoard + 1);
                            if (boardcode[yBoard + 2, xBoard + 1] != 0) mossa.cattura = true;
                            mosse_pos[indexmossa] = mossa;
                            indexmossa++;
                        }

                    }
                    if (xBoard > 0)
                    {
                        if (boardcode[yBoard + 2, xBoard - 1] <= 0)
                        {
                            mossa = new MoveCode();
                            mossa.yPartenza = yBoard;
                            mossa.xPartenza = xBoard;
                            mossa.yArrivo = (byte)(yBoard + 2);
                            mossa.xArrivo = (byte)(xBoard - 1);
                            if (boardcode[yBoard + 2, xBoard - 1] != 0) mossa.cattura = true;
                            mosse_pos[indexmossa] = mossa;
                            indexmossa++;
                        }
                    }
                }
                if (yBoard > 0)
                {
                    if (xBoard < 6)
                    {
                        if (boardcode[yBoard - 1, xBoard + 2] <= 0)
                        {
                            mossa = new MoveCode();
                            mossa.yPartenza = yBoard;
                            mossa.xPartenza = xBoard;
                            mossa.yArrivo = (byte)(yBoard - 1);
                            mossa.xArrivo = (byte)(xBoard + 2);
                            if (boardcode[yBoard - 1, xBoard + 2] != 0) mossa.cattura = true;
                            mosse_pos[indexmossa] = mossa;
                            indexmossa++;
                        }

                    }
                    if (xBoard > 1)
                    {

                        if (boardcode[yBoard - 1, xBoard - 2] <= 0)
                        {
                            mossa = new MoveCode();
                            mossa.yPartenza = yBoard;
                            mossa.xPartenza = xBoard;
                            mossa.yArrivo = (byte)(yBoard - 1);
                            mossa.xArrivo = (byte)(xBoard - 2);
                            if (boardcode[yBoard - 1, xBoard - 2] != 0) mossa.cattura = true;
                            mosse_pos[indexmossa] = mossa;
                            indexmossa++;
                        }
                    }
                }
                if (yBoard > 1)
                {
                    if (xBoard < 7)
                    {
                        bitboard[yBoard - 2, xBoard + 1]++;
                        if (boardcode[yBoard - 2, xBoard + 1] <= 0)
                        {
                            mossa = new MoveCode();
                            mossa.yPartenza = yBoard;
                            mossa.xPartenza = xBoard;
                            mossa.yArrivo = (byte)(yBoard - 2);
                            mossa.xArrivo = (byte)(xBoard + 1);
                            if (boardcode[yBoard - 2, xBoard + 1] != 0) mossa.cattura = true;
                            mosse_pos[indexmossa] = mossa;
                            indexmossa++;
                        }
                    }
                    if (xBoard > 0)
                    {
                        bitboard[yBoard - 2, xBoard - 1]++;
                        if (boardcode[yBoard - 2, xBoard - 1] <= 0)
                        {
                            mossa = new MoveCode();
                            mossa.yPartenza = yBoard;
                            mossa.xPartenza = xBoard;
                            mossa.yArrivo = (byte)(yBoard - 2);
                            mossa.xArrivo = (byte)(xBoard - 1);
                            if (boardcode[yBoard - 2, xBoard - 1] != 0) mossa.cattura = true;
                            mosse_pos[indexmossa] = mossa;
                            indexmossa++;
                        }
                    }
                }
            }
            else
            {
                if (yBoard < 7)
                {
                    if (xBoard < 6)
                    {
                        if (boardcode[yBoard + 1, xBoard + 2] >= 0)
                        {
                            mossa = new MoveCode();
                            mossa.yPartenza = yBoard;
                            mossa.xPartenza = xBoard;
                            mossa.yArrivo = (byte)(yBoard + 1);
                            mossa.xArrivo = (byte)(xBoard + 2);
                            if (boardcode[yBoard + 1, xBoard + 2] != 0) mossa.cattura = true;
                            mosse_pos[indexmossa] = mossa;
                            indexmossa++;
                        }
                    }
                    if (xBoard > 1)
                    {
                        if (boardcode[yBoard + 1, xBoard - 2] >= 0)
                        {
                            mossa = new MoveCode();
                            mossa.yPartenza = yBoard;
                            mossa.xPartenza = xBoard;
                            mossa.yArrivo = (byte)(yBoard + 1);
                            mossa.xArrivo = (byte)(xBoard - 2);
                            if (boardcode[yBoard + 1, xBoard - 2] != 0) mossa.cattura = true;
                            mosse_pos[indexmossa] = mossa;
                            indexmossa++;
                        }
                    }
                }
                if (yBoard < 6)
                {
                    if (xBoard < 7)
                    {
                        if (boardcode[yBoard + 2, xBoard + 1] >= 0)
                        {
                            mossa = new MoveCode();
                            mossa.yPartenza = yBoard;
                            mossa.xPartenza = xBoard;
                            mossa.yArrivo = (byte)(yBoard + 2);
                            mossa.xArrivo = (byte)(xBoard + 1);
                            if (boardcode[yBoard + 1, xBoard + 2] != 0) mossa.cattura = true;
                            mosse_pos[indexmossa] = mossa;
                            indexmossa++;
                        }

                    }
                    if (xBoard > 0)
                    {
                        if (boardcode[yBoard + 2, xBoard - 1] >= 0)
                        {
                            mossa = new MoveCode();
                            mossa.yPartenza = yBoard;
                            mossa.xPartenza = xBoard;
                            mossa.yArrivo = (byte)(yBoard + 2);
                            mossa.xArrivo = (byte)(xBoard - 1);
                            if (boardcode[yBoard + 2, xBoard - 1] != 0) mossa.cattura = true;
                            mosse_pos[indexmossa] = mossa;
                            indexmossa++;
                        }
                    }
                }
                if (yBoard > 0)
                {
                    if (xBoard < 6)
                    {
                        if (boardcode[yBoard - 1, xBoard + 2] >= 0)
                        {
                            mossa = new MoveCode();
                            mossa.yPartenza = yBoard;
                            mossa.xPartenza = xBoard;
                            mossa.yArrivo = (byte)(yBoard - 1);
                            mossa.xArrivo = (byte)(xBoard + 2);
                            if (boardcode[yBoard - 1, xBoard + 2] != 0) mossa.cattura = true;
                            mosse_pos[indexmossa] = mossa;
                            indexmossa++;
                        }

                    }
                    if (xBoard > 1)
                    {

                        if (boardcode[yBoard - 1, xBoard - 2] >= 0)
                        {
                            mossa = new MoveCode();
                            mossa.yPartenza = yBoard;
                            mossa.xPartenza = xBoard;
                            mossa.yArrivo = (byte)(yBoard - 1);
                            mossa.xArrivo = (byte)(xBoard - 2);
                            if (boardcode[yBoard - 1, xBoard - 2] != 0) mossa.cattura = true;
                            mosse_pos[indexmossa] = mossa;
                            indexmossa++;
                        }
                    }
                }
                if (yBoard > 1)
                {
                    if (xBoard < 7)
                    {
                        bitboard[yBoard - 2, xBoard + 1]++;
                        if (boardcode[yBoard - 2, xBoard + 1] >= 0)
                        {
                            mossa = new MoveCode();
                            mossa.yPartenza = yBoard;
                            mossa.xPartenza = xBoard;
                            mossa.yArrivo = (byte)(yBoard - 2);
                            mossa.xArrivo = (byte)(xBoard + 1);
                            if (boardcode[yBoard - 2, xBoard + 1] != 0) mossa.cattura = true;
                            mosse_pos[indexmossa] = mossa;
                            indexmossa++;
                        }
                    }
                    if (xBoard > 0)
                    {
                        bitboard[yBoard - 2, xBoard - 1]++;
                        if (boardcode[yBoard - 2, xBoard - 1] >= 0)
                        {
                            mossa = new MoveCode();
                            mossa.yPartenza = yBoard;
                            mossa.xPartenza = xBoard;
                            mossa.yArrivo = (byte)(yBoard - 2);
                            mossa.xArrivo = (byte)(xBoard - 1);
                            if (boardcode[yBoard - 2, xBoard - 1] != 0) mossa.cattura = true;
                            mosse_pos[indexmossa] = mossa;
                            indexmossa++;
                        }
                    }
                }
            }

        }

        public void PawnMoveReal(byte yBoard, byte xBoard, bool iswhite)
        {
            MoveCode mossa;
            if (iswhite)
            {
                if (yBoard == 6)
                {
                    if (boardcode[yBoard - 1, xBoard] == 0 && boardcode[yBoard - 2, xBoard] == 0)
                    {
                        mossa = new MoveCode();
                        mossa.yPartenza = yBoard;
                        mossa.xPartenza = xBoard;
                        mossa.yArrivo = (byte)(yBoard - 2);
                        mossa.xArrivo = xBoard;
                        mosse_pos[indexmossa] = mossa;
                        indexmossa++;
                    }
                }
                if (boardcode[yBoard - 1, xBoard] == 0)
                {
                    mossa = new MoveCode();
                    mossa.yPartenza = yBoard;
                    mossa.xPartenza = xBoard;
                    mossa.yArrivo = (byte)(yBoard - 1);
                    mossa.xArrivo = xBoard;
                    if (yBoard == 1)
                    {
                        mossa.promozione = 1;
                        mosse_pos[indexmossa] = mossa;
                        indexmossa++;
                        mossa.promozione = 2;
                        mosse_pos[indexmossa] = mossa;
                        indexmossa++;
                        mossa.promozione = 3;
                        mosse_pos[indexmossa] = mossa;
                        indexmossa++;
                        mossa.promozione = 4;
                        mosse_pos[indexmossa] = mossa;
                        indexmossa++;
                    }
                    else
                    {
                        mosse_pos[indexmossa] = mossa;
                        indexmossa++;
                    }
                }

                if (xBoard > 0)
                {
                    if (boardcode[yBoard - 1, xBoard - 1] < 0)
                    {
                        mossa = new MoveCode();
                        mossa.yPartenza = yBoard;
                        mossa.xPartenza = xBoard;
                        mossa.yArrivo = (byte)(yBoard - 1);
                        mossa.xArrivo = (byte)(xBoard - 1);
                        mossa.cattura = true;

                        if (yBoard == 1)
                        {
                            mossa.promozione = 1;
                            mosse_pos[indexmossa] = mossa;
                            indexmossa++;
                            mossa.promozione = 2;
                            mosse_pos[indexmossa] = mossa;
                            indexmossa++;
                            mossa.promozione = 3;
                            mosse_pos[indexmossa] = mossa;
                            indexmossa++;
                            mossa.promozione = 4;
                            mosse_pos[indexmossa] = mossa;
                            indexmossa++;
                        }
                        else
                        {
                            mosse_pos[indexmossa] = mossa;
                            indexmossa++;
                        }
                    }
                }
                if (xBoard < 7)
                {

                    if (boardcode[yBoard - 1, xBoard + 1] < 0)
                    {
                        mossa = new MoveCode();
                        mossa.yPartenza = yBoard;
                        mossa.xPartenza = xBoard;
                        mossa.yArrivo = (byte)(yBoard - 1);
                        mossa.xArrivo = (byte)(xBoard + 1);
                        mossa.cattura = true;

                        if (yBoard == 1)
                        {
                            mossa.promozione = 1;
                            mosse_pos[indexmossa] = mossa;
                            indexmossa++;
                            mossa.promozione = 2;
                            mosse_pos[indexmossa] = mossa;
                            indexmossa++;
                            mossa.promozione = 3;
                            mosse_pos[indexmossa] = mossa;
                            indexmossa++;
                            mossa.promozione = 4;
                            mosse_pos[indexmossa] = mossa;
                            indexmossa++;
                        }
                        else
                        {
                            mosse_pos[indexmossa] = mossa;
                            indexmossa++;
                        }
                    }
                }
            }
            else
            {
                if (yBoard == 1)
                {
                    if (boardcode[yBoard + 1, xBoard] == 0 && boardcode[yBoard + 2, xBoard] == 0)
                    {
                        mossa = new MoveCode();
                        mossa.yPartenza = yBoard;
                        mossa.xPartenza = xBoard;
                        mossa.yArrivo = (byte)(yBoard + 2);
                        mossa.xArrivo = xBoard;
                        mosse_pos[indexmossa] = mossa;
                        indexmossa++;
                    }
                }
                if (boardcode[yBoard + 1, xBoard] == 0)
                {
                    mossa = new MoveCode();
                    mossa.yPartenza = yBoard;
                    mossa.xPartenza = xBoard;
                    mossa.yArrivo = (byte)(yBoard + 1);
                    mossa.xArrivo = xBoard;
                    if (yBoard == 6)
                    {
                        mossa.promozione = -1;
                        mosse_pos[indexmossa] = mossa;
                        indexmossa++;
                        mossa.promozione = -2;
                        mosse_pos[indexmossa] = mossa;
                        indexmossa++;
                        mossa.promozione = -3;
                        mosse_pos[indexmossa] = mossa;
                        indexmossa++;
                        mossa.promozione = -4;
                        mosse_pos[indexmossa] = mossa;
                        indexmossa++;
                    }
                    else
                    {
                        mosse_pos[indexmossa] = mossa;
                        indexmossa++;
                    }
                }

                if (xBoard > 0)
                {
                    if (boardcode[yBoard + 1, xBoard - 1] > 0)
                    {
                        mossa = new MoveCode();
                        mossa.yPartenza = yBoard;
                        mossa.xPartenza = xBoard;
                        mossa.yArrivo = (byte)(yBoard + 1);
                        mossa.xArrivo = (byte)(xBoard - 1);
                        mossa.cattura = true;

                        if (yBoard == 6)
                        {
                            mossa.promozione = -1;
                            mosse_pos[indexmossa] = mossa;
                            indexmossa++;
                            mossa.promozione = -2;
                            mosse_pos[indexmossa] = mossa;
                            indexmossa++;
                            mossa.promozione = -3;
                            mosse_pos[indexmossa] = mossa;
                            indexmossa++;
                            mossa.promozione = -4;
                            mosse_pos[indexmossa] = mossa;
                            indexmossa++;
                        }
                        else
                        {
                            mosse_pos[indexmossa] = mossa;
                            indexmossa++;
                        }
                    }
                }
                if (xBoard < 7)
                {

                    if (boardcode[yBoard + 1, xBoard + 1] < 0)
                    {
                        mossa = new MoveCode();
                        mossa.yPartenza = yBoard;
                        mossa.xPartenza = xBoard;
                        mossa.yArrivo = (byte)(yBoard + 1);
                        mossa.xArrivo = (byte)(xBoard + 1);
                        mossa.cattura = true;

                        if (yBoard == 6)
                        {
                            mossa.promozione = -1;
                            mosse_pos[indexmossa] = mossa;
                            indexmossa++;
                            mossa.promozione = -2;
                            mosse_pos[indexmossa] = mossa;
                            indexmossa++;
                            mossa.promozione = -3;
                            mosse_pos[indexmossa] = mossa;
                            indexmossa++;
                            mossa.promozione = -4;
                            mosse_pos[indexmossa] = mossa;
                            indexmossa++;
                        }
                        else
                        {
                            mosse_pos[indexmossa] = mossa;
                            indexmossa++;
                        }
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

        public void convalidate_move(MoveCode sasso)
        {
            bool wich_color;
            if (boardcode[sasso.yPartenza, sasso.xPartenza] > 0) { wich_color = true; }
            else { wich_color = false; }



            GenerazioneMosse(wich_color);
            bool isthere = false;
            for (int h = 0; h < indexmossa + 1; h++)
                if (mosse_pos[h].xPartenza == sasso.xPartenza && mosse_pos[h].yPartenza == sasso.yPartenza && mosse_pos[h].xArrivo == sasso.xArrivo && mosse_pos[h].yArrivo == sasso.yArrivo)
                {
                    isthere = true;
                    break;
                }
            if (isthere) Make_move(sasso);

        }

    }
}

﻿#define debug

using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ChessApp_3._0
{

    public class MoveCode
    {
        public byte xPartenza = 8;
        public byte yPartenza = 8;
        public byte xArrivo = 8;
        public byte yArrivo = 8;

        public bool cattura = false;
        public SByte promozione = 0;
        public byte arrocco = 0; // 1 for long white, 2 for short white, 3 for long black, 4 for short black
    }


    public class ChessEngine
    {
        


        static int[,] pawn_table =
         {
            {0, 0, 0, 0, 0, 0, 0, 0 },
            { 50, 50, 50, 50, 50, 50, 50, 50},
            { 10, 10, 20, 30, 30, 20, 10, 10},
            { 5, 5, 10, 25, 25, 10, 5, 5},
            { 0, 0, 0, 20, 20, 0, 0, 0},
            { 5, -5, -10, 0, 0, -10, -5, 5},
            { 5, 10, 10, -20, -20, 10, 10, 5 },
            { 0, 0, 0, 0, 0, 0, 0, 0}
        };
        static int[,] knight_table =
        {
            { -50, -40, -30, -30, -30, -30, -40, -50 },
            {-40, -20, 0, 0, 0, 0, -20, -40},
            {-30, 0, 10, 15, 15, 10, 0, -30},
            {-30, 5, 15, 20, 20, 15, 5, -30},
            {-30, 0, 15, 20, 20, 15, 0, -30},
            {-30, 5, 10, 15, 15, 10, 5, -30},
            {-40, -20, 0, 5, 5, 0, -20, -40 },
            { -50, -90, -30, -30, -30, -30, -90, -50}
        };
        static int[,] bishop_table =
            {
            { -20, -10, -10, -10, -10, -10, -10, -20 },
                { -10, 0, 0, 0, 0, 0, 0, -10 },
                { -10, 0, 5, 10, 10, 5, 0, -10 },
                { -10, 5, 5, 10, 10, 5, 5, -10 },
                { -10, 0, 10, 10, 10, 10, 0, -10 },
                { -10, 10, 10, 10, 10, 10, 10, -10 },
                { -10, 5, 0, 0, 0, 0, 5, -10 },
                { -20, -10, -90, -10, -10, -90, -10, -20}
            };
        static int[,] rook_table =
            {
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            {5, 10, 10, 10, 10, 10, 10, 5 },
            {-5, 0, 0, 0, 0, 0, 0, -5},
            {-5, 0, 0, 0, 0, 0, 0, -5},
            {-5, 0, 0, 0, 0, 0, 0, -5},
            {-5, 0, 0, 0, 0, 0, 0, -5},
            {-5, 0, 0, 0, 0, 0, 0, -5 },
            { 0, 0, 0, 5, 5, 0, 0, 0}
            };
        static int[,] queen_table =
            {
            {-20, -10, -10, -5, -5, -10, -10, -20 },
            { -10, 0, 0, 0, 0, 0, 0, -10},
            { -10, 0, 5, 5, 5, 5, 0, -10},
            { -5, 0, 5, 5, 5, 5, 0, -5 },
            { 0, 0, 5, 5, 5, 5, 0, -5 },
            { -10, 5, 5, 5, 5, 5, 0, -10},
            { -10, 0, 5, 0, 0, 0, 0, -10},
            { -20, -10, -10, 70, -5, -10, -10, -20}
            };
        static int[,] king_table =
            {
            {-30, -40, -40, -50, -50, -40, -40, -30 },
            {-30, -40, -40, -50, -50, -40, -40, -30},
            {-30, -40, -40, -50, -50, -40, -40, -30},
            {-30, -40, -40, -50, -50, -40, -40, -30},
            {-20, -30, -30, -40, -40, -30, -30, -20},
            {-10, -20, -20, -20, -20, -20, -20, -10},
            {20, 20, 0, 0, 0, 0, 20, 20 },
            { 20, 30, 10, 0, 0, 10, 30, 20}
            };


        //int[,] king_endgame_table = {-50, -40, -30, -20, -20, -30, -40, -50,
        //                      -30, -20, -10, 0, 0, -10, -20, -30,
        //                      -30, -10, 20, 30, 30, 20, -10, -30,
        //                      -30, -10, 30, 40, 40, 30, -10, -30,
        //                      -30, -10, 30, 40, 40, 30, -10, -30,
        //                      -30, -10, 20, 30, 30, 20, -10, -30,
        //                      -30, -30, 0, 0, 0, 0, -30, -30,
        //                      -50, -30, -30, -30, -30, -30, -30, -50}

        //public int[,] boardcode { get; } = new int[,]
        //    {
        //    { -4,-3,-2,-5,-6,-2,-3,-4 },
        //    { -1,-1,-1,-1,-1,-1,-1,-1 },
        //    { 0,0,0,0,0,0,0,0 },
        //    { 0,0,0,0,0,0,0,0 },
        //    { 0,0,0,0,0,0,0,0 },
        //    { 0,0,0,0,0,0,0,0 },
        //    { 1,1,1,1,1,1,1,1 },
        //    { 4,3,2,5,6,2,3,4 }
        //    };

        // debug starting board

        public int[,] boardcode { get; } = new int[,]
       {
            { -4,0,-2,-5,-6,-2,0,-4 },
            { -1,-1,-1,-1,-1,-1,-1,-1 },
            { 0,0,0,0,0,-3,0,0 },
            { 0,0,0,1,-3,0,0,0 },
            { 0,0,0,0,1,0,0,0 },
            { 0,0,3,0,0,0,0,0 },
            { 1,1,1,0,0,1,1,1 },
            { 4,0,2,5,6,2,3,4 }
       };



        int[,] bitboard = new int[,]
          { { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 }};

        public MoveCode[] mosse_pos;
        int indexmossa = 0;

        bool[] castle = new bool[4] { true, true, true, true };


        public ChessEngine()
        {
            loadDebug();
        }
        public ChessEngine(int[,] _boardcode)
        {
            Array.Copy(_boardcode, boardcode, _boardcode.Length);
        }
        public ChessEngine(int[,] _boardcode, bool[] _castle)
        {
            Array.Copy(_boardcode, boardcode, _boardcode.Length);
            Array.Copy(_castle, castle, _castle.Length);
        }

        void Pallino(int x, int y)
        {
            Global.board[x, y].BackgroundImage = Global.SvgBitMap[12];
        }




        void loadDebug()
        {

            int mossa = 0;
            //int babba = MinMaxTree(true, ref mossa, 0, 1);
            //  MessageBox.Show(babba.ToString());
            // MessageBox.Show(mossa.ToString());
            // StampaBitBoard();
         //   Debug_print_single_bitboard(true);


            BitBoardGenerator(true, ref mossa, ref mossa);
            StampaBitBoard();
        }


        public int GenerazioneMosse(bool iswhite)
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
                    return indexmossa;
                }
                else
                {
                    MoveCode mossa_castle;
                    KingMoveReal((byte)(kingy), (byte)(kingx), true);
                    for (byte j1 = 0; j1 < 8; j1++)
                        for (byte j2 = 0; j2 < 8; j2++)
                        {
                            switch (boardcode[j1, j2])
                            {
                                case 0: break;
                                case 1: PawnMoveReal(j1, j2, true); break;
                                case 2: BishopMoveReal(j1, j2, true); break;
                                case 3: KnightMoveReal(j1, j2, true); break;
                                case 4: RookMoveReal(j1, j2, true); break;
                                case 5: QueenMoveReal(j1, j2, true); break;
                            }
                        }

                    // white castle
                    if (castle[0] && boardcode[7, 1] == 0 && boardcode[7, 2] == 0 && boardcode[7, 3] == 0 && bitboard[7, 2] == 0 && bitboard[7, 3] == 0)
                    {
                        mossa_castle = new MoveCode();
                        mossa_castle.arrocco = 1;
                        mosse_pos[indexmossa] = mossa_castle;
                        indexmossa++;
                    }
                    if (castle[1] && boardcode[7, 5] == 0 && boardcode[7, 6] == 0 && bitboard[7, 5] == 0 && bitboard[7, 6] == 0)
                    {
                        mossa_castle = new MoveCode();
                        mossa_castle.arrocco = 2;
                        mosse_pos[indexmossa] = mossa_castle;
                        indexmossa++;
                    }




                }

                // add castlings
            }
            else
            {
                mosse_pos = new MoveCode[100];
                indexmossa = 0;
                int kingx = 0;
                int kingy = 0;
                BitBoardGenerator(true, ref kingx, ref kingy);

                if (bitboard[kingy, kingx] > 1)
                {
                    KingMoveReal((byte)(kingy), (byte)(kingx), false);
                    return indexmossa;
                }
                else
                {
                    MoveCode mossa_castle;
                    KingMoveReal((byte)(kingy), (byte)(kingx), false);
                    for (byte j1 = 0; j1 < 8; j1++)
                        for (byte j2 = 0; j2 < 8; j2++)
                        {
                            switch (boardcode[j1, j2])
                            {
                                case 0: break;
                                case -1: PawnMoveReal(j1, j2, false); break;
                                case -2: BishopMoveReal(j1, j2, false); break;
                                case -3: KnightMoveReal(j1, j2, false); break;
                                case -4: RookMoveReal(j1, j2, false); break;
                                case -5: QueenMoveReal(j1, j2, false); break;
                            }
                        }

                    if (castle[2] && boardcode[0,1] == 0 && boardcode[0,2] == 0 && bitboard[0, 2] == 0 && boardcode[0,3] == 0 && bitboard[0, 3] == 0)
                    {
                        mossa_castle = new MoveCode();
                        mossa_castle.arrocco = 3;
                        mosse_pos[indexmossa] = mossa_castle;
                        indexmossa++;
                    }
                    if (castle[3] && boardcode[0, 5] == 0 && boardcode[0, 6] == 0 && bitboard[0, 5] == 0 && bitboard[0, 6] == 0)
                    {
                        mossa_castle = new MoveCode();
                        mossa_castle.arrocco = 4;
                        mosse_pos[indexmossa] = mossa_castle;
                        indexmossa++;
                    }
                }
                // add castlings
            }
            return indexmossa;
        }

        public void Debug_print_single_bitboard(bool iswhite)
        {
            for (int y = 0; y < 8; y++)
                for (int x = 0; x < 8; x++)
                {
                    switch (boardcode[y, x])
                    {
                        case 0: break;
                        case 1:
                            PawnMoveWhiteBitBoard(y, x); StampaBitBoard(); bitboard = new int[,]
        { { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 }}; break;
                        case 4:
                            RookmMoveBitBoard(y, x); StampaBitBoard(); bitboard = new int[,]
         { { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 }}; break;
                        case 3:
                            KnightMoveBitBoard(y, x); StampaBitBoard(); bitboard = new int[,]
         { { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 }}; break;
                        case 2:
                            BishopMoveBitBoard(y, x); StampaBitBoard(); bitboard = new int[,]
         { { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 }}; break;
                        case 5:
                            QueenMoveBitBoard(y, x); StampaBitBoard(); bitboard = new int[,]
        { { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 }}; break;
                        case 6:
                            KingMoveBitBoard(y, x); StampaBitBoard(); bitboard = new int[,]
         { { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 }}; break;
                    }
                }
        }

        public int Evaluate_Position()
        {
            int evaluation = 0;
            for (int j1 = 0; j1 < 8; j1++)
                for (int j2 = 0; j2 < 8; j2++)
                {
                    switch (boardcode[j1, j2])
                    {
                        case 0: break;
                        case 1: evaluation += pawn_table[j1, j2] + 100; break;
                        case 2: evaluation += bishop_table[j1, j2] + 300; break;
                        case 3: evaluation += knight_table[j1, j2] + 300; break;
                        case 4: evaluation += rook_table[j1, j2] + 500; break;
                        case 5: evaluation += queen_table[j1, j2] + 900; break;
                        case 6: evaluation += king_table[j1, j2] + 5000; break;
                        case -1: evaluation -= pawn_table[7 - j1, j2] + 100; break;
                        case -2: evaluation -= bishop_table[7 - j1, j2] + 300; break;
                        case -3: evaluation -= knight_table[7 - j1, j2] + 300; break;
                        case -4: evaluation -= rook_table[7 - j1, j2] + 500; break;
                        case -5: evaluation -= queen_table[7 - j1, j2] + 900; break;
                        case -6: evaluation -= king_table[7 - j1, j2] + 5000; break;
                    }
                }
            return evaluation;
        }

        #if debug
        public void Countmosse()
        {
            int j = 0;
            for (j = 0; j < 100; j++)
            {
                if (mosse_pos[j] == null) break;
            }
            MessageBox.Show((j).ToString());
        }

        
        public void StampaBitBoard() //debug
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

#endif

        public int MinMaxTree(bool iswhite, ref int mossa, int depth, int dep)
        {
            int best_evaluation = 0;
            int testx = -1;
            int testy = -1;

            if (iswhite)
            {
                if (depth == dep) return Evaluate_Position();



                BitBoardGenerator(true, ref testx, ref testy);
                if (bitboard[testy, testx] > 0) return 10001;

                int num_mosse = GenerazioneMosse(true);
                
                //if (num_mosse == 0) return 0;  // non sicuro di questo ma va aggiunto un controllo stallo
                
                best_evaluation = -10001;
                int actual_evaluation;
                int useless = 0;
                ChessEngine scacchiera_ricorsiva;
                for (int j = 0; j < num_mosse; j++)
                {
                    Global.debug_nodes++;
                    scacchiera_ricorsiva = new ChessEngine(boardcode);
                    scacchiera_ricorsiva.Make_move(mosse_pos[j]);
                    actual_evaluation = scacchiera_ricorsiva.MinMaxTree(false, ref useless, depth + 1, dep);
                    if (actual_evaluation > best_evaluation)
                    {
                        best_evaluation = actual_evaluation;
                        mossa = j;
                    }
                }
            }
            else
            {
                if (depth == dep) return Evaluate_Position();

                BitBoardGenerator(false, ref testx, ref testy);
                if (bitboard[testy, testx] > 0) return -10001;

                int num_mosse = GenerazioneMosse(false);
                //int best_move;
                best_evaluation = 10001;
                int actual_evaluation;
                int useless = 0;
                ChessEngine scacchiera_ricorsiva;
                for (int j = 0; j < num_mosse; j++)
                {
                    Global.debug_nodes++;
                    scacchiera_ricorsiva = new ChessEngine(boardcode);
                    scacchiera_ricorsiva.Make_move(mosse_pos[j]);
                    actual_evaluation = scacchiera_ricorsiva.MinMaxTree(true, ref useless, depth + 1, dep);
                    if (actual_evaluation < best_evaluation)
                    {
                        best_evaluation = actual_evaluation;
                        mossa = j;
                    }
                }
            }
            return best_evaluation;



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
                            case 6: KingMoveBitBoard(y, x);  break;
                            case -6: kingex = x; kingey = y; break;
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
                            case -6: KingMoveBitBoard(y, x); break;
                            case 6: kingex = x; kingey = y; break;
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
                        mossa.yArrivo = yBoard;
                        mosse_pos[indexmossa] = mossa;
                        indexmossa++;
                        break;
                    }
                    mossa.xPartenza = xBoard;
                    mossa.yPartenza = yBoard;
                    mossa.xArrivo = (byte)(a);
                    mossa.yArrivo = yBoard;
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
                        mossa.yArrivo = yBoard;
                        mosse_pos[indexmossa] = mossa;
                        indexmossa++;
                        break;
                    }
                    mossa.xPartenza = xBoard;
                    mossa.yPartenza = yBoard;
                    mossa.xArrivo = (byte)(a);
                    mossa.yArrivo = yBoard;
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
                        mossa.yArrivo = yBoard;
                        mosse_pos[indexmossa] = mossa;
                        indexmossa++;
                        break;
                    }
                    mossa.xPartenza = xBoard;
                    mossa.yPartenza = yBoard;
                    mossa.xArrivo = (byte)(a);
                    mossa.yArrivo = yBoard;
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
                        mossa.yArrivo = yBoard;
                        mosse_pos[indexmossa] = mossa;
                        indexmossa++;
                        break;
                    }
                    mossa.xPartenza = xBoard;
                    mossa.yPartenza = yBoard;
                    mossa.xArrivo = (byte)(a);
                    mossa.yArrivo = yBoard;
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
                for (byte y = (byte)(yBoard - 1), x = (byte)(xBoard - 1); y < 8 && x < 8; y--, x--)
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

                for (byte y = (byte)(yBoard - 1), x = (byte)(xBoard + 1); y < 8 && x < 8; y--, x++)
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
                for (byte y = (byte)(yBoard + 1), x = (byte)(xBoard - 1); y < 8 && x < 8; y++, x--)
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
                for (byte y = (byte)(yBoard - 1), x = (byte)(xBoard - 1); y < 8 && x < 8; y--, x--)
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

                for (byte y = (byte)(yBoard - 1), x = (byte)(xBoard + 1); y < 8 && x < 8; y--, x++)
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
                for (byte y = (byte)(yBoard + 1), x = (byte)(xBoard - 1); y < 8 && x < 8; y++, x--)
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
            BishopMoveReal(yBoard, xBoard, iswhite);
            RookMoveReal(yBoard, xBoard, iswhite);
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
                            if (boardcode[yBoard + 2, xBoard + 1] != 0) mossa.cattura = true;
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

                    if (boardcode[yBoard + 1, xBoard + 1] > 0)
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
          
            switch(sasso.arrocco)
            {
                case 0:
                    {
                        boardcode[sasso.yArrivo, sasso.xArrivo] = boardcode[sasso.yPartenza, sasso.xPartenza] + sasso.promozione;
                        boardcode[sasso.yPartenza, sasso.xPartenza] = 0;




                        if ((castle[0] || castle[1]) && sasso.yPartenza == 7)
                        {
                            switch (sasso.xPartenza)
                            {
                                case 4:
                                    castle[0] = false;
                                    castle[1] = false;
                                    break;
                                case 0:
                                    castle[0] = false;
                                    break;
                                case 7:
                                    castle[1] = false;
                                    break;
                            }
                        }
                        else if ((castle[1] || castle[2]) && sasso.yPartenza == 0)
                        {
                            switch (sasso.xPartenza)
                            {
                                case 4:
                                    castle[2] = false;
                                    castle[3] = false;
                                    break;
                                case 0:
                                    castle[2] = false;
                                    break;
                                case 7:
                                    castle[3] = false;
                                    break;
                            }
                        }
                        break;
                    }
                case 1:
                    boardcode[7, 0] = 0;
                    boardcode[7, 4] = 0;
                    boardcode[7, 2] = 6;
                    boardcode[7, 3] = 4;
                    castle[0] = false;
                    castle[1] = false;
                    break;
                case 2:
                    boardcode[7, 7] = 0;
                    boardcode[7, 4] = 0;
                    boardcode[7, 6] = 6;
                    boardcode[7, 5] = 4;
                    castle[0] = false;
                    castle[1] = false;
                    break;
                case 3:
                    boardcode[0, 0] = 0;
                    boardcode[0, 4] = 0;
                    boardcode[0, 2] = -6;
                    boardcode[0, 3] = -4;
                    castle[2] = false;
                    castle[3] = false;
                    break;
                case 4:
                    boardcode[0, 7] = 0;
                    boardcode[0, 4] = 0;
                    boardcode[0, 6] = -6;
                    boardcode[0, 5] = -4;
                    castle[2] = false;
                    castle[3] = false;
                    break;
               
            }

        }

 

    }
}

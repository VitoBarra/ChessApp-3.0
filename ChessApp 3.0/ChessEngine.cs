using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessApp_3._0
{
    public class ChessEngine
    {
        int[,] BoardCode;
        int[,] BitBoard;
        
        //BitBoard = new int[8, 8] 
        //    { { 0,0,0,0,0,0,0,0 },
        //    { 0,0,0,0,0,0,0,0 },
        //    { 0,0,0,0,0,0,0,0 },
        //    { 0,0,0,0,0,0,0,0 },
        //    { 0,0,0,0,0,0,0,0 },
        //    { 0,0,0,0,0,0,0,0 },
        //    { 0,0,0,0,0,0,0,0 },
        //    { 0,0,0,0,0,0,0,0 }};

    public ChessEngine(int[,] _boardCode)
        {
            BoardCode = _boardCode;
        }

        public void BitBoardUpdate()
        {

        }

        public void ToweMove(int yBoard, int xBoard)
        {



            for (int y = yBoard; y < 8; y++)
            {
                if (BitBoard[y, xBoard] == 0)
                    BitBoard[y, xBoard] = 1;
            }
            for (int y = yBoard; y > 0; y--)
            {
                if (BitBoard[y, xBoard] ==0)
                BitBoard[y, xBoard] = 1;
            }
            for (int x = xBoard; x < 8; x++)
            {
                if (BitBoard[yBoard, x] == 0)
                    BitBoard[yBoard, x] = 1;
            }
            for (int x = xBoard; x >0; x--)
            {
                if (BitBoard[yBoard, x] == 0)
                    BitBoard[yBoard, x] = 1;
            }

        }

        public void MoveGenerator()
        {
            for (int y = 0; y< 8; y++)
                for (int x=0; x< 8; x++)
                {
                    switch (BoardCode[y, x])
                    {
                        case 1: break;
                        case -1: break;
                        case 4: ToweMove(y, x); break;
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

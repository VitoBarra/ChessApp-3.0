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
            KnightMove(6,7);
            bitBoard[6, 7] = 9;
            StampaBitBoard();
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

        public void BitBoardUpdate(int yBoard, int xBoard)
        {
            
        }

        #region ---------------------------------------PiceMove---------------------------------------
        public void ToweMove(int yBoard, int xBoard)
        {
            for (int y = yBoard+1; y < 8; y++)
                bitBoard[y, xBoard]++;
            for (int y = yBoard-1; y >= 0; y--) 
                bitBoard[y, xBoard]++;
            for (int x = xBoard+1; x < 8; x++)  
                bitBoard[yBoard, x]++;
            for (int x = xBoard-1; x >= 0; x--) 
                bitBoard[yBoard, x]++;
        }

        public void BishopMove(int yBoard, int xBoard)
        {
            for (int y = yBoard+1, x = xBoard+1; y < 8 && x < 8; y++, x++)
                bitBoard[y, x] ++;
            for (int y = yBoard-1, x = xBoard-1; y >= 0 && x >= 0; y--, x--)
                bitBoard[y, x] ++;
            for (int y = yBoard-1, x = xBoard+1; y >= 0 && x < 8; y--, x++)
                bitBoard[y, x]++;
            for (int y = yBoard+1, x = xBoard-1; y < 8 && x >= 0; y++, x--)
                bitBoard[y, x]++;
        }

        public void QueenMove(int yBoard, int xBoard)
        {
            for (int y = yBoard + 1; y < 8; y++)
                bitBoard[y, xBoard]++;
            for (int y = yBoard - 1; y >= 0; y--)
                bitBoard[y, xBoard]++;
            for (int x = xBoard + 1; x < 8; x++)
                bitBoard[yBoard, x]++;
            for (int x = xBoard - 1; x >= 0; x--)
                bitBoard[yBoard, x]++;
            for (int y = yBoard + 1, x = xBoard + 1; y < 8 && x < 8; y++, x++)
                bitBoard[y, x]++;
            for (int y = yBoard - 1, x = xBoard - 1; y >= 0 && x >= 0; y--, x--)
                bitBoard[y, x]++;
            for (int y = yBoard - 1, x = xBoard + 1; y >= 0 && x < 8; y--, x++)
                bitBoard[y, x]++;
            for (int y = yBoard + 1, x = xBoard - 1; y < 8 && x >= 0; y++, x--)
                bitBoard[y, x]++;
        }

        public void KnightMove(int yBoard, int xBoard)
        {
            if (yBoard < 7)
            {
                if (xBoard < 6)
                    bitBoard[yBoard + 1, xBoard + 2] += 1;
                if (xBoard > 1)
                    bitBoard[yBoard + 1, xBoard - 2] += 1;
            }
            if (yBoard < 6)
            {
                if (xBoard < 7)
                    bitBoard[yBoard + 2, xBoard + 1] += 1;
                if (xBoard > 1)
                    bitBoard[yBoard + 2, xBoard - 1] += 1;
            }
            if (yBoard > 0)
            {
                if (xBoard < 6)
                    bitBoard[yBoard - 1, xBoard + 2] += 1;
                if (xBoard > 1)
                    bitBoard[yBoard - 1, xBoard - 2] += 1;
            }
            if (yBoard > 1)
            {
                if (xBoard < 7)
                    bitBoard[yBoard - 2, xBoard + 1] += 1;
                if (xBoard > 0)
                    bitBoard[yBoard - 2, xBoard - 1] += 1;
            }

        }
        #endregion
        //public void MoveGenerator()
        //{
        //    for (int y = 0; y< 8; y++)
        //        for (int x=0; x< 8; x++)
        //        {
        //            switch (BoardCode[y, x])
        //            {
        //                case 1: break;
        //                case -1: break;
        //                case 4: ToweMove(y, x); break;
        //                case -4:  break;
        //                case 3: break;
        //                case -3: break;
        //                case 2: break;
        //                case -2:  break;
        //                case 5: break;
        //                case -5:  break;
        //                case 6:  break;
        //                case -6:  break;
        //            }





        //        }
        //}
        //public  void GenerateTree()
        //{

        //}



    }
}

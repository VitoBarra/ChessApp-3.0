using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

using Svg;


namespace ChessApp_3._0
{
    public partial class Form1 : Form
    {
        //  Thread chessEngine = new Thread(ChessStart);

        bool clickdStart = false;
        bool clickdReset = false;
        ChessEngine engine;
        static string chesspath = FindPath() + "\\ChessApp 3.0";
      



        //------------------------------------------------------In Form1----------------------------------------
        public Form1()
        {
            InitializeComponent();
            #region Timer ini
            tLeft = new System.Timers.Timer() { Interval = 1000 };
            tLeft.Elapsed += OnTimeEventLeft;
            LeftTimerButton.Text = leftMinutes + ":" + leftSecond;

            tRight = new System.Timers.Timer() { Interval = 1000 };
            tRight.Elapsed += OnTimeEventRight;
            RightTimerButton.Text = rightMinutes + ":" + rightSecond;


            tRef = new System.Timers.Timer() { Interval = 400 };
            tRef.Elapsed += Refras;
            tRef.Start();

            #endregion
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            Global.Conf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            

            Global.width_Height = int.Parse(Global.Conf.AppSettings.Settings["Dimension"].Value);
            Global.SvgBitMap = LoadSvg();
            this.Width = Global.width_Height * 8 + 15 + TurnCount.Width + WhiteMove.Width + BlackMove.Width;
            this.Height = Global.width_Height * 8 + Global.width_Height + 39;



            Bildboard();
        }

        private void Start_Click(object sender, EventArgs e)
        {
            if (!clickdStart)
            {
                if (!clickdStart && clickdReset)
                {
                    clickdReset = false;
                }
                else
                {

                }
                clickdStart = true;
            }
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            clickdReset = true;
            clickdStart = false;

            if (Global.Player)
                Global.boardCod = new int[8, 8]
                {
                 {-4,-3,-2,-6,-5,-2,-3,-4 },
                 { -1,-1,-1,-1,-1,-1,-1,-1 },
                 { 0,0,0,0,0,0,0,0 },
                 { 0,0,0,0,0,0,0,0 },
                 { 0,0,0,0,0,0,0,0 },
                 { 0,0,0,0,0,0,0,0 },
                 { 1,1,1,1,1,1,1,1 },
                 { 4,3,2,6,5,2,3,4 }
                };
            else
                Global.boardCod = new int[8, 8]
                 {
                 {4,3,2,6,5,2,3,4 },
                 {1,1,1,1,1,1,1,1 },
                 {0,0,0,0,0,0,0,0 },
                 {0,0,0,0,0,0,0,0 },
                 {0,0,0,0,0,0,0,0 },
                 {0,0,0,0,0,0,0,0 },
                 {-1,-1,-1,-1,-1,-1,-1,-1 },
                 {-4,-3,-2,-6,-5,-2,-3,-4 }
                 };
            RenderPiceOnboard();
            WhiteMove.Controls.Clear();
            BlackMove.Controls.Clear();
            TurnCount.Controls.Clear();
        }

        #region -------------ToolStrip item function-------------


        private void GameModeSelector(object sender, EventArgs e)
        {
            if (sender == PlayerVsPlayerGameMode)
                Global.Conf.AppSettings.Settings["GameMode"].Value = "";
            else if (sender == PlayWhitWhiteGameMode)
                Global.Conf.AppSettings.Settings["GameMode"].Value = "";
            else if (sender == PlayWhitBlackGameMode)
                Global.Conf.AppSettings.Settings["GameMode"].Value = "";
            else if (sender == AiVsAiGameMode)
                Global.Conf.AppSettings.Settings["GameMode"].Value = "";
            Global.Conf.Save(ConfigurationSaveMode.Modified);
        }
        private void DifficultyWSelector(object sender, EventArgs e)
        {
            if (sender == DificultyW2)
                Global.Conf.AppSettings.Settings["DifficultyWhiteAi"].Value = "2";
            else if (sender == DificultyW3)
                Global.Conf.AppSettings.Settings["DifficultyWhiteAi"].Value = "3";
            else if (sender == DificultyW4)
                Global.Conf.AppSettings.Settings["DifficultyWhiteAi"].Value = "4";
            Global.Conf.Save(ConfigurationSaveMode.Modified);
        }
        private void DifficultyBSelector(object sender, EventArgs e)
        {
            if (sender == DificultyB2)
                Global.Conf.AppSettings.Settings["DifficultyBlackAi"].Value = "2";
            else if (sender == DificultyB3)
                Global.Conf.AppSettings.Settings["DifficultyBlackAi"].Value = "3";
            else if (sender == DificultyB4)
                Global.Conf.AppSettings.Settings["DifficultyBlackAi"].Value = "4";
            Global.Conf.Save(ConfigurationSaveMode.Modified);
        }



        private void SetPositionTool_Click(object sender, EventArgs e)
        {

        }





        private void OptionTool_Click(object sender, EventArgs e)
        {
            OptionForm optionForm = new OptionForm { StartPosition = FormStartPosition.CenterParent };
            optionForm.ShowDialog();
            Form1_Load(sender, e);
        }


        #endregion


        //------------------------------------------------------method Form1-----------------------------------
        #region ------------------------board-------------------
        public void Bildboard()
        {
            int offset = 0;
            int pixelPice = Global.width_Height;
            if (Global.board == null)
                Global.board = new Board[8, 8];
            Global.boardCod = new int[8, 8]
               {{4,3,2,6,5,2,3,4 },
                 {1,1,1,1,1,1,1,1 },
                 {0,0,0,0,0,0,0,0 },
                 {0,0,0,0,0,0,0,0 },
                 {0,0,0,0,0,0,0,0 },
                 {0,0,0,0,0,0,0,0 },
                 {-1,-1,-1,-1,-1,-1,-1,-1 },
                 {-4,-3,-2,-6,-5,-2,-3,-4 } };
            engine = new ChessEngine(Global.boardCod);
            //----------------------------------board AREA----------------------------------

            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    if (Global.board[i, j] == null)
                        Global.board[i, j] = new Board();
                    Global.board[i, j].xPos = j;
                    Global.board[i, j].yPos = i;
                    Global.board[i, j].Parent = this;
                    Global.board[i, j].Location = new Point(j * pixelPice + offset, i * pixelPice + pixelPice);
                    Global.board[i, j].Size = new Size(pixelPice, pixelPice);

                    if (i % 2 == 0)
                    {
                        if (j % 2 == 1)
                            Global.board[i, j].BackColor = ColorTranslator.FromHtml(Global.Conf.AppSettings.Settings["ThemeB"].Value);
                        else
                            Global.board[i, j].BackColor = ColorTranslator.FromHtml(Global.Conf.AppSettings.Settings["ThemeW"].Value);
                    }
                    else
                    {
                        if (j % 2 == 0)
                            Global.board[i, j].BackColor = ColorTranslator.FromHtml(Global.Conf.AppSettings.Settings["ThemeB"].Value);
                        else
                            Global.board[i, j].BackColor = ColorTranslator.FromHtml(Global.Conf.AppSettings.Settings["ThemeW"].Value);
                    }
                }

            RenderPiceOnboard();
        }

        private Bitmap[] LoadSvg()
        {
            int count = 0;
            SvgDocument[] document = new SvgDocument[12];
            Bitmap[] bp = new Bitmap[12];
            string[] Files = Directory.GetFiles("pice");

            foreach (string File in Files)
            {
                document[count] = SvgDocument.Open(File);
                document[count].Width = Global.width_Height;
                document[count].Height = Global.width_Height;
                bp[count] = document[count].Draw();
                count++;
            }
            return bp;
        }

        public static void RenderPiceOnboard()
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    switch (Global.boardCod[i, j])
                    {
                        case 0: Global.board[i, j].BackgroundImage = null; break;
                        case 1: Global.board[i, j].BackgroundImage = Global.SvgBitMap[7]; break;
                        case -1: Global.board[i, j].BackgroundImage = Global.SvgBitMap[6]; break;
                        case 4: Global.board[i, j].BackgroundImage = Global.SvgBitMap[11]; break;
                        case -4: Global.board[i, j].BackgroundImage = Global.SvgBitMap[10]; break;
                        case 3: Global.board[i, j].BackgroundImage = Global.SvgBitMap[3]; break;
                        case -3: Global.board[i, j].BackgroundImage = Global.SvgBitMap[2]; break;
                        case 2: Global.board[i, j].BackgroundImage = Global.SvgBitMap[1]; break;
                        case -2: Global.board[i, j].BackgroundImage = Global.SvgBitMap[0]; break;
                        case 5: Global.board[i, j].BackgroundImage = Global.SvgBitMap[9]; break;
                        case -5: Global.board[i, j].BackgroundImage = Global.SvgBitMap[8]; break;
                        case 6: Global.board[i, j].BackgroundImage = Global.SvgBitMap[5]; break;
                        case -6: Global.board[i, j].BackgroundImage = Global.SvgBitMap[4]; break;
                    }

                }
        }


        public static string FindPath() => Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).ToString()).ToString();

        #endregion


        #region ----------------------track----------------------
        private void TrackBackButton_Click(object sender, EventArgs e)
        {
          
        }

        private void TrackForwardButton_Click(object sender, EventArgs e)
        {

        }
        #endregion




        private void Refras(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (Global.MoveW != "")
            {
                Invoke(new Action(() =>
                {
                    WhiteMove.Controls.Add(new Button() { Height = 30, Width = TurnCount.Width, Text = Global.MoveW });
                    Global.MoveW = "";
                }
           ));
            }
            else if (Global.MoveB != "")
            {
                Invoke(new Action(() =>
                {
                    BlackMove.Controls.Add(new Button() { Height = 30, Width = TurnCount.Width, Text = Global.MoveB });
                    Global.MoveB = "";
                    Global.MoveBool = true;
                }
           ));
            }
            else if (Global.countStr % 2 == 0 && Global.MoveBool)
            {
                Invoke(new Action(() =>
                {

                    TurnCount.Controls.Add(new Label() { Height = 36, Width = TurnCount.Width, TextAlign = ContentAlignment.MiddleCenter, Text = (Global.countStr / 2).ToString() });
                    Global.MoveBool = false;
                }
                   ));
            }
        }

        public static string ConvertNumberToPgn(string NumMove)
        {
            string pgn = "";
            return pgn;
        }



        #region ---------------------timer--------------------

        int leftMinutes = 15, leftSecond = 00;
        int rightMinutes = 15, rightSecond = 00;

        System.Timers.Timer tLeft;
        System.Timers.Timer tRight;
        System.Timers.Timer tRef;

        bool clickdTimerLeft = false;
        bool clickdTimerRight = false;

        private void LeftTimerButton_Click(object sender, EventArgs e)
        {
            if (!clickdTimerLeft)
            {
                tLeft.Start();
                clickdTimerLeft = true;
            }
            else
            {
                tLeft.Stop();
                clickdTimerLeft = false;
            }
        }

        private void RightTimerButton_Click(object sender, EventArgs e)
        {
            if (!clickdTimerRight)
            {
                tRight.Start();
                clickdTimerRight = true;
            }
            else
            {
                tRight.Stop();
                clickdTimerRight = false;
            }
        }


        private void OnTimeEventLeft(object sender, System.Timers.ElapsedEventArgs e)
        {
            Invoke(new Action(() =>
            {
                if (leftSecond == 0)
                {
                    if (leftMinutes >= 0)
                    {
                        leftMinutes -= 1;
                        leftSecond = 59;
                    }
                }
                else
                    leftSecond -= 1;

                LeftTimerButton.Text = leftMinutes + ":" + leftSecond;
            }
            ));
        }
        private void OnTimeEventRight(object sender, System.Timers.ElapsedEventArgs e)
        {
            Invoke(new Action(() =>
            {
                if (rightSecond == 0)
                {
                    if (rightMinutes >= 0)
                    {
                        rightMinutes -= 1;
                        rightSecond = 59;
                    }
                }
                else
                    rightSecond -= 1;


                RightTimerButton.Text = rightMinutes + ":" + rightSecond;
            }
            ));
        }
        #endregion
    }




    public static class Global
    {
        public static int countStr = 0;
        public static Board[,] board;
        public static Configuration Conf;
        public static Bitmap[] SvgBitMap;
        public static int width_Height = 50;
        public static int[,] boardCod;
        public static bool Player = false;
        public static bool MoveBool = true;
        public static string MoveW = "";
        public static string MoveB = "";
        public static string[] movePgn = new string[1000];
    }
}


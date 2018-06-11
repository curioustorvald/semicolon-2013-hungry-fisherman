using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using TDBase;

namespace TowerDefense
{
    class SceneGameOverAndHighscore : Scene
    {
        private int[] Scoreboard;
        //private int[] Scoreboard2;
        private string[] ScoreboardName;
        private const int noScoresToBeShown = 10;

        private bool inputStreamOpened = false;
        private char[] stringInput = new char[16];
        private int stringInputSize = 0;

        private FileStream fileStream;

        private bool scoreWritable = true;

        string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public override void Start()
        {
            loadHighscore();
        }

        public override void Draw(Graphics g, float deltaSeconds)
        {
            /*float stringW = GDIController.GetStringWidth(g, new Font(new FontFamily("Segoe UI"), 32f), "Stage " + Player.Player.currentStage + ": Game over!");
            g.DrawString("Stage " + Player.Player.currentStage + ": Game over!", new Font(new FontFamily("Segoe UI"), 32f), new SolidBrush(Color.Black), (640 - stringW) / 2, 15);

            drawStringLeftCenter(g, "Your score", 24f, Color.Gray, 80);

            String cardinalSuffix;

            if (scoreRank % 10 == 1 && scoreRank % 100 != 11)
            {
                cardinalSuffix = "st";
            }
            else if (scoreRank % 10 == 2 && scoreRank % 100 != 12)
            {
                cardinalSuffix = "nd";
            }
            else if (scoreRank % 10 == 3 && scoreRank % 100 != 13)
            {
                cardinalSuffix = "rd";
            }
            else
            {
                cardinalSuffix = "th";
            }

            drawStringLeftCenter(g, Player.Player.getScore().ToString(), 30f, Color.Black, 130);

            int scoreRank = Array.IndexOf(Scoreboard, Player.Player.getScore());

            if (scoreRank != -1)
            {
                drawStringLeftCenter(g, scoreRank.ToString() + " " + cardinalSuffix, 20f, Color.Gray, 170);
            }

            /* === */

            /*drawStringRightCenter(g, "Highscore", 24f, Color.Gray, 80);

            for (int i = 0; i < noScoresToBeShown; i++)
            {
                drawStringRightCenter(g, Scoreboard[i].ToString(), 20f, Color.Black, 130 + i * 30);
            }*/

            //g.FillRectangle(new SolidBrush(Color.White), 0, 0, 640, 480);

            drawStringCenter(g, "Stage " + Player.Player.currentStage + ": Game over", 28f, Color.Black, 10);

            drawStringCenter(g, "이름 입력", 18, Color.FromArgb(80, 80, 80), 60);
            inputStreamOpened = true;

            drawInputBox(g, 100);

            drawStringCenter(g, "Score: " + Player.Player.getScore(), 15f, Color.Black, 140);

            for (int i = 0; i < 10; i++)
            {
                drawStringLeftCenter(g, ScoreboardName[i], 12f, Color.Black, 200 + 18 * i);
                drawStringRightCenter(g, Scoreboard[i].ToString(), 12f, Color.Black, 200 + 18 * i);
            }

            drawStringCenter(g, "점수 기록하고 나가기", 12f, Color.Gray, 440);

            if (scoreWritable && stringInputSize > 0 && MouseClass.mouseY > 435 && MouseClass.isClicked)
            {
                scoreWritable = false;
                
                if (Player.Player.getScore() > 0)
                {
                    try
                    {
                        writeHighscore();
                    }
                    catch
                    {
                        SceneManager.SetScene(new SceneMain());
                    }
                    finally
                    {
                        MessageBox.Show("점수가 기록되었습니다.");

                        //Application.Exit();
                        //System.Environment.Exit(0);

                        SceneManager.SetScene(new SceneMain());
                    }
                }

                //Application.Exit();
                //System.Environment.Exit(0);

                SceneManager.SetScene(new SceneMain());
            }
        }

        private void writeHighscore()
        {
            String filename = appData + "\\Semicolon\\Hungry Fisherman\\";

			if (Player.Player.currentStage == 1)
            {
				filename += "highscore_1";
			}
			else if (Player.Player.currentStage == 2)
            {
				filename += "highscore_2";
			}
            else if (Player.Player.currentStage == 3)
            {
                filename += "highscore_3";
            }
            else
            {
                filename += "highscore_1";
            }

            byte[] bytesToWrite = new byte[20];

            for (int i = 4; i < 20; i++)
            {
                bytesToWrite[i] = (byte)stringInput[i - 4];
            }

            bytesToWrite[0] = (byte)(Player.Player.getScore() >> 24);
            bytesToWrite[1] = (byte)(Player.Player.getScore() >> 16);
            bytesToWrite[2] = (byte)(Player.Player.getScore() >> 8);
            bytesToWrite[3] = (byte)Player.Player.getScore();

            try
            {
                fileStream = new FileStream(filename, FileMode.Append);
                fileStream.Write(bytesToWrite, 0, bytesToWrite.Length);
                fileStream.Close();
                fileStream.Dispose();
            }
            catch (IOException e)
            {
                //throw new IOException();
                //Console.WriteLine(e.StackTrace);

                MessageBox.Show("IOException occured\n" + e.StackTrace);
            }

            //Console.WriteLine("[GameOver] Wrote highscore of {0}", Player.Player.getScore());
        }

        private void loadHighscore()
        {
            String filename = appData + "\\Semicolon\\Hungry Fisherman\\";

			if (Player.Player.currentStage == 1){
				filename += "highscore_1";
			}
			else if (Player.Player.currentStage == 2){
				filename += "highscore_2";
			}
            else if (Player.Player.currentStage == 3)
            {
                filename += "highscore_3";
            }
            else
            {
                filename += "highscore_1";
            }
			
			long fileLength = new FileInfo(filename).Length;

            ////Console.WriteLine("[GameOver] File length: {0}", fileLength);

            Scoreboard = new int[fileLength / 20];
            ScoreboardName = new string[Scoreboard.Length];

            ////Console.WriteLine("[GameOver] Scoreboard length: {0}", Scoreboard.Length);

            byte[] readFile = new byte[fileLength];
            try
            {
                BinaryReader binReader = new BinaryReader(new FileStream(filename, FileMode.Open));
                binReader.Read(readFile, 0, readFile.Length);
                binReader.Close();
                binReader.Dispose();
            }
            catch
            {
                MessageBox.Show("스코어 파일이 임의로 수정되었습니다.");
            }

            for (int i = 0; i < Scoreboard.Length; i++)
            {
                ////Console.WriteLine(i);

                Scoreboard[i] = readFile[i * 20] * 16777216 + readFile[i * 20 + 1] * 65536 + readFile[i * 20 + 2] * 256 + readFile[i * 20 + 3];
                ////Console.WriteLine("[GameOver] score index {0}: {1}", i, Scoreboard[i]);

                string scoreName = "";
                for (int j = 4; j < 20; j++)
                {
                    if (readFile[i * 20 + j] != 0)
                    {
                        scoreName += (char)(readFile[i * 20 + j]);
                    }
                }

                ScoreboardName[i] = scoreName;
                ////Console.WriteLine("[GameOver] scoreName index {0}: {1}", i, scoreName);
                
            }

            ////Console.WriteLine("[GameOver] Loaded score file.");

            ////Console.WriteLine("[GameOver] FileReadCounter: {0}", fileReadCounter);

            //Manually sort and reverse Scoreboard and ScoreboardName synchronised
            for (int i = 0; i < Scoreboard.Length - 1; i++)
            {
                for (int j = 0; j < Scoreboard.Length - 1 - i; j++)
                {
                    if (Scoreboard[j] > Scoreboard[j + 1])
                    {
                        int r0 = Scoreboard[j + 1];
                        Scoreboard[j + 1] = Scoreboard[j];
                        Scoreboard[j] = r0;

                        string r1 = ScoreboardName[j + 1];
                        ScoreboardName[j + 1] = ScoreboardName[j];
                        ScoreboardName[j] = r1;
                    }
                }
            }

            Array.Reverse(Scoreboard);
            Array.Reverse(ScoreboardName);

            for (int i = 0; i < Scoreboard.Length - 1; i++)
            {
                ////Console.WriteLine("[GameOver] [Score] {0}: {1}", ScoreboardName[i], Scoreboard[i]);
            }

            //Console.WriteLine("[GameOver] Loaded highscore");
        }

        private void drawStringLeftCenter(Graphics g, String str, float pt, Color colour, float y)
        {
            Font font = new Font(new FontFamily("Segoe UI"), pt);
            
            float stringW = GDIController.GetStringWidth(g, font, str);

            g.DrawString(str, font, new SolidBrush(colour), (320 - stringW)/2, y);
        }

        private void drawStringRightCenter(Graphics g, String str, float pt, Color colour, float y)
        {
            Font font = new Font(new FontFamily("Segoe UI"), pt);

            float stringW = GDIController.GetStringWidth(g, font, str);

            g.DrawString(str, font, new SolidBrush(colour), (320 - stringW) / 2 + 320, y);
        }

        private void drawStringCenter(Graphics g, String str, float pt, Color colour, float y)
        {
            Font font = new Font(new FontFamily("Segoe UI"), pt);

            float stringW = GDIController.GetStringWidth(g, font, str);

            g.DrawString(str, font, new SolidBrush(colour), (640 - stringW) / 2, y);
        }

        private void drawStringCenterWithCursor(Graphics g, String str, float pt, Color colour, float y)
        {
            Font font = new Font(new FontFamily("Segoe UI"), pt);

            float stringW = GDIController.GetStringWidth(g, font, str);
            float stringH = GDIController.GetStringHeight(g, font, "^~`_");

            g.DrawString(str, font, new SolidBrush(colour), (640 - stringW) / 2, y);

            g.FillRectangle(new SolidBrush(colour), (640 - stringW) / 2 + stringW, y + stringH - 4, 12, 2);
        }

        public override void MouseDown(float x, float y, System.Windows.Forms.MouseButtons button)
        {
            MouseClass.setMouse(x, y, button, true);
        }

        public override void MouseUp(float x, float y, System.Windows.Forms.MouseButtons button)
        {
            MouseClass.setMouse(x, y, button, false);
        }

        public override void MouseMove(float x, float y, System.Windows.Forms.MouseButtons button)
        {
            MouseClass.setMouse(x, y, button);
        }

        public override void KeyDown(System.Windows.Forms.Keys key, bool ctrl, bool alt, bool shift)
        {
            if (stringInputSize < 16 && inputStreamOpened &&
                ( ( (int)key >= 65 && (int)key <= 90 ) || (int)key == 32 || ( (int)key >= 48 && (int)key <= 57 ) ) )
            {
                stringInput[stringInputSize] = (char)key;
                stringInputSize++;
                ////Console.WriteLine("stringInput: " + stringInput);
            }
            else if (stringInputSize > 0 && stringInputSize <= 16 && inputStreamOpened && key == Keys.Back)
            {
                stringInputSize--;
                stringInput[stringInputSize] = (char)0;
            }
        }

        private void drawInputBox(Graphics g, int posY)
        {
            //g.DrawLine(new Pen(new SolidBrush(Color.DarkGray), 2f), 0, posY, 640, posY);

            string stringInputMerged = "";

            Font font = new Font(new FontFamily("Segoe UI"), 15f);

            float stringH = GDIController.GetStringHeight(g, font, "^gy_`~'");

            /*g.FillRectangle(new SolidBrush(Color.FromArgb(0x59, 0x59, 0x59)), 0, posY, 640, stringH + 4);

            g.DrawLine(new Pen(new SolidBrush(Color.FromArgb(0x82, 0x82, 0x82))), 0, posY + stringH + 5, 640, posY + stringH + 5);
            g.DrawLine(new Pen(new SolidBrush(Color.FromArgb(0xAD, 0xAD, 0xAD))), 0, posY + stringH + 6, 640, posY + stringH + 6);
            g.DrawLine(new Pen(new SolidBrush(Color.FromArgb(0xC9, 0xC9, 0xC9))), 0, posY + stringH + 7, 640, posY + stringH + 7);
            g.DrawLine(new Pen(new SolidBrush(Color.FromArgb(0xED, 0xED, 0xED))), 0, posY + stringH + 8, 640, posY + stringH + 8);
             */

            g.FillRectangle(new SolidBrush(Color.FromArgb(80, 80, 80)), 0, posY, 640, stringH + 8);
            //g.DrawRectangle(new Pen(new SolidBrush(Color.FromArgb(80, 80, 80)), 2f), 1, posY, 638, stringH + 8);

            for (int i = 0; i < stringInputSize; i++)
            {
                if (stringInput[i] == 0){
                    break;
                }
                else{
                    stringInputMerged += stringInput[i];
                }
            }

            drawStringCenterWithCursor(g, stringInputMerged, 15f, Color.White, posY + 4);
        }

        public override void KeyUp(System.Windows.Forms.Keys key, bool ctrl, bool alt, bool shift)
        {
        }

        public override void End()
        {
        }
    }
}

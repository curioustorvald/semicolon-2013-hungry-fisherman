using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDBase;
using System.Drawing;

namespace TowerDefense
{
    class SceneMain : Scene
    {
        //Resources res = Resources.resouces;
        Button startbtn;
        private Boolean transitionOngoing = false;
        private float transitionCounter = 0;
        private const float transitionLength = 1f;

        public override void Start()
        {
            startbtn = new Button(Resources.LPull(NameSet.Res.START_BUTTON)[0], Resources.LPull(NameSet.Res.START_BUTTON)[1], Resources.LPull(NameSet.Res.START_BUTTON)[2]);
            startbtn.setLocation((640 - Resources.LPull(NameSet.Res.START_BUTTON)[0].Width) / 2, 360);


            // Draw the image unaltered with its upper-left corner at (0, 0).

            startbtn.onClick(startbtn_click);



        }
        public override void Draw(Graphics g, float deltaSeconds)
        {
            if (!transitionOngoing)
            {
                drawMainOffset(g, transitionCounter);
            }
            else
            {
                if (transitionCounter < transitionLength)
                {
                    drawMainOffset(g, -1 * smoothEffect(640, transitionLength, transitionCounter));
                    drawMapSelectOffset(g, 640 - smoothEffect(640, transitionLength, transitionCounter));

                    ////Console.WriteLine("T: " + transitionCounter);
                    ////Console.WriteLine(smoothEffect(640, transitionLength, transitionCounter));

                    //transitionCounter = (int)(stopWatch.ElapsedMilliseconds);
                    transitionCounter += deltaSeconds;
                }
            }
        }

        public override void End()
        {
        }

        public override void MouseDown(float x, float y, System.Windows.Forms.MouseButtons button)
        {
        }

        public override void MouseMove(float x, float y, System.Windows.Forms.MouseButtons button)
        {
        }

        public override void MouseUp(float x, float y, System.Windows.Forms.MouseButtons button)
        {
        }
        public override void KeyDown(System.Windows.Forms.Keys key, bool ctrl, bool alt, bool shift)
        {
        }

        public override void KeyUp(System.Windows.Forms.Keys key, bool ctrl, bool alt, bool shift)
        {
        }

        private void drawMainOffset(Graphics g, float offX)
        {            
            g.DrawImage(Resources.Pull(NameSet.Res.MAIN_BACK), offX, 0, (int)Resources.Pull(NameSet.Res.MAIN_BACK).Width, (int)Resources.Pull(NameSet.Res.MAIN_BACK).Height);
            startbtn.DrawOffset(g, offX, 0);

            g.DrawString(System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(), new Font(new FontFamily("Segoe UI"), 10f), new SolidBrush(Color.AntiqueWhite), 5 + offX, 440);
            g.DrawString("Copyright © 2013 semicolon team. Do not distribute.", new Font(new FontFamily("Segoe UI"), 10f), new SolidBrush(Color.AntiqueWhite), 5 + offX, 455);

            //g.DrawString("FPS: " + VirtualRealm.Mdx.Common.Utility.CalculateFrameRate(), new Font(new FontFamily("Segoe UI"), 10f), new SolidBrush(Color.Black), 5, 5);
        }

        private void drawMapSelectOffset(Graphics g, float offX)
        {
            g.FillRectangle(new SolidBrush(Color.Yellow), offX, 0, 640, 480);
        }

        private static float smoothEffect(float scrollSize, float duration, float step)
        {
            return (float)(-0.5 * scrollSize * Math.Cos(Math.PI / duration * step) + 0.5 * scrollSize);
        }

        private static float fastEffect(float scrollSize, float duration, float step)
        {
            Single ret = (float)(-1 * (scrollSize / Math.Pow(duration, 2)) * Math.Pow(step - duration, 2) + scrollSize);
            
            if (ret == (1.0f / 0.0f))
            {
                //Console.WriteLine("Attempt to return infinite; maybe openingDuration is null");
                throw new ArithmeticException();
            }
            else
            {
                return ret;
            }
        }

        public void startbtn_click()
        {
            //scene transition
            /*transitionOngoing = true;

            if (transitionCounter >= transitionLength)
            {
                transitionOngoing = false;
                //stopWatch.Stop();
                //SceneManager.SetScene(new SceneMapSelect());
                SceneManager.SetScene(new SceneGame(1));
            }*/
            //SceneManager.SetScene(new SceneGame(1));
			//SceneManager.SetScene(new SceneStory(1));
            SceneManager.SetScene(new SceneMapSelect());
        }
    }
}

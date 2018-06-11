/*
 *      SceneExample.cs
 *      
 *      박혜준 (parkhj1114@gmail.com)
 *      2013 금산고등학교
 */
/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using TDBase;

namespace TowerDefense
{
    class SceneExample : Scene
    {
        Tex2D background;
        Font defaultFont, discFont;
        string disc = "인터넷(Internet, 누리망, 문화어: 인터네트)은 전 세계의 컴퓨터가 서로 연결되어 TCP/IP(Transmission Control Protocol/Internet Protocol)라는 통신 프로토콜을 이용해 정보를 주고받는 컴퓨터 네트워크이다." +
                        "인터넷이란 이름은 1973년 TCP/IP의 기본 아이디어를 생각해 낸 빈튼 서프와 밥 간이 \'네트워크의 네트워크\'를 지향하며 모든 컴퓨터를 하나의 통신망 안에 연결(Inter Network)하고자 하는 의도에서 이를 줄여 인터넷(Internet)이라고 처음 명명하였던 데 어원을 두고 있다. 이후 인터넷은 \"정보의 바다\"라고 불리면서 전 세계의 컴퓨터가 서로 연결되어 TCP/IP를 이용해 정보를 주고 받게 되었다.";

        Tex2D[] bttest = new Tex2D[3];
        Button bt;
        ImageSprite ip;
        

        float x, y;
        
        public override void Start()
        {
            background = new Tex2D("images\\ex_background.jpg");
            defaultFont = new Font(new FontFamily("맑은 고딕"), 16.0f);
            discFont = new Font(defaultFont.FontFamily, 8.0f);

            bttest[0] = new Tex2D("images\\over.jpg");
            bttest[1] = new Tex2D("images\\pressed.jpg");
            bttest[2] = new Tex2D("images\\released.jpg");


            bt = new Button(bttest[2], bttest[0], bttest[1], 100, 50);

        }

        public override void Draw(Graphics graphics, float deltaSeconds)
        {
            graphics.Clear(Color.Black);
            GDIController.DrawImage(graphics, background, 0, 0, 600, 480);
            bt.Draw(graphics, 210, 300);
            GDIController.DrawString(graphics, Color.White, discFont, -1, "x: " + x + ", y: " + y, 333, 330);

            //ip.Draw(graphics, 10);
        }

        public override void MouseDown(float x, float y, System.Windows.Forms.MouseButtons button)
        {
            MouseClass.setMouse(x, y, button, true);
        }

        public override void MouseMove(float x, float y, System.Windows.Forms.MouseButtons button)
        {
            MouseClass.setMouse(x, y, button);
        }

        public override void MouseUp(float x, float y, System.Windows.Forms.MouseButtons button)
        {
            MouseClass.setMouse(x, y, button, false);
        }

        public override void End()
        {
        }

        public override void KeyDown(System.Windows.Forms.Keys key, bool ctrl, bool alt, bool shift)
        {
            if (key == System.Windows.Forms.Keys.A)
            {
                Console.Write("A");
            }
        }

        public override void KeyUp(System.Windows.Forms.Keys key, bool ctrl, bool alt, bool shift)
        {
        }
    }
}*/

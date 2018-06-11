using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using TDBase;

namespace TowerDefense
{
    class SceneMapSelect : Scene
    {
        private Image background;

        private Button map1;
        private Button map2;
        private Button map3;

        public override void Draw(System.Drawing.Graphics g, float deltaSeconds)
        {
            //g.DrawString("FPS: " + VirtualRealm.Mdx.Common.Utility.CalculateFrameRate(), new Font(new FontFamily("Segoe UI"), 10f), new SolidBrush(Color.Black), 5, 5);
            g.DrawImage(background, 0, 0);

            map1.Draw(g);
            map2.Draw(g);
            map3.Draw(g);
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

        private void map1Click()
        {
            SceneManager.SetScene(new SceneStory(1));
        }

        private void map2Click()
        {
            SceneManager.SetScene(new SceneStory(2));
        }

        private void map3Click()
        {
            SceneManager.SetScene(new SceneStory(3));
        }

        public override void Start()
        {
            background = Image.FromFile("images/mapselect/bgr.png");

            map1 = new Button(Resources.LPull(NameSet.Res.MAPSEL_1)[0], Resources.LPull(NameSet.Res.MAPSEL_1)[1], Resources.LPull(NameSet.Res.MAPSEL_1)[1]);
            map2 = new Button(Resources.LPull(NameSet.Res.MAPSEL_2)[0], Resources.LPull(NameSet.Res.MAPSEL_2)[1], Resources.LPull(NameSet.Res.MAPSEL_2)[1]);
            map3 = new Button(Resources.LPull(NameSet.Res.MAPSEL_3)[0], Resources.LPull(NameSet.Res.MAPSEL_3)[1], Resources.LPull(NameSet.Res.MAPSEL_3)[1]);

            map1.setLocation(104, 214);
            map2.setLocation(266, 214);
            map3.setLocation(428, 214);

            map1.onClick(map1Click);
            map2.onClick(map2Click);
            map3.onClick(map3Click);
        }
    }
}

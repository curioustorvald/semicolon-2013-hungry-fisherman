using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using TDBase;

namespace TowerDefense
{
    class SceneSplash : Scene
    {

        private const float splashShowupTime = 3;
        private float splashTTL = 0;

        private float frameRate = 1 / 16f;
        private float splashFrameCounter = 0;

        private int splashFrame = 0;
        private ImageSprite splashImages;

        public override void Draw(Graphics graphics, float deltaSeconds)
        {
            splashTTL += deltaSeconds;
            splashFrameCounter += deltaSeconds;

            graphics.Clear(Color.White);

            if (splashTTL < splashShowupTime)
            {
                splashImages.DrawSpecificFrame(graphics, splashFrame);

                if (splashFrameCounter >= frameRate && splashFrame < 8)
                {
                    splashFrame++;
                    splashFrameCounter = 0;
                }

				/*if (splashFrame < 9){
					splashImages.Draw(graphics, 5);
					splashFrame++;
				}
				else{
					splashImages.DrawSpecificFrame(graphics, 8);
				}*/

                //Console.WriteLine(splashFrame);
            }
            else
            {
                SceneManager.SetScene(new SceneMain());
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

        public override void Start()
        {
            splashImages = new ImageSprite(Resources.LPull(NameSet.Res.SPLASH), 9, 1, 1);
            splashImages.setLocation(320, 240);
        }
    }
}

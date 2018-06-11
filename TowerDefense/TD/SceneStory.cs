using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using TDBase;

namespace TowerDefense
{
    class SceneStory : Scene
    {
	
		private Image storyImage = Image.FromFile("images/stories/storyConnected.png");
		private const float STORY_SHOWUP_TIME_PER_IMAGE = 4f;
		private float storyShowupCounter = 0;
		
		private int selectedStage;
		
		public SceneStory(int i){
			selectedStage = i;
		}
		
        public override void Draw(Graphics g, float deltaSeconds)
        {
            if (storyShowupCounter < STORY_SHOWUP_TIME_PER_IMAGE * 8)
            {
                if (storyShowupCounter < STORY_SHOWUP_TIME_PER_IMAGE * 7)
                {
                    g.DrawImage(storyImage, -(640 * 7 / (STORY_SHOWUP_TIME_PER_IMAGE * 7)) * storyShowupCounter, 0, storyImage.Width, storyImage.Height);
                }
                else
                {
                    g.DrawImage(storyImage, -640 * 7, 0, storyImage.Width, storyImage.Height);
                }

                storyShowupCounter += deltaSeconds;

            }
            else
            {
                SceneManager.SetScene(new SceneGame(selectedStage));
            }

            g.DrawString("Skip", new Font(new FontFamily("Segoe UI"), 12f), new SolidBrush(Color.White), 2, 452);
        }

        public override void End()
        {
        }

        public override void MouseDown(float x, float y, System.Windows.Forms.MouseButtons button)
        {
            if (y > 450)
            {
                SceneManager.SetScene(new SceneGame(selectedStage));
            }
        }

        public override void MouseMove(float x, float y, System.Windows.Forms.MouseButtons button)
        {
        }

        public override void MouseUp(float x, float y, System.Windows.Forms.MouseButtons button)
        {
        }

        public override void Start()
        {
        }
    }
}

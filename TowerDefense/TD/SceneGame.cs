using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using TDBase;
using TowerDefense.Mobs;
using TowerDefense.Towers;
using TowerDefense.Entities;
using TowerDefense.Player;
using TowerDefense.Stages;

namespace TowerDefense
{
    public class SceneGame: Scene
    {
        //public BaseMob[] mobContainer;
        //public int mobsNo = 0;
        //public BaseTower[] towerContainer;
        //public int towersNo = 0;
        //public BaseEntity[] entityContainer;
        //public int entitiesNo = 0;
        //Path path;
        public BaseStage currentStage;
        public int currentStageNo = 0;

        //디버그 옵션
        public bool isDebugMode = false;
        public bool infiniteLife = false;
        public bool infiniteMoney = false;

        public Font font = new Font(new FontFamily("Segoe UI"), 12f);

        public SceneGame(int stageNumber){
            currentStageNo = stageNumber;
            }

        public override void Start()
        {
            switch (currentStageNo)
            {
                case 1:
                    currentStage = new Map1();
                    break;
                case 2:
                    currentStage = new Map2();
                    break;
                case 3:
                    currentStage = new Map3();
                    break;
                default:
                    SceneManager.SetScene(new SceneGameOverAndHighscore());
                    break;
            }

            //Console.WriteLine("[GameScene] Loaded stage");

            //초기화
            new MenubarController();
            new GameController();
            new Player.Player();
            //Console.WriteLine("[GameScore] Initialised two game controllers.");
        }
        public override void Draw(Graphics g, float deltaSeconds)
        {
            if (currentStage.getLife() > 0)
            {
                //업데이트
                if (!GameController.isPaused)
                {
                    currentStage.Update(this, deltaSeconds);
                }

                Player.Player.Update(this);

                //렌더
                if (!GameController.isPaused)
                {
                    currentStage.Render(this, g);
                    Player.Player.Render(this, g);
                }
                else
                {
                    g.DrawImage(currentStage.stageImage, 0, 0);
                    Player.Player.Render(this, g);

                    float textHeight = GDIController.GetStringHeight(g, new Font(new FontFamily("Segoe UI"), 32), "P A U S E D");
                    float textWidth = GDIController.GetStringWidth(g, new Font(new FontFamily("Segoe UI"), 32), "P A U S E D");

                    g.FillRectangle(new SolidBrush(Color.FromArgb(127, 0, 0, 0)), 0, 0, 640, 480);
                    g.FillRectangle(new SolidBrush(Color.FromArgb(127, 0, 0, 0)), 0, 0, 480, 480);
                    g.DrawString("P A U S E D", new Font(new FontFamily("Segoe UI"), 32), new SolidBrush(Color.White), (480 - textWidth) / 2, (480 - textHeight) / 2);
                }

                //디버그 정보
                if (isDebugMode)
                {
                    g.DrawString("MouseX: " + MouseClass.mouseX + ", MouseY: " + MouseClass.mouseY, font, new SolidBrush(Color.White), 10, 10);
                    g.DrawString("# of tower: " + currentStage.towersNo, font, new SolidBrush(Color.White), 10, 25);
                    g.DrawString("# of mob: " + currentStage.mobsNo, font, new SolidBrush(Color.White), 10, 40);
                    g.DrawString("# of entity: " + currentStage.entitiesNo, font, new SolidBrush(Color.White), 10, 55);

                    Vector[] pvector = currentStage.path.getVector();

                    for (int i = 0; i < currentStage.path.getPathno() - 1; i++)
                    {
                        g.DrawLine(new Pen(new SolidBrush(Color.Red)), pvector[i].getX(), pvector[i].getY(), pvector[i + 1].getX(), pvector[i + 1].getY());
                    }
                }
            }
            else
            {
                SceneManager.SetScene(new SceneGameOverAndHighscore());
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

    }
}

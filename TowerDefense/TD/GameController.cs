using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDBase;
using TowerDefense.Towers;
using TowerDefense.Stages;

namespace TowerDefense.Player
{
    class GameController
    {
        private static int currentMode = 0; //0: 타워 선택, 1: 타워 배치
        public static int selectedTower = -1;

        private static bool towerBuildable = true;
        private static bool towerSelectable = true;
        //                                       North, West, South, East
        private static Color[] buildableColour = { Color.FromArgb(255, 0, 0), Color.FromArgb(0, 255, 0), Color.FromArgb(0, 0, 255), Color.FromArgb(255, 255, 0) };

        private static Color mousePositionColour;
        private static Bitmap imageBitmap;

        public static BaseTower selectedTowerClass;

        public static bool isMobSpawning;
        public static bool isPaused = false;

        public GameController()
        {
            isMobSpawning = false;
            currentMode = 0;
            selectedTower = -1;

            towerBuildable = true;
            towerSelectable = true;

            isPaused = false;
        }

        public static void Update(SceneGame gs)
        {
            //selectedTowerClass = new FishingNet();
            //selectedTowerClass = new StoneThrower();
            //selectedTowerClass = new Harpoon();
            //selectedTowerClass = new Fishing();
            //selectedTowerClass = new Bait();

            imageBitmap = new Bitmap(gs.currentStage.stageImageBuildable);
            //stageImageBuildable에서 마우스 위치의 색상 얻기
            if (MouseClass.mouseX >= 0 && MouseClass.mouseY >= 0 && MouseClass.mouseX <= 479 && MouseClass.mouseY <= 479)
            {
                mousePositionColour = imageBitmap.GetPixel((int)MouseClass.mouseX, (int)MouseClass.mouseY); //맵 상에서 마우스 위치의 색상 코드 구하기
            }
            //타워 배치 테스트
            if (currentMode == 1 && MouseClass.mouseX >= 0 && MouseClass.mouseY >= 0 && MouseClass.mouseX <= 479 && MouseClass.mouseY <= 479 && MouseClass.isClicked)
            {                
                bool towerNotOverlapping = true;
                bool towerBuildableByColour = false;
                int towerHeading = -1;

                //겹치는 타워가 있는지 보기
                for (int i = 0; i < gs.currentStage.towersNo; i++)
                {
                    BaseTower tower =  gs.currentStage.towerContainer[i];


                    if (MouseClass.mouseX > tower.posX - tower.Width && MouseClass.mouseX < tower.posX + tower.Width &&
                        MouseClass.mouseY > tower.posY - tower.Height && MouseClass.mouseY < tower.posY + tower.Height)
                    {
                        towerNotOverlapping = false;
                        //Console.WriteLine("[GameController] Tower overlapping");
                        break;
                    }
                }
                //타워를 지을 수 있는 곳인지 보기
                for (int i = 0; i < buildableColour.Length; i++)
                {
                    if (mousePositionColour == buildableColour[i])
                    {
                        towerBuildableByColour = true;
                        towerHeading = i;
                        break;
                    }
                }
                //타워를 지음
                if (towerBuildableByColour && towerBuildable && towerNotOverlapping)
                {
                    //BaseTower tower = new TestTower();
                    if (gs.currentStage.towersNo < BaseStage.maxTowers && Player.getMoney() >= selectedTowerClass.getBuyPrice())
                    {
                        gs.currentStage.spawnNewTower(gs, selectedTowerClass, MouseClass.mouseX, MouseClass.mouseY, towerHeading);
                    }
                    else if (gs.currentStage.towersNo >= BaseStage.maxTowers)
                    {
                        towerBuildable = false;
                        towerSelectable = false;
                        currentMode = 0;
                        System.Windows.Forms.MessageBox.Show("타워가 너무 많습니다.");
                    }
                    towerBuildable = false;
                    towerSelectable = false;
                    currentMode = 0;
                    //Console.WriteLine("[GameController] Placed tower " + selectedTowerClass.Name + ", heading " + towerHeading);
                }
            }
            if (currentMode == 1 && !MouseClass.isClicked){
                towerBuildable = true;
            }

            //타워 선택 테스트
            if (currentMode == 0 && MouseClass.mouseX >= 0 && MouseClass.mouseY >= 0 && towerSelectable && MouseClass.mouseX <= 479 && MouseClass.mouseY <= 479 && MouseClass.isClicked)
            {
                
                for (int i = 0; i < gs.currentStage.towersNo; i++)
                {
                    BaseTower tower = gs.currentStage.towerContainer[i];


                    if (MouseClass.mouseX > tower.posX - tower.Width/2 && MouseClass.mouseX < tower.posX + tower.Width/2 &&
                        MouseClass.mouseY > tower.posY - tower.Height/2 && MouseClass.mouseY < tower.posY + tower.Height/2)
                    {
                        //선택 해제
                        /*if (selectedTower == i)
                        {
                            selectedTower = -1;
                            towerSelectable = false;
                            break;
                        }*/
                        //선택
                        //else
                        if (selectedTower != i)
                        {
                            selectedTower = i;
                            towerSelectable = false;
                            //Console.WriteLine("[GameController] Selected tower {0}", i);
                            break;
                        }
                    }
                    else
                    {
                        selectedTower = -1;
                    }
                }
            }
            if (currentMode == 0 && !MouseClass.isClicked)
            {
                towerSelectable = true;
            }
        }

        public static void Render(SceneGame gs, Graphics g)
        {
            //타워가 선택되었을 때 감지 범위 그리기
            if (gs.isDebugMode && selectedTower >= 0 && gs.currentStage.towerContainer[selectedTower] != null)
            {
                gs.currentStage.towerContainer[selectedTower].RenderRange(g);
            }
            if (selectedTower >= 0 && gs.currentStage.towerContainer[selectedTower] != null)
            {
                BaseTower tower = gs.currentStage.towerContainer[selectedTower];
                g.DrawEllipse(new Pen(Color.AliceBlue), tower.posX - tower.Width / 2 - 5, tower.posY - tower.Height / 2 - 5, tower.Width + 10, tower.Height + 10);
            }
            //오버레이
            if (currentMode == 1)
            {
                //방향에 맞게 그리기
                for (int i = 0; i < buildableColour.Length; i++)
                {
                    if (mousePositionColour == buildableColour[i])
                    {
                        int radius = selectedTowerClass.getRadius();
                        int towerPosX = (int)(MouseClass.mouseX);
                        int towerPosY = (int)(MouseClass.mouseY);

                        int towerW = selectedTowerClass.Width;
                        int towerH = selectedTowerClass.Height;

                        selectedTowerClass.Spawn((int)(MouseClass.mouseX), (int)(MouseClass.mouseY), i);

                        if (MouseClass.mouseX < 480 && MouseClass.mouseY < 480)
                        {
                            selectedTowerClass.Render(gs, g, 1);

                            //g.FillEllipse(new SolidBrush(Color.FromArgb(31, 240, 240, 255)), towerPosX - radius, towerPosY - radius, radius * 2, radius * 2);
                            //g.DrawEllipse(new Pen(Color.FromArgb(63, 138, 130, 255), 2), towerPosX - radius, towerPosY - radius, radius * 2, radius * 2);
                            selectedTowerClass.RenderRange(g);
                        }
                         
                        break;
                    }
                }

                //현재 존재하는 타워의 감지범위 그리기
                /*for (int i = 0; i < gs.currentStage.towersNo; i++)
                {
                    if (gs.isDebugMode)
                    {
                        gs.currentStage.towerContainer[i].RenderRange(g);
                    }
                }*/
            }
            //디버그
            if (gs.isDebugMode)
            {
                g.DrawString("Selected tower: " + selectedTower, gs.font, new SolidBrush(Color.White), 10, 70);
                //g.FillRectangle(new SolidBrush(mousePositionColour), 10, 85, 15, 15);
                g.DrawString("Score: " + Player.getScore(), gs.font, new SolidBrush(Color.White), 10, 85);
                g.DrawString("Playing time: " + gs.currentStage.elapsedTime, gs.font, new SolidBrush(Color.White), 10, 100);
                g.DrawString("Mob spawn interval: " + gs.currentStage.mobSpawnInterval, gs.font, new SolidBrush(Color.White), 10, 115);
            }
        }

        public static void changeMode()
        {
            selectedTower = -1;
            currentMode = (currentMode == 0) ? 1 : 0;
        }

        public static void setSelectMode()
        {
            selectedTower = -1;
            currentMode = 0;
            //selectedTowerClass = null;
        }

        public static void setBuildMode()
        {
            selectedTower = -1;
            currentMode = 1;
        }

        public static int getCurrentMode()
        {
            return currentMode;
        }
    }
}

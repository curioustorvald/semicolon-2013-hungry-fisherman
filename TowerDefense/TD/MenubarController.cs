using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using TowerDefense.Towers;
using TowerDefense;
using TowerDefense.Stages;
using TDBase;

namespace TowerDefense.Player
{
    class MenubarController
    {
        private static Button[] towerBuildButton;

        private static Button[] towerBaitUpgradeButton;
        private static Button[] towerFishingUpgradeButton;
        private static Button[] towerHarpoonUpgradeButton;
        private static Button[] towerNetUpgradeButton;
        private static Button[] towerStoneUpgradeButton;
          
        private static Button goButton;
        private static Button stopButton;
          
        private static Button homeButton;
        private static Button soundToggleButton;
        //private static Button optionButton;

        private static Button escMenuResume;
        private static Button escMenuRestart;
        private static Button escMenuExit;
        private static Button deselect;
        private static Button towerSellButton;

        private static ImageSprite menubarBuild;
        private static ImageSprite menubarUpgrade;
        //private static ImageSprite escMenu;

        private static BaseTower selectedTower;

        private static bool towerUpgrade = false;
        private static bool towerSell = false;

        private static bool gameStarted;

        private static bool resetGame = false;

        private static bool noPause = true;

        public MenubarController()
        {
            gameStarted = false;

            menubarBuild = new ImageSprite(Resources.LPull(NameSet.Res.SIDEBAR_1), 1, 1, 1);
            menubarUpgrade = new ImageSprite(Resources.LPull(NameSet.Res.SIDEBAR_2), 1, 1, 1);
            //escMenu = new ImageSprite(Resources.LPull(NameSet.Res.ESCMENU_BACKGROUND), 1, 1, 1);

            menubarBuild.setLocation(480 + 80, 240);
            menubarUpgrade.setLocation(480 + 80, 240);
            //escMenu.setLocation(320, 240);

            towerBuildButton = new Button[5];
            towerBaitUpgradeButton = new Button[4];
            towerFishingUpgradeButton = new Button[4];
            towerHarpoonUpgradeButton = new Button[4];
            towerNetUpgradeButton = new Button[4];
            towerStoneUpgradeButton = new Button[4];

            towerBuildButton[0] = new Button(Resources.LPull(NameSet.Res.TOWER_BUTTON_1)[0], Resources.LPull(NameSet.Res.TOWER_BUTTON_1)[1], Resources.LPull(NameSet.Res.TOWER_BUTTON_1)[2]);
            towerBuildButton[1] = new Button(Resources.LPull(NameSet.Res.TOWER_BUTTON_2)[0], Resources.LPull(NameSet.Res.TOWER_BUTTON_2)[1], Resources.LPull(NameSet.Res.TOWER_BUTTON_2)[2]);
            towerBuildButton[2] = new Button(Resources.LPull(NameSet.Res.TOWER_BUTTON_3)[0], Resources.LPull(NameSet.Res.TOWER_BUTTON_3)[1], Resources.LPull(NameSet.Res.TOWER_BUTTON_3)[2]);
            towerBuildButton[3] = new Button(Resources.LPull(NameSet.Res.TOWER_BUTTON_4)[0], Resources.LPull(NameSet.Res.TOWER_BUTTON_4)[1], Resources.LPull(NameSet.Res.TOWER_BUTTON_4)[2]);
            towerBuildButton[4] = new Button(Resources.LPull(NameSet.Res.TOWER_BUTTON_5)[0], Resources.LPull(NameSet.Res.TOWER_BUTTON_5)[1], Resources.LPull(NameSet.Res.TOWER_BUTTON_5)[2]);
            deselect = new Button(Resources.LPull(NameSet.Res.DESELECT_BUTTON)[0], Resources.LPull(NameSet.Res.DESELECT_BUTTON)[0], Resources.LPull(NameSet.Res.DESELECT_BUTTON)[0]);

            towerBuildButton[0].setLocation(480 + 30, 125);
            towerBuildButton[1].setLocation(480 + 95, 125);
            towerBuildButton[2].setLocation(480 + 30, 195);
            towerBuildButton[3].setLocation(480 + 95, 195);
            towerBuildButton[4].setLocation(480 + 30, 265);
            deselect.setLocation(480 + 95, 265);


            towerBaitUpgradeButton[0] = new Button(Resources.LPull(NameSet.Res.BAIT_UPGRADE)[0], Resources.LPull(NameSet.Res.BAIT_UPGRADE)[1], Resources.LPull(NameSet.Res.BAIT_UPGRADE)[2]);
            towerBaitUpgradeButton[1] = new Button(Resources.LPull(NameSet.Res.BAIT_UPGRADE)[3], Resources.LPull(NameSet.Res.BAIT_UPGRADE)[4], Resources.LPull(NameSet.Res.BAIT_UPGRADE)[5]);
            towerBaitUpgradeButton[2] = new Button(Resources.LPull(NameSet.Res.BAIT_UPGRADE)[6], Resources.LPull(NameSet.Res.BAIT_UPGRADE)[7], Resources.LPull(NameSet.Res.BAIT_UPGRADE)[8]);
            towerBaitUpgradeButton[3] = new Button(Resources.LPull(NameSet.Res.BAIT_UPGRADE)[9], Resources.LPull(NameSet.Res.BAIT_UPGRADE)[10], Resources.LPull(NameSet.Res.BAIT_UPGRADE)[11]);

            towerBaitUpgradeButton[0].setLocation(480 + 32, 126);
            towerBaitUpgradeButton[1].setLocation(480 + 32, 176);
            towerBaitUpgradeButton[2].setLocation(480 + 32, 226);
            towerBaitUpgradeButton[3].setLocation(480 + 32, 276);

            
            towerFishingUpgradeButton[0] = new Button(Resources.LPull(NameSet.Res.FISHING_UPGRADE)[0], Resources.LPull(NameSet.Res.FISHING_UPGRADE)[1], Resources.LPull(NameSet.Res.FISHING_UPGRADE)[2]);
            towerFishingUpgradeButton[1] = new Button(Resources.LPull(NameSet.Res.FISHING_UPGRADE)[3], Resources.LPull(NameSet.Res.FISHING_UPGRADE)[4], Resources.LPull(NameSet.Res.FISHING_UPGRADE)[5]);
            towerFishingUpgradeButton[2] = new Button(Resources.LPull(NameSet.Res.FISHING_UPGRADE)[6], Resources.LPull(NameSet.Res.FISHING_UPGRADE)[7], Resources.LPull(NameSet.Res.FISHING_UPGRADE)[8]);
            towerFishingUpgradeButton[3] = new Button(Resources.LPull(NameSet.Res.FISHING_UPGRADE)[9], Resources.LPull(NameSet.Res.FISHING_UPGRADE)[10], Resources.LPull(NameSet.Res.FISHING_UPGRADE)[11]);
            
            towerFishingUpgradeButton[0].setLocation(480 + 32, 126);
            towerFishingUpgradeButton[1].setLocation(480 + 32, 176);
            towerFishingUpgradeButton[2].setLocation(480 + 32, 226);
            towerFishingUpgradeButton[3].setLocation(480 + 32, 276);


            towerHarpoonUpgradeButton[0] = new Button(Resources.LPull(NameSet.Res.HARPOON_UPGRADE)[0], Resources.LPull(NameSet.Res.HARPOON_UPGRADE)[1], Resources.LPull(NameSet.Res.HARPOON_UPGRADE)[2]);
            towerHarpoonUpgradeButton[1] = new Button(Resources.LPull(NameSet.Res.HARPOON_UPGRADE)[3], Resources.LPull(NameSet.Res.HARPOON_UPGRADE)[4], Resources.LPull(NameSet.Res.HARPOON_UPGRADE)[5]);
            towerHarpoonUpgradeButton[2] = new Button(Resources.LPull(NameSet.Res.HARPOON_UPGRADE)[6], Resources.LPull(NameSet.Res.HARPOON_UPGRADE)[7], Resources.LPull(NameSet.Res.HARPOON_UPGRADE)[8]);
            towerHarpoonUpgradeButton[3] = new Button(Resources.LPull(NameSet.Res.HARPOON_UPGRADE)[9], Resources.LPull(NameSet.Res.HARPOON_UPGRADE)[10], Resources.LPull(NameSet.Res.HARPOON_UPGRADE)[11]);

            towerHarpoonUpgradeButton[0].setLocation(480 + 32, 126);
            towerHarpoonUpgradeButton[1].setLocation(480 + 32, 176);
            towerHarpoonUpgradeButton[2].setLocation(480 + 32, 226);
            towerHarpoonUpgradeButton[3].setLocation(480 + 32, 276);


            towerNetUpgradeButton[0] = new Button(Resources.LPull(NameSet.Res.NET_UPGRADE)[0], Resources.LPull(NameSet.Res.NET_UPGRADE)[1], Resources.LPull(NameSet.Res.NET_UPGRADE)[2]);
            towerNetUpgradeButton[1] = new Button(Resources.LPull(NameSet.Res.NET_UPGRADE)[3], Resources.LPull(NameSet.Res.NET_UPGRADE)[4], Resources.LPull(NameSet.Res.NET_UPGRADE)[5]);
            towerNetUpgradeButton[2] = new Button(Resources.LPull(NameSet.Res.NET_UPGRADE)[6], Resources.LPull(NameSet.Res.NET_UPGRADE)[7], Resources.LPull(NameSet.Res.NET_UPGRADE)[8]);
            towerNetUpgradeButton[3] = new Button(Resources.LPull(NameSet.Res.NET_UPGRADE)[9], Resources.LPull(NameSet.Res.NET_UPGRADE)[10], Resources.LPull(NameSet.Res.NET_UPGRADE)[11]);

            towerNetUpgradeButton[0].setLocation(480 + 32, 126);
            towerNetUpgradeButton[1].setLocation(480 + 32, 176);
            towerNetUpgradeButton[2].setLocation(480 + 32, 226);
            towerNetUpgradeButton[3].setLocation(480 + 32, 276);


            towerStoneUpgradeButton[0] = new Button(Resources.LPull(NameSet.Res.STONE_UPGRADE)[0], Resources.LPull(NameSet.Res.STONE_UPGRADE)[1], Resources.LPull(NameSet.Res.STONE_UPGRADE)[2]);
            towerStoneUpgradeButton[1] = new Button(Resources.LPull(NameSet.Res.STONE_UPGRADE)[3], Resources.LPull(NameSet.Res.STONE_UPGRADE)[4], Resources.LPull(NameSet.Res.STONE_UPGRADE)[5]);
            towerStoneUpgradeButton[2] = new Button(Resources.LPull(NameSet.Res.STONE_UPGRADE)[6], Resources.LPull(NameSet.Res.STONE_UPGRADE)[7], Resources.LPull(NameSet.Res.STONE_UPGRADE)[8]);
            towerStoneUpgradeButton[3] = new Button(Resources.LPull(NameSet.Res.STONE_UPGRADE)[9], Resources.LPull(NameSet.Res.STONE_UPGRADE)[10], Resources.LPull(NameSet.Res.STONE_UPGRADE)[11]);

            towerStoneUpgradeButton[0].setLocation(480 + 32, 126);
            towerStoneUpgradeButton[1].setLocation(480 + 32, 176);
            towerStoneUpgradeButton[2].setLocation(480 + 32, 226);
            towerStoneUpgradeButton[3].setLocation(480 + 32, 276);


            goButton = new Button(Resources.LPull(NameSet.Res.GO_BUTTON)[0], Resources.LPull(NameSet.Res.GO_BUTTON)[1], Resources.LPull(NameSet.Res.GO_BUTTON)[2]);

            goButton.setLocation(480 + 20, 380);
            goButton.onClick(eventGoButton);

            stopButton = new Button(Resources.LPull(NameSet.Res.STOP_BUTTON)[0], Resources.LPull(NameSet.Res.STOP_BUTTON)[1], Resources.LPull(NameSet.Res.STOP_BUTTON)[2]);

            stopButton.setLocation(480 + 20, 380);
            stopButton.onClick(eventStopButton);


            homeButton = new Button(Resources.LPull(NameSet.Res.HOME_BUTTON)[0], Resources.LPull(NameSet.Res.HOME_BUTTON)[1], Resources.LPull(NameSet.Res.HOME_BUTTON)[2]);

            homeButton.setLocation(480 + 20, 410);


            soundToggleButton = new Button(Resources.LPull(NameSet.Res.SOUND_TOGGLE_BUTTON)[0], Resources.LPull(NameSet.Res.SOUND_TOGGLE_BUTTON)[1], Resources.LPull(NameSet.Res.SOUND_TOGGLE_BUTTON)[2]);

            soundToggleButton.setLocation(480 + 68, 410);


            towerSellButton = new Button(Resources.LPull(NameSet.Res.SELL_BUTTON)[0], Resources.LPull(NameSet.Res.SELL_BUTTON)[1], Resources.LPull(NameSet.Res.SELL_BUTTON)[2]);

            towerSellButton.setLocation(480 + 32, 325);
        }

        public static void Update(SceneGame gs)
        {
            /*if (modeChangable && MouseClass.mouseX >= 480 && MouseClass.isClicked)
            {
                GameController.changeMode();
                modeChangable = false;

                Console.WriteLine("[MenubarCotroller] Changed controller mode.");
            }

            if (!MouseClass.isClicked)
            {
                modeChangable = true;
            }*/

            if (!GameController.isPaused)
            {
                if (GameController.selectedTower >= 0)
                {
                    selectedTower = gs.currentStage.towerContainer[GameController.selectedTower];
                }

                if (getMenubarMode() == "towerBuild")
                {
                    deselect.onClick(eventDeselect);
                }
                else if (getMenubarMode() == "towerSelecting")
                {
                    towerBuildButton[0].onClick(towerBuild1Click);
                    towerBuildButton[1].onClick(towerBuild2Click);
                    towerBuildButton[2].onClick(towerBuild3Click);
                    towerBuildButton[3].onClick(towerBuild4Click);
                    towerBuildButton[4].onClick(towerBuild5Click);
                }
                //else if (getMenubarMode() == "towerUpgrade")
                {
                    towerSellButton.onClick(eventSellButton);
                    
                    //if (GameController.selectedTowerClass != null)
                    if (GameController.selectedTower >= 0)
                    {
                        if (GameController.selectedTowerClass.Classifier == NameSet.Tower.TOWER_BAIT)
                        {
                            towerBaitUpgradeButton[0].onClick(setTowerUpgrade);
                            towerBaitUpgradeButton[1].onClick(setTowerUpgrade);
                            towerBaitUpgradeButton[2].onClick(setTowerUpgrade);
                            towerBaitUpgradeButton[3].onClick(setTowerUpgrade);
                        }
                        else if (GameController.selectedTowerClass.Classifier == NameSet.Tower.TOWER_FISHER)
                        {
                            towerFishingUpgradeButton[0].onClick(setTowerUpgrade);
                            towerFishingUpgradeButton[1].onClick(setTowerUpgrade);
                            towerFishingUpgradeButton[2].onClick(setTowerUpgrade);
                            towerFishingUpgradeButton[3].onClick(setTowerUpgrade);
                        }
                        else if (GameController.selectedTowerClass.Classifier == NameSet.Tower.TOWER_FISHING_NET)
                        {
                            towerNetUpgradeButton[0].onClick(setTowerUpgrade);
                            towerNetUpgradeButton[1].onClick(setTowerUpgrade);
                            towerNetUpgradeButton[2].onClick(setTowerUpgrade);
                            towerNetUpgradeButton[3].onClick(setTowerUpgrade);
                        }
                        else if (GameController.selectedTowerClass.Classifier == NameSet.Tower.TOWER_HARPOON)
                        {
                            towerHarpoonUpgradeButton[0].onClick(setTowerUpgrade);
                            towerHarpoonUpgradeButton[1].onClick(setTowerUpgrade);
                            towerHarpoonUpgradeButton[2].onClick(setTowerUpgrade);
                            towerHarpoonUpgradeButton[3].onClick(setTowerUpgrade);
                        }
                        else if (GameController.selectedTowerClass.Classifier == NameSet.Tower.TOWER_STONE_THROWER)
                        {
                            towerStoneUpgradeButton[0].onClick(setTowerUpgrade);
                            towerStoneUpgradeButton[1].onClick(setTowerUpgrade);
                            towerStoneUpgradeButton[2].onClick(setTowerUpgrade);
                            towerStoneUpgradeButton[3].onClick(setTowerUpgrade);
                        }
                    }
                    /*else
                    {
                        towerStoneUpgradeButton[0].onClick(null);
                        towerStoneUpgradeButton[1].onClick(null);
                        towerStoneUpgradeButton[2].onClick(null);
                        towerStoneUpgradeButton[3].onClick(null);
                    }*/

                    doTowerUpgrade(gs);
                    doTowerSell(gs);
                }
            }
            else
            {
                towerBuildButton[0].onClick(null);
                towerBuildButton[1].onClick(null);
                towerBuildButton[2].onClick(null);
                towerBuildButton[3].onClick(null);
                towerBuildButton[4].onClick(null);
            }

            goButton.onClick(eventGoButton);
            stopButton.onClick(eventStopButton);

            if (noPause && !MouseClass.isClicked && gameStarted)
            {
                noPause = false;
            }


            if (resetGame)
            {
                new SceneManager().SetScene(new SceneGame(Player.currentStage));
            }
        }

        public static void Render(SceneGame gs, Graphics g)
        {
            //기본 판 그리기
            //homeButton.Draw(g);
            //soundToggleButton.Draw(g);
            //optionButton.Draw(g);
            
            if (getMenubarMode() == "towerBuild")
            {
                //구입 판 그리기
                menubarBuild.Draw(g);

                deselect.Draw(g);
            }
            else if (getMenubarMode() == "towerSelecting")
            {
                menubarBuild.Draw(g);

                drawMenubarBuildButtons(g);
            }
            else if (getMenubarMode() == "towerUpgrade")
            {
                //업그레이드 판 그리기
                menubarUpgrade.Draw(g);

                drawMenubarUpgradeButtons(g);
            }

            //라이프와 소지금
            int moneyStrHeight = (int)(GDIController.GetStringHeight(g, new Font(new FontFamily("Segoe UI"), 15), Player.getMoney().ToString()));
            int lifeStrHeight = (int)(GDIController.GetStringHeight(g, new Font(new FontFamily("Segoe UI"), 15), gs.currentStage.getLife().ToString()));
            int towersStrHeight = (int)(GDIController.GetStringHeight(g, new Font(new FontFamily("Segoe UI"), 15), gs.currentStage.towersNo.ToString() + " / " + BaseStage.maxTowers.ToString()));

            g.DrawString(Player.getMoney().ToString(), new Font(new FontFamily("Segoe UI"), 15), new SolidBrush(Color.Black), 480 + 64, 25 + (30 - moneyStrHeight) / 2);
            g.DrawString(gs.currentStage.getLife().ToString(), new Font(new FontFamily("Segoe UI"), 15), new SolidBrush(Color.Black), 480 + 64, 65 + (30 - lifeStrHeight) / 2);
            g.DrawString(gs.currentStage.towersNo.ToString() + " / " + BaseStage.maxTowers.ToString(), new Font(new FontFamily("Segoe UI"), 15), new SolidBrush(Color.Black), 480 + 64, 444 + (30 - towersStrHeight) / 2);

            if (!gameStarted || GameController.isPaused)
            {
                goButton.Draw(g);
            }
            else
            {
                stopButton.Draw(g);
            }
        }

        private static string getMenubarMode()
        {
            //if (GameController.getCurrentMode() == 0 && GameController.selectedTower >= 0)
            if (GameController.selectedTower >= 0)
            {
                return "towerUpgrade"; //업그레이드
            }
            else if (GameController.getCurrentMode() == 0 && GameController.selectedTower < 0)
            {
                return "towerSelecting"; //선택
            }
            else
            {
                return "towerBuild"; //타워 건설
            }
        }

        //버튼 클릭 이벤트: 타워 건설/배치
        private static void towerBuild1Click()
        {
            if (getMenubarMode() == "towerSelecting")
            {
                GameController.selectedTowerClass = new Fishing();
                GameController.setBuildMode();
            }
        }

        private static void towerBuild2Click()
        {
            if (getMenubarMode() == "towerSelecting")
            {
                GameController.selectedTowerClass = new Harpoon();
                GameController.setBuildMode();
            }
        }

        private static void towerBuild3Click()
        {
            if (getMenubarMode() == "towerSelecting")
            {
                GameController.selectedTowerClass = new FishingNet();
                GameController.setBuildMode();

            }
        }

        private static void towerBuild4Click()
        {
            if (getMenubarMode() == "towerSelecting")
            {
                GameController.selectedTowerClass = new Bait();
                GameController.setBuildMode();
            }
        }

        private static void towerBuild5Click()
        {
            if (getMenubarMode() == "towerSelecting")
            {
                GameController.selectedTowerClass = new StoneThrower();
                GameController.setBuildMode();
            }
        }

        //버튼 클릭 이벤트: 단계별 타워 업그레이드
        private static void setTowerUpgrade()
        {
            towerUpgrade = true;
        }


        private static void eventGoButton()
        {

            //initiate wave
            GameController.isMobSpawning = true;
            GameController.isPaused = false;
            gameStarted = true;
            //remove button
            //goButton = null;
            noPause = true;
        }

        private static void eventStopButton()
        {
            if (!noPause)
            {
                GameController.isPaused = true;
            }
        }

        private static void eventSellButton()
        {
            towerSell = true;
        }

        private static void eventEscMenuRestart()
        {
            resetGame = true;
        }

        private static void eventEscMenuExit()
        {
            new SceneManager().SetScene(new SceneMain());
        }

        private static void eventDeselect()
        {
            GameController.setSelectMode();
        }
        //버튼 클릭 이벤트 추가 바람


        private static void doTowerUpgrade(SceneGame gs)
        {
            if (towerUpgrade)
            {
                gs.currentStage.towerContainer[GameController.selectedTower].upgrade();
                Console.WriteLine("[Menubar] Performed tower upgrading.");
                towerUpgrade = false;
            }
        }

        private static void doTowerSell(SceneGame gs)
        {
            if (towerSell)
            {
                gs.currentStage.towerContainer[GameController.selectedTower].Sell(gs);

                GameController.setSelectMode();

                towerSell = false;
            }
        }

        private static void drawMenubarBuildButtons(Graphics g)
        {
            towerBuildButton[0].Draw(g);
            towerBuildButton[1].Draw(g);
            towerBuildButton[2].Draw(g);
            towerBuildButton[3].Draw(g);
            towerBuildButton[4].Draw(g);
        }

        private static void drawMenubarUpgradeButtons(Graphics g)
        {
            if (GameController.selectedTower >= 0 && selectedTower != null)
            {
                //완료된 업그레이드는 ImageSprite, 이용 가능한 업그레이드는 버튼으로 그리기
                if (selectedTower.Classifier == NameSet.Tower.TOWER_BAIT)
                {
                    if (selectedTower.getTier() == 0)
                    {
                        towerBaitUpgradeButton[0].Draw(g);
                    }
                    else if (selectedTower.getTier() == 1)
                    {
                        towerBaitUpgradeButton[1].Draw(g);
                    }
                    else if (selectedTower.getTier() == 2)
                    {
                        towerBaitUpgradeButton[2].Draw(g);
                    }
                    else if (selectedTower.getTier() == 3)
                    {
                        towerBaitUpgradeButton[3].Draw(g);
                    }
                    else
                    {
                        //아무것도 그리지 않음
                    }
                }
                else if (selectedTower.Classifier == NameSet.Tower.TOWER_FISHER)
                {
                    if (selectedTower.getTier() == 0)
                    {
                        towerFishingUpgradeButton[0].Draw(g);
                    }
                    else if (selectedTower.getTier() == 1)
                    {
                        towerFishingUpgradeButton[1].Draw(g);
                    }
                    else if (selectedTower.getTier() == 2)
                    {
                        towerFishingUpgradeButton[2].Draw(g);
                    }
                    else if (selectedTower.getTier() == 3)
                    {
                        towerFishingUpgradeButton[3].Draw(g);
                    }
                    else
                    {
                        //아무것도 그리지 않음
                    }
                }
                else if (selectedTower.Classifier == NameSet.Tower.TOWER_FISHING_NET)
                {
                    if (selectedTower.getTier() == 0)
                    {
                        towerNetUpgradeButton[0].Draw(g);
                    }
                    else if (selectedTower.getTier() == 1)
                    {
                        towerNetUpgradeButton[1].Draw(g);
                    }
                    else if (selectedTower.getTier() == 2)
                    {
                        towerNetUpgradeButton[2].Draw(g);
                    }
                    else if (selectedTower.getTier() == 3)
                    {
                        towerNetUpgradeButton[3].Draw(g);
                    }
                    else
                    {

                    }
                }
                else if (selectedTower.Classifier == NameSet.Tower.TOWER_HARPOON)
                {
                    if (selectedTower.getTier() == 0)
                    {
                        towerHarpoonUpgradeButton[0].Draw(g);
                    }
                    else if (selectedTower.getTier() == 1)
                    {
                        towerHarpoonUpgradeButton[1].Draw(g);
                    }
                    else if (selectedTower.getTier() == 2)
                    {
                        towerHarpoonUpgradeButton[2].Draw(g);
                    }
                    else if (selectedTower.getTier() == 3)
                    {
                        towerHarpoonUpgradeButton[3].Draw(g);
                    }
                    else
                    {

                    }
                }
                else if (selectedTower.Classifier == NameSet.Tower.TOWER_STONE_THROWER)
                {
                    if (selectedTower.getTier() == 0)
                    {
                        towerStoneUpgradeButton[0].Draw(g);
                    }
                    else if (selectedTower.getTier() == 1)
                    {
                        towerStoneUpgradeButton[1].Draw(g);
                    }
                    else if (selectedTower.getTier() == 2)
                    {
                        towerStoneUpgradeButton[2].Draw(g);
                    }
                    else if (selectedTower.getTier() == 3)
                    {
                        towerStoneUpgradeButton[3].Draw(g);
                    }
                    else
                    {

                    }
                }
                else
                {
                    throw new TDNoSuchTowerException();
                }

                towerSellButton.Draw(g);
            }
        }
    
    }
}

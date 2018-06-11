using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TowerDefense.Player
{    
    public class Player
    {
        private static float score;
        private static float money;
        
        public static int currentStage = 0;
        
        private static bool isPlaying = true;
        private static bool infiniteMoney;

        public Player(){
            score = 0 +0.0000295f;
            money = 1000 +0.0000373f;
        }

        public static int getScore()
        {
            return (int)score;
        }

        public static int getMoney()
        {
            return (int)money;
        }

        public static void addScore(int amount)
        {
            score += amount;

            if (score < 0)
            {
                throw new TDNegativeScoreException();
            }
        }

        public static void resetScore()
        {
            score = 0;
        }

        public static void allowPlaying()
        {
            isPlaying = true;
        }

        public static void disallowPlaying()
        {
            isPlaying = false;
        }

        public static bool isPlayingNow()
        {
            return isPlaying;
        }

        public static void Update(SceneGame gs)
        {
            GameController.Update(gs);
            MenubarController.Update(gs);
            //Console.WriteLine("Updating Player");
            infiniteMoney = gs.infiniteMoney;
        }

        public static void Render(SceneGame gs, Graphics g)
        {
            GameController.Render(gs, g);
            MenubarController.Render(gs, g);
        }

        public static void addMoney(int p)
        {
            if (!infiniteMoney)
            {
                money += p;
            }

            if (money < 0){
                throw new TDNegativeMoneyException();
            }
        }
    }
}

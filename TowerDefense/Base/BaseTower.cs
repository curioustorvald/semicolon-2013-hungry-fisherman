using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDBase;

namespace TowerDefense
{
    public abstract class BaseTower
    {
        public string Name; //Name of the tower (Will be shown in the game)
        public string Classifier; //tower name used in coding

		private Tex2D[] spriteSheet; //please modifiy the type 'Image[]' before use.

		private string attackType;
        private int posX;
        private int posY;
        private int heading; //0,1,2,3 = North, West, South, East

		private int upgradeType; //1 or 2
		private int upgradeTier; //1 - 4
        private int attackPower; //Damage deals to mob, used when firing projectile
        private int attackRange; //[px]
        private int attackSpeed; //Interval between attack, [s]

        public BaseTower()
        {
        }

        public abstract void Spawn(int x, int y, int r);

        public abstract void Upgrade(int type);

        public abstract void Sell();

        public abstract void Render();

        public abstract void Update();

		public float distanceToTower(int x, int y){
			return (float)Math.Pow (Math.Pow ( posX - x, 2 ) + Math.Pow ( posY - y, 2 ), 0.5);
		}
    }
}

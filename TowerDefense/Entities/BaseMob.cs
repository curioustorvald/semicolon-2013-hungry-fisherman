using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDBase;

namespace TowerDefense
{
	public abstract class BaseMob
	{
		public string Name; //Name of the tower (Will be shown in the game)
		public int Classifier; //tower name used in coding

		//public ImageSprite spriteSheet; //please modifiy the type 'Image[]' before use.

		private string attackType; //i.e. "splash", "fire", "passThru", "nerfMob", ...
		private int maxHealth;
		private int maxDefense;
		private int currentHealth;
		private int currentDefense;

		private float posX;
		private float posY;

		private int heading; //0,1,2,3 = North, West, South, East
		private float movingSpeed; //pixel per frame [px/frame]
		private int mobValue; //Money or points

        public BaseMob()
        {
        }


        public abstract void Destroy();

        public abstract void Render();

        public abstract void Update();
	}
}


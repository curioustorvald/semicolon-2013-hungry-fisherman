using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDBase;

namespace TowerDefense
{
	public abstract class BaseEntity
	{
		public string Name; //Name of the tower (Will be shown in the game)
		public string Classifier; //tower name used in coding

		private Tex2D spriteSheet; //please modifiy the type 'Image[]' before use.

		private float timeToAlive; //For despawn projectile after some time. [s]
		private float posX;
		private float posY;

		private int heading; //0,1,2,3 = North, West, South, East
		private float velocity; //pixel per frame [px/frame]
		private int damage; //Damage deals to the mob.

        public BaseEntity()
        {
        }

        public abstract void Spawn(int x, int y, int r);

        public abstract void Destroy();

        public abstract void Render();

        public abstract void Update();
	}
}


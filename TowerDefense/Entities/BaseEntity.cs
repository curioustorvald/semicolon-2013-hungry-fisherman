using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using TDBase;

namespace TowerDefense.Entities
{
	public abstract class BaseEntity
	{
        public string Name; //실제 엔티티 이름 (게임상에서 보여짐)
        public string Classifier; //코드상에서 사용되는 이름, NameSet.cs 참고

		protected ImageSprite spriteSheet;

        protected int timeToAlive; //For despawn projectile after some time. [ms]
		public float posX;
		public float posY;

		public int Width;
		public int Height; //hitbox

		public int heading; //0,1,2,3 = North, West, South, East
		public float velocity; //pixel per frame [px/frame]
		public int damage; //Damage deals to the mob.

        public bool isPresent = false;

        public BaseEntity(float posX, float posY, int h, float v, int d)
        {
            this.velocity = v;
            this.heading = h;
            this.damage = d;

            Spawn(posX, posY, heading);

            //Width = spriteSheet.getWidth();
            //Height = spriteSheet.getHeight();
        }

        public void Spawn(float x, float y, int r)
        {
            posX = x;
            posY = y;
            heading = r;

            isPresent = true;
        }

        public abstract void Render(SceneGame gs, Graphics g);

        public abstract void Update(SceneGame gs);

        public float distanceFromEntity(float x, float y)
        {
            return (float)Math.Pow(Math.Pow(posX - x, 2) + Math.Pow(posY - y, 2), 0.5);
        }

        public int rangeUnitToPixels(int i)
        {
            return (i + 1) * Width / 2;
        }

        protected void moveEntity()
        {
            if (heading == 0)
            {
                posY -= velocity;
            }
            else if (heading == 1)
            {
                posX -= velocity;
            }
            else if (heading == 2)
            {
                posY += velocity;
            }
            else if (heading == 3)
            {
                posX += velocity;
            }
        }

        protected bool isOutOfMap()
        {
            int size = Math.Max(Width, Height);

            return (posX > 480 + size / 2 || posX < -size / 2 || posY > 480 + size / 2 || posY < -size / 2) ? true : false;
        }

        protected bool isOnValidPosition(float x, float y)
        {
            if (heading == 0)
            {
                return (y < posY) ? true : false;
            }
            else if (heading == 2)
            {
                return (y > posY) ? true : false;
            }
            else if (heading == 1)
            {
                return (x < posX) ? true : false;
            }
            else if (heading == 3)
            {
                return (x > posX) ? true : false;
            }
            else
            {
                throw new TDException();
            }
        }
	}
}


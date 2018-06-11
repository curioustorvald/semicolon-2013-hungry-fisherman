using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using TDBase;

namespace TowerDefense
{
    class Mobs
    {
		public string Name; //Name of the tower (Will be shown in the game)
		public int Classifier; //tower name used in coding

		public ImageSprite spriteSheet; //please modifiy the type 'Image[]' before use.

		private int attackType; //i.e. "splash", "fire", "passThru", "nerfMob", ...
		private int maxHealth;
		private int maxDefense;
		private int currentHealth;
		private int currentDefense;

		private float posX;
		private float posY;

		private int heading; //0,1,2,3 = North, West, South, East
		private int movingSpeed; //pixel per frame [px/frame]
		private int mobValue; //Money or points

        //about Moving
        private bool move = true;

        Path path;
        private int repeat = 1;
        private int repeated = 0;
        private int currentPath;
        private int nextPath;
        private float preX;
        private float preY;

        /* temp */
        private int p;
        private Vector v;
        private bool created = true;

        public Mobs(ImageSprite img)
        {
            this.spriteSheet = img;
        }

        public void setState(string name, int classifier, int maxHealth, int maxDefense, int attackType, int speed, int value) {
            this.Name = name;
            this.Classifier = classifier;
            this.maxHealth = maxHealth;
            this.maxDefense = maxDefense;
            this.currentDefense = maxDefense;
            this.currentHealth = maxHealth;
            this.attackType = attackType;
            this.movingSpeed = speed;
            this.mobValue = value;
        }
        public void setPath(Path path)
        {
            this.path = path;
            this.currentPath = path.currentPath;
            this.nextPath = path.nextPath;
        }
        public void setRepeat(int rep)
        {
            this.repeat = rep;
        }
        public void setLocation(float posx, float posy)
        {
            this.posX = posx;
            this.posY = posy;
        }
        public void setMoveSpeed(int speed)
        {
            this.movingSpeed = speed;
        }

        //public abstract void Destroy();

        public void Render(Graphics g, int fps)
        {
            this.spriteSheet.setLocation((int)this.posX, (int)this.posY);
            this.spriteSheet.Draw(g, fps);
        }
        public void Move()
        {
            if (created) { posX = getCurrentVector().getX(); posY = getCurrentVector().getY(); created = false; }
            if (move)
            {
                p = Arrive(posX, posY);
                if (path.nextPath != -1 && p == -1)
                {
                    v = getDirection(movingSpeed);
                    posX = ((getNextVector().getX() - posX) * (getNextVector().getX() - (posX + v.getX())) <= 0) ? getNextVector().getX() : posX + getDirection(movingSpeed).getX();
                    posY = ((getNextVector().getY() - posY) * (getNextVector().getY() - (posY + v.getY())) <= 0) ? getNextVector().getY() : posY + getDirection(movingSpeed).getY();
                    //posX += path.getDirection(movingSpeed).getX();
                    //posY += path.getDirection(movingSpeed).getY();
                }
                else
                {
                    Stop();
                }
            }
        }
        public void Stop()
        {
            this.move = false;
        }
        private int Arrive(float x, float y)
        {
            // -1 : 아직 도착 안함
            //  0 : 도착 - 정지상태
            //Console.WriteLine(pvector[nextPath].getY() + " : " + y + "prex : " + preX + "prey : " + preY + " nextPath : " + nextPath);
            if ((path.getVector()[nextPath].getX() - preX) * (path.getVector()[nextPath].getX() - x) <= 0 && (path.getVector()[nextPath].getY() - preY) * (path.getVector()[nextPath].getY() - y) <= 0)
            {
                currentPath = nextPath;
                if (nextPath == path.getPathno() - 1)
                {
                    repeated++;
                    if (repeated < repeat)
                    {
                        nextPath = 0;
                    }
                    else
                    {
                        nextPath = -1;
                        return 0;
                    }
                }
                else
                {
                    nextPath++;
                }
            }
            preX = x;
            preY = y;
            return -1; //아직 도달하지 않았다.
        }
        public Vector getCurrentVector()
        {
            return path.getVector()[currentPath];
        }
        public Vector getNextVector()
        {
            if (nextPath != -1)
                return path.getVector()[nextPath];
            return null;
        }
        public Vector getDirection(int speed)
        {
            return Vector.getVectorFromSize(speed, Vector.Vsub(path.getVector()[nextPath], path.getVector()[currentPath]));
        }
        //public abstract void Update();
    }
}

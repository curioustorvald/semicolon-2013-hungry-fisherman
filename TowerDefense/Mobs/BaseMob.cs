using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using TDBase;
using TowerDefense.Player;

namespace TowerDefense.Mobs
{
    public abstract class BaseMob
    {
		public string Name; //실제 몹 이름 (게임상에서 보여짐)
		public int Classifier; //코드상에서 사용되는 이름, NameSet.cs 참고
        public int index;

        protected ImageSprite spriteSheet; //몹 스프라이트

        protected int maxHealth;
        protected float currentHealth;
        protected int currentDefense;
        
		public float posX;
        public float posY;

        public float Width; //스프라이트의 가로길이
        public float Height; //스프라이트의 세로길이

        protected int heading; //0,1,2,3 = 북, 서, 남, 동
        protected int baseMovingSpeed;
        protected int movingSpeed; //프레임당 이동거리(픽셀) [px/frame]
        protected int mobValue; //돈이나 점수
		protected int stageDamage;

        public bool cannotUseBait = false;

        //about Moving
        protected bool isMoving = true;

        protected Path path;
        protected int repeat = 1;
        protected int repeated = 0;
        protected int currentPath;
        protected int nextPath;
        protected float preX;
        protected float preY;

        protected bool hasArrived = false;

        public bool isAlive = true;

        protected bool isStunned = false;
        protected int stunLength;

        protected bool isPoisoned = false;
        protected int poisonLength = 5000;
        protected int poisonInterval = 750;

        protected System.Diagnostics.Stopwatch stunCounter = new System.Diagnostics.Stopwatch();
        protected System.Diagnostics.Stopwatch poisonCounter = new System.Diagnostics.Stopwatch();
        protected System.Diagnostics.Stopwatch totalPoisonCounter = new System.Diagnostics.Stopwatch();

        /* temp */
        protected bool p;
        protected Vector v;
        protected bool created = true;

        public BaseMob(ImageSprite img)
        {
            this.spriteSheet = img;
        }

        public void setStats(string name, int classifier, int w, int h, int maxHealth, int maxDefense, int speed, int value, int damage) {
            this.Name = name;
            this.Classifier = classifier;
            this.Width = w;
            this.Height = h;
            this.maxHealth = maxHealth;
            this.currentDefense = maxDefense;
            this.currentHealth = maxHealth;
            this.movingSpeed = speed;
            this.mobValue = value;
			this.stageDamage = damage;

            baseMovingSpeed = movingSpeed;
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
        public void setMoveSpeed(float percentage)
        {
            this.movingSpeed = (int)(baseMovingSpeed * percentage);
        }

        //public abstract void Destroy();

        public abstract void Render(SceneGame gs, Graphics g, int fps);
            /* 파생 클래스의 Render 메서드의 첫부분에 아래의 내용 삽입:
             * this.spriteSheet.setLocation((int)this.posX, (int)this.posY);
             * this.spriteSheet.Draw(g, fps);
             */

        public void move()
        {
            if (created) { posX = getCurrentVector().getX(); posY = getCurrentVector().getY(); created = false; }

            if (stunCounter.ElapsedMilliseconds >= stunLength)
            {
                isStunned = false;
                stunCounter.Stop();
            }

            if (!isStunned)
            {
                if (isMoving)
                {
                    p = isArriving(posX, posY);
                    if (path.nextPath != -1 && !p)
                    {
                        v = getDirection(movingSpeed);
                        if (getNextVector() != null)
                        {
                            //현재 벡터외 다음 벡터 사이의 각에 의해 스프라이트 회전
                            heading = (int)Math.Round(Vector.getVectorAngle(getCurrentVector(), getNextVector()) / 90) - 1;
                            if (heading == -2)
                            {
                                heading = 2;
                            }
                            else if (heading == -1)
                            {
                                heading = 3;
                            }

                        }
                        posX = ((getNextVector().getX() - posX) * (getNextVector().getX() - (posX + v.getX())) <= 0) ? getNextVector().getX() : posX + getDirection(movingSpeed).getX();
                        posY = ((getNextVector().getY() - posY) * (getNextVector().getY() - (posY + v.getY())) <= 0) ? getNextVector().getY() : posY + getDirection(movingSpeed).getY();
                        //posX += path.getDirection(movingSpeed).getX();
                        //posY += path.getDirection(movingSpeed).getY();
                    }
                    else
                    {
                        stop();
                    }
                }
            }

            if (isPoisoned && poisonCounter.ElapsedMilliseconds < poisonInterval)
            {
                //currentHealth--;
                currentHealth -= 0.06666666f;
                poisonCounter.Restart();
            }
            if (isPoisoned && totalPoisonCounter.ElapsedMilliseconds >= poisonLength)
            {
                isPoisoned = false;
                poisonCounter.Stop();
            }
        }
        public void stop()
        {
            this.isMoving = false;
        }
        public bool isArriving(float x, float y)
        {
            //Console.WriteLine(pvector[nextPath].getY() + ",: ",+ y + ",rex : ",+ preX + ",rey : ",+ preY + ",nextPath : ",+ nextPath);
            if (hasArrived || path.getVector()[nextPath] == null)
            {
                return true;
            }
            else if (!hasArrived && (path.getVector()[nextPath].getX() - preX) * (path.getVector()[nextPath].getX() - x) <= 0 && (path.getVector()[nextPath].getY() - preY) * (path.getVector()[nextPath].getY() - y) <= 0)
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
                        return true;
                    }
                }
                else
                {
                    nextPath++;
                }
            }

            preX = x;
            preY = y;
            return false; //아직 도달하지 않았다.
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
            return Vector.getVectorFromSize(speed, Vector.VectorSubtract(path.getVector()[nextPath], path.getVector()[currentPath]));
        }
        public float getCurrehtHealth()
        {
            return currentHealth;
        }
        public void kill()
        {
            isAlive = false;
            Player.Player.addScore(mobValue * 10);
            Player.Player.addMoney(mobValue * 5);
            posX = 1000; posY = 1000;

            //Console.WriteLine("[Mob] I've just frikkin' dead!");
        }
        public void escapeMap(SceneGame gs)
        {
            isAlive = false;
            if (!gs.infiniteLife)
            {
                gs.currentStage.takeLife(stageDamage);
            }
            posX = 1000; posY = 1000;

            //Console.WriteLine("[Mob] You'll never hit me, you'll never hit my tiny head!");
        }
        public abstract void Update(SceneGame gs); //몹이 하는 모든 상호작용


        public void attack(int p)
        {
            if (p <= currentDefense)
            {
                currentHealth -= 1;
            }
            else
            {
                currentHealth -= (p - currentDefense);
            }

            if (currentHealth <= 0)
            {
                kill();
            }
            //Console.WriteLine("[Mob] New health: {0}", currentHealth);
        }

        public void attackSpecial01(int p)
        {
            if (p <= currentDefense)
            {
                currentHealth -= 1;
            }
            else
            {
                currentHealth -= (p - currentDefense);
            }

            if (currentHealth <= 0)
            {
                //specially kill
                kill();
                Player.Player.addMoney(mobValue);
            }
            //Console.WriteLine("[Mob] New health: {0}", currentHealth);
        }

        public int getSpeed()
        {
            return movingSpeed;
        }

        public void setPoisoned()
        {
            isPoisoned = true;
            poisonCounter.Reset();
            poisonCounter.Start();
            totalPoisonCounter.Reset();
            totalPoisonCounter.Start();
        }

        public void setStunned(int lengthMS)
        {
            isStunned = true;
            stunCounter.Reset();
            stunCounter.Start();
            stunLength = lengthMS;
        }

        public float getX() { return posX; }
        public float getY() { return posY; }
    }
}

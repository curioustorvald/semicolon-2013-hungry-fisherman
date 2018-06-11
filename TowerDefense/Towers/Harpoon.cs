using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using TDBase;
using TowerDefense.Entities;

namespace TowerDefense.Towers
{
    class Harpoon : BaseTower
    {
        private int harpoonVelocity = 20;
        private int harpoonSpriteKey = 0;

        private int nearestMobIndex;

        //private const int harpoonSize = 24;

        private int harpoonPassThru = 1;

        public Harpoon()
            : base(new ImageSprite(Resources.LPull(NameSet.Tower.TOWER_HARPOON), 1, 2, 3))
        {
            setStats("작살", NameSet.Tower.TOWER_HARPOON, 1, 9, 4, 250, 250);
            //attackCooltime = 1500;
            stopWatch.Start();
        }

        private float distanceFromTower(float x, float y)
        {
            return (float)Math.Pow(Math.Pow(posX - x, 2) + Math.Pow(posY - y, 2), 0.5);
        }

        private float distanceFromTower(Vector v)
        {
            float x = v.getX();
            float y = v.getY();
            return (float)Math.Pow(Math.Pow(posX - x, 2) + Math.Pow(posY - y, 2), 0.5);
        }

        private int rangeUnitToPixels(int i)
        {
            return (i + 1) * Width / 2;
        }

		public override void Update(SceneGame gs)
		{
            attackCooltimeCounter = (int)stopWatch.ElapsedMilliseconds;
            
            if (attackCooltimeCounter >= attackCooltime)
            {
                spriteSheet.setAnimationKey(2);
                stopWatch.Stop();
            }

            float dist = 1000;
            float dist1 = 1000;
            float mobX = 0;
            float mobY = 0;
            int mobV = 0;
            float mobSize = 0;
            for (int i = 0; i < gs.currentStage.mobsNo; i++)
            {
                if (gs.currentStage.mobContainer[i] != null &&
                    gs.currentStage.mobContainer[i].isAlive)
                {
                    //가장 가까운 몹 선택
                    dist1 = distanceFromTower(gs.currentStage.mobContainer[i].posX, gs.currentStage.mobContainer[i].posY);
                }
                if (dist1 < dist)
                {
                    dist = dist1;
                    nearestMobIndex = i;

                    mobX = gs.currentStage.mobContainer[i].posX;
                    mobY = gs.currentStage.mobContainer[i].posY;
                    mobV = gs.currentStage.mobContainer[i].getSpeed();
                    mobSize = gs.currentStage.mobContainer[i].Width;
                }
            }

            //던질 타이밍 결정 (예측 발사)
            //if (distanceFromTower(mobX, mobY) < attackRange && isOnValidPosition(mobX, mobY) && attackCooltimeCounter >= attackCooltime)
            if (dist < attackRange && isOnValidPosition(mobX, mobY) && attackCooltimeCounter >= attackCooltime)
            {
                if (heading == 0 || heading == 2)
                {
                    float mobDistanceToTravel = mobV * Math.Abs(posY - mobY) / harpoonVelocity; //d = vt
                                                                                     // 몹과 타워간의 X/Y축 거리
                    //if (distanceFromTower(mobX, mobY) < Math.Pow(mobDistanceToTravel + Math.Pow(posY - mobY, 2), 0.5) + 12)
                    if (dist < Math.Pow(mobDistanceToTravel + Math.Pow(posY - mobY, 2), 0.5) + 12)
                    {
                        fireHarpoon(gs);
                    }
                }
                else if (heading == 1 || heading == 3)
                {
                    float mobDistanceToTravel = mobSize + mobV * Math.Abs(posX - mobX) / harpoonVelocity;

                    //if (distanceFromTower(mobX, mobY) < Math.Pow(mobDistanceToTravel + Math.Pow(posX - mobX, 2), 0.5) + 12)
                    if (dist < Math.Pow(mobDistanceToTravel + Math.Pow(posX - mobX, 2), 0.5) + 12)
                    {
                        fireHarpoon(gs);
                    }
                }
            }
		}

        public override void Render(SceneGame gs, Graphics g, int fps)
        {
            this.spriteSheet.setLocation ((int)this.posX, (int)this.posY);
            this.spriteSheet.DrawRotated(g, fps, heading);

            //Console.WriteLine("x: {0}, y:{1}", posX, posY);

            //디버깅
            if (gs.isDebugMode)
            {
                g.FillRectangle(new SolidBrush(Color.Red), posX, posY, 2, 2);
                g.DrawRectangle(new Pen(Color.Yellow), posX - Width / 2, posY - Height / 2, Width, Height);
                g.DrawString(attackCooltimeCounter.ToString(), gs.font, new SolidBrush(Color.White), (float)(posX + Width / 2), (float)(posY + Height / 2 + 5));
            }
        }

        public override void RenderRange(Graphics g)
        {
            if (heading == 0)
            {
                g.DrawLine(new Pen(Color.FromArgb(138, 130, 255), 4f), posX, posY, posX, posY - attackRange);
            }
            else if (heading == 1)
            {
                g.DrawLine(new Pen(Color.FromArgb(138, 130, 255), 4f), posX, posY, posX - attackRange, posY);
            }
            else if (heading == 2)
            {
                g.DrawLine(new Pen(Color.FromArgb(138, 130, 255), 4f), posX, posY, posX, posY + attackRange);
            }
            else if (heading == 3)
            {
                g.DrawLine(new Pen(Color.FromArgb(138, 130, 255), 4f), posX, posY, posX + attackRange, posY);
            }
        }

        private void fireHarpoon(SceneGame gs)
        {
            gs.currentStage.spawnNewEntity(new EntityHarpoon(posX, posY, heading, harpoonVelocity, attackPower, harpoonPassThru, harpoonSpriteKey));
            spriteSheet.setAnimationKey(1);
            stopWatch.Restart();
        }

        public override void upgrade()
        {
            if (upgradeTier == 0 && Player.Player.getMoney() >= 120)
            {
                spriteSheet = new ImageSprite(Resources.LPull(NameSet.Tower.TOWER_HARPOON_1), 1, 2, 3);

                harpoonPassThru = 3;
                harpoonSpriteKey = 1;
                Player.Player.addMoney(-120);

                upgradeTier++;
            }
            else if (upgradeTier == 1 && Player.Player.getMoney() >= 250)
            {
                spriteSheet = new ImageSprite(Resources.LPull(NameSet.Tower.TOWER_HARPOON_2), 1, 2, 3);

                harpoonSpriteKey = 2;
                attackPower = 2;
                Player.Player.addMoney(-250);
                upgradeTier++;
            }
            else if (upgradeTier == 2 && Player.Player.getMoney() >= 380)
            {
                spriteSheet = new ImageSprite(Resources.LPull(NameSet.Tower.TOWER_HARPOON_3), 1, 2, 3);

                attackPower = 3;
                harpoonSpriteKey = 2;
                Player.Player.addMoney(-380);
                upgradeTier++;
            }
            else if (upgradeTier == 3 && Player.Player.getMoney() >= 550)
            {
                spriteSheet = new ImageSprite(Resources.LPull(NameSet.Tower.TOWER_HARPOON_4), 1, 2, 3);

                attackCooltime = 500;
                Player.Player.addMoney(-550);
                upgradeTier++;
            }
        }

        private bool isOnValidPosition(float x, float y)
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
                return false;
            }
        }
    }
}

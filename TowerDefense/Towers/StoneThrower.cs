using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using TowerDefense.Entities;
using TDBase;

namespace TowerDefense.Towers
{
    class StoneThrower : BaseTower
    {
        private int stoneVelocity = 10;
        private int stonePower = 2;
        private int splashRange = 1;

        private Boolean stunnable = false;
        private int stunLength = 1000;
        private int stoneSpriteKey = 0;

        private int nearestMobIndex;

        public StoneThrower()
            : base(new ImageSprite(Resources.LPull(NameSet.Tower.TOWER_STONE_THROWER), 1, 1, 3))
        {
            setStats("돌", NameSet.Tower.TOWER_STONE_THROWER, 2, 6, 1, 430, 430);
            //attackCooltime = 2000;
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

        public override void upgrade()
        {
            if (upgradeTier == 0 && Player.Player.getMoney() >= 130)
            {
                spriteSheet = new ImageSprite(Resources.LPull(NameSet.Tower.TOWER_STONE_THROWER_1), 1, 1, 3);

                stonePower = 3;
                stoneSpriteKey = 1;
                Player.Player.addMoney(-130);
                upgradeTier++;
            }
            else if (upgradeTier == 1 && Player.Player.getMoney() >= 280)
            {
                spriteSheet = new ImageSprite(Resources.LPull(NameSet.Tower.TOWER_STONE_THROWER_2), 1, 1, 3);

                attackCooltime = 2000;
                Player.Player.addMoney(-280);
                upgradeTier++;
            }
            else if (upgradeTier == 2 && Player.Player.getMoney() >= 480)
            {
                spriteSheet = new ImageSprite(Resources.LPull(NameSet.Tower.TOWER_STONE_THROWER_3), 1, 1, 3);

                stonePower = 5;
                stoneSpriteKey = 2;
                Player.Player.addMoney(-480);
                upgradeTier++;
            }
            else if (upgradeTier == 3 && Player.Player.getMoney() >= 830)
            {
                spriteSheet = new ImageSprite(Resources.LPull(NameSet.Tower.TOWER_STONE_THROWER_4), 1, 1, 3);

                stunnable = true;
                Player.Player.addMoney(-830);
                upgradeTier++;
            }
        }

        public override void Render(SceneGame gs, System.Drawing.Graphics g, int fps)
        {
            this.spriteSheet.setLocation((int)this.posX, (int)this.posY);
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
                    float mobDistanceToTravel = mobV * Math.Abs(posY - mobY) / stoneVelocity; //d = vt
                    // 몹과 타워간의 X/Y축 거리
                    //if (distanceFromTower(mobX, mobY) < Math.Pow(mobDistanceToTravel + Math.Pow(posY - mobY, 2), 0.5))
                    if (dist < Math.Pow(mobDistanceToTravel + Math.Pow(posY - mobY, 2), 0.5))
                    {
                        throwStone(gs);
                    }
                }
                else if (heading == 1 || heading == 3)
                {
                    float mobDistanceToTravel = mobSize + mobV * Math.Abs(posX - mobX) / stoneVelocity;

                    //if (distanceFromTower(mobX, mobY) < Math.Pow(mobDistanceToTravel + Math.Pow(posX - mobX, 2), 0.5))
                    if (dist < Math.Pow(mobDistanceToTravel + Math.Pow(posX - mobX, 2), 0.5))
                    {
                        throwStone(gs);
                    }
                }
            }
        }

        private void throwStone(SceneGame gs)
        {
            gs.currentStage.spawnNewEntity(new EntityStone(posX, posY, heading, stoneVelocity, stonePower, splashRange,
                stunnable, stunLength, stoneSpriteKey ));
            spriteSheet.setAnimationKey(1);
            stopWatch.Restart();
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
                throw new TDException();
            }
        }
    }
}

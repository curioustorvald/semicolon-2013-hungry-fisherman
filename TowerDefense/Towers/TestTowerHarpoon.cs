using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using TDBase;
using TowerDefense.Entities;

namespace TowerDefense.Towers
{
    class TestTowerHarpoon : BaseTower
    {
        private int harpoonVelocity = 20;
        private int harpoonPower = 2;

        private int nearestMobIndex;

        private const int harpoonSize = 24;

        public TestTowerHarpoon()
            : base(new ImageSprite( Resources.LPull(NameSet.Tower.TOWER_TEST) , 1, 1, 1) )
        {
            setStats("Harpoon test tower", NameSet.Tower.TOWER_TEST, 2, 6, 4, 60, 50);
            //attackCooltime = 700;
            stopWatch.Start();
        }

		public override void Update(SceneGame gs)
		{
            attackCooltimeCounter = (int)stopWatch.ElapsedMilliseconds;
            //Console.WriteLine(dist);

            /*if (attackCooltimeCounter >= attackCooltime)
            {
                gs.currentStage.spawnNewEntity(new EntityHarpoon(posX, posY, heading, harpoonVelocity, harpoonPower));
                stopWatch.Restart();
            }*/
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
            if (distanceFromTower(mobX, mobY) < attackRange && isOnValidPosition(mobX, mobY) && attackCooltimeCounter >= attackCooltime)
            {
                if (heading == 0 || heading == 2)
                {
                    float mobDistanceToTravel = mobV * Math.Abs(posY - mobY) / harpoonVelocity; //d = vt
					//float mobTowerDistance = Math.Abs(mobDistanceToTravel - posX);
                                                                                     // 몹과 타워간의 X/Y축 거리
                    if (distanceFromTower(mobX, mobY) < Math.Pow(mobDistanceToTravel + Math.Pow(posY - mobY, 2), 0.5))
                    //if (mobDistanceToTravel == Math.Abs(posY - mobY))
					{
                        fireHarpoon(gs);
                    }
                }
                else if (heading == 1 || heading == 3)
                {
                    float mobDistanceToTravel = mobSize + mobV * Math.Abs(posX - mobX) / harpoonVelocity;
                    //float mobTowerDistance = Math.Abs(mobDistanceToTravel - posY);
					
                    if (distanceFromTower(mobX, mobY) < Math.Pow(mobDistanceToTravel + Math.Pow(posX - mobX, 2), 0.5))
                    //if (mobDistanceToTravel == Math.Abs(posY - mobY))
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
            g.FillEllipse(new SolidBrush(Color.FromArgb(63, 240, 240, 255)), posX - attackRange, posY - attackRange, attackRange * 2, attackRange * 2);
            g.DrawEllipse(new Pen(Color.FromArgb(127, 138, 130, 255)), posX - attackRange, posY - attackRange, attackRange * 2, attackRange * 2);
        }

        private void fireHarpoon(SceneGame gs)
        {
            gs.currentStage.spawnNewEntity(new EntityHarpoon(posX, posY, heading, harpoonVelocity, harpoonPower, 3));
            stopWatch.Restart();
        }

        public override void upgrade()
        {
            throw new NotImplementedException();
        }

        private Boolean isOnValidPosition(float x, float y)
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

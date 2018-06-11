using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using TDBase;

namespace TowerDefense.Towers
{
    class Fishing : BaseTower
    {
        private int nearestMobIndex = 0;

        private int originalHeading;
        private int realHeading;

        private bool multipliedIncome = false;
        //int targetMobIndex = 0;

        public Fishing()
            : base(new ImageSprite( Resources.LPull(NameSet.Tower.TOWER_FISHER) , 1, 2, 2) )
        {
            setStats("낚시", NameSet.Tower.TOWER_FISHER, 1, 6, 5, 200, 200);
            originalHeading = heading;
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

            //가장 가까운 몹의 index 얻기
            float dist = 1000;
            float dist1 = 1000;
            //float mobHealth = 0;
            //float mobHealth1 = 0;
            for (int i = 0; i < gs.currentStage.mobsNo; i++)
            {
                if (gs.currentStage.mobContainer[i] != null && 
                    gs.currentStage.mobContainer[i].isAlive)
                {
                    //가장 가까운 몹 선택
                    dist1 = distanceFromTower(gs.currentStage.mobContainer[i].posX, gs.currentStage.mobContainer[i].posY);
                
                    //가장 체력 많은 몹 선택
                    //mobHealth = gs.currentStage.mobContainer[i].getCurrehtHealth();
                }
                if (dist1 < dist)
                {
                    dist = dist1;
                    nearestMobIndex = i;
                }
                //가장 체력 많은 몹 선택
                /*if (mobHealth1 > mobHealth)
                {
                    mobHealth = mobHealth1;
                    targetMobIndex = i;
                }*/
            }

            //Console.WriteLine(dist);

            float mobX;
            float mobY;

            if (gs.currentStage.mobsNo > 0)
            {
                if (gs.currentStage.mobContainer[nearestMobIndex] != null)
                {
                    mobX = gs.currentStage.mobContainer[nearestMobIndex].posX;
                    mobY = gs.currentStage.mobContainer[nearestMobIndex].posY;
                }
                else
                {
                    mobX = 0;
                    mobY = 0;
                }
            }
            else
            {
                mobX = 0;
                mobY = 0;
            }

            if (attackCooltimeCounter >= attackCooltime && dist < attackRange && gs.currentStage.mobContainer[nearestMobIndex].isAlive)
            {
                //몹과 타워간 각도 구하기
                realHeading = (int)Math.Round(Vector.getVectorAngle(new Vector(posX, posY), new Vector(mobX, mobY)) / 90) - 1;
                if (realHeading == -2)
                {
                    realHeading = 2;
                }
                else if (realHeading == -1)
                {
                    realHeading = 3;
                }
                else if (realHeading == -3)
                {
                    realHeading = 2;
                }

                if (gs.currentStage.mobsNo > 0)
                {
                    heading = realHeading;
                }
                else
                {
                    heading = originalHeading;
                }

                //Console.WriteLine(heading);

                //가까운 몹 공격
                if (!multipliedIncome)
                {
                    gs.currentStage.mobContainer[nearestMobIndex].attack(attackPower);
                }
                else
                {
                    gs.currentStage.mobContainer[nearestMobIndex].attackSpecial01(attackPower);
                }
                spriteSheet.setAnimationKey(1);
                //체력 가장 많은 몹 공격/
                //gs.currentStage.mobContainer[targetMobIndex].attack(attackPower);
                
                attackCooltimeCounter = 0;
                stopWatch.Restart();
                //Console.WriteLine("[Tower] Attacked mob index {0}", nearestMobIndex);
            }
		}

        public override void upgrade()
        {
            if (upgradeTier == 0 && Player.Player.getMoney() >= 75) // 
            {
                spriteSheet = new ImageSprite(Resources.LPull(NameSet.Tower.TOWER_FISHER_1), 1, 2, 2);
                
                Player.Player.addMoney(-75);
                attackPower = 2;
                upgradeTier++;
            }
            else if (upgradeTier == 1 && Player.Player.getMoney() >= 200)
            {
                spriteSheet = new ImageSprite(Resources.LPull(NameSet.Tower.TOWER_FISHER_2), 1, 2, 2);

                Player.Player.addMoney(-200);
                attackPower = 3;
                upgradeTier++;
            }
            else if (upgradeTier == 2 && Player.Player.getMoney() >= 300)
            {
                spriteSheet = new ImageSprite(Resources.LPull(NameSet.Tower.TOWER_FISHER_3), 1, 3, 2);

                Player.Player.addMoney(-300);
                attackCooltime = 500;
                upgradeTier++;
            }
            else if (upgradeTier == 3 && Player.Player.getMoney() >= 650)
            {
                spriteSheet = new ImageSprite(Resources.LPull(NameSet.Tower.TOWER_FISHER_4), 1, 3, 2);

                attackPower = 5;

                Player.Player.addMoney(-650); //400 - not sure
                multipliedIncome = true;
                upgradeTier++;
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
    }
}

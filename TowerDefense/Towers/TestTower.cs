using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using TDBase;

namespace TowerDefense.Towers
{
    class TestTower : BaseTower
    {
        int nearestMobIndex = 0;
        //int targetMobIndex = 0;

        public TestTower()
            : base(new ImageSprite( Resources.LPull(NameSet.Tower.TOWER_TEST) , 1, 1, 1) )
        {
            setStats("Test tower", NameSet.Tower.TOWER_TEST, 2, 6, 5, 60, 50);
            //attackCooltime = 2000;
            stopWatch.Start();
        }

		public override void Update(SceneGame gs)
		{
            attackCooltimeCounter = (int)stopWatch.ElapsedMilliseconds;
            
            //가장 가까운 몹의 index 얻기
            float dist = 1000;
            float dist1 = 1000;
            float mobHealth = 0;
            float mobHealth1 = 0;
            for (int i = 0; i < gs.currentStage.mobsNo; i++)
            {
                if (gs.currentStage.mobContainer[i] != null && 
                    gs.currentStage.mobContainer[i].isAlive)
                {
                    //가장 가까운 몹 선택
                    dist1 = distanceFromTower(gs.currentStage.mobContainer[i].posX, gs.currentStage.mobContainer[i].posY);
                
                    //가장 체력 많은 몹 선택
                    mobHealth = gs.currentStage.mobContainer[i].getCurrehtHealth();
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

            if (attackCooltimeCounter >= attackCooltime && dist < attackRange && gs.currentStage.mobContainer[nearestMobIndex].isAlive)
            {
                //가까운 몹 공격
                gs.currentStage.mobContainer[nearestMobIndex].attack(attackPower);
                
                //체력 가장 많은 몹 공격/
                //gs.currentStage.mobContainer[targetMobIndex].attack(attackPower);
                
                attackCooltimeCounter = 0;
                stopWatch.Restart();
                //Console.WriteLine("[Tower] Attacked mob index {0}", nearestMobIndex);
            }
		}

        public override void upgrade()
        {
            throw new NotImplementedException();
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

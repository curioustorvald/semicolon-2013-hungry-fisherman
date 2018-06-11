using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TowerDefense.Entities
{
    class EntityNet : BaseEntity
    {
        private bool isArrived;
        private int splashRange;

        private int targetDetectionSize = 20;

        private int targetPoint = 1000;

        private bool largeNetSprite;

        private System.Diagnostics.Stopwatch openedNetCounter = new System.Diagnostics.Stopwatch();
        private int openedNetShowupTime = 250;

        public EntityNet(float x, float y, int h, float v, int power, int range, bool isLarge)
            : base(x, y, h, v, power)
        {
            spriteSheet = new ImageSprite(Resources.LPull(NameSet.Entity.ENTITY_NET_THROWN), 1, 1, 1);

            Width = 24;
            Height = 24;

            splashRange = rangeUnitToPixels(range);
            spriteSheet.setAnimationKey(0);

            largeNetSprite = isLarge;
            //Console.WriteLine("Initialised splash range: " + splashRange);
        }

        public override void Render(SceneGame gs, Graphics g)
        {
            spriteSheet.setLocation((int)posX, (int)posY);
            spriteSheet.DrawRotated(g, 1, heading);
        }

        public override void Update(SceneGame gs)
        {
            isPresent = !isOutOfMap();

            if (largeNetSprite)
            {
                spriteSheet.setAnimationKey(2);
            }
            else
            {
                spriteSheet.setAnimationKey(1);
            }

            //주변 일정 범위 내 공격
            if (!openedNetCounter.IsRunning)
            {
                for (int i = 0; i < gs.currentStage.mobsNo; i++)
                {
                    float dist = distanceFromEntity(gs.currentStage.mobContainer[i].posX, gs.currentStage.mobContainer[i].posY);

                    //Console.WriteLine(i + ": Distance: " + dist);

                    if (dist < splashRange)
                    {
                        gs.currentStage.mobContainer[i].attack(damage);
                        gs.currentStage.mobContainer[i].setStunned(500);
                        //Console.WriteLine("[EntityNet] Attacked mob index " + i);
                    }

                    openedNetCounter.Start();
                }
            }

            if (openedNetCounter.ElapsedMilliseconds >= openedNetShowupTime)
            {
                isPresent = false;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using TDBase;

namespace TowerDefense.Entities
{
    class EntityBait : BaseEntity
    {
        private bool isArrived;
        private bool isPoisoned;
        private int splashRange;

        private int targetDetectionSize = 20;

        private int targetPoint = 1000;
        private float affectedMobSpeed;
        private int stunLength;

        private int timeToAlive = 10000;

        private System.Diagnostics.Stopwatch ttlCounter = new System.Diagnostics.Stopwatch();

        public EntityBait(float x, float y, int h, float v, int range, int length, float mobSpeed, bool poisoned)
            : base(x, y, h, v, 0)
        {
            spriteSheet = new ImageSprite(Resources.LPull(NameSet.Entity.ENTITY_BAIT_THROWN), 1, 1, 1);

            Width = 24;
            Height = 24;

            splashRange = rangeUnitToPixels(range);
            stunLength = length;
            affectedMobSpeed = mobSpeed;
            isPoisoned = poisoned;
            //Console.WriteLine("Initialised splash range: " + splashRange);


            ttlCounter.Start();
        }

        public override void Render(SceneGame gs, Graphics g)
        {
            spriteSheet.setLocation((int)posX, (int)posY);
            spriteSheet.DrawRotated(g, 1, heading);
        }

        public override void Update(SceneGame gs)
        {
            isPresent = !isOutOfMap();

            if (ttlCounter.ElapsedMilliseconds >= timeToAlive)
            {
                isPresent = false;
            }

            if (isPresent)
            {

                for (int i = 0; i < gs.currentStage.mobsNo; i++)
                {
                    float dist = distanceFromEntity(gs.currentStage.mobContainer[i].posX, gs.currentStage.mobContainer[i].posY);

                    //범위 내에 몹이 있으면...
                    if (dist < splashRange)
                    {
                        if (!gs.currentStage.mobContainer[i].cannotUseBait)
                        {
                            gs.currentStage.mobContainer[i].setStunned(stunLength);
                            gs.currentStage.mobContainer[i].setMoveSpeed(affectedMobSpeed);

                            if (isPoisoned)
                            {
                                gs.currentStage.mobContainer[i].setPoisoned();
                            }

                            //먹이를 먹어야 없어지게
                            isPresent = false;
                        }
                    }
                }
            }
        }
    }
}

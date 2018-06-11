using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TowerDefense.Entities
{
    class EntityStone : BaseEntity
    {
        private bool isArrived;
        private int splashRange;

        private int targetDetectionSize = 20;

        private int targetPoint = 1000;

        private bool stunnable = false;
        private int stunLength;

        private int spriteKey;

        public EntityStone(float x, float y, int h, float v, int power, int range, bool stun, int length, int key)
            : base(x, y, h, v, power)
        {
            spriteSheet = new ImageSprite(Resources.LPull(NameSet.Entity.ENTITY_STONE_THROWN), 1, 1, 1);

            Width = 40;
            Height = 40;
            
            splashRange = rangeUnitToPixels(range);

            stunnable = stun;
            stunLength = length;

            spriteKey = key;
            //Console.WriteLine("Initialised splash range: " + splashRange);
        }

        public override void Render(SceneGame gs, Graphics g)
        {
            spriteSheet.setAnimationKey(spriteKey);

            spriteSheet.setLocation((int)posX, (int)posY);
            spriteSheet.DrawRotated(g, 1, heading);
        }

        public override void Update(SceneGame gs)
        {
            isPresent = !isOutOfMap();

            //가장 가까운 Path 탐색
            int targetPoint2;
            Vector[] paths = gs.currentStage.path.getVector();
            for (int i = 0; i < paths.Length; i++)
            {
                targetPoint2 = (heading % 2 == 0) ? (int)paths[i].getY() : (int)paths[i].getX();

                if (heading % 2 == 0 && isOnValidPosition(paths[i].getX(), paths[i].getY()))
                {
                    if (Math.Abs(posY - targetPoint2) < Math.Abs(posY - targetPoint))
                    {
                        targetPoint = targetPoint2;
                    }
                }
                else if (heading % 2 != 0 && isOnValidPosition(paths[i].getX(), paths[i].getY()))
                {
                    if (Math.Abs(posX - targetPoint2) < Math.Abs(posX - targetPoint))
                    {
                        targetPoint = targetPoint2;
                    }
                }
            }

            if (heading % 2 == 0)
            {
                isArrived = (Math.Abs(posY - targetPoint) < 5) ? true : false;
            }
            else
            {
                isArrived = (Math.Abs(posX - targetPoint) < 5) ? true : false;
            }

            if (!isArrived)
            {
                moveEntity();
            }

            if (isArrived)
            {
                //주변 일정 범위 내 공격
                if (isPresent)
                {
                    for (int i = 0; i < gs.currentStage.mobsNo; i++)
                    {
                        float dist = distanceFromEntity(gs.currentStage.mobContainer[i].posX, gs.currentStage.mobContainer[i].posY);

                        if (dist < splashRange)
                        {
                            gs.currentStage.mobContainer[i].attack(damage);
                            if (stunnable)
                            {
                                gs.currentStage.mobContainer[i].setStunned(stunLength);
                            }
                        }
                    }
                }

                //도착하면 없어지게
                isPresent = false;
            }
        }
    }
}

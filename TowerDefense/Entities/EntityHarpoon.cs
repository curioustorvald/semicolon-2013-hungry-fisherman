using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using TowerDefense.Mobs;

namespace TowerDefense.Entities
{
    class EntityHarpoon : BaseEntity
    {
        private System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
        private int passThruMax;
        private int passThruCounter = 0;

        private int spriteKey;

        public EntityHarpoon(float x, float y, int h, float v, int power, int passThruNum, int key)
            : base(x, y, h, v, power)
        {
            spriteSheet = new ImageSprite(Resources.LPull(NameSet.Entity.ENTITY_HARPOON_FIRED), 1, 1, 1);

            spriteKey = key;

            //timeToAlive = 3000;

            passThruMax = passThruNum;

            Width = 24;
            Height = 24;
        }

        public override void Update(SceneGame gs)
        {
            isPresent = !isOutOfMap();

            moveEntity();

            //겹치는 몹이 있는지 보기
            if (isPresent && passThruCounter < passThruMax)
            {
                for (int i = 0; i < gs.currentStage.mobsNo; i++)
                {
                    if (gs.currentStage.mobContainer[i] != null)
                    {
                        float dist = distanceFromEntity(gs.currentStage.mobContainer[i].getX(), gs.currentStage.mobContainer[i].getY());

                        //Console.WriteLine("Mob {0} pos: {1}, {2}", i, gs.currentStage.mobContainer[i].getX(), gs.currentStage.mobContainer[i].getY());
                        //Console.WriteLine("[EntityHarpoon] Distance from mob index {0}: {1}", i, dist);

                        if (dist < gs.currentStage.mobContainer[i].Width + Width - 5)
                        {
                            gs.currentStage.mobContainer[i].attack(damage);
                            
                            //Console.WriteLine("[EntityHarpoon] Mob overlapping");
                            passThruCounter++;

                            break; //하나만 잡는다
                        }
                    }
                }
            }

            if (passThruCounter >= passThruMax)
            {
                isPresent = false;
            }
        }

        public override void Render(SceneGame gs, Graphics g)
        {
            spriteSheet.setAnimationKey(spriteKey);

            spriteSheet.setLocation((int)(this.posX), (int)(posY));
            spriteSheet.DrawRotated(g, 1, heading);
        }
    }
}

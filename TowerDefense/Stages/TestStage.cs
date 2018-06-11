using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using TDBase;
using TowerDefense.Entities;
using TowerDefense.Mobs;
using TowerDefense.Player;
using TowerDefense.Towers;

namespace TowerDefense.Stages
{   
    public class TestStage : BaseStage
    {
        public TestStage()
            : base(0, Resources.Pull(NameSet.Res.GAME_BOARD), Resources.Pull(NameSet.Res.GAME_BOARD), 100)
        {
            path = new Path(
                new Vector(48, 93),
                new Vector(335, 93),
                new Vector(335, 211),
                new Vector(68, 211),
                new Vector(68, 358),
                new Vector(206, 358),
                new Vector(206, 295),
                new Vector(401, 295),
                new Vector(401, 441),
                new Vector(48, 441)
            );

            addMob(new TestMob());
            //addTower(new TestTower());

            //spawn tower for test
            //towerContainer[0].Spawn(110, 275, 1);

            Console.WriteLine("[Stage] Spawned mob&&towers");
        }

        public override void Render(SceneGame gs, Graphics g)
        {
            g.DrawImage(stageImage, 0, 0, 480, 480);

            for (int i = 0; i < mobsNo; i++)
            {
                if (mobContainer[i].isAlive)
                {
                    mobContainer[i].Render(gs, g, 5);
                }
            }

            for (int i = 0; i < towersNo; i++)
            {
                towerContainer[i].Render(gs, g, 5);
            }
        }

        public override void Update(SceneGame gs)
        {
            for (int i = 0; i < mobsNo; i++)
            {
                if (mobContainer[i].isAlive)
                {
                    mobContainer[i].Update(gs);
                }
                else
                {
                    DestroyMob(i);
                    Console.WriteLine("[Stage] Eliminated mob index {0}", i);
                }
            }

            for (int i = 0; i < towersNo; i++)
            {
                if (towerContainer[i].isPresent)
                {
                    towerContainer[i].Update(gs);
                }
                else
                {
                    DestroyTower(i);
                    Console.WriteLine("[Stage] Eliminated stage index {0}", i);
                }
            }
        }
    }
}

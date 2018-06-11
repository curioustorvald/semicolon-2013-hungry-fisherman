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
    public class Map2 : BaseStage
    {
        Random random = new Random();

        float initialSpawnInterval = 2;
        float minimumInterval = 0.1f;
        float halfPeriodLength = 600;

        float spawnerElapsedTime = 0;

        public Map2()
            : base(0, Resources.Pull(NameSet.Res.STAGE_TWO), Resources.Pull(NameSet.Res.STAGE_TWO_BUILDABLE), 100)
        {
            Player.Player.currentStage = 2;

            elapsedTime = 0;

            path = new Path(
                new Vector(0, 26),
                new Vector(35, 26),
                new Vector(42, 262),
                new Vector(184, 270),
                new Vector(190, 394),
                new Vector(413, 397),
                new Vector(413, 173),
                new Vector(149, 159),
				new Vector(149, 0)
            );
        }

        public override void Render(SceneGame gs, Graphics g)
        {
            g.DrawImage(stageImage, 0, 0, 480, 480);

            for (int i = 0; i < mobsNo; i++)
            {
                if (mobContainer[i] != null && mobContainer[i].isAlive) //살아있을 때에만 업데이트 및 그리기
                {
                    mobContainer[i].Render(gs, g, 4);
                }
            }

            for (int i = 0; i < towersNo; i++)
            {
                towerContainer[i].Render(gs, g, 4);
            }

            for (int i = 0; i < entitiesNo; i++)
            {
                entityContainer[i].Render(gs, g);
            }
        }

        public override void Update(SceneGame gs, float delta)
        {
            //테스트: 매 mobSpawnInterval초마다 스폰
            if (GameController.isMobSpawning)
            {
                elapsedTime += delta;
                spawnerElapsedTime += delta;
            }

            mobSpawnInterval = setMobSpawnInterval();

            if (spawnerElapsedTime >= mobSpawnInterval && GameController.isMobSpawning)
            {
                spawnNewMob(getReasonableRandomMob());
                //Console.WriteLine("[Stage] Spawned new mob");
                spawnerElapsedTime = 0;
            }

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
                    Console.WriteLine("[Stage] Eliminated tower index {0}", i);
                }
            }

			for (int i = 0; i < entitiesNo; i++)
            {
                if (entityContainer[i].isPresent)
                {
                    entityContainer[i].Update(gs);
                }
                else
                {
                    DestroyEntity(i);
                    Console.WriteLine("[Stage] Eliminated entity index {0}", i);
                }
            }
        }

        private float setMobSpawnInterval()
        {
            return (elapsedTime < halfPeriodLength) ? (float)((initialSpawnInterval - minimumInterval) * Math.Pow(Math.Cos((elapsedTime * Math.PI * 0.5) / halfPeriodLength), 2) + minimumInterval) : minimumInterval;
        }
    }
}

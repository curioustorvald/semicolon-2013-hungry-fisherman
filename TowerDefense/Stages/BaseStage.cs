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
    public abstract class BaseStage
    {
        public int StageNo;

        public Image stageImage;
        public Image stageImageBuildable;
        protected int maxLife;

        protected float currentLife;

        public const int maxMobs = 32;
        public const int maxTowers = 64; //64
        public const int maxEntities = 16;

        public float elapsedTime = 0;
        public float mobSpawnInterval;

        public BaseMob[] mobContainer;
        public int mobsNo = 0;

        public BaseTower[] towerContainer;
        public int towersNo = 0;

        public BaseEntity[] entityContainer;
        public int entitiesNo = 0;

        protected System.Diagnostics.Stopwatch gameplayWatch = new System.Diagnostics.Stopwatch();

        public Path path;

        private BaseMob[] lowLevelMobs; 
        private BaseMob[] midLevelMobs;
        private BaseMob[] highLevelMobs;

        /* 진행방식: 점수내기
         * 죽을때까지 무한정 플레이
         * ...
         * WTF?
         */

        public BaseStage(int StageNo, Image mapImage, Image buildImage, int maxLife)
        {
            this.StageNo = StageNo;
            this.stageImage = mapImage;
            this.stageImageBuildable = buildImage;
            this.maxLife = maxLife;
            this.currentLife = maxLife;

            mobContainer = new BaseMob[maxMobs];
            towerContainer = new BaseTower[maxTowers];
            entityContainer = new BaseEntity[maxEntities];

            lowLevelMobs = new BaseMob[7];
            midLevelMobs = new BaseMob[6];
            highLevelMobs = new BaseMob[2];

            for (int i = 0; i < lowLevelMobs.Length; i++)
            {
                lowLevelMobs[i] = getMobByIndex(i);
            }

            for (int i = 0; i < midLevelMobs.Length; i++)
            {
                midLevelMobs[i] = getMobByIndex(lowLevelMobs.Length + i);
            }

            for (int i = 0; i < highLevelMobs.Length; i++)
            {
                highLevelMobs[i] = getMobByIndex(lowLevelMobs.Length + midLevelMobs.Length + i);
            }

                gameplayWatch.Start();

            //Console.WriteLine("[Stage] Initialised stage {0}", this.StageNo);

            /* Sub class - add these:
             * path = new Path(
             *  new Vector(x, y),
             *  new Vector(moreX, moreY),
             *  ...;
             * );
             */

            currentLife += 0.0000149f;
        }

        public void addMob(BaseMob mob)
        {
            if (mobsNo < mobContainer.Length)
            {
                mobContainer[mobsNo] = mob;
                mobContainer[mobsNo].setPath(path);
                mobContainer[mobsNo].setRepeat(1);
                mobsNo++;
            }
        }

        public void addTower(BaseTower tower)
        {
            if (towersNo < towerContainer.Length)
            {
                towerContainer[towersNo] = tower;
                towersNo++;
            }
        }

        public void addEntity(BaseEntity entity)
        {
            if (entitiesNo < entityContainer.Length)
            {
                entityContainer[entitiesNo] = entity;
                entitiesNo++;
            }
        }

        public abstract void Render(SceneGame gs, Graphics g);

        public abstract void Update(SceneGame gs, float delta);

        public void DestroyTower(int index)
        {
            towerContainer[index] = towerContainer[towersNo - 1];
            towerContainer[towersNo - 1] = null;
            --towersNo;
        }

        public void DestroyMob(int index)
        {
            mobContainer[index] = mobContainer[mobsNo - 1];
            mobContainer[mobsNo - 1] = null;
            --mobsNo;
        }

        public void DestroyEntity(int index)
        {
            entityContainer[index] = entityContainer[entitiesNo - 1];
            entityContainer[entitiesNo - 1] = null;
            --entitiesNo;
        }

        public void spawnNewTower(SceneGame gs, BaseTower tower, float x, float y, int r)
        {
            addTower(tower);
            towerContainer[towersNo - 1].BuildTower((int)x, (int)y, r);
        }

        public void spawnNewMob(BaseMob mob)
        {
            addMob(mob);
        }

        public void spawnNewEntity(BaseEntity entity)
        {
            addEntity(entity);
        }

        public void takeLife(int i)
        {
            currentLife -= i * 2;
        }

        public int getLife()
        {
            return (int)(currentLife);
        }

        public BaseMob getMobByIndex(int i)
        {
            int index = i;
            
            if (i >= 20000)
            {
                index = (i - 20000) / 10;
            }

            switch (index)
            {
                case 0:
                    return new Default1();
                case 1:
                    return new Default2();
                case 2:
                    return new Default3();
                case 3:
                    return new Default4();
                case 4:
                    return new Default5();

                case 5:
                    return new FlyingFish();
                case 6:
                    return new Squid();
                case 7:
                    return new Transparent();
                case 8:
                    return new Skeleton();
                case 9:
                    return new Jellyfish();

                case 10:
                    return new Tortoise();
                case 11:
                    return new Python();
                case 12:
                    return new AnglerFish();
                case 13:
                    return new Shark();
                case 14:
                    return new Whale();

                default:
                    throw new TDNoSuchMobException();
                    //return null;
            }
        }

        public BaseMob getReasonableRandomMob()
        {
            /*int diceRoll;

            if (elapsedTime >= 300)
            {

                diceRoll = new Random().Next(0, 13 * 4 + 2);

                return (diceRoll < 13 * 4) ? getMobByIndex(diceRoll % 13) :
                    getMobByIndex(13 + diceRoll % 2);
            }
            else
            {
                return getMobByIndex(new Random().Next(0, 12));
            }*/

            int diceRoll;

            if (elapsedTime >= 360)
            {

                diceRoll = new Random().Next(0, 13 * 4 + 2);

                return (diceRoll < 13 * 4) ? getMobByIndex(diceRoll % 13) :
                    getMobByIndex(13 + diceRoll % 2);
            }
            else if (elapsedTime >= 100)
            {
                diceRoll = new Random().Next(0, 13);

                return getMobByIndex(diceRoll);
            }
            else
            {
                diceRoll = new Random().Next(0, 7);

                return getMobByIndex(diceRoll);
            }
        }

        
    }
}

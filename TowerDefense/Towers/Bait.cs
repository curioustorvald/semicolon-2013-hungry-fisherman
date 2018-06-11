using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using TDBase;
using TowerDefense.Entities;
using TowerDefense.Player;

namespace TowerDefense.Towers
{
    class Bait : BaseTower
    {
        private int nearestMobIndex;

        private int baitVelocity = 0;
        private int baitRange = 1;

        private float affectedMobSpeed = 1.0f;

        private int stunLength = 500;

        private bool poisoned = false;

        //private float targetPoint;

        public Bait()
            : base(new ImageSprite(Resources.LPull(NameSet.Tower.TOWER_BAIT), 1, 1, 1))
        {
            setStats("미끼", NameSet.Tower.TOWER_BAIT, 0, 6, 2, 550, 550);
            stopWatch.Start();

            attackCooltime = 10000;
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

        public override void upgrade()
        {
            if (upgradeTier == 0 && Player.Player.getMoney() >= 150)
            {
                spriteSheet = new ImageSprite(Resources.LPull(NameSet.Tower.TOWER_BAIT_1), 1, 1, 1);

                affectedMobSpeed = 0.9f;
                Player.Player.addMoney(-150);
                upgradeTier++;
            }
            else if (upgradeTier == 1 && Player.Player.getMoney() >= 275)
            {
                spriteSheet = new ImageSprite(Resources.LPull(NameSet.Tower.TOWER_BAIT_2), 1, 2, 1);

                baitRange = 2;
                attackRange = rangeUnitToPixels(10);
                Player.Player.addMoney(-275);
                upgradeTier++;
            }
            else if (upgradeTier == 2 && Player.Player.getMoney() >= 450)
            {
                spriteSheet = new ImageSprite(Resources.LPull(NameSet.Tower.TOWER_BAIT_3), 1, 2, 1);

                affectedMobSpeed = 0.6f;
                Player.Player.addMoney(-450);
                upgradeTier++;
            }
            else if (upgradeTier == 3 && Player.Player.getMoney() >= 800)
            {
                spriteSheet = new ImageSprite(Resources.LPull(NameSet.Tower.TOWER_BAIT_4), 1, 2, 1);

                poisoned = true;
                Player.Player.addMoney(-800);
                upgradeTier++;
            }
        }

        public override void Render(SceneGame gs, System.Drawing.Graphics g, int fps)
        {
            this.spriteSheet.setLocation((int)this.posX, (int)this.posY);
            this.spriteSheet.DrawRotated(g, fps, heading);

            ////Console.WriteLine("x: {0}, y:{1}", posX, posY);

            //디버깅
            if (gs.isDebugMode)
            {
                g.FillRectangle(new SolidBrush(Color.Red), posX, posY, 2, 2);
                g.DrawRectangle(new Pen(Color.Yellow), posX - Width / 2, posY - Height / 2, Width, Height);
                g.DrawString(attackCooltimeCounter.ToString(), gs.font, new SolidBrush(Color.White), (float)(posX + Width / 2), (float)(posY + Height / 2 + 5));
            }
        }

        public override void Update(SceneGame gs)
        {
            attackCooltimeCounter = (int)stopWatch.ElapsedMilliseconds;

            if (attackCooltimeCounter >= attackCooltime)
            {
                spriteSheet.setAnimationKey(2);

                throwBait(gs);

                stopWatch.Restart();
            }
        }

        private void throwBait(SceneGame gs)
        {
            Vector baitArrivingPoint = getBaitArrivingPosition(gs);

            gs.currentStage.spawnNewEntity(new EntityBait(baitArrivingPoint.getX(), baitArrivingPoint.getY(), heading, baitVelocity, baitRange, stunLength, affectedMobSpeed, poisoned));

            spriteSheet.setAnimationKey(1);
            stopWatch.Restart();
        }

        public override void RenderRange(System.Drawing.Graphics g)
        {
            //g.FillEllipse(new SolidBrush(Color.FromArgb(63, 240, 240, 255)), posX - attackRange, posY - attackRange, attackRange * 2, attackRange * 2);
            //g.DrawEllipse(new Pen(Color.FromArgb(127, 138, 130, 255)), posX - attackRange, posY - attackRange, attackRange * 2, attackRange * 2);
        
            if (heading == 0){
                g.DrawLine(new Pen(Color.FromArgb(138, 130, 255), 4f), posX, posY, posX, posY - attackRange);
            }
            else if (heading == 1)
            {
                g.DrawLine(new Pen(Color.FromArgb(138, 130, 255), 4f), posX, posY, posX - attackRange, posY);
            }
            else if (heading == 2)
            {
                g.DrawLine(new Pen(Color.FromArgb(138, 130, 255), 4f), posX, posY, posX, posY + attackRange);
            }
            else if (heading == 3)
            {
                g.DrawLine(new Pen(Color.FromArgb(138, 130, 255), 4f), posX, posY, posX + attackRange, posY);
            }
        }

        private bool isOnValidPosition(float x, float y)
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
                throw new TDException();
            }
        }

        /*private float getNearestPathDistance(SceneGame gs)
        {
            float targetPoint = 1000;
            float targetPoint2;
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

            return targetPoint;
        }*/

        private Vector getBaitArrivingPosition(SceneGame gs)
        {
            Vector[] paths = gs.currentStage.path.getVector();

            Vector p0;
            Vector p1;

            float dist1 = 1000;
            float dist2;
            int p0Index = -1;

            int modifier;

            if (gs.currentStageNo == 3 && posY > 372)
            {
                p0 = new Vector(429, 352);
                p1 = new Vector(63, 349);

                modifier = 1;
            }
            else
            {
                //get p0
                for (int i = 0; i < paths.Length; i++)
                {
                    if (isOnValidPosition(paths[i].getX(), paths[i].getY()))
                    {
                        dist2 = distanceFromTower(paths[i].getX(), paths[i].getY());

                        if (dist2 < dist1)
                        {
                            dist1 = dist2;
                            p0Index = i;
                        }
                    }
                }

                ////Console.WriteLine("[Bait] p0 index:" + p0Index);

                p0 = paths[p0Index];

                //get p1
                if (p0Index + 1 >= paths.Length)
                {
                    p1 = paths[p0Index - 1];
                    modifier = -1;
                }
                else if (p0Index == 0)
                {
                    p1 = paths[p0Index + 1];
                    modifier = 1;
                }
                else
                {
                    if (isOnValidPosition(paths[p0Index + 1].getX(), paths[p0Index + 1].getY()) &&
                        !isOnValidPosition(paths[p0Index - 1].getX(), paths[p0Index - 1].getY()))
                    {
                        p1 = paths[p0Index + 1];
                        modifier = 1;
                    }
                    else if (!isOnValidPosition(paths[p0Index + 1].getX(), paths[p0Index + 1].getY()) &&
                        isOnValidPosition(paths[p0Index - 1].getX(), paths[p0Index - 1].getY()))
                    {
                        p1 = paths[p0Index - 1];
                        modifier = -1;
                    }
                    else if (isOnValidPosition(paths[p0Index + 1].getX(), paths[p0Index + 1].getY()) &&
                        isOnValidPosition(paths[p0Index - 1].getX(), paths[p0Index - 1].getY()))
                    {
                        //get nearest
                        if (distanceFromTower(paths[p0Index + 1].getX(), paths[p0Index + 1].getY()) <
                            distanceFromTower(paths[p0Index - 1].getX(), paths[p0Index - 1].getY()))
                        {
                            p1 = paths[p0Index + 1];
                            modifier = 1;
                        }
                        else
                        {
                            p1 = paths[p0Index - 1];
                            modifier = -1;
                        }
                    }
                    else
                    {
                        p1 = p0;
                        modifier = 1;
                    }
                }
            }

            //Console.WriteLine("[Bait] p0 x: {0}, y: {1}", p0.getX(), p0.getY());
            //Console.WriteLine("[Bait] p1 x: {0}, y: {1}", p1.getX(), p1.getY());
            
            Vector px = linearInterpolate(p0, p1, posX, posY, modifier);

            ////Console.WriteLine("[Bait] bait arriving pos: {0}, {1}", px.getX(), px.getY());

            //get px
            return px;
        }

        private Vector linearInterpolate(Vector p0, Vector p1, float x, float y, int modifier)
        {
            float percentage;

            if (heading % 2 == 0)
            {
                if (p0.getX() - p1.getX() == 0)
                {
                    percentage = 0.5f;
                }
                else
                {
                    //percentage = (x - Math.Max(p0.getX(), p1.getX())) / ((p0.getX() - p1.getX()));
                    percentage = (modifier == 1) ?
                        (x - p0.getX()) / ((p1.getX() - p0.getX())) :
                        (x - p1.getX()) / ((p0.getX() - p1.getX()));
                }
            }
            else
            {
                if (p0.getY() - p1.getY() == 0)
                {
                    percentage = 0.5f;
                }
                else
                {
                    //percentage = (y - Math.Max(p0.getY(), p1.getY())) / ((p0.getY() - p1.getY()));
                    percentage = (modifier == 1) ?
                        (y - p0.getY()) / ((p1.getY() - p0.getY())) :
                        (y - p1.getY()) / ((p0.getY() - p1.getY()));
                }
            }

            //Console.WriteLine("[Bait.interpolate] percentage: {0}", percentage);

            float interpolatedPoint;
            if (heading % 2 == 0)
            {
                interpolatedPoint = (modifier == 1) ?
                    p0.getY() * (1 - percentage) + p1.getY() * percentage :
                    p1.getY() * (1 - percentage) + p0.getY() * percentage;
            }
            else
            {
                interpolatedPoint = (modifier == 1) ?
                    p0.getX() * (1 - percentage) + p1.getX() * percentage :
                    p1.getX() * (1 - percentage) + p0.getX() * percentage;
            }

            return (heading % 2 == 0) ?
                new Vector(x, interpolatedPoint) :
                new Vector(interpolatedPoint, y);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using TowerDefense.Towers;

namespace TowerDefense.Mobs
{
    class Skeleton : BaseMob
    {
        bool nearTower = false;
        public Skeleton() : base(new imageSprite(Resources.LPull(NameSet.Mob.SKELETON)))
    {
        //몹 특성 설정
        setStats(",컬피쉬", NameSet.Mob.SKELETON, 40, 40, 20, 1, NameSet.AttackType.xxATTACK_TYPExx, 7, 가치);
    }
        public override void Render(SceneGame gs, Graphics g, int fps)
    {
        this.spriteSheet.setLocation((int)(this.posX), (int)(this.posY));
        this.spriteSheet,DrawRotated(g, fps, heading);

    }
        public override void Update(SceneGame gs)

        hasArrived = isArriving(posX, posY);

        if (currentHealth <= 0)
        {
            kill();
        }
    }
}

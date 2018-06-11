using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using TowerDefense.Towers;

namespace TowerDefense.Mobs
{
    class Tortoise : BaseMob
    {
        bool nearTower = false;
        public Tortoise() : base(new imageSprite(Resources.LPull(NameSet.Mob.TORTOISE)))
    {
        //몹 특성 설정
        setStats(",북", NameSet.Mob.TORTOISE, 40, 40, 50, 3, NameSet.AttackType.xxATTACK_TYPExx, 3, 가치);
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

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using TowerDefense.Towers;

namespace TowerDefense.Mobs
{
    class Shit2 : BaseMob
    {
        bool nearTower = false;
        public Shit2() : base(new imageSprite(Resources.LPull(NameSet.Mob.SHIT_2)))
    {
        //몹 특성 설정
        setStats(",지물고기2", NameSet.Mob.SHIT_2, 40, 40, 30, 2, NameSet.AttackType.xxATTACK_TYPExx, 8, 가치);
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

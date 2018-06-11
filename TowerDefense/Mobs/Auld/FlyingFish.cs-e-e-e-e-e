using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using TowerDefense.Towers;

namespace TowerDefense.Mobs
{
    class FlyingFish: BaseMob
        bool nearTower = false;
        public FlyingFish() : base(new ImageSprite(Resources.LPull(NameSet.Mob.FLYING_FISH))) 
    {
        //몹 특성 설정
        setStats(",치", NameSet.Mob.FLYING_FISH, 40 , 40, 6, 0, NameSet.attackType.xxATTACK_TYPExx, 8, 가치);
    
    }
    public override void Render(SceneGame gs, Graphics g, int fps)
{
    this.spriteSheet.setLocation((int)(this.posX), (int)(this.posY));
    this.spriteSheet.DrawRotated(g, fps, heading);

    public override void Update(gamsScene gs)
    hasArrived = isArriving(posX, posY);
    if (currentHealth <=0)
{
    kill();
}


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using TowerDefense.Towers;
using TowerDefense.Player;

namespace TowerDefense.Mobs
{
    class Default3 : BaseMob
    {

        public Default3()
            : base(new ImageSprite(Resources.LPull(NameSet.Mob.DEFAULT_3), 18, 1, 1))
        {
            //몹 특성 설정
            setStats("복어", NameSet.Mob.DEFAULT_3, 40, 40, 10, 0, 6, 4, 1);
            

        }

        public override void Render(SceneGame gs, Graphics g, int fps)
    		{
    		this.spriteSheet.setLocation((int)(this.posX), (int)(this.posY));
    		this.spriteSheet.DrawRotated(g, fps, heading);
    		
    		//HP
            g.FillRectangle(new SolidBrush(Color.AntiqueWhite), posX - Width / 2, posY - Height / 2 - 9, 40, 5);
            g.FillRectangle(new SolidBrush(Color.Green), posX - Width / 2, posY - Height / 2 - 9, 40 * (currentHealth / (float)(maxHealth)), 5);

            //디버깅
            if (gs.isDebugMode)
            {
                g.FillRectangle(new SolidBrush(Color.Red), posX, posY, 2, 2);
                g.DrawRectangle(new Pen(Color.Yellow), posX - Width / 2, posY - Height / 2, Width, Height);
            }
        }


        public override void Update(SceneGame gs)
        {

            hasArrived = isArriving(posX, posY);

            if (hasArrived) //맵 탈출
            {
                escapeMap(gs);
            }

            if (isAlive)
            {
                move();
            }

            if (currentHealth <= 0)
            {
                kill();
            }
        }
    }
}

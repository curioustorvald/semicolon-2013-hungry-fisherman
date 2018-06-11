using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDBase;
using System.Drawing;

namespace TowerDefense
{
    //public delegate void callback(); safely disabled for class 'Button'

    class Button_TDBase
    {
        /* setLocation(x, y) : 해당 버튼의 위치 지정
         * setSize(width, height) : 해당 버튼의 크기 조정
         * onClick(func), onRelease(func), onMove(func) : 클릭했을때 발생 / 뗀 후 발생 / 버튼이 눌리고 있을 때 항상 발생
         * 버튼은 Over, Press시 눌렸다고 인지
         * MouseDown 상태에서 버튼과 충돌하지 않으면 No Press
         */

        int width, height;
        int x = 0;
        int y = 0;
        int click = 0;
        Tex2D released;
        Tex2D over;
        Tex2D pressed;
        callback clicked;
        callback release;
        callback move;
        
        public Button_TDBase(Tex2D released, Tex2D over, Tex2D pressed, int width, int height)
        {
            this.width = width;
            this.height = height;
            this.released = released;
            this.over = over;
            this.pressed = pressed;
        }
        public Button_TDBase(Tex2D released, Tex2D over, Tex2D pressed)
        {
            this.width = (int)released.Width();
            this.height = (int)released.Height();
            this.released = released;
            this.over = over;
            this.pressed = pressed;
        }
        public void Draw(Graphics graphics, int x, int y)
        {
            this.x = x;
            this.y = y;
            if (isPressed(x, y))
                GDIController.DrawImage(graphics, pressed, x, y, width, height);
            else if (isOver(x, y))
                GDIController.DrawImage(graphics, over, x, y, width, height);
            else
                GDIController.DrawImage(graphics, released, x, y, width, height);
        }
        public void Draw(Graphics graphics)
        {
            if (isPressed(x, y))
                GDIController.DrawImage(graphics, pressed, x, y, width, height);
            else if (isOver(x, y))
                GDIController.DrawImage(graphics, over, x, y, width, height);
            else
                GDIController.DrawImage(graphics, released, x, y, width, height);
        }
        public void setLocation(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public void setSize(int width, int height)
        {
            this.width = width;
            this.height = height;
        }
        private bool isOver(int x, int y)
        {
            if (x <= MouseClass.mouseX &&
               (x + width) >= MouseClass.mouseX &&
               y <= MouseClass.mouseY &&
               (y + height) >= MouseClass.mouseY)
                return true;
            else
                return false;
        }
        private bool isStartOver(int x, int y)
        {
            if (x <= MouseClass.startX &&
               (x + width) >= MouseClass.startX &&
               y <= MouseClass.startY &&
               (y + height) >= MouseClass.startY)
                return true;
            else
                return false;
        }
        private bool isPressed(int x, int y)
        {
            if (isOver(x, y) == true && MouseClass.isClicked == true && isStartOver(x, y))
            {
                if (clicked != null && click == 0)
                {
                    clicked();
                    click = 1;
                }
                if (move != null)
                {
                    move();
                }
                return true;
            }
            else
                if (release != null && click == 1)
                {
                    release();
                }
                click = 0;
                return false;
        }
        public void onClick(callback mouseDown)
        {
            clicked = mouseDown;
        }
        public void onRelease(callback mouseup)
        {
            release = mouseup;
        }
        public void onMove(callback mousemove)
        {
            move = mousemove;
        }

    }
}

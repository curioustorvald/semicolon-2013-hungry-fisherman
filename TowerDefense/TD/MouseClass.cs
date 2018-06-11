using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDefense
{
    public class MouseClass
    {
        public static float mouseX;
        public static float mouseY;
        public static float startX;
        public static float startY;
        public static bool isClicked;

        public static System.Windows.Forms.MouseButtons button;
        public MouseClass()
        {

        }
        public static void setMouse(float x, float y, System.Windows.Forms.MouseButtons button)
        {
            MouseClass.mouseX = x;
            MouseClass.mouseY = y;
            MouseClass.button = button;
        }
        public static void setMouse(float x, float y, System.Windows.Forms.MouseButtons button, bool clicked)
        {
            if (!MouseClass.isClicked && clicked)
            {
                MouseClass.startX = x;
                MouseClass.startY = y;
            }
            else if (MouseClass.isClicked && !clicked)
            {
                MouseClass.startX = 0;
                MouseClass.startY = 0;
            }
            MouseClass.mouseX = x;
            MouseClass.mouseY = y;
            MouseClass.button = button;
            MouseClass.isClicked = clicked;
        }
    }
}

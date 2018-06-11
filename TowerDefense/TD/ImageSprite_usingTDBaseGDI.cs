using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDBase;
using System.Drawing;

namespace TowerDefense
{
    public class ImageSpriteTDBase
    {
        /* setLocation(x, y) : 해당 스프라이트의 위치 지정
         * setSize(width, height) : 해당 스프라이트의 크기 조정
         * Draw(graphics, frame) : 스프라이트의 갱신 주기 (단위 : fps)
         */
        Dictionary<int, Tex2D> image;
        private int currentframe = 0;
        private int delta = 1;
        private int x = 0, y = 0;
        private int width, height;

        public ImageSpriteTDBase(Dictionary<int, Tex2D> image)
        {
            this.width = (int)image[0].Width();
            this.height = (int)image[0].Height();
            this.image = image;
        }
        public void Draw(Graphics graphics)
        {
            GDIController.DrawImage(graphics, image[currentframe], x, y, width, height);
            if (currentframe >= image.Count - 1)
                currentframe = 0;
            else
                currentframe++;
        }
        public void Draw(Graphics graphics, int frame)
        {
            delta++;
            GDIController.DrawImage(graphics, image[currentframe], x, y, width, height);
            if (delta == frame)
            {
                Draw(graphics);
                delta = 1;
            }
            if (delta > frame) delta = 1;
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
        public int getX()
        {
            return this.x;
        }
        public int getY()
        {
            return this.y;
        }
    }
}

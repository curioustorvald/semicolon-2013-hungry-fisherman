using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TowerDefense
{
    public class ImageSprite
    {
        /* setLocation(x, y) : 해당 스프라이트의 위치 지정
         * setSize(width, height) : 해당 스프라이트의 크기 조정
         * Draw(graphics, frame) : 스프라이트의 갱신 주기 (단위 : fps)
         */
        Dictionary<int, Image> spriteAnim;
        private int currentframe = 0;
        private int delta = 1;
        private int x = 0, y = 0;
        private int Width, Height;

        private int defaultAnimCount;
        private int attackAnimCount;
        private int cooltimeAnimCount;

        private int animationKey = 0; //0: default, 1: attack, 2: cooltime

        public ImageSprite(Dictionary<int, Image> image, int p1, int p2, int p3)
        {
            try
            {
                this.Width = (int)image[0].Width;
                this.Height = (int)image[0].Height;
                this.spriteAnim = image;

                this.defaultAnimCount = p1;
                this.attackAnimCount = p2;
                this.cooltimeAnimCount = p3;
            }
            catch
            {
                throw new TDResourceException();
            }
        }

        private void advanceFrame()
        {
            int animCount;
            int indexStart;
            
            switch (animationKey)
            {
                case 0:
                    animCount = defaultAnimCount;
                    indexStart = 0;
                    break;
                case 1:
                    animCount = attackAnimCount + defaultAnimCount;
                    indexStart = attackAnimCount;
                    break;
                case 2:
                    animCount = cooltimeAnimCount + attackAnimCount + defaultAnimCount;
                    indexStart = attackAnimCount + cooltimeAnimCount;
                    break;
                default:
                    throw new TDException();
            }

            if (currentframe >= animCount - 1)
            {
                currentframe = indexStart;
            }
            else
            {
                currentframe++;
            }
        }
        public void Draw(Graphics g)
        {
            g.DrawImage(spriteAnim[currentframe], x - Width/2, y - Height/2, Width, Height);
            if (currentframe >= spriteAnim.Count - 1)
                currentframe = 0;
            else
                //currentframe++;
                advanceFrame();
        }

        public void Draw(Graphics g, int frame)
        {
            delta++;
            g.DrawImage(spriteAnim[currentframe], x - Width/2, y - Height/2, Width, Height);
            if (delta == frame)
            {
                //Draw(g);
                advanceFrame();
                delta = 1;
            }
            if (delta > frame) delta = 1;
        }

        public void DrawSpecificFrame(Graphics g, int imageFrame)
        {
            g.DrawImage(spriteAnim[imageFrame], x - Width / 2, y - Height / 2, Width, Height);
        }

        public void DrawRotated(Graphics g, int frame, int heading)
        {
            delta++;

            if (heading == 0)
            {
                g.DrawImage(spriteAnim[currentframe], x - Width/2, y - Height/2, Width, Height);
            }
            else if (heading == 1)
            {
                Point[] pts = {new Point(x - Height/2, y + Width/2), new Point(x - Height/2, y - Width/2), new Point(x + Height/2, y + Width/2)};
                g.DrawImage(spriteAnim[currentframe], pts);
            }
            else if (heading == 2)
            {
                Point[] pts = { new Point(x + Width/2, y + Height/2), new Point(x - Width/2, y + Height/2), new Point(x + Width/2, y - Height/2) };
                g.DrawImage(spriteAnim[currentframe], pts);
            }
            else if (heading == 3)
            {
                Point[] pts = { new Point(x + Height/2, y - Width/2), new Point(x + Height/2, y + Width/2), new Point(x - Width/2, y  - Height/2) };
                g.DrawImage(spriteAnim[currentframe], pts);
            }
            
            if (delta == frame)
            {
                advanceFrame();
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
            this.Width = width;
            this.Height = height;
        }
        public void setAnimationKey(int p)
        {
            animationKey = p;

            switch (animationKey)
            {
                case 0:
                    currentframe = 0;
                    break;
                case 1:
                    currentframe = attackAnimCount;
                    break;
                case 2:
                    currentframe = attackAnimCount + cooltimeAnimCount;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
        public int getPosX()
        {
            return this.x;
        }
        public int getPosY()
        {
            return this.y;
        }
        public int getWidth()
        {
            return this.Width;
        }
        public int getHeight()
        {
            return this.Height;
        }
        public Image getImage(int key)
        {
            return spriteAnim[key];
        }
    }
}

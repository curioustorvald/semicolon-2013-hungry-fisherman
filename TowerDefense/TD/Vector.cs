using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDefense
{
    public class Vector
    {
        private float vecx;
        private float vecy;

        public Vector(float x, float y)
        {
            this.vecx = x;
            this.vecy = y;
        }

        public Vector()
        {
            this.vecx = 0;
            this.vecy = 0;
        }

        public static float getDistance(Vector v1, Vector v2)
        {
            return (float)Math.Sqrt(Math.Pow(v1.getX() - v2.getX(), 2) + Math.Pow(v1.getY() - v2.getY(), 2));
        }
        public static Vector VectorAdd(Vector v1, Vector v2)
        {
            return new Vector(v1.getX() + v2.getX(), v1.getY() + v2.getY());
        }
        public static Vector VectorSubtract(Vector v1, Vector v2) //v1 - v2
        {
            return new Vector(v1.getX() - v2.getX(), v1.getY() - v2.getY());
        }
        public static float getSize(Vector v)
        {
            return (float)Math.Sqrt(Math.Pow(v.getX(), 2) + Math.Pow(v.getY(), 2));
        }
        public static Vector getVectorFromSize(int size, Vector direction)
        {
            float cos = ((float)direction.getX() / Vector.getSize(direction)); //방향 코사인
            float sin = ((float)direction.getY() / Vector.getSize(direction)); //방향 코사인
            float x = ((float)size * cos);
            float y = ((float)size * sin);
            return new Vector(x, y);
        }

        public static float getVectorAngle(Vector v1, Vector v2)
        {
            float dX = v2.getX() - v1.getX();
            float dY = v1.getY() - v2.getY();

            return (float)(Math.Atan2(dY, dX) * 180 / Math.PI); //60분법 단위로 변환
        }

        public void setX(float x)
        {
            vecx = x;
            //return this;
        }
        public void setY(float y)
        {
            vecy = y;
        }
        public float getX()
        {
            return this.vecx;
        }
        public float getY()
        {
            return this.vecy;
        }

    }
}

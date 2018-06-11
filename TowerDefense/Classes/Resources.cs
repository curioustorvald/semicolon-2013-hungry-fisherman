using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using TDBase;

namespace TowerDefense
{
    class Resources
    {
        //public static Resources resouces = new Resources(); // 사용 시 Resource.resource 사용

        /* Resources
         * L이 붙지 않은것과 붙은 것은, 단일 저장 <-> 여러 리소스를 하나로 묶어 저장<dictionary>
         * Add(Type, t) : Type 이름의 리소스 t를 저장 사용 시 Pull(Type)
         * LAdd(Type, t (, t, t ...)) : Type 이름의 여러 리소스를 저장
         * LAdd(Type, (dictionary)t) : Type 이름의 묶여있는 리소스 저장
         * Pull / LPull (Type) : Type 리소스 꺼내기
         * Remove / LRemove : Type 리소스 지우기
         * Type 은 NameSet클래스 이용. ex) NameSet.Mob.FOOBAR
         * getResno / getLResno : 리소스 개수 반환
         */
        private static Dictionary<int, Image> res = new Dictionary<int, Image>();
        private static Dictionary<int, Dictionary<int, Image>> lres = new Dictionary<int, Dictionary<int, Image>>();
        private static Dictionary<int, Audio> audioDict = new Dictionary<int, Audio>();
        private static int _resno = 0;
        private static int _lresno = 0;

        public Resources()
        {
        }

        public static int Resno {
            get {
                return _resno;
            }
            private set {
                _resno = value;
            }
        }

        public static int LResno
        {
            get {
                return _lresno;
            }
            private set {
                _lresno = value;
            }
        }

        public static void Add(int Type, Image t)
        {
            if (!res.ContainsKey(Type))
            {
                Resno++;
                res.Add(Type, t);
            }
            else
            {
                throw new TDResourceException();
            }
        }

        public static void Add(int type, Audio t)
        {
            if (!audioDict.ContainsKey(type))
            {
                Resno++;
                audioDict.Add(type, t);
            }
            else
            {
                throw new TDResourceException();
            }
        }

        public static void Remove(int Type)
        {
            if (res.ContainsKey(Type))
            {
                Resno--;
                res.Remove(Type);
            }
        }

        public static Image Pull(int Type)
        {
            if (res.ContainsKey(Type))
            {
                return res[Type];
            }
            return null;
        }

        public static void LAdd(int Type, params Image[] t)
        {
            if (!lres.ContainsKey(Type))
            {
                Dictionary<int, Image> temp = new Dictionary<int,Image>();
                for (int i = 0; i < t.Length; i++)
                {
                    temp.Add(i, t[i]);
                }
                lres.Add(Type, temp);
                LResno++;
            }
            else
            {
                throw new TDKeyCollisionException();
            }
        }

        public static void LAdd(int Type, Dictionary<int, Image> t)
        {
            if (!lres.ContainsKey(Type))
            {
                lres.Add(Type, t);
                LResno++;
            }
            else
            {
                throw new TDKeyCollisionException();
            }
        }

        public static void LRemove(int Type)
        {
            if (lres.ContainsKey(Type))
            {
                lres.Remove(Type);
                LResno--;
            }
        }

        public static Dictionary<int, Image> LPull(int Type)
        {
            if (lres.ContainsKey(Type))
            {
                return lres[Type];
            }
            else
            {
                return null;
            }
        }

        public static int getResno()
        {
            return res.Count;
        }

        public static int getLResno()
        {
            return lres.Count;
        }
    }
}

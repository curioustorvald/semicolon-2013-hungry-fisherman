using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDefense
{
    public class Path
    {
        private int pathno;
        public int currentPath;
        public int nextPath;
        private Vector[] pvector;
        public Path(params Vector[] v)
        {
            pathno = v.Length;
            this.pvector = v;
            if(pathno == 1) {
                currentPath = 0;
                nextPath = 0;
            } else if(pathno > 1) {
                currentPath = 0;
                nextPath = 1;
            } else {
                //Console.WriteLine("[Path] Could not generate path.");
                throw new TDException();
            }
        }
        public Vector[] getVector()
        {
            return pvector;
        }
        public int getPathno()
        {
            return this.pathno;
        }
    }
}

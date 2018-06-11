using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    public class TDException : Exception
    {
        public TDException(string msg)
            : base(msg)
        {
        }

        public TDException()
            : base()
        {
        }
    }

    public class TDNegativeMoneyException : Exception
    {
        public TDNegativeMoneyException(string msg)
            : base(msg)
        {
        }

        public TDNegativeMoneyException()
            : base()
        {
        }
    }

    public class TDNegativeScoreException : Exception
    {
        public TDNegativeScoreException(string msg)
            : base(msg)
        {
        }

        public TDNegativeScoreException()
            : base()
        {
        }
    }

    public class TDResourceException : Exception
    {
        public TDResourceException(string msg)
            : base(msg)
        {
        }

        public TDResourceException()
            : base()
        {
        }
    }

    public class TDTowerNotSelectedException : Exception
    {
        public TDTowerNotSelectedException(string msg)
            : base(msg)
        {
        }

        public TDTowerNotSelectedException()
            : base()
        {
        }
    }

    public class TDNoSuchMobException : Exception
    {
        public TDNoSuchMobException(string msg)
            : base(msg)
        {
        }

        public TDNoSuchMobException()
            : base()
        {
        }
    }

    public class TDNoSuchTowerException : Exception
    {
        public TDNoSuchTowerException(string msg)
            : base(msg)
        {
        }

        public TDNoSuchTowerException()
            : base()
        {
        }
    }

    public class TDKeyCollisionException : Exception
    {
        public TDKeyCollisionException(string msg)
            : base(msg)
        {
        }

        public TDKeyCollisionException()
            : base()
        {
        }
    }
}

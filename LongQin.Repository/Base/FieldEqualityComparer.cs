using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongQin.Repository
{
    public class FieldEqualityComparer : IEqualityComparer<string>
    {
        public bool Equals(string x, string y)
        {
            if(x == null && y == null)
            {
                return true;
            }

            if(x != null && y != null)
            {
                return x.ToLower () == y.ToLower();
            }

            return false;
        }

        public int GetHashCode(string obj)
        {
            return obj.GetHashCode();
        }
    }
}

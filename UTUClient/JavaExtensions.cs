using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTUClient
{
    public static class JavaExtensions
    {
        public static IEnumerable<T> toIEnumerable<T>(this java.util.List list)
        {
            if (list != null)
            {
                java.util.Iterator itr = list.iterator();

                while (itr.hasNext())
                {
                    yield return (T)itr.next();
                }
            }
        }
    }
}

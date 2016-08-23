using com.farast.utuapi.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTUClient
{
    class Static
    {
        public static readonly DataLoader loader = new DataLoader("http://utu.herokuapp.com");
    }
}

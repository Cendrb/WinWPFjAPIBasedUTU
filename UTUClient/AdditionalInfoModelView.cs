using com.farast.utuapi.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTUClient
{
    class AdditionalInfoModelView
    {
        AdditionalInfo source;

        public string Title
        {
            get
            {
                return source.getName();
            }
        }

        public string Url
        {
            get
            {
                return source.getUrl();
            }
        }

        public AdditionalInfoModelView(AdditionalInfo source)
        {
            this.source = source;
        }
    }
}

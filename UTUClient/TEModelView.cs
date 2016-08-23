using com.farast.utuapi.data;
using com.farast.utuapi.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTUClient
{
    class TEModelView
    {
        private TEItem source;

        public string Title
        {
            get
            {
                return source.getTitle();
            }
        }

        public string Description
        {
            get
            {
                return source.getDescription();
            }
        }

        public string Subject
        {
            get
            {
                return source.getSubject().getName();
            }
        }

        public string Date
        {
            get
            {
                return DateUtil.CZ_SHORT_DATE_FORMAT.format(source.getDate());
            }
        }

        public string Group
        {
            get
            {
                return source.getSgroup().getName();
            }
        }

        public List<AdditionalInfoModelView> AdditionalInfos
        {
            get
            {
                List<AdditionalInfoModelView> additionalInfos = new List<AdditionalInfoModelView>();
                foreach (AdditionalInfo info in source.getAdditionalInfos().toIEnumerable<AdditionalInfo>())
                    additionalInfos.Add(new AdditionalInfoModelView(info));
                return additionalInfos;
            }
        }

        public TEModelView(TEItem source)
        {
            this.source = source;
        }
    }
}

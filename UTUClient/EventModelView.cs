using com.farast.utuapi.data;
using com.farast.utuapi.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTUClient
{
    class EventModelView
    {
        private Event source;

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

        public string Location
        {
            get
            {
                return source.getLocation();
            }
        }

        public string Date
        {
            get
            {
                if (source.getStart() == source.getEnd())
                    return DateUtil.CZ_SHORT_DATE_FORMAT.format(source.getStart());
                else
                    return DateUtil.CZ_SHORT_DATE_FORMAT.format(source.getStart()) + " - " + DateUtil.CZ_SHORT_DATE_FORMAT.format(source.getEnd());
            }
        }

        public string Pay
        {
            get
            {
                if (source.getPrice() == 0)
                    return "zdarma";
                else
                {
                    if (source.getStart() == source.getPayDate())
                        return "zaplatit " + source.getPrice().ToString() + " Kč na místě";
                    else
                        return "zaplatit " + source.getPrice().ToString() + " Kč do " + DateUtil.CZ_SHORT_DATE_FORMAT.format(source.getPayDate());
                }
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

        public EventModelView(Event source)
        {
            this.source = source;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Review_Api.Models.Query
{
    public class Filter
    {

        public List<string> Filters;

        public Filter(List<string> filters)
        {
            Filters = filters;
        }

        public Filter()
        {
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("");
            if (Filters != null)
            {
                foreach (string filter in Filters)
                {
                    sb.Append(filter.ToString());
                }
            }
            return sb.ToString();
        }
    }
}

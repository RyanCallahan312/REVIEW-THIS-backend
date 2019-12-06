using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Review_Api.Models.Query
{
    public class Filter
    {

        public string field;
        public string value;

        public Filter(string field, string value)
        {
            this.field = field;
            this.value = value;
        }

        public Filter()
        {
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            if(field == null)
            {
                sb.Append(field + " ");
            }

            if (value == null)
            {
                sb.Append(value);
            }

            return sb.ToString();
        }
    }
}

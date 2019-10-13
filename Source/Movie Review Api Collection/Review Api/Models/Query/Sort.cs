using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace Review_Api.Models.Query
{
    public class Sort
    {
        public string Direction;
        public string Field;

        public Sort(string direction, string field)
        {
            Direction = direction;
            Field = field;
        }
        public Sort()
        {
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("");
            if(Direction != null)
            {
                sb.Append(Direction);
            }

            if(Field != null)
            {
                sb.Append("");
            }

            return sb.ToString();
        }
    }
}

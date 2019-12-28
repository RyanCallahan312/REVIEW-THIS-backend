using System.Text;

namespace review_api.Models.Query
{
    public class Sort 
    {
        public string Direction { get; set; }
        public string Field { get; set; }

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
            if (Direction != null)
            {
                sb.Append(Direction);
            }

            if (Field != null)
            {
                sb.Append("");
            }

            return sb.ToString();
        }
    }
}

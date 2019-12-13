using System.Text;

namespace Review_Api.Models.Query
{
    public class Filter
    {

        public string Field { get; set; }
        public string Value { get; set; }

        public Filter(string field, string value)
        {
            Field = field;
            Value = value;
        }

        public Filter()
        {
            Field = null;
            Value = null;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            if (Field == null)
            {
                sb.Append(Field + " ");
            }

            if (Value == null)
            {
                sb.Append(Value);
            }

            return sb.ToString();
        }
    }
}

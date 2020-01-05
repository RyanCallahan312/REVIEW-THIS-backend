using MongoDB.Bson.Serialization.Attributes;

namespace review_api.Models
{
    public class Section
    {
        [BsonRequired]
        public string Header { get;}
        [BsonRequired]
        public string Body { get;}

        public Section(string header, string body)
        {
            Header = header;
            Body = body;
        }
    }
}

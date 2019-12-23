using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace review_api.Models
{
    public class Section
    {
        [BsonRequired]
        public string Header { get; set; }
        [BsonRequired]
        public string Body { get; set; }

        public Section(string header, string body)
        {
            Header = header;
            Body = body;
        }
    }
}

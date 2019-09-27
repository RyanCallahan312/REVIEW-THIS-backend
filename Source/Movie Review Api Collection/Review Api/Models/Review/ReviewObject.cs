using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Review_Api.Models
{
    public class ReviewObject
    {
        public string ReviewId { get; }
        public string UserId { get; }
        public DateTime Time { get; }
        public string Movie { get; }
        public string Genre { get; }
        public float Rating { get { return Rating; } set { Rating = value; } }
        public string Headline { get; set; }
        public List<SectionsObject> Sections { get; set; }
        public List<ModificationsObject> Modifications { get; set; }
        public List<DeletionsObject> Deletions { get; set; }
        public List<string> Comments { get; set; }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Review_Api.models
{
    public class ReviewObject
    {
        public string reviewId;
        public string userId;
        public DateTime time;
        public string movie;
        public string genre;
        public float rating;
        public string headline;
        public List<SectionsObject> sections;
        public List<ModificationsObject> modifications;
        public List<string> comments;
    }
}

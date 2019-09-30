using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Review_Api.Models
{
    public class DeletionsObject
    {

        public bool Deleted { get; set; }
        public string User { get; set; }
        public DateTime Time { get; set; }

        public DeletionsObject(bool deleted, string user, DateTime time)
        {
            Deleted = deleted;
            User = user;
            Time = time;
        }
    }
}

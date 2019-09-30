using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Review_Api.Models
{
    public class SectionsObject
    {
        public string Header { get; set; }
        public string Body { get; set; }

        public SectionsObject(string header, string body)
        {
            Header = header;
            Body = body;
        }
    }
}

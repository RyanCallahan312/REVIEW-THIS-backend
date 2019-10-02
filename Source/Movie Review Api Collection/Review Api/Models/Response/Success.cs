using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Review_Api.Models.Response
{
    public class Success
    {
        public string Message { get; set; }
        public Guid ReviewId { get; set; }
    }
}

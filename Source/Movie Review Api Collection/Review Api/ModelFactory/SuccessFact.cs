using Review_Api.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Review_Api.ModelFactory
{
    public class SuccessFact
    {
        public static Success Default(Guid id)
        {
            return new Success("Completed Operation", id);
        }
    }
}

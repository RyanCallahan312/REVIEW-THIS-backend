using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Review_Api.Models.Response;

namespace Review_Api.ModelFactory
{
    public class FailureFact
    {
        public static Failure BadId()
        {
            return new Failure("Invalid Id", "NO_GUID_ID");
        }
        public static Failure IdNotFound()
        {
            return new Failure("ID not found", "NO_REC_BY_ID");
        }
        public static Failure Default() {
            return new Failure("Something went wrong", "UNSEEN_ERROR");
        }
    }
}

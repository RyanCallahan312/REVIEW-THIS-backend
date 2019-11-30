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
        public static Failure NoRecordsFound()
        {
            return new Failure("No records found for current search", "NO_REC_FOUND");
        }
        public static Failure UnevenFilters()
        {
            return new Failure("Uneven filters", "VALUE_FIELD_COUNT_MISMATCH");
        }
        public static Failure Default() {
            return new Failure("Something went wrong", "UNSEEN_ERROR");
        }
    }
}

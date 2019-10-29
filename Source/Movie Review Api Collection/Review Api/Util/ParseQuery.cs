using Review_Api.Models.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Review_Api.Util
{
    public class ParseQuery
    {
        public Sort ParseSort(string sortDirection, string sortField)
        {
            return new Sort(sortDirection, sortField);
        }

        public List<Filter> ParseFilters(string filterFields, string filterValues)
        {
            List<Filter> filters = new List<Filter>();
            List<string> values = filterValues.Split(',').ToList();
            List<string> fields = filterFields.Split(',').ToList();
            for (int i = 0; i < fields.Count; i++)
            {
                filters.Add(new Filter(fields.ElementAt(i), values.ElementAt(i)));
            }
            return filters;
        }

        public Page ParsePage(int pageNumber, int pageItems)
        {
            return new Page(pageNumber, pageItems);
        }
    }
}

﻿using Review_Api.Models.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Review_Api.Util
{
    public static class ParseQuery
    {
        public static Sort ParseSort(string sortDirection, string sortField)
        {
            if (sortDirection == null || sortField == null)
            {
                return new Sort("asc", "time");
            }

            return new Sort(sortDirection, sortField);
        }

        public static List<Filter> ParseFilters(string filterFields, string filterValues)
        {

            List<Filter> filters = new List<Filter>();

            if (filterFields == null || filterValues == null)
            {
                return filters;
            }

            List<string> values = filterValues.Split(',').ToList();
            List<string> fields = filterFields.Split(',').ToList();

            for (int i = 0; i < fields.Count; i++)
            {
                filters.Add(new Filter(fields.ElementAt(i), values.ElementAt(i)));
            }

            return filters;
        }

        public static Page ParsePage(int pageNumber, int pageItems)
        {
            return new Page(pageNumber, pageItems);
        }
    }
}

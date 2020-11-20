﻿using System.Collections.Generic;

namespace EPS.Administration.Models.APICommunication.Filter
{
    public class DeviceFilter
    {
        public int Page { get; set; }
        public int AllPages { get; set; }
        public int PageSize { get; set; } = 50;
        private string InitializeQuery => "x =>";
        public string SerialNumberFilter { get; set; }
        public string ModelFilter { get; set; }
        public string ClassificationFilter { get; set; }
        public string EventsCountFilter { get; set; }
        public string LastUpdateFilter { get; set; }
        public string OrderBy { get; set; }
        public bool ReverseOrder { get; set; }

        public string GetQuery()
        {
            List<string> queries = new List<string>();

            if (!string.IsNullOrEmpty(SerialNumberFilter))
            {
                queries.Add($"x.SerialNumber.StartsWith(\"{ SerialNumberFilter }\")");
            }

            if (!string.IsNullOrEmpty(ModelFilter))
            {
                queries.Add($"x.Model.Name.StartsWith(\"{ ModelFilter }\")");
            }

            if (!string.IsNullOrEmpty(ClassificationFilter))
            {
                queries.Add($"x.Classification.Model.StartsWith(\"{ ClassificationFilter }\")");
            }

            if (!string.IsNullOrEmpty(LastUpdateFilter))
            {
                queries.Add($"x.LastUpdate.StartsWith(\"{LastUpdateFilter}\")");
            }

            if (!string.IsNullOrEmpty(EventsCountFilter))
            {
                queries.Add($"x.DeviceEvents.Count() >= { EventsCountFilter }");
            }

            string query = InitializeQuery;
            if (queries.Count > 0)
            {
                query += string.Join(" && ", queries);
            }

            return queries.Count == 0 ? string.Empty : query;
        }
    }
}

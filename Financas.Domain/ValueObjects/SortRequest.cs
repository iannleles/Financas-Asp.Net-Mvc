using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Financas.Domain.ValueObjects
{
    public class SortRequest
    {
        private string sort;
        public SortField[] SortMapping { get; private set; }

        public SortRequest(string sort)
        {
            this.sort = sort;

            List<SortField> orders = new List<SortField>();

            if (this.sort != null)
            {
                string[] sortAndDirection = this.sort.Split(',');
                List<string> sortDir = new List<string>();

                foreach (string item in sortAndDirection)
                {
                    string field = item.Replace("-", "").Replace("+", "");

                    //string field = string.Empty;
                    //TextInfo text = new CultureInfo("pt-BR", false).TextInfo;
                    //field = text.ToTitleCase(fieldParameter);

                    SortFieldDirection direction;

                    switch (item.Substring(0, 1))
                    {
                        case "-":
                            direction = SortFieldDirection.Descending;
                            break;
                        case "+":
                        default:
                            direction = SortFieldDirection.Ascending;
                            break;
                    }

                    orders.Add(new SortField(field, direction));
                }
            }

            SortMapping = orders.ToArray();
        }

        public string Fields
        {
            get
            {
                string sortFields = string.Empty;
                foreach (var sort in SortMapping.Select(i => new { i.Field, i.Direction }))
                {
                    string direction = "+";

                    if (sort.Direction == SortFieldDirection.Descending)
                    {
                        direction = "-";
                    }

                    sortFields += direction + sort.Field + ",";
                }
                return sortFields.Substring(0, sortFields.Length - 1);
            }
        }

        public bool IsValid<T>(out string invalidSort)
            where T : class
        {
            Type entityType = typeof(T);

            foreach (SortField item in SortMapping)
            {
                if (entityType.GetProperty(item.Field.Trim(), BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance) == null)
                {
                    invalidSort = item.Field + " não é uma ordenação possível.";
                    return false;
                }
            }
            invalidSort = string.Empty;
            return true;
        }
    }
}

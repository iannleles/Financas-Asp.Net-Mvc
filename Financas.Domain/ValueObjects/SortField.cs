using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financas.Domain.ValueObjects
{
    public class SortField
    {
        public string Field { get; set; }
        public SortFieldDirection Direction { get; set; }

        public SortField(string field, SortFieldDirection direction)
        {
            Field = field;
            Direction = direction;
        }

        public SortField(
            string field,
            string direction)
        {
            Field = field;

            direction = direction ?? "ASC";
            direction = direction.ToUpper();
            direction = direction == "ASC" || direction == "DESC" ? direction : "ASC";

            Direction = direction == "ASC" ? SortFieldDirection.Ascending
                                           : SortFieldDirection.Descending;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financas.Domain.ValueObjects
{
    public enum SortFieldDirection
    {
        [Description("ASC")]
        Ascending,
        [Description("DESC")]
        Descending
    }

    public static class SortFieldExtensions
    {
        public static string GetDescription(this SortFieldDirection value)
        {
            DescriptionAttribute attribute = value.GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .SingleOrDefault() as DescriptionAttribute;
            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}

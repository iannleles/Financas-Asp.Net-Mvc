using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financas.Domain.Common.Extensions
{
    public static class BooleanExtensions
    {
        public static bool? ParseFlag(this string value)
        {
            switch (value)
            {
                case "S":
                    return true;
                case "N":
                    return false;
                default:
                    return null;
            }
        }

        public static string ParseFlag(this bool? flag)
        {
            switch (flag)
            {
                case true:
                    return "S";
                case false:
                    return "N";
                default:
                    return string.Empty;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financas.Infrastructure.Data.Extensions
{
    public static class FormatExtensions
    {
        private static string Format(string value, string mask)
        {
            StringBuilder inputData = new StringBuilder();

            // remove caracteres nao numericos
            foreach (char c in value)
            {
                if (Char.IsNumber(c))
                    inputData.Append(c);
            }

            int indMask = mask.Length;
            int indField = inputData.Length;

            for (; indField > 0 && indMask > 0; )
            {
                if (mask[--indMask] == '#')
                    indField--;
            }

            StringBuilder output = new StringBuilder();
            for (; indMask < mask.Length; indMask++)
                output.Append((mask[indMask] == '#') ? inputData[indField++] : mask[indMask]);

            return output.ToString();
        }


        public static string FormatDate(this DateTime date)
        {
            return date == DateTime.MinValue ? string.Empty : ((DateTime)date).ToString("dd/MM/yyyy");
        }

        public static string FormatDate(this DateTime? date)
        {
            return date == null ? string.Empty : ((DateTime)date).FormatDate();
        }




        public static string FormatDateAndHour(this DateTime date)
        {
            return date == null ? string.Empty : (date == DateTime.MinValue ? string.Empty : ((DateTime)date).ToString("dd/MM/yyyy HH:mm"));
        }

        public static string FormatDateAndHour(this DateTime? date)
        {
            return date == null ? string.Empty : ((DateTime)date).FormatDateAndHour();
        }




        public static string FormatHour(this DateTime date)
        {
            return date == null ? string.Empty : (date == DateTime.MinValue ? string.Empty : ((DateTime)date).ToString("HH:mm"));
        }

        public static string FormatHour(this DateTime? date)
        {
            return date == null ? string.Empty : ((DateTime)date).FormatHour();
        }




        public static string FormatDateAndHourWithSeconds(this DateTime date)
        {
            return date == null ? string.Empty : (date == DateTime.MinValue ? string.Empty : ((DateTime)date).ToString("dd/MM/yyyy HH:mm:ss"));
        }

        public static string FormatDateAndHourWithSeconds(this DateTime? date)
        {
            return date == null ? string.Empty : ((DateTime)date).FormatDateAndHourWithSeconds();
        }




        public static string FormatDateAndHourWithMiliseconds(this DateTime date)
        {
            return date == null ? string.Empty : (date == DateTime.MinValue ? string.Empty : ((DateTime)date).ToString("dd/MM/yyyy HH:mm:ss:fff"));
        }

        public static string FormatDateAndHourWithMiliseconds(this DateTime? date)
        {
            return date == null ? string.Empty : ((DateTime)date).FormatDateAndHourWithMiliseconds();
        }




        public static string FormatValue(this decimal value)
        {
            return string.Format("{0:N}", value);
        }

        public static string FormatValue(this decimal? value)
        {
            return value == null ? string.Empty : string.Format("{0:N}", value);
        }



        public static string FormatValue(this decimal value, byte digits)
        {
            return string.Format("{0:N" + digits + "}", value);
        }

        public static string FormatValue(this decimal? value, byte digits)
        {
            return value == null ? string.Empty : string.Format("{0:N" + digits + "}", value);
        }



        public static string FormatCurrency(this decimal value)
        {
            return string.Format("{0:C}", value);
        }

        public static string FormatCurrency(this decimal? value)
        {
            return value == null ? string.Empty : string.Format("{0:C}", value);
        }


        public static string FormatCurrency(this decimal value, byte digits)
        {
            return string.Format("{0:C" + digits + "}", value);
        }

        public static string FormatCurrency(this decimal? value, byte digits)
        {
            return value == null ? string.Empty : string.Format("{0:C" + digits + "}", value);
        }



        public static string FormatZipCode(this string zipCode)
        {
            return Format(zipCode, "######.###");
        }

        public static string FormatGenre(this string genre)
        {
            return genre == "M" ? "Masculino" : "Feminino";
        }

        public static string FormatCPF(this string cpf)
        {
            if (cpf == null) return cpf;

            return Format(cpf, "###.###.###-##");
        }

        public static string FormatCNPJ(this string cnpj)
        {
            if (cnpj == null) return cnpj;

            return Format(cnpj, "##.###.###/####-##");
        }

        public static string FormatPhone(this string phone)
        {
            if (phone != null)
            {
                if (phone.Length == 9)
                {
                    return Format(phone, "#####.####");
                }
                else if (phone.Length <= 8)
                {
                    return Format(phone, "####.####");
                }
                else
                {
                    return Format(phone, "########.####");
                }
            }
            return string.Empty;
        }

        public static string FormatPhoneWithAreaCode(this string phone, string areaCode)
        {
            string formatPhone = string.Empty;

            if (phone != null)
            {
                if (phone.Length == 9)
                {
                    formatPhone = areaCode + "." + Format(phone, "#####.####");
                }
                else if (phone.Length <= 8)
                {
                    formatPhone = areaCode + "." + Format(phone, "####.####");
                }
                else
                {
                    formatPhone = areaCode + "." + Format(phone, "########.####");
                }
            }
            return formatPhone;
        }
    }
}

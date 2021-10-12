using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class ClassCompareExtension
    {
        public static List<CompareModel> Compare<T>(this T classA, T classB)
        {
            var dontCompare = new List<string> { "NormalizedUserName", "NormalizedEmail", "EmailConfirmed", "PasswordHash", "SecurityStamp", "ConcurrencyStamp", "PhoneNumberConfirmed", "LockoutEnabled" };
            if (classA != null && classB != null)
            {
                var type = typeof(T);
                var allProperties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                var allSimpleProperties = allProperties.Where(p => p.PropertyType.IsSimpleType());
                var unequalProperties =
                       from p in allSimpleProperties
                       let AValue = type.GetProperty(p.Name).GetValue(classA, null)
                       let BValue = type.GetProperty(p.Name).GetValue(classB, null)
                       where AValue != BValue && (AValue == null || !AValue.Equals(BValue)) && !dontCompare.Equals(p.Name)
                       select new CompareModel
                       {
                           Column = p.Name,
                           OldValue = AValue != null ? AValue.ToString() : "",
                           NewValue = BValue != null ? BValue.ToString() : ""
                       };
                return unequalProperties.ToList();
            }
            else
            {
                return null;
            }
        }

        public static Dictionary<string, string> GetColumnNames<T>(this T classA)
        {
            var type = typeof(T);
            var allProperties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var result = allProperties.Select(x => new
            {
                x.Name,
                RussName = type.GetProperty(x.Name).GetCustomAttributes<DisplayAttribute>().FirstOrDefault()
            }).ToDictionary(x => x.Name, y => y.RussName?.Name);

            return result;
        }

        public static bool IsSimpleType(this Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                // nullable type, check if the nested type is simple.
                return type.GetGenericArguments()[0].IsSimpleType();
            }
            return type.IsPrimitive
              || type.IsEnum
              || type.Equals(typeof(string))
              || type.Equals(typeof(decimal))
              || type.Equals(typeof(int))
              || type.Equals(typeof(bool));
            //|| type.Equals(typeof(DateTime));
        }

        public class CompareModel
        {
            public string Column { get; set; }
            public string OldValue { get; set; }
            public string NewValue { get; set; }
        }
    }
}

using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace POEItemFilter.Library.Enumerables
{
    public static class EnumToString
    {
        public static string BaseTypeToString(this BaseTypes enumValue)
        {
            try
            {
                return enumValue.GetType()
                    .GetMember(enumValue.ToString())
                    .First()
                    .GetCustomAttribute<DisplayAttribute>()
                    .Name;
            }
            catch (System.Exception)
            {
                return enumValue.ToString();
            }
        }

        public static string TypeToString(this Types enumValue)
        {
            try
            {
                return enumValue.GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>()
                .GetName();
            }
            catch (System.Exception)
            {
                return enumValue.ToString();
            }
        }
    }
}
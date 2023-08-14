using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Timetable.RazorWeb.Extentions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            FieldInfo field = enumValue.GetType().GetField(enumValue.ToString());

            DisplayAttribute attribute = field.GetCustomAttribute<DisplayAttribute>();

            return attribute != null ? attribute.Name : enumValue.ToString();
        }
    }
}

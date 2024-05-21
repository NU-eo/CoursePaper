using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace OnlineCourses.Domain.Enum
{
    public static class EnumExtensions
    {
        public static string GetDisplayName<T>(this T enumValue)
        {
            return enumValue.GetType().GetMember(enumValue.ToString())
                            .First().GetCustomAttribute<DisplayAttribute>()
                            .GetName();
        }
    }
}

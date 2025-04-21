using System.Reflection;

namespace ToDoListApp.Extensions;

public static class StringExtensions
{
    public static T GetValue<T>(this string s)
    {
        MethodInfo mi = typeof(T).GetMethod("Parse", new Type[] { typeof(string) });
        if (s == null)
        {
            throw new ArgumentNullException(nameof(s));
        }
        else if (mi != null)
        {
            return (T)mi.Invoke(typeof(T), new object[] { s });
        }
        else if (typeof(T).IsEnum)
        {
            if (Enum.TryParse(typeof(T), s, out object ev))
                return (T)(object)ev;
            else
                throw new ArgumentException($"{s} is not a valid member of {typeof(T).Name}");
        }
        else
        {
                throw new ArgumentException($"No conversion supported for {typeof(T).Name}");
        }
    }

    public static string ToPascalCase(this string value,params char[] chars)
    {
        if (string.IsNullOrEmpty(value))
            return string.Empty;
        if(value.Length == 1)
            return value.ToUpperInvariant();

        if(!value.Intersect(chars).Any())
            return char.ToUpperInvariant(value[0]) + value[1..];

        return string.Join(string.Empty, value
                                .Split(chars)
                                .Select(x => x.ToPascalCase()));
    }

}

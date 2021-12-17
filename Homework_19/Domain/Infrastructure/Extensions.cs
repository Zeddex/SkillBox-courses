using System;

namespace Homework_19.Infrastructure
{
    public static class StringExtensions
    {
        public static T ParseEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static string ClientNameParse(string name)
        {
            return name.TrimStart('[').Split(',')[0];
        }
    }
}

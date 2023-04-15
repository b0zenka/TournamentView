using System;
using System.ComponentModel;

public static class ObjectExtrensions
{
    public static T Convert<T>(this string input)
    {
        try
        {
            var converter = TypeDescriptor.GetConverter(typeof(T));
            if (converter == null)
                return default;

            return (T)converter.ConvertFrom(input);
        }
        catch (NotSupportedException)
        {
            return default;
        }
    }
}

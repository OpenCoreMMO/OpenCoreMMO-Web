﻿using System.Text;

namespace OCM.Infrastructure.Extensions;

public static class StringExtensions
{
    public static string RemoveEntitySuffix(this string value)
    {
        return value.Replace("Entity", string.Empty);
    }

    public static string ToSnakeCase(this string value)
    {
        if (string.IsNullOrEmpty(value))
            return value;

        StringBuilder stringBuilder = new();
        for (var i = 0; i < value.Length; i++)
        {
            var c = value[i];
            if (char.IsUpper(c))
            {
                if (i > 0 && (char.IsLower(value[i - 1]) || char.IsDigit(value[i - 1]))) stringBuilder.Append('_');
                stringBuilder.Append(char.ToLower(c));
            }
            else
            {
                stringBuilder.Append(c);
            }
        }

        return stringBuilder.ToString();
    }
}
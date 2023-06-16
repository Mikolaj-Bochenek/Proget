using System.Reflection;

namespace Proget.Messaging;

public class ConventionsBuilder<TAttribute> where TAttribute : Attribute
{
    protected virtual TAttribute? GetAttribute(MemberInfo type)
        => type.GetCustomAttribute<TAttribute>();
    
    protected virtual string WithSnakeCasing(string? @string) => SnakeCase(@string);

    private static string SnakeCase(string? value)
        => string.Concat((value ?? string.Empty).Select((x, i) =>
                i > 0 && (value ?? string.Empty)[i - 1] != '.' && (value ?? string.Empty)[i - 1] != '/' && char.IsUpper(x) ? "_" + x : x.ToString()))
            .ToLowerInvariant();
}


namespace Proget.ORM;

public static class Extensions
{
    private const string Section = "orm";

    public static IProgetBuilder AddStorage(this IProgetBuilder builder, Action<IStorageConfigurator> configure, string section = Section)
    {
        if (string.IsNullOrWhiteSpace(section))
        {
            section = Section;
        }

        var options = builder.GetOptions<StorageOptions>(section);
        builder.Services.AddSingleton(options);

        var configurator = new StorageConfigurator(builder, options);
        configure(configurator);

        return builder;
    }

    public static Expression<Func<T, object>> ToLambda<T>(string propertyName)
    {
        var parameter = Expression.Parameter(typeof(T));
        var property = Expression.Property(parameter, propertyName);
        var propAsObject = Expression.Convert(property, typeof(object));

        return Expression.Lambda<Func<T, object>>(propAsObject, parameter);
    }
}

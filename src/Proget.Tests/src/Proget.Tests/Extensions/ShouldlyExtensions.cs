using Shouldly;

namespace Proget.Tests.Extensions;
public static class ShouldlyExtensions
{
    public static void ShouldBeEquivalentTo<T>(this IEnumerable<T> collection, IEnumerable<T> equivalent)
        => collection.ShouldAllBe(element => equivalent.Contains(element));

    public static void ShouldNotBeEquivalentTo<T>(this IEnumerable<T> collection, IEnumerable<T> equivalent)
        => collection.ShouldAllBe(element => !equivalent.Contains(element));
}

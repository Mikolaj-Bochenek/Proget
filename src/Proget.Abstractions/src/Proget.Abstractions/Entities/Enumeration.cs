using System.Reflection;

namespace Proget.Abstractions.Entities
{
    /// <remarks> 
    /// Card type class should be marked as abstract with protected constructor to encapsulate known enum types
    /// this is currently not possible as OrderingContextSeed uses this constructor to load cardTypes from csv file
    /// </remarks>
    public abstract class Enumeration : IComparable
    {
        public string Name { get; }
        public int Id { get; }
        protected internal Enumeration(int id, string name) => (Id, Name) = (id, name);

        public override string ToString() => Name;

        public static IEnumerable<T> GetAll<T>() where T : Enumeration
            => typeof(T).GetFields(BindingFlags.Public
                | BindingFlags.Static
                | BindingFlags.DeclaredOnly)
                    .Select(f => f.GetValue(null))
                    .Cast<T>();

        public override bool Equals(object? obj)
        {
            if (obj is not Enumeration otherValue)
                return false;

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode() => Id.GetHashCode();

        public int CompareTo(object? other) => Id.CompareTo(((Enumeration)other!).Id);
    }
}
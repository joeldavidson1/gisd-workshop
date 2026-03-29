namespace Gisd.Models.Common;

public abstract record Option<T>;
public record Some<T>(T Value) : Option<T>;
public record None<T>() : Option<T>;

public static class Optional
{
    extension<T>(T value)
    {
        public Option<T> AsOption() =>
            new Some<T>(value);
        
        public static None<T> None() =>
            new None<T>();
    }

    extension<T>(Option<T> option)
    {
        public Option<U> Bind<U>(Func<T, Option<U>> f) => option switch
        {
            Some<T> some => f(some.Value),
            _ => new None<U>()
        };

        public Option<U> Map<U>(Func<T, U> f) => option switch
        {
            Some<T> some => new Some<U>(f(some.Value)),
            _ => new None<U>()
        };

        public R Match<R>(Func<T, R> onSome, Func<R> onNone) => option switch
        {
            Some<T> some => onSome(some.Value),
            _ => onNone()
        };

        public T OrElse(T @default) => option switch
        {
            Some<T> some => some.Value,
            _ => @default
        };

        public T OrElse(Func<T> defaultFactory) => option switch
        {
            Some<T> some => some.Value,
            _ => defaultFactory()
        };

        public Option<T> When(Func<T, bool> predicate) => option switch
        {
            Some<T> some when predicate(some.Value) => some,
            _ => new None<T>()
        };

        public Option<T> Assert(Func<T, bool> predicate) => option switch
        {
            Some<T> some when predicate(some.Value) => option,
            Some<T> _ => throw new ArgumentException("Option value does not satisfy the assertion"),
            _ => option
        };
    }

    extension<T>(IEnumerable<T> enumerable)
    {
        public Option<T> FirstOrNone()
        {
            using var enumerator = enumerable.GetEnumerator();
            if (!enumerator.MoveNext()) return new None<T>();
            return new Some<T>(enumerator.Current);
        }
    }
}
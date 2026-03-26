namespace Gisd.Models.Common;

public abstract record Option<T>;
public record Some<T>(T Value) : Option<T>;
public record None<T>() : Option<T>;

// RULE #8 - USE MONADS TO CAPTURE RESPONSIBILITIES (E.G. EXISTENCE CHECK)
public static class Optional
{
    extension<T>(T value)
    {
        public Option<T> AsSome() =>
            new Some<T>(value);
        
        public static None<T> None() =>
            new None<T>();
    }

    extension<T>(Option<T> option)
    {
        public Option<U> Bind<U>(Func<T, Option<U>> f) => option switch
        {
            T some => f(some),
            _ => new None<U>()
        };

        public Option<U> Map<U>(Func<T, U> f) => option switch
        {
            T some => new Some<U>(f(some)),
            _ => new None<U>()
        };

        public R Match<R>(Func<T, R> onSome, Func<R> onNone) => option switch
        {
            T some => onSome(some),
            _ => onNone()
        };

        public T OrElse(T @default) => option switch
        {
            T some => some,
            _ => @default
        };

        public Option<T> When(Func<T, bool> predicate) => option switch
        {
            T some when predicate(some) => new Some<T>(some),
            _ => new None<T>()
        };

        public Option<T> Assert(Func<T, bool> predicate) => option switch
        {
            T some when predicate(some) => option,
            T _ => throw new ArgumentException("Option value does not satisfy the assertion"),
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
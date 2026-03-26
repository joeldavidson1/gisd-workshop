namespace Gisd.Models.Common;

public static class NullableMonad
{
    extension<T>(T value) where T : class
    {
        public T? NullableUnit() =>
            value;
    }

    extension<T>(T? monad) where T : class
    {
        public U? BindNullable<U>(Func<T, U?> f) where U : class =>
            monad is null ? null : f(monad);
        
        public U? MapNullable<U>(Func<T, U> f) where U : class =>
            monad is null ? null : f(monad);
        
        public R MatchNullable<R>(Func<T, R> onValue, Func<R> onNull) =>
            monad is null ? onNull() : onValue(monad);
    }
}
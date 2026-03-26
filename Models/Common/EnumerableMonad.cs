namespace Gisd.Models.Common;

public static class EnumerableMonad
{
    extension<T>(T value)
    {
        public IEnumerable<T> Unit() =>
            new[] { value };
    }

    extension<T>(IEnumerable<T> monad)
    {
        public IEnumerable<U> Bind<U>(Func<T, IEnumerable<U>> f) =>
            monad.SelectMany(f);
        
        public IEnumerable<U> Map<U>(Func<T, U> f) =>
            monad.Select(f);
    }

    extension(IEnumerable<string> monad)
    {
        public string Join(string separator) =>
            string.Join(separator, monad);
    }
}
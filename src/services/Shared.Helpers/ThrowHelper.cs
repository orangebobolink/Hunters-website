namespace Shared.Helpers
{
    public static class ThrowHelper
    {
        public static void ThrowException(string message)
            => throw new Exception(message);

        public static void ThrowKeyNotFoundException(string message)
            => throw new KeyNotFoundException(message);

        public static void ThrowInvalidOperationException(string message)
            => throw new InvalidOperationException(message);
    }
}

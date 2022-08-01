namespace BrowserCookieReader.Exceptions
{

    [Serializable]
    public class CookieExpiredException : Exception
    {
        public CookieExpiredException() { }
        public CookieExpiredException(string message) : base(message) { }
        public CookieExpiredException(string message, Exception inner) : base(message, inner) { }
        protected CookieExpiredException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}

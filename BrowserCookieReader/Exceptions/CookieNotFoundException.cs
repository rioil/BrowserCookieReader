namespace BrowserCookieReader.Exceptions
{

    [Serializable]
    public class CookieNotFoundException : Exception
    {
        public CookieNotFoundException() { }
        public CookieNotFoundException(string message) : base(message) { }
        public CookieNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected CookieNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}

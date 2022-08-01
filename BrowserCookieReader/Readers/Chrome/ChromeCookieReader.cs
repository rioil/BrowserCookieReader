using BrowserCookieReader.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace BrowserCookieReader.Readers.Chrome
{
    public class ChromeCookieReader : IBrowserCookieReader
    {
        private readonly ChromeCookiesContext _context;

        public ChromeCookieReader()
        {
            _context = new ChromeCookiesContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public string ReadValue(string name, string host, string path = "/")
        {
            var cookies = _context.Cookies.Where(c => c.Name == name && c.Host == host && c.Path == path).ToArray();
            if (!cookies.Any()) { throw new CookieNotFoundException(); }

            var validCookie = cookies.FirstOrDefault(c => !c.HasExpires || c.Expires > DateTimeOffset.UtcNow);
            if (validCookie is null) { throw new CookieExpiredException(); }

            return validCookie.Value;
        }

        public bool TryReadValue([NotNullWhen(true)] out string? value, string name, string host, string path = "/")
        {
            var cookies = _context.Cookies.Where(c => c.Name == name && c.Host == host && c.Path == path).ToArray();
            if (!cookies.Any())
            {
                value = null;
                return false;
            }

            var validCookie = cookies.FirstOrDefault(c => !c.HasExpires || c.Expires > DateTimeOffset.UtcNow);
            if (validCookie is null)
            {
                value = null;
                return false;
            }

            value = validCookie.Value;
            return true;
        }
    }
}

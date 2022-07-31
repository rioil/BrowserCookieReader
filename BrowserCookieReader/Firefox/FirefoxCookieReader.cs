using System.Diagnostics.CodeAnalysis;

namespace BrowserCookieReader.Firefox
{
    public sealed class FirefoxCookieReader : IBrowserCookieReader
    {
        private readonly FirefoxCookiesContext _context;

        public FirefoxCookieReader()
        {
            _context = new FirefoxCookiesContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public string GetValue(string name, string host, string path = "/")
        {
            var cookie = _context.Cookies.FirstOrDefault(c => c.Name == name && c.Host == host && c.Path == path);
            if (cookie is null) { throw new KeyNotFoundException(); }

            return cookie.Value;
        }

        public bool TryGetValue([NotNullWhen(true)] out string? value, string name, string host, string path = "/")
        {
            var cookie = _context.Cookies.FirstOrDefault(c => c.Name == name && c.Host == host && c.Path == path);
            if (cookie is null)
            {
                value = null;
                return false;
            }

            value = cookie.Value;
            return true;
        }
    }
}
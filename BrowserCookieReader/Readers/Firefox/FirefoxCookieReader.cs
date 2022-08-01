using BrowserCookieReader.Exceptions;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace BrowserCookieReader.Readers.Firefox
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

        public string ReadValue(string name, string host, string path = "/")
        {
            var cookies = _context.Cookies.Where(c => c.Name == name && c.Host == host && c.Path == path).ToArray();
            if (!cookies.Any()) { throw new CookieNotFoundException(); }

            var validCookie = cookies.FirstOrDefault(c => c.Expires > DateTimeOffset.UtcNow);
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

            var validCookie = cookies.FirstOrDefault(c => c.Expires > DateTimeOffset.UtcNow);
            if (validCookie is null)
            {
                value = null;
                return false;
            }

            value = validCookie.Value;
            return true;
        }

        public Cookie Read(string name, string host, string path = "/")
        {
            var cookie = _context.Cookies.FirstOrDefault(c => c.Name == name && c.Host == host && c.Path == path);
            if (cookie is null) { throw new CookieNotFoundException(); }

            return new Cookie()
            {
                Name = cookie.Name,
                Value = cookie.Value,
                Path = cookie.Path,
                Domain = cookie.Host,
                Expires = cookie.Expires.LocalDateTime,
                HttpOnly = cookie.IsHttpOnly,
                Secure = cookie.IsSecure,
            };
        }

        public bool TryRead([NotNullWhen(true)] out Cookie? value, string name, string host, string path = "/")
        {
            var cookie = _context.Cookies.FirstOrDefault(c => c.Name == name && c.Host == host && c.Path == path);
            if (cookie is null)
            {
                value = null;
                return false;
            }

            value = new Cookie()
            {
                Name = cookie.Name,
                Value = cookie.Value,
                Path = cookie.Path,
                Domain = cookie.Host,
                Expires = cookie.Expires.LocalDateTime,
                HttpOnly = cookie.IsHttpOnly,
                Secure = cookie.IsSecure,
            };
            return true;
        }
    }
}
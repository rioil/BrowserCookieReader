using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserCookieReader.Chrome
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

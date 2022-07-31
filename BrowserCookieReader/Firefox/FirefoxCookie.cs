using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable disable

namespace BrowserCookieReader.Firefox
{
    internal class FirefoxCookie : ICookie
    {
        public int Id { get; }
        public string OriginAttributes { get; }
        public string Name { get; }
        public string Value { get; }
        public string Host { get; }
        public string Path { get; }
        public long Expiry { get; }
        public long LastAccessed { get; }
        public long CreationTime { get; }
        public bool IsSecure { get; }
        public bool IsHttpOnly { get; }
        public bool InBrowserElement { get; }
        public bool IsSameSite { get; }
        public bool RawSameSite { get; }
        public int SchemeMap { get; }
    }
}

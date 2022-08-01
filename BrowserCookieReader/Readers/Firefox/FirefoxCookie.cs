#nullable disable

using BrowserCookieReader.Readers;

namespace BrowserCookieReader.Readers.Firefox
{
    internal class FirefoxCookie : ICookie
    {
        public int Id { get; }

        public string Name { get; }
        public string Value { get; }
        public string Host { get; }
        public string Path { get; }
        public DateTimeOffset Expires => DateTimeOffset.FromUnixTimeSeconds(Expiry);
        public DateTimeOffset LastAccessed => DateTimeOffset.FromUnixTimeSeconds(RawLastAccessed);
        public DateTimeOffset CreationTime => DateTimeOffset.FromUnixTimeSeconds(RawCreationTime);
        public bool IsSecure { get; }
        public bool IsHttpOnly { get; }
        public bool IsSameSite { get; }

        public string OriginAttributes { get; }
        public long Expiry { get; }
        public long RawLastAccessed { get; }
        public long RawCreationTime { get; }
        public bool InBrowserElement { get; }
        public bool RawSameSite { get; }
        public int SchemeMap { get; }
    }
}

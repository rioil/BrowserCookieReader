#nullable disable

using BrowserCookieReader.Readers;

namespace BrowserCookieReader.Readers.Chrome
{
    public class ChromeCookie : ICookie
    {
        public string Name { get; }
        public string Value => DecryptedValue;
        public string Host { get; }
        public string Path { get; } = null!;
        public DateTimeOffset Expires => ConvertToDateTimeOffset(ExpiresUtc);
        public DateTimeOffset LastAccessed => ConvertToDateTimeOffset(LastAccessUtc);
        public DateTimeOffset CreationTime => ConvertToDateTimeOffset(CreationUtc);
        public bool IsSecure { get; }
        public bool IsHttpOnly { get; }
        public bool IsSameSite { get; }

        public long CreationUtc { get; }
        public string TopFrameSiteKey { get; }
        public byte[] EncryptedValue { get; }
        public long ExpiresUtc { get; }
        public long LastAccessUtc { get; }
        public bool HasExpires { get; }
        public long IsPersistent { get; }
        public long Priority { get; }
        public long SourceScheme { get; }
        public long SourcePort { get; }
        public long IsSameParty { get; }
        public long LastUpdateUtc { get; }

        private string DecryptedValue => _decryptedValue ??= CookieDecryptionUtility.DecryptValue(EncryptedValue);
        private string _decryptedValue;

        private static DateTimeOffset ConvertToDateTimeOffset(long chromeUtcTime)
        {
            // timestamp of Chrome is microseconds since 1601-01-01T00:00:00Z
            return DateTimeOffset.FromUnixTimeSeconds(chromeUtcTime / 1000000 - 11644473600);
        }
    }
}

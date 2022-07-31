#nullable disable

namespace BrowserCookieReader.Chrome
{
    public class ChromeCookie : ICookie
    {
        public long CreationTime { get; }
        public string Host { get; }
        public string TopFrameSiteKey { get; }
        public string Name { get; }
        public string Value => _value ?? DecryptedValue;
        private string _value;

        private string DecryptedValue => _decryptedValue ??= CookieDecryptionUtility.DecryptValue(EncryptedValue);
        private string _decryptedValue;

        public byte[] EncryptedValue { get; }
        public string Path { get; } = null!;
        public long Expiry { get; }
        public bool IsSecure { get; }
        public bool IsHttpOnly { get; }
        public long LastAccessed { get; }
        public long HasExpires { get; }
        public long IsPersistent { get; }
        public long Priority { get; }
        public bool IsSameSite { get; }
        public long SourceScheme { get; }
        public long SourcePort { get; }
        public long IsSameParty { get; }
        public long LastUpdateUtc { get; }
    }
}

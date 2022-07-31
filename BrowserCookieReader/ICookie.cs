namespace BrowserCookieReader
{
    internal interface ICookie
    {
        public string Name { get; }
        public string Value { get; }
        public string Host { get; }
        public string Path { get; }
        public long Expiry { get; }
        public long LastAccessed { get; }
        public long CreationTime { get; }
        public bool IsSecure { get; }
        public bool IsHttpOnly { get; }
        public bool IsSameSite { get; }
    }
}

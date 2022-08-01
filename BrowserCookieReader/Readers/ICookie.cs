namespace BrowserCookieReader.Readers
{
    internal interface ICookie
    {
        public string Name { get; }
        public string Value { get; }
        public string Host { get; }
        public string Path { get; }
        public DateTimeOffset Expires { get; }
        public DateTimeOffset LastAccessed { get; }
        public DateTimeOffset CreationTime { get; }
        public bool IsSecure { get; }
        public bool IsHttpOnly { get; }
        public bool IsSameSite { get; }
    }
}

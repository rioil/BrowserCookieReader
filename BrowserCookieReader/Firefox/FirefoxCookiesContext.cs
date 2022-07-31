using Microsoft.EntityFrameworkCore;

namespace BrowserCookieReader.Firefox
{
    internal class FirefoxCookiesContext : DbContext
    {
        public FirefoxCookiesContext() { }

        public FirefoxCookiesContext(DbContextOptions<FirefoxCookiesContext> options) : base(options) { }

        public DbSet<FirefoxCookie> Cookies { get; set; } = default!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite($"DataSource={GetDbPath()}");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FirefoxCookie>(entity =>
            {
                entity.ToTable("moz_cookies");

                entity.HasIndex(e => new { e.Name, e.Host, e.Path, e.OriginAttributes }, "IX_moz_cookies_name_host_path_originAttributes")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CreationTime).HasColumnName("creationTime");

                entity.Property(e => e.Expiry).HasColumnName("expiry");

                entity.Property(e => e.Host).HasColumnName("host");

                entity.Property(e => e.InBrowserElement)
                    .HasColumnName("inBrowserElement")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.IsHttpOnly).HasColumnName("isHttpOnly");

                entity.Property(e => e.IsSecure).HasColumnName("isSecure");

                entity.Property(e => e.LastAccessed).HasColumnName("lastAccessed");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.OriginAttributes)
                    .HasColumnName("originAttributes")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Path).HasColumnName("path");

                entity.Property(e => e.RawSameSite)
                    .HasColumnName("rawSameSite")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.IsSameSite)
                    .HasColumnName("sameSite")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.SchemeMap)
                    .HasColumnName("schemeMap")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Value).HasColumnName("value");
            });
        }

        private static string GetDbPath()
        {
            var profilesDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Mozilla", "Firefox", "Profiles");

            var enumarationOptions = new EnumerationOptions { RecurseSubdirectories = true, MaxRecursionDepth = 2 };
            var cookieDbPath = Directory.EnumerateFiles(profilesDir, "cookies.sqlite", enumarationOptions)
                .FirstOrDefault(path => Path.GetDirectoryName(path)!.EndsWith(".default-release"));

            if (cookieDbPath is null)
            {
                throw new FileNotFoundException("Can not find a cookie DB file of Firefox.");
            }

            return cookieDbPath;
        }
    }
}

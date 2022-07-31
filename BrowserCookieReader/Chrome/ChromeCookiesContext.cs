using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserCookieReader.Chrome
{
    internal class ChromeCookiesContext : DbContext
    {
        public ChromeCookiesContext() { }

        public ChromeCookiesContext(DbContextOptions<ChromeCookiesContext> options) : base(options) { }

        public DbSet<ChromeCookie> Cookies { get; set; } = default!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite($"DataSource={GetDbPath()}");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChromeCookie>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("cookies");

                entity.HasIndex(e => new { e.Host, e.TopFrameSiteKey, e.Name, e.Path }, "cookies_unique_index")
                    .IsUnique();

                entity.Property(e => e.CreationTime).HasColumnName("creation_utc");

                entity.Property(e => e.EncryptedValue).HasColumnName("encrypted_value");

                entity.Property(e => e.Expiry).HasColumnName("expires_utc");

                entity.Property(e => e.HasExpires).HasColumnName("has_expires");

                entity.Property(e => e.Host).HasColumnName("host_key");

                entity.Property(e => e.IsHttpOnly).HasColumnName("is_httponly");

                entity.Property(e => e.IsPersistent).HasColumnName("is_persistent");

                entity.Property(e => e.IsSameParty).HasColumnName("is_same_party");

                entity.Property(e => e.IsSecure).HasColumnName("is_secure");

                entity.Property(e => e.LastAccessed).HasColumnName("last_access_utc");

                entity.Property(e => e.LastUpdateUtc).HasColumnName("last_update_utc");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Path).HasColumnName("path");

                entity.Property(e => e.Priority).HasColumnName("priority");

                entity.Property(e => e.IsSameSite).HasColumnName("samesite");

                entity.Property(e => e.SourcePort).HasColumnName("source_port");

                entity.Property(e => e.SourceScheme).HasColumnName("source_scheme");

                entity.Property(e => e.TopFrameSiteKey).HasColumnName("top_frame_site_key");

                entity.Property(e => e.Value).HasColumnName("value");
            });
        }

        private static string GetDbPath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "AppData", "Local", "Google", "Chrome", "User Data", "Default", "Network", "Cookies");
        }
    }
}

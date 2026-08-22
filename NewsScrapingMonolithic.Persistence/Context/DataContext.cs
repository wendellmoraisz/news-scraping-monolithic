using Microsoft.EntityFrameworkCore;
using NewsScrapingMonolithic.Domain.Entities;

namespace NewsScrapingMonolithic.Persistence.Context;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Email>(entity =>
        {
            entity.Property(email => email.Address)
                .HasMaxLength(254)
                .IsRequired();

            entity.HasIndex(email => email.Address)
                .IsUnique();
        });

        modelBuilder.Entity<NewsPage>(entity =>
        {
            entity.Property(host => host.Url)
                .IsRequired();

            entity.HasIndex(host => host.Url)
                .IsUnique();
        });
    }

    public DbSet<Email> Emails { get; set; }
    public DbSet<News> News { get; set; }
    public DbSet<NewsPage> NewsPages { get; set; }
}
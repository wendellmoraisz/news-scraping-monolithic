using Microsoft.EntityFrameworkCore;
using NewsScrapingMonolithic.Domain.Entities;

namespace NewsScrapingMonolithic.Persistence.Context;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    
    public DbSet<Email> Emails { get; set; }
    public DbSet<News> News { get; set; }
}
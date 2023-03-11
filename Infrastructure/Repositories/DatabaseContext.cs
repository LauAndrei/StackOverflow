using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    private DbSet<Tag> Tags { get; set; }
}
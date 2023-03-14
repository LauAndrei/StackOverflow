using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class DatabaseContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    private DbSet<Tag> Tags { get; set; }
    private DbSet<User> Users { get; set; }
    private DbSet<Question> Questions { get; set; }
    private DbSet<Answer> Answers { get; set; }
    public DbSet<Vote> Votes { get; set; }
    public DbSet<QuestionTag> QuestionTags { get; set; }
}
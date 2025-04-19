using BasicMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace BasicMVC.Context
{
    public class TutorialDbContext:DbContext
    {
        public TutorialDbContext(DbContextOptions<TutorialDbContext> options) : base(options) { }   

        public DbSet<Tutorial> Tutorials { get; set; }
        public DbSet<Article> Articles { get; set; }

        // seed database tables with initial data 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>().HasData(
                new Article
                {
                    ArticleId = 1,
                    ArticleTitle = "Introduction to C#",
                    ArticleContent = "C# is an Object oriented language",
                    TutorialId = 1
                },
                    new Article
                    {
                        ArticleId = 2,
                        ArticleTitle = "Introduction to EFCore",
                        ArticleContent = "EFCore used for DB operations",
                        TutorialId = 1
                    }
            );
        }
    }
}

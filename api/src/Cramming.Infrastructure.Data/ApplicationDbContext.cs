using Cramming.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using EntityFrameworkContext = Microsoft.EntityFrameworkCore.DbContext;

namespace Cramming.Infrastructure.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : EntityFrameworkContext(options)
    {
        public DbSet<TopicData> Topics { get; set; }

        public DbSet<TopicTagData> TopicTags { get; set; }

        public DbSet<TopicQuestionData> TopicQuestions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}

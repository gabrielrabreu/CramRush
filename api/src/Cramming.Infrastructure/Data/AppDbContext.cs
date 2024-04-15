﻿using Cramming.Domain.QuizAttemptAggregate;
using Cramming.Domain.StaticQuizAggregate;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Cramming.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<StaticQuiz> StaticQuizzes => Set<StaticQuiz>();

        public DbSet<QuizAttempt> QuizAttempts => Set<QuizAttempt>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}

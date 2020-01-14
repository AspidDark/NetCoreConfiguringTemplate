using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TB.Domain;

/// <summary>
/// Nuget :
/// Microsoft.EntityFrameworkCore  -> Microsoft.EntityFrameworkCore.Sql
/// Microsoft.AspNetCore.Identity.EntityFrameworkCore
/// </summary>
namespace TB.Data
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

       public DbSet<Post> Posts { get; set; }

       public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<PostTag> PostTags { get; set; }

    }
}

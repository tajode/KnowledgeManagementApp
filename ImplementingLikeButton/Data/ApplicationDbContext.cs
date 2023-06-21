using ImplementingLikeButton.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ImplementingLikeButton.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Knowledge>? Knowledge_Dbset { get; set; }
        public DbSet<LikedKnowledge>? LikedKnowledge_Dbset { get; set; }
    }
}
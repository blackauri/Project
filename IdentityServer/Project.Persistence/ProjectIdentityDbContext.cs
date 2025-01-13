using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.Domain.IdentityEntities;

namespace Project.Persistence
{
    public class ProjectIdentityDbContext(DbContextOptions<ProjectIdentityDbContext> options)
        : IdentityDbContext<ProjectUser>(options)
    {
        public DbSet<ProjectUser> ProjectUsers { get; set; }
    }
}

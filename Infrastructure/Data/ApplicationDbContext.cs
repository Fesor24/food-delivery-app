using Core.Entities;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach(var change in ChangeTracker.Entries<BaseEntity>())
            {
                change.Entity.DateModified = DateTimeOffset.UtcNow;

                if(change.State == EntityState.Added)
                {
                    change.Entity.DateCreated = DateTimeOffset.UtcNow;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Products> Products { get; set; }

        public DbSet<Restaurant> Restaurants { get; set; }
    }
}

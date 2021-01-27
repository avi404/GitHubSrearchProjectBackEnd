
using GitHubRestApiService.Model;
using Microsoft.EntityFrameworkCore;

namespace GitHubRestApiService.DB
{
    public class UsersDbContext : DbContext
    {
        public DbSet<UserCredentials> Users { get; set; }
        public DbSet<FavoritesRepository> Favorites { get; set; }

        public UsersDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}

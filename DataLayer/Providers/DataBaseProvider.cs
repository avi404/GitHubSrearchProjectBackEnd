using GitHubRestApiService.DB;
using GitHubRestApiService.Model;
using GitHubRestApiService.Model.RequestsModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitHubRestApiService.DataLayer.Providers
{
    public class DataBaseProvider : IDataBaseProvider
    {
        private readonly UsersDbContext dbContext;
        private readonly ILogger<DataBaseProvider> logger;

        public DataBaseProvider(UsersDbContext dbContext, ILogger<DataBaseProvider> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            AddSomeSampleUsers();
        }

        private void AddSomeSampleUsers()
        {
            //sample data 
            //in memeory DB 
            try
            {
                if (!dbContext.Users.Any())
                {
                    dbContext.Users.Add(new UserCredentials() { Id = Guid.NewGuid(), UserName = "tiba", Password = "123456", Email = "tiba@tiba.com" });
                    dbContext.Users.Add(new UserCredentials() { Id = Guid.NewGuid(), UserName = "avi", Password = "123456", Email = "avi@tiba.com" });
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex )
            {
                logger?.LogError(ex.ToString());
                
            }
        }

        public async Task<UserCredentials> IsUserRegisteredAsync(UserCredentials userCredential)
        {
            bool userWasFound = false;
            UserCredentials user = await dbContext.Users.FirstOrDefaultAsync(user => user.UserName == userCredential.UserName && user.Password == userCredential.Password);
            if (user != null)
            {
                userWasFound = true;
            }

            return user;

        }


        public async Task<bool> AddUserFavoriteRepository(FavoriteRepositoryRequest request)
        {
            bool successfullyAdded = false;
            var favoriteRepo = await dbContext.Favorites.FirstOrDefaultAsync(favorite => favorite.RepositoryName == request.RepositoryName && favorite.UserName == request.UserName);
            if (favoriteRepo == null)
            {
                dbContext.Favorites.Add(new FavoritesRepository() { Id = Guid.NewGuid(), UserName = request.UserName, RepositoryName = request.RepositoryName });
                dbContext.SaveChanges();
                successfullyAdded = true;
            }

            return successfullyAdded;

        }

        public async Task<List<FavoritesRepository>> GetUserFavorites(string userName)
        {
            var favoriteRepo = await dbContext.Favorites.Where(favorite => favorite.UserName == userName).ToListAsync();
            return favoriteRepo;
        }

        /// <summary>
        /// TO DO !!
        /// </summary>
        /// <param name="userCredentials"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, string ErrorMessage)> AddNewUser(UserCredentials userCredentials)
        {
            // to be implemented 
            return (true, string.Empty);
        }

        /// <summary>
        /// TO DO !!
        /// </summary>
        /// <param name="userCredentials"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, string ErrorMessage)> RemoveUser(UserCredentials userCredentials)
        {
            // to be implemented 
            return (true, string.Empty);
        }
    }
}

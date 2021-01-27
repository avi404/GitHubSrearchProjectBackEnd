using GitHubRestApiService.Model;
using GitHubRestApiService.Model.RequestsModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GitHubRestApiService.DataLayer.Providers
{
    public interface IDataBaseProvider
    {
        Task<(bool IsSuccess, string ErrorMessage)> AddNewUser(UserCredentials userCredentials);
        Task<(bool IsSuccess, string ErrorMessage)> RemoveUser(UserCredentials userCredentials);
        Task<UserCredentials> IsUserRegisteredAsync(UserCredentials userCredential);
        Task<bool> AddUserFavoriteRepository(FavoriteRepositoryRequest request);
        Task<List<FavoritesRepository>> GetUserFavorites(string userName);
    }
}
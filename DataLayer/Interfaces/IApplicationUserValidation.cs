using GitHubRestApiService.Model;

namespace GitHubRestApiService.DataLayer
{
    public interface IApplicationUserValidation
    {
        bool ValidateUser(UserCredentials userCredentials);
    }
}
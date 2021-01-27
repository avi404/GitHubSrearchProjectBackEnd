using GitHubRestApiService.Model;
using GitHubRestApiService.Model.RequestsModel;

using System.Threading.Tasks;

namespace GitHubRestApiService.DataLayer.Interfaces
{
    public interface ITokenProvider
    {
        Task<AuthenticationStatus> Login(UserCredentials userCredentials);
        Task<AuthenticationStatus> RenewToken(UserCredentials userCredentials);
    }
}

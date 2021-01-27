using GitHubRestApiService.Model;
using GitHubRestApiService.Model.RequestsModel;
 

namespace GitHubRestApiService.DataLayer
{
    public class ApplicationUserValidation : IApplicationUserValidation
    {
        public bool ValidateUser(UserCredentials userCredentials)
        {
            bool isAuthorized = false;           
            return isAuthorized;
        }       
    }
}

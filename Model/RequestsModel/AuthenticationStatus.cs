using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitHubRestApiService.Model.RequestsModel
{
    public class AuthenticationStatus
    {
        public bool Authenticated { get; set; } = false;
        public string ErrorMessage { get; set; }
        public string Token { get; set; }
    }
}

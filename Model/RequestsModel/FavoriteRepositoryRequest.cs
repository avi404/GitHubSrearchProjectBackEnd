using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitHubRestApiService.Model.RequestsModel
{
    public class FavoriteRepositoryRequest
    {
        public string RepositoryName { get; set; }
        public string UserName { get; set; }
    }
}

using GitHubRestApiService.Model.RequestsModel;
using GitHubRestApiService.Model.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitHubRestApiService.DataLayer.Interfaces
{
    public interface ISerachProvider
    {
        Task<IEnumerable<SearchResults>> SerachInRepository(SearchRequest searchRequest);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitHubRestApiService.Model.ResponseModel
{
    public class SearchResultsResponse : ResponseBase
    {
        List<SearchResults> SearchResults { get; set; }
    }
}

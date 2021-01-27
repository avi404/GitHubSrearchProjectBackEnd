
using System.ComponentModel.DataAnnotations;

namespace GitHubRestApiService.Model.RequestsModel
{
    public class SearchRequest
    {
        // [Required]
        public ProgramingLanguage Language { get; set; }

        public string RepositoryName { get; set; }
    }
}

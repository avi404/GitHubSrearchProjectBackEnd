using System;
using System.ComponentModel.DataAnnotations;

namespace GitHubRestApiService.Model
{
    public class FavoritesRepository
    {
        [Key]
        public Guid Id { get; set; }
        public string RepositoryName { get; set; }
        public string UserName { get; set; }
    }
}

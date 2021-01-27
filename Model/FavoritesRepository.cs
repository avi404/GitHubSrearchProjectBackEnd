using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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

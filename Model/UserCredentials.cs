using System;
using System.ComponentModel.DataAnnotations;

namespace GitHubRestApiService.Model
{
    public class UserCredentials
    {
        [Key]
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}

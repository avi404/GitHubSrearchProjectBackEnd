using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitHubRestApiService.Model.ResponseModel
{
    public class ResponseBase
    {
        public bool Success { get; set; } = false;
        public string ErrorMessage { get; set; } = string.Empty;
        public string StackTrace { get; set; } = string.Empty;

    }
}

using GitHubRestApiService.DataLayer.Interfaces;
using GitHubRestApiService.DataLayer.Providers;
using GitHubRestApiService.Model;
using GitHubRestApiService.Model.RequestsModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GitHubRestApiService.Controllers
{

    [ApiController]
    [Route("/api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ITokenProvider tokenProvider;
        public AuthenticationController(ITokenProvider tokenProvider)
        {
            this.tokenProvider = tokenProvider;
        }


        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(UserCredentials userCredentials)
        {
            try
            {
                AuthenticationStatus authenticationStatus = await tokenProvider.Login(userCredentials);
                if (authenticationStatus.Authenticated == true)
                {
                    return Ok(new { token = authenticationStatus.Token });
                }
                else
                {
                    return BadRequest(authenticationStatus.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return BadRequest($"{ex.Message}");
            }
        }
    }
}

using GitHubRestApiService.DataLayer.Interfaces;
using GitHubRestApiService.DataLayer.Providers;
using GitHubRestApiService.Model;
using GitHubRestApiService.Model.RequestsModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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



        //[Route("Login")]
        //[HttpPost]
        //public async Task<IActionResult> Login2(UserCredentials userCredentials)
        //{
        //    UserCredentials currentUser = await dataBaseProvider.IsUserRegisteredAsync(userCredentials);
        //    try
        //    {
        //        if (currentUser != null)
        //        {
        //            //create claims details based on the user information
        //            var claims = new[] {
        //            new Claim(JwtRegisteredClaimNames.Sub, config["Jwt:Subject"]),
        //            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
        //            new Claim("Id",currentUser.Id.ToString()),
        //            new Claim("FirstName",currentUser.UserName),
        //            new Claim("LastName", currentUser.UserName),
        //            new Claim("UserName",currentUser.UserName),
        //            new Claim("Email", currentUser.Email)
        //           };

        //            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
        //            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        //            var token = new JwtSecurityToken(config["Jwt:Issuer"], config["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);
        //            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        //            return Ok(new { token = tokenString });
        //            //return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        //        }
        //        else
        //        {
        //            return BadRequest("Invalid credentials");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        string message = ex.Message;
        //        return BadRequest($"{ex.Message}");
        //    }
        //}


    }
}

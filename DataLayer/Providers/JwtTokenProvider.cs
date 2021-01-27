using GitHubRestApiService.DataLayer.Interfaces;
using GitHubRestApiService.Model;
using GitHubRestApiService.Model.RequestsModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace GitHubRestApiService.DataLayer.Providers
{
    public class JwtTokenProvider : ITokenProvider
    {
        private readonly IConfiguration config;
        private readonly IDataBaseProvider dataBaseProvider;
        private readonly ILogger<JwtTokenProvider> logger;

        public JwtTokenProvider(IConfiguration config, IDataBaseProvider dataBaseProvider , ILogger<JwtTokenProvider> logger)
        {
            this.config = config;
            this.dataBaseProvider = dataBaseProvider;
        }

        public async Task<AuthenticationStatus> Login(UserCredentials userCredentials)
        {
            logger?.LogInformation("In Login function ...");

            AuthenticationStatus authenticationStatus = new AuthenticationStatus();

            UserCredentials currentUser = await dataBaseProvider.IsUserRegisteredAsync(userCredentials);
            try
            {
                if (currentUser != null)
                {
                    //create claims details based on the user information
                    var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, config["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Id",currentUser.Id.ToString()),
                    new Claim("FirstName",currentUser.UserName),
                    new Claim("LastName", currentUser.UserName),
                    new Claim("UserName",currentUser.UserName),
                    new Claim("Email", currentUser.Email)
                   };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(config["Jwt:Issuer"], config["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                    authenticationStatus.Authenticated = true;
                    authenticationStatus.Token = tokenString;

                    //return Ok(new { token = tokenString });
                    //return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    authenticationStatus.Authenticated = false;
                    authenticationStatus.ErrorMessage = "Invalid credentials";
                }

            }
            catch (Exception ex)
            {
                authenticationStatus.Authenticated = false;
                authenticationStatus.ErrorMessage = $"Error has occurred {ex.Message}";
                logger?.LogError(ex.ToString());  //and example of  Error hndling
            }
            return authenticationStatus;
        }

        public Task<AuthenticationStatus> RenewToken(UserCredentials userCredentials)
        {
            //To Be Implemnted 
            throw new NotImplementedException();
        }
    }
}

 
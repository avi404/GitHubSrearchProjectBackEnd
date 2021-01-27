using GitHubRestApiService.DataLayer.Providers;
using GitHubRestApiService.Model;
using GitHubRestApiService.Model.RequestsModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitHubRestApiService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class FavoritesRepositoryController : ControllerBase
    {
        private readonly IDataBaseProvider dataBaseProvider;

        public FavoritesRepositoryController(IDataBaseProvider dataBaseProvider)
        {
            this.dataBaseProvider = dataBaseProvider;
        }


        [HttpGet]
        [Route("IsAlive")]
        public ActionResult<string> IsAlive()
        {
            return Ok($"Favorites Repository Controller Api is up { DateTime.Now.ToString()}");
        }


        [HttpPost]
        [Route("AddFavoriteRepository")]
        public async Task<ActionResult<(bool Success, string ErrorMessage)>> AddFavoriteRepositoryAsync(FavoriteRepositoryRequest favoriteRepositoryRequest)
        {
            bool success = false;
            try
            {
                success = await dataBaseProvider.AddUserFavoriteRepository(favoriteRepositoryRequest);
            }
            catch (Exception ex)
            {
                return (success, ex.Message);
            }
           
            return (success, string.Empty);
        }


        [HttpGet]
        [Route("GetUserFavoritesAsync")]
        public async Task<ActionResult<FavoritesRepository>> GetUserFavoritesAsync(string userName)
        {
            try
            {
                List<FavoritesRepository> results = await dataBaseProvider.GetUserFavorites(userName);
                if (results.Any())
                {
                    return Ok(results);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error {ex.Message}");
            }
        }

    }
}

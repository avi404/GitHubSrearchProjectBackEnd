﻿using GitHubRestApiService.DataLayer.Interfaces;
using GitHubRestApiService.Model.RequestsModel;
using GitHubRestApiService.Model.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GitHubRestApiService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly ISerachProvider serachProvider;

        public SearchController(ISerachProvider serachProvider)
        {
            this.serachProvider = serachProvider;
        }

   
        [HttpPost]
        [Route("SearchInRepositoryAsync")]
        public async Task<ActionResult<SearchResults>> SearchInRepositoryAsync(SearchRequest searchRequest)
        {
       
           IEnumerable<Claim> userClaims = ((ClaimsIdentity)User.Identity).Claims;
           var list =  userClaims.ToList();
           var username =  list[6];

           var currentUser = username.Value;

            try
            {                
                var searchResults = await serachProvider.SerachInRepository(searchRequest);
                {
                    return Ok(searchResults);
                }
                if (!searchResults.Any())
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error {ex.Message}");
            }
        }


        [HttpGet]
        [Route("IsAlive")]
        public ActionResult<string> IsAlive()
        {
            return Ok($"SearchController Api is up { DateTime.Now.ToString()}");
        }
    }
}
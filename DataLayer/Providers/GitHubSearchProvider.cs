using GitHubRestApiService.DataLayer.Interfaces;
using GitHubRestApiService.Model.RequestsModel;
using GitHubRestApiService.Model.ResponseModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Octokit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductHeaderValue = Octokit.ProductHeaderValue;

namespace GitHubRestApiService.DataLayer
{
    public class GitHubSearchProvider : ISerachProvider
    {

        private readonly IConfiguration Configuration;
        private readonly ILogger<GitHubSearchProvider> logger;

        public GitHubSearchProvider(IConfiguration config, ILogger<GitHubSearchProvider> logger)
        {
            Configuration = config;
            this.logger = logger;
        }

        public async Task<IEnumerable<SearchResults>> SerachInRepository(SearchRequest searchRequest)
        {
            string errorMessage = string.Empty;
            List<SearchResults> searchResults = new List<SearchResults>();       
            var productInformation = new ProductHeaderValue("GithubProxy");
            var credentials = new Credentials(Configuration["TokkenGitHub"]);
            var client = new GitHubClient(productInformation) { Credentials = credentials };

            try
            {
                Language language = (Language)searchRequest.Language;
                SearchCodeResult result = await client.Search.SearchCode(
                                          new SearchCodeRequest()
                                          {
                                              In = new CodeInQualifier[] { CodeInQualifier.Path },
                                              Language = language,
                                              Repos = new RepositoryCollection { searchRequest.RepositoryName }

                                          });

                if (result != null && result.Items.Count > 0)
                {
                    foreach (var item in result.Items)
                    {
                        searchResults.Add(new SearchResults() { FileName = item.Name, Path = item.Path });
                    }
                }
            }

            catch (Octokit.ApiValidationException ex)
            {
                if (ex.ApiError != null && ex.ApiError.Errors != null)
                {
                    foreach (var item in ex.ApiError.Errors)
                    {
                        errorMessage += item.Message.ToString();
                    }


                }
                logger?.LogError(ex.ToString() + errorMessage);  //Example of  Error handling

            }
            catch (Octokit.RepositoryFormatException ex)
            {
                logger?.LogError(ex.ToString());
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                logger?.LogError(ex.ToString());  //Example of  Error handling
            }
            finally
            {

            }
            return searchResults;
        }
    }
}

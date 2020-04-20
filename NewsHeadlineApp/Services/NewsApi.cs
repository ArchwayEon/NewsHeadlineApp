using Microsoft.Extensions.Configuration;
using NewsHeadlineApp.Models.ViewModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NewsHeadlineApp.Services
{
   public class NewsApi
   {
      private readonly HttpClient _client = new HttpClient();
      private readonly IConfiguration _configuration;

      public NewsApi(IConfiguration configuration)
      {
         _configuration = configuration;
      }

      public async Task<string> ReadSourcesAsync(string category, string language, string country)
      {
         string sourceURL = _configuration.GetValue<string>("SourceURL");
         string apiKey = _configuration.GetValue<string>("ApiKey");
         string url = $"{sourceURL}" +
            $"category={category.ToLower()}&" +
            $"language={language.ToLower()}&" +
            $"country={country.ToLower()}&" +
            $"apiKey={apiKey}";
         try
         {
            string json = await _client.GetStringAsync(url);

            JObject result = JObject.Parse(json);

            IList<JToken> results = result["sources"].ToList();

            StringBuilder sb = new StringBuilder();
            foreach (JToken r in results)
            {
               sb.Append(r["id"] + ",");
            }
            // Remove the last comma
            sb.Remove(sb.Length - 1, 1);

            return sb.ToString();
         }
         catch (HttpRequestException e)
         {
            return $"Error: {e.Message}";
         }
      } // ReadSourcesAsync

      public async Task<ICollection<NewsArticleVM>> ReadArticlesAsync(
         string source, string language)
      {
         string headlineURL = _configuration.GetValue<string>("TopHeadlinesURL");
         string apiKey = _configuration.GetValue<string>("ApiKey");
         string url = $"{headlineURL}" +
            $"sources={source.ToLower()}&" +
            $"language={language.ToLower()}&" +
            $"apiKey={apiKey}";
         var articles = new List<NewsArticleVM>();
         try
         {
            string json = await _client.GetStringAsync(url);

            JObject result = JObject.Parse(json);

            IList<JToken> results = result["articles"].ToList();
            foreach (JToken r in results)
            {
               var article = new NewsArticleVM
               {
                  AuthorName = r["author"].ToString(),
                  Title = r["title"].ToString(),
                  Description = r["description"].ToString(),
                  Url = r["url"].ToString(),
                  UrlToImage = r["urlToImage"].ToString(),
                  PublishedAt = DateTimeOffset.Parse(r["publishedAt"].ToString())
               };
               articles.Add(article);
            }
         }
         catch (HttpRequestException)
         {
         }
         return articles;
      } // ReadSourcesAsync


   }
}

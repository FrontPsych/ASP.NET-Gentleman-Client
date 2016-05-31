using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Domain;
using Domain.Models;
using Microsoft.Owin.Security;
using NLog;

namespace DAL.Repositories
{
    public class ArticleRepository : WebApiRepository<Article>, IArticleRepository
    {
        private readonly ILogger _logger = NLog.LogManager.GetCurrentClassLogger();
        public ArticleRepository(HttpClient httpClient, string endPoint, IAuthenticationManager authenticationManager) : base(httpClient, endPoint, authenticationManager)
        {
        }

        public Article FindArticleByName(string articleName)
        {
            var response = HttpClient.GetAsync(EndPoint + nameof(FindArticleByName) + "/" + articleName).Result;
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsAsync<Article>().Result;
                return res;
            }
            _logger.Debug("Web API statuscode: " + response.StatusCode.ToString() + " Uri:" + response.RequestMessage.RequestUri);
            return new Article()
            {
                ArticleName = "NotFound",
                ArticleHeadline = new MultiLangString("Article not found!"),
                ArticleBody = new MultiLangString("Article not found!")
            };
        }

        public List<Article> AllWithTranslations()
        {
            throw new NotImplementedException();
        }

        public void DeleteArticleWithTranslations(params object[] id)
        {
            throw new NotImplementedException();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace DAL.Interfaces
{
    public interface IArticleRepository : IBaseRepository<Article>
    {
        Article FindArticleByName(string articleName);

        List<Article> AllWithTranslations();

        void DeleteArticleWithTranslations(params object[] id);
    }
}
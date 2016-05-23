
using Domain.Models;

namespace Web.ViewModels
{
    public class HomeSinglePageViewModel
    {
        //ToDo: Is there a better way?

        public Article Article { get; set; }


        public Article AboutArticle { get; set; }

        public Article AboutArticleColumnOne { get; set; }

        public Article AboutArticleColumnTwo { get; set; }

        public Article AboutArticleColumnThree { get; set; }

        public Article AboutLastLongArticle { get; set; }


        public Article FeatureArticle { get; set; }

        public Article FeatureArticleColumnOne { get; set; }

        public Article FeatureArticleColumnTwo { get; set; }

        public Article FeatureArticleColumnThree { get; set; }


        public Article ContactArticle { get; set; }

    }
}
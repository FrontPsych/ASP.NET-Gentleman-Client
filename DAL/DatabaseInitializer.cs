using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using Domain.Models;
using Domain.Identity;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;

namespace DAL
{
    //    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<DataBaseContext>
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<DataBaseContext>
    {
        protected override void Seed(DataBaseContext context)
        {
            var autoDetectChangesEnabled = context.Configuration.AutoDetectChangesEnabled;
            // much-much faster for bulk inserts!!!!
            context.Configuration.AutoDetectChangesEnabled = false;

            SeedIdentity(context);
            SeedArticles(context);


            // restore state
            context.Configuration.AutoDetectChangesEnabled = autoDetectChangesEnabled;

            base.Seed(context);
        }

        private void SeedArticles(DataBaseContext context)
        {
            var articleHeadLine = "Gentleman score saver";
            var articleBody =
                "save scores and see statistics";
            var article = new Article()
            {
                ArticleName = "HomeIndex",
                ArticleHeadline = new MultiLangString(articleHeadLine, "en", articleHeadLine, "Article.HomeIndex.ArticleHeadline"),
                ArticleBody = new MultiLangString(articleBody, "en", articleBody, "Article.HomeIndex.ArticleBody")
            };
            context.Articles.Add(article);
            context.SaveChanges();

            context.Translations.Add(new Translation()
            {
                Value = "Gentleman punktiarvestussüsteem",
                Culture = "et",
                MultiLangString = article.ArticleHeadline
            });

            context.Translations.Add(new Translation()
            {
                Value = "salvesta punkte ja vaata statistikat",
                Culture = "et",
                MultiLangString = article.ArticleBody
            });
            context.SaveChanges();

            //public Article AboutArticle { get; set; }

            var aboutArticleBody = "Our website is made to hold your Gentleman card game scores, keep running track of statistics and show user specific history.";

            var aboutArticle = new Article()
            {
                ArticleName = "HomeIndexAbout",
                ArticleHeadline = null,
                ArticleBody = new MultiLangString(aboutArticleBody, "en", articleBody, "Article.HomeIndex.ArticleBody")
            };
            context.Articles.Add(aboutArticle);
            context.SaveChanges();

            context.Translations.Add(new Translation()
            {
                Value = "Meie veebisait on loodud hoidmaks Teie Gentleman kaardimängu punktiskoore, pidama jooksvat statistikat ja näitama kasutaja ajalugu.",
                Culture = "et",
                MultiLangString = aboutArticle.ArticleBody
            });
            context.SaveChanges();

            //public Article AboutArticleColumnOne { get; set; }

            var aboutArticleColumnOneTitle = "No more paper";
            var aboutArticleColumnOneBody = "Stop using pen and paper to keep track of your scores. Now you can just log on, create a new game instance and save your data with ease. You're saving time and getting a more enjoyable experience";

            var aboutArticleColumnOne = new Article()
            {
                ArticleName = "HomeIndexAboutColumnOne",
                ArticleHeadline = new MultiLangString(aboutArticleColumnOneTitle, "en", aboutArticleColumnOneTitle, "Article.HomeIndex.ArticleTitle"),
                ArticleBody = new MultiLangString(aboutArticleColumnOneBody, "en", aboutArticleColumnOneBody, "Article.HomeIndex.ArticleBody")
            };
            context.Articles.Add(aboutArticleColumnOne);
            context.SaveChanges();

            context.Translations.Add(new Translation()
            {
                Value = "Ei paberile ja pliiatsile",
                Culture = "et",
                MultiLangString = aboutArticleColumnOne.ArticleHeadline
            });

            context.Translations.Add(new Translation()
            {
                Value = "Viimaks võid lõpetada paberi ja pliiatsi kasutamise. Nüüd saad lihtsalt sisselogides luua uue mängu ja salvestada oma mänguandmed kergelt ja kiirelt. Säästad aega ning saad veel meeldivama mängukogemuse!",
                Culture = "et",
                MultiLangString = aboutArticleColumnOne.ArticleBody
            });
            context.SaveChanges();

            //ToDo: Finish the remaining article texts.

            //public Article AboutArticleColumnTwo { get; set; }
            //public Article AboutArticleColumnThree { get; set; }
            //public Article AboutLastLongArticle { get; set; }
            //public Article FeatureArticle { get; set; }
            //public Article FeatureArticleColumnOne { get; set; }
            //public Article FeatureArticleColumnTwo { get; set; }
            //public Article FeatureArticleColumnThree { get; set; }
            //public Article ContactArticle { get; set; }

        }


        private void SeedIdentity(DataBaseContext context)
        {
            var pwdHasher = new PasswordHasher();

            // Roles
            context.RolesInt.Add(new RoleInt()
            {
                Name = "Admin"
            });

            context.SaveChanges();

            // Users
            context.UsersInt.Add(new UserInt()
            {
                UserName = "lebo@lebo.ee",
                Email = "lebo@lebo.ee",
                FirstName = "Das",
                LastName = "User",
                PasswordHash = pwdHasher.HashPassword("admin"),
                SecurityStamp = Guid.NewGuid().ToString(),

                PersonName = "fakingtestjuuser",
                RegisterDate = DateTime.Now.Date
            });

            context.SaveChanges();

            // Users in Roles
            context.UserRolesInt.Add(new UserRoleInt()
            {
                User = context.UsersInt.FirstOrDefault(a => a.UserName == "lebo@lebo.ee"),
                Role = context.RolesInt.FirstOrDefault(a => a.Name == "Admin")
            });

            context.SaveChanges();


            //Person for that Identity User
            //context.Persons.Add(new Person()
            //{
            //    PersonName = "Peeter",
            //    Nickname = "Pets",
            //    User = context.UsersInt.FirstOrDefault(a => a.UserName == "lebo@lebo.ee")
            //});

            //context.SaveChanges();

        }
    }
}
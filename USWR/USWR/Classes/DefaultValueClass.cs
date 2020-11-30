using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using USWR.Models;

namespace USWR.Classes
{
    public class DefaultValueClass
    {
        public static void Initialize(USWRContext context)
        {
            //default id for users and two sites
            Guid userId = new Guid("a5187e2d-8bee-45c5-93cc-b87b603e9c9d");
            Guid siteOne = new Guid("efd219ad-100b-4447-a194-2a0526840036");
            Guid siteTwo = new Guid("c5ebcb92-8560-47be-9d69-376de45fb787");

            //if database doesn't consist this sites, add this sites into databse
            if (!context.Sites.Any())
            {
                context.Sites.AddRange(
                    new Sites
                    {
                        Id=siteOne,
                        Header="Лучшая курица в округе",
                        Link= "https://mail.ru//",
                        Keywords="Почта",
                        Description= "Сайт,который представляет собой почту для отправки сообщений"

                    },
                     new Sites
                     {
                         Id = siteTwo,
                         Header = "Metanit",
                         Link = "https://metanit.com/",
                         Keywords = "Программирование, JS, ASP.NET, HTML, C#",
                         Description = "METANIT.COM - Сайт о программировании на C#, .NET, Java, Python, Golang, Dart, Flutter," +
                         " мобильной разработке на Android, iOS, Xamarin, веб-разработке на ASP.NET..."

                     });
                context.SaveChanges();
            }
            //if database doesn't consist test user, add him
            if (!context.Users.Any(p=>p.Login.Equals("test")))
            {
                context.Users.AddRange(
                    new Users
                    {
                        Id = userId,
                        Login="test",
                        Password="lsnbfgdnasflinba31232113221332144prwqaubnvr",
                        RoleId=2
                    }
                    );
                context.SaveChanges();
            }

            //add default rating for two sites with id test user
            if (!context.Ratings.Any(p => p.UserId.Equals(userId)))
            {
                context.Ratings.AddRange(
                    new Ratings
                    {
                        UserId=userId,
                        SiteId=siteOne,
                        Rating=0
                    },
                    new Ratings
                    {
                        UserId = userId,
                        SiteId = siteTwo,
                        Rating = 0
                    }
                    );
                context.SaveChanges();
            }
            
        }
    }
}

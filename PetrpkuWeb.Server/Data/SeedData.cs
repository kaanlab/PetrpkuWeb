using PetrpkuWeb.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetrpkuWeb.Server.Data
{
    public class SeedData
    {
        public static void Initialize(AppDbContext db)
        {
            var users = new AppUser[]
            {
                new AppUser()
                {
                    AppUserId = 1,
                    
                    FirstName = "Петр",
                    LastName = "Петров",
                    MidleName = "Петрович",
                    WorkingPosition = "Начальник спортивного центра",
                    Office = "130",
                    Phone = "+79114000333",
                    PhotoUrl = "img/user/bart.png"
                },
                new AppUser()
                {
                    AppUserId = 2,
                    
                    FirstName = "Марина",
                    LastName = "Матросова",
                    MidleName = "Андреевна",
                    WorkingPosition = "Начальник ФЭО",
                    Office = "100",
                    Phone = "+79114001234",
                    PhotoUrl = "img/user/marge.png"
                },
                new AppUser()
                {
                    AppUserId = 3,
                    
                    FirstName = "Оксана",
                    LastName = "Малкина",
                    MidleName = "Владимировна",
                    WorkingPosition = "Начальник ОКиС",
                    Office = "120",
                    Phone = "+79114000666",
                    PhotoUrl = "img/user/liza.png"
                },
                new AppUser()
                {
                    AppUserId = 4,
                    
                    FirstName = "Иван",
                    LastName = "Иванов",
                    MidleName = "Иванович",
                    WorkingPosition = "Начальник отдела МТО",
                    Office = "130",
                    Phone = "+79115004040",
                    PhotoUrl = "img/user/gomer.png"
                }
            };

            var duties = new Duty[]
            {
                new Duty()
                {
                    DutyId = 1,
                    AppUserId = 1,
                    DayOfDuty = new DateTime(2019, 08, 31)
                },
                 new Duty()
                 {
                     DutyId = 2,
                     AppUserId = 2,
                     DayOfDuty = new DateTime(2019, 08, 11)
                 },
                 new Duty()
                 {
                     DutyId = 3,
                     AppUserId = 3,
                     DayOfDuty = new DateTime(2019, 08, 12)
                 },
                 new Duty()
                {
                    DutyId = 4,
                    AppUserId = 4,
                    DayOfDuty = new DateTime(2019, 08, 13)
                },
                 new Duty()
                 {
                     DutyId = 5,
                     AppUserId = 1,
                     DayOfDuty = new DateTime(2019, 08, 14)
                 },
                 new Duty()
                 {
                     DutyId = 6,
                     AppUserId = 2,
                     DayOfDuty = new DateTime(2019, 08, 15)
                 },
                 new Duty()
                {
                    DutyId = 7,
                    AppUserId = 3,
                    DayOfDuty = new DateTime(2019, 08, 16)
                },
                 new Duty()
                 {
                     DutyId = 8,
                     AppUserId = 4,
                     DayOfDuty = new DateTime(2019, 08, 17)
                 },
                 new Duty()
                 {
                     DutyId = 9,
                     AppUserId = 1,
                     DayOfDuty = new DateTime(2019, 08, 18)
                 }
            };

            var articles = new Article[]
            {
                new Article()
                {
                    ArticleId = 1,
                    Title = "Новость №1",
                    Content = "Очень длинная новость, которая сама не знает о чем она но это и не важно, т.к. это просто тестовый текст",
                    AppUserId = 1,
                    PublishDate = new DateTime(2019,08,13)
                },
                new Article()
                {
                    ArticleId = 2,
                    Title = "Новость №2",
                    Content = "Очень длинная новость, которая сама не знает о чем она но это и не важно, т.к. это просто тестовый текст",
                    AppUserId = 3,
                    PublishDate = new DateTime(2019,08,13)
                },
                new Article()
                {
                    ArticleId = 3,
                    Title = "Новость №3",
                    Content = "Очень длинная новость, которая сама не знает о чем она но это и не важно, т.к. это просто тестовый текст",
                    AppUserId = 3,
                    PublishDate = new DateTime(2019,08,13)
                },
                new Article()
                {
                    ArticleId = 4,
                    Title = "Новость №4",
                    Content = "Очень длинная новость, которая сама не знает о чем она но это и не важно, т.к. это просто тестовый текст",
                    AppUserId = 3,
                    PublishDate = new DateTime(2019,08,13)
                },
                new Article()
                {
                    ArticleId = 5,
                    Title = "Новость №5",
                    Content = "Очень длинная новость, которая сама не знает о чем она но это и не важно, т.к. это просто тестовый текст",
                    AppUserId = 1,
                    PublishDate = new DateTime(2019,08,13)
                },
            };

            db.AppUsers.AddRange(users);
            db.Duties.AddRange(duties);
            db.Articles.AddRange(articles);
            db.SaveChanges();
        }
    }
}

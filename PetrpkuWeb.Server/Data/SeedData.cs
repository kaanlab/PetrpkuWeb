using PetrpkuWeb.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetrpkuWeb.Server.Data
{
    public class SeedData
    {
        public static void Initialize(DbStorageContext db)
        {
            var users = new UserInfo[]
            {
                new UserInfo()
                {
                    UserInfoId = 1,
                    LoginName = "bart",
                    FirstName = "Петр",
                    LastName = "Петров",
                    MidleName = "Петрович",
                    WorkingPosition = "Начальник спортивного центра",
                    Office = "130",
                    Phone = "+79114000333",
                    PhotoUrl = "img/user/bart.png"
                },
                new UserInfo()
                {
                    UserInfoId = 2,
                    LoginName = "marge",
                    FirstName = "Марина",
                    LastName = "Матросова",
                    MidleName = "Андреевна",
                    WorkingPosition = "Начальник ФЭО",
                    Office = "100",
                    Phone = "+79114001234",
                    PhotoUrl = "img/user/marge.png"
                },
                new UserInfo()
                {
                    UserInfoId = 3,
                    LoginName = "liza",
                    FirstName = "Оксана",
                    LastName = "Малкина",
                    MidleName = "Владимировна",
                    WorkingPosition = "Начальник ОКиС",
                    Office = "120",
                    Phone = "+79114000666",
                    PhotoUrl = "img/user/liza.png"
                },
                new UserInfo()
                {
                    UserInfoId = 4,
                    LoginName = "gomer",
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
                    UserInfoId = 1,
                    DayOfDuty = new DateTime(2019, 07, 31)
                },
                 new Duty()
                 {
                     DutyId = 2,
                     UserInfoId = 2,
                     DayOfDuty = new DateTime(2019, 08, 1)
                 },
                 new Duty()
                 {
                     DutyId = 3,
                     UserInfoId = 3,
                     DayOfDuty = new DateTime(2019, 08, 2)
                 },
                 new Duty()
                {
                    DutyId = 4,
                    UserInfoId = 4,
                    DayOfDuty = new DateTime(2019, 07, 31)
                },
                 new Duty()
                 {
                     DutyId = 5,
                     UserInfoId = 1,
                     DayOfDuty = new DateTime(2019, 08, 1)
                 },
                 new Duty()
                 {
                     DutyId = 6,
                     UserInfoId = 2,
                     DayOfDuty = new DateTime(2019, 08, 2)
                 },
                 new Duty()
                {
                    DutyId = 7,
                    UserInfoId = 3,
                    DayOfDuty = new DateTime(2019, 07, 31)
                },
                 new Duty()
                 {
                     DutyId = 8,
                     UserInfoId = 4,
                     DayOfDuty = new DateTime(2019, 08, 1)
                 },
                 new Duty()
                 {
                     DutyId = 9,
                     UserInfoId = 1,
                     DayOfDuty = new DateTime(2019, 08, 2)
                 }
            };

            var articles = new Article[]
            {
                new Article()
                {
                    ArticleId = 1,
                    Title = "Новость №1",
                    Content = "Очень длинная новосьт, которая сама не знает о чем она но это и не важно, т.к. это просто тестовый текст",
                    UserInfoId = 1
                },
                new Article()
                {
                    ArticleId = 2,
                    Title = "Новость №2",
                    Content = "Очень длинная новосьт, которая сама не знает о чем она но это и не важно, т.к. это просто тестовый текст",
                    UserInfoId = 3
                },
                new Article()
                {
                    ArticleId = 3,
                    Title = "Новость №3",
                    Content = "Очень длинная новосьт, которая сама не знает о чем она но это и не важно, т.к. это просто тестовый текст",
                    UserInfoId = 3
                },
                new Article()
                {
                    ArticleId = 4,
                    Title = "Новость №4",
                    Content = "Очень длинная новосьт, которая сама не знает о чем она но это и не важно, т.к. это просто тестовый текст",
                    UserInfoId = 3
                },
                new Article()
                {
                    ArticleId = 5,
                    Title = "Новость №5",
                    Content = "Очень длинная новосьт, которая сама не знает о чем она но это и не важно, т.к. это просто тестовый текст",
                    UserInfoId = 1
                },
            };

            db.Users.AddRange(users);
            db.Duties.AddRange(duties);
            db.Articles.AddRange(articles);
            db.SaveChanges();
        }
    }
}

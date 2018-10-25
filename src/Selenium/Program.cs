using System;
using System.Threading;
using Selenium.Common;
using Selenium.PageObject;

namespace Selenium
{
    public class Program
    {
        private const string BaseUrl = @"http://localhost:8080";

        private static void Main(string[] args)
        {
            using (var webDriver = WebDriverFactory.CreateInstance(AppSettings.BrowserName.InternetExplorer))
            {
                try
                {
                    // 開始時間を遅らせる為にスリープ
                    Thread.Sleep(TimeSpan.FromSeconds(5));

                    var loginPage = new LoginPage(webDriver);
                    var homePage = loginPage.Login(BaseUrl, "user01", "password");
                    var projectPage = homePage.SelectProject("Seleniumの実験");
                    var ticketListPage = projectPage.SelectTicketTab();
                    var newTicketPage = ticketListPage.MoveTicketPage();

                    for (var i = 0; i < 10; i++)
                    {
                        var ticketCompletePage = CreateTicket(newTicketPage, i + 1);
                        ticketListPage = ticketCompletePage.SelectTicketTab();
                        newTicketPage = ticketListPage.MoveTicketPage();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                // 一瞬で完了するため3秒スリープ

                // ブラウザを閉じる
                webDriver.Quit();
            }
        }

        private static TicketCompletePage CreateTicket(NewTicketPage newTicketPage, int num)
        {
            // チケット情報の入力
            newTicketPage.SelectTracker(Enums.Tracker.Bug);
            newTicketPage.EnterSubject($"【実験】題名ですだよ:{num}");
            newTicketPage.EnterDescription($"【実験】説明ですよ\n{num}");
            newTicketPage.SelectPriority(Enums.Priority.Urgent);
            newTicketPage.SelectAssignedTo(Enums.AssignedTo.User1);

            // チケット登録
            return newTicketPage.CreateClick();
        }
    }
}

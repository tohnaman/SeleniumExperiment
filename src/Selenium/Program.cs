using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using OpenQA.Selenium;
using Selenium.Common;
using Selenium.PageObject;

namespace Selenium
{
    public class Program
    {
        private const string BaseUrl = @"http://localhost:8080";
        private static readonly HttpClient Client = new HttpClient();

        private static void Main(string[] args)
        {
            ServicePointManager.Expect100Continue = false;

            using (var webDriver = WebDriverFactory.CreateInstance(AppSettings.BrowserName.InternetExplorer))
            {
                try
                {
                    // Scenario1(webDriver);
                    // Scenario2(webDriver);

                    Scenario3(webDriver);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                // ブラウザを閉じる
                webDriver.Quit();
            }
        }

        /// <summary>
        /// ログインしてチケットを10個登録する
        /// </summary>
        private static void Scenario1(IWebDriver webDriver)
        {
            var ticketListPage = MoveFromLoginPageToTicketListPage(webDriver);
            var newTicketPage = ticketListPage.MoveTicketPage();
            for (var i = 0; i < 10; i++)
            {
                var ticketCompletePage = CreateTicket(newTicketPage, i + 1);
                ticketListPage = ticketCompletePage.SelectTicketTab();
                newTicketPage = ticketListPage.MoveTicketPage();
            }
        }

        /// <summary>
        /// ログインしてチケット一覧をダウンロードする
        /// </summary>
        private static async void Scenario2(IWebDriver webDriver)
        {
            var ticketListPage = MoveFromLoginPageToTicketListPage(webDriver);

            var bytes = await DownloadImageAsync(@"http://localhost:8080/projects/selenium/issues.pdf");
            File.WriteAllBytes(@"D:\hoge.pdf", bytes);
        }

        /// <summary>
        /// ログイン画面からチケット一覧画面に遷移する
        /// </summary>
        private static TicketListPage MoveFromLoginPageToTicketListPage(IWebDriver webDriver)
        {
            var loginPage = new LoginPage(webDriver);
            var homePage = loginPage.Login(BaseUrl, "user01", "password");
            var projectPage = homePage.SelectProject("Seleniumの実験");
            return projectPage.SelectTicketTab();
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

        /// <summary>
        /// PDFをダウンロードする
        ///   テキトー実装
        /// </summary>
        /// <param name="url">PDFのURL</param>
        /// <returns>PDFのbyte配列</returns>
        private static async Task<byte[]> DownloadImageAsync(string url)
        {
            try
            {
                using (var response = await Client.GetAsync(url))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return await response.Content.ReadAsByteArrayAsync();
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }


        /// <summary>
        /// SeleniumとHttpClientの組み合わせ
        ///   成功しない・・・419になる
        /// </summary>
        /// <param name="webDriver"></param>
        private static async void Scenario3(IWebDriver webDriver)
        {
            // Seleniumでログイン
            var login = new PageObject.Voyager.LoginPage(webDriver);
            login.Login(@"http://localhost/admin", "admin@admin.com", "password");
            var cookies = webDriver.Manage().Cookies.AllCookies.Select(item => $"{item.Name}={item.Value}");
            var token = webDriver.FindElement(By.XPath("//input[@name='_token']")).GetAttribute("value");

            // HttpClientでPOST通信
            var request = new HttpRequestMessage(HttpMethod.Post, @"http://localhost/admin/categories")
            {
                //Content = new FormUrlEncodedContent(new Dictionary<string, string>
                //{
                //    {"order", "10"},
                //    {"name",  "Category 10"},
                //    {"slug",  "category-10"},
                //    {"_token", token},
                //})

                Content = new MultipartFormDataContent
                {
                    {
                        new StringContent("order"), "10"
                    },
                    {
                        new StringContent("name"), "Category 10"
                    },
                    {
                        new StringContent("slug"), "category-10"
                    },
                    {
                        new StringContent("_token"), token
                    },
                }
            };
            request.Headers.Add("Cookie", cookies);

            var response = await Client.SendAsync(request);
            Debug.WriteLine("******");
            Debug.WriteLine(response.StatusCode == HttpStatusCode.OK
                ? "★★★ OK ★★★"
                : $"★★★ NG ★★★ {response.StatusCode}");
        }
    }
} 

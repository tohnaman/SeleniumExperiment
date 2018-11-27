using System;
using System.Threading;
using OpenQA.Selenium;
using Selenium.Common;

namespace Selenium.PageObject.Voyager
{
    public class LoginPage : AbstractPage
    {
        /// <summary>
        ///     コンストラクタ
        /// </summary>
        /// <param name="webDriver">ブラウザ操作用のWebDriver</param>
        public LoginPage(IWebDriver webDriver) {this.webDriver = webDriver;}

        public Dashboard Login(string url, string loginId, string password)
        {
            webDriver.Url = url;
            Thread.Sleep(TimeSpan.FromSeconds(1));

            // SeleniumHelper.EnterTextById(webDriver, "email", loginId);
            SeleniumHelper.EnterText(webDriver, By.XPath("//input[@name='password']"), password);
            SeleniumHelper.EnterTextById(webDriver, "email", loginId);
            SeleniumHelper.Click(webDriver, By.XPath("//button[@type='submit']"));

            // 検証エラーチェック
            try
            {
                var element = webDriver.FindElement(By.XPath("//div[@class='alert alert-red'"));
                throw new Exception($"ログイン失敗！{element.Text}");
            }
            catch (NoSuchElementException)
            {
                // 検証エラー時にだけ存在するので無い場合に成功になる
                return new Dashboard(webDriver);
            }
        }
    }
}

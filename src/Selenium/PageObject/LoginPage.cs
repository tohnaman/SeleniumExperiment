using System;
using OpenQA.Selenium;
using Selenium.Common;

namespace Selenium.PageObject
{
    public class LoginPage : AbstractPage
    {
        /// <summary>
        ///     コンストラクタ
        /// </summary>
        /// <param name="webDriver">ブラウザ操作用のWebDriver</param>
        public LoginPage(IWebDriver webDriver) {this.webDriver = webDriver;}

        public HomePage Login(string url, string loginId, string password)
        {
            webDriver.Url = url;
            SeleniumHelper.EnterTextById(webDriver, "username", loginId);
            SeleniumHelper.EnterTextById(webDriver, "password", password);
            SeleniumHelper.ClickById(webDriver, "login-submit");

            // 検証エラーチェック
            try
            {
                var element = webDriver.FindElement(By.Id("flash_error"));
                throw new Exception($"ログイン失敗！{element.Text}");
            }
            catch (NoSuchElementException)
            {
                // 検証エラー時にだけ存在するので無い場合に成功になる
                return new HomePage(webDriver);
            }
        }
    }
}

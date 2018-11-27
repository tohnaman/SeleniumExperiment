using OpenQA.Selenium;
using Selenium.Common;

namespace Selenium.PageObject
{
    public class TicketListPage : AbstractPage
    {
        /// <summary>
        ///     コンストラクタ
        /// </summary>
        /// <param name="webDriver">ブラウザ操作用のWebDriver</param>
        public TicketListPage(IWebDriver webDriver) {this.webDriver = webDriver;}

        public NewTicketPage MoveTicketPage()
        {
            SeleniumHelper.Click(webDriver, By.XPath("//a[@class='icon icon-add new-issue']"));
            return new NewTicketPage(webDriver);
        }
    }
}

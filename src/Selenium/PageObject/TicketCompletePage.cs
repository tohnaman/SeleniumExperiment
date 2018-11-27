using OpenQA.Selenium;
using Selenium.Common;

namespace Selenium.PageObject
{
    public class TicketCompletePage : AbstractPage
    {
        /// <summary>
        ///     コンストラクタ
        /// </summary>
        /// <param name="webDriver">ブラウザ操作用のWebDriver</param>
        public TicketCompletePage(IWebDriver webDriver) {this.webDriver = webDriver;}

        public TicketListPage SelectTicketTab()
        {
            SeleniumHelper.Click(webDriver, By.XPath("//a[@class='issues selected']"));
            return new TicketListPage(webDriver);
        }
    }
}

using OpenQA.Selenium;
using Selenium.Common;

namespace Selenium.PageObject
{
    public class ProjectPage : AbstractPage
    {
        /// <summary>
        ///     コンストラクタ
        /// </summary>
        /// <param name="webDriver">ブラウザ操作用のWebDriver</param>
        public ProjectPage(IWebDriver webDriver) {this.webDriver = webDriver;}

        public TicketListPage SelectTicketTab()
        {
            SeleniumHelper.Click(webDriver, By.XPath("//a[@class='issues']"));
            return new TicketListPage(webDriver);
        }
    }
}

using OpenQA.Selenium;
using Selenium.Common;

namespace Selenium.PageObject
{
    public class HomePage : AbstractPage
    {
        /// <summary>
        ///     コンストラクタ
        /// </summary>
        /// <param name="webDriver">ブラウザ操作用のWebDriver</param>
        public HomePage(IWebDriver webDriver) {this.webDriver = webDriver;}

        public ProjectPage SelectProject(string projectName)
        {
            SeleniumHelper.Click(webDriver, By.XPath("//span[@class='drdn-trigger']"));
            SeleniumHelper.Click(webDriver, By.XPath("//a[@title='" + projectName + "']"));

            return new ProjectPage(webDriver);
        }
    }
}

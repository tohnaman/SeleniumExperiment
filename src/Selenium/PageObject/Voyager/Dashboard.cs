using OpenQA.Selenium;

namespace Selenium.PageObject.Voyager
{
    public class Dashboard : AbstractPage
    {
        /// <summary>
        ///     コンストラクタ
        /// </summary>
        /// <param name="webDriver">ブラウザ操作用のWebDriver</param>
        public Dashboard(IWebDriver webDriver) {this.webDriver = webDriver;}
    }
}

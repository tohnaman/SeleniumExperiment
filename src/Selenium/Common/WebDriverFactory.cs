using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace Selenium.Common
{
    public class WebDriverFactory
    {
        public static IWebDriver CreateInstance(AppSettings.BrowserName browserName)
        {
            switch (browserName)
            {
                case AppSettings.BrowserName.None:
                    throw new ArgumentException(string.Format("Not Definition. BrowserName:{0}", browserName));

                case AppSettings.BrowserName.Chrome:
                    return new ChromeDriver();

                case AppSettings.BrowserName.Firefox:
                    var driverService = FirefoxDriverService.CreateDefaultService();
                    driverService.FirefoxBinaryPath = @"D:\Tools\MozillaFirefox\firefox.exe";
                    driverService.HideCommandPromptWindow = true;
                    driverService.SuppressInitialDiagnosticInformation = true;
                    return new FirefoxDriver(driverService);

                case AppSettings.BrowserName.InternetExplorer:
                    return new InternetExplorerDriver();

                case AppSettings.BrowserName.Edge:
                    return new EdgeDriver();

                default:
                    throw new ArgumentException(string.Format("Not Definition. BrowserName:{0}", browserName));
            }
        }
    }
}

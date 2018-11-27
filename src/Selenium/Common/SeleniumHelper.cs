using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Selenium.Common
{
    public static class SeleniumHelper
    {
        #region 要素取得
        /// <summary>
        ///     id指定で要素取得
        /// </summary>
        /// <param name="webDriver">ブラウザ操作用のWebDriver</param>
        /// <param name="id">操作する要素のid</param>
        /// <returns></returns>
        public static IWebElement FindElementById(IWebDriver webDriver, string id)
        {
            return FindElement(webDriver, By.Id(id));
        }

        /// <summary>
        ///     要素取得
        /// </summary>
        /// <param name="webDriver">ブラウザ操作用のWebDriver</param>
        /// <param name="by"></param>
        /// <returns></returns>
        public static IWebElement FindElement(IWebDriver webDriver, By by)
        {
            try
            {
                return webDriver.FindElement(by);
            }
            catch (NoSuchElementException e)
            {
                throw new Exception($"取得できません[{by}]", e);
            }
        }
        #endregion

        #region 文字入力操作
        /// <summary>
        ///     id指定で文字入力を実施
        /// </summary>
        /// <param name="webDriver">ブラウザ操作用のWebDriver</param>
        /// <param name="id">操作する要素のid</param>
        /// <param name="text">入力する値</param>
        public static void EnterTextById(IWebDriver webDriver, string id, string text)
        {
            EnterText(webDriver, By.Id(id), text);
        }

        /// <summary>
        ///     文字入力を実施
        /// </summary>
        /// <param name="webDriver">ブラウザ操作用のWebDriver</param>
        /// <param name="by"></param>
        /// <param name="text">入力する値</param>
        public static void EnterText(IWebDriver webDriver, By by, string text)
        {
            FindElement(webDriver, by).SendKeys(text);
        }
        #endregion

        #region クリック操作
        /// <summary>
        ///     id指定でクリックを実施
        /// </summary>
        /// <param name="webDriver">ブラウザ操作用のWebDriver</param>
        /// <param name="id">操作する要素のid</param>
        public static void ClickById(IWebDriver webDriver, string id)
        {
            Click(webDriver, By.Id(id));
        }

        /// <summary>
        ///     クリックを実施
        /// </summary>
        /// <param name="webDriver">ブラウザ操作用のWebDriver</param>
        /// <param name="by"></param>
        public static void Click(IWebDriver webDriver, By by)
        {
            FindElement(webDriver, by).Click();
        }
        #endregion

        #region プルダウン操作
        /// <summary>
        ///     id指定でプルダウン操作を実施
        /// </summary>
        /// <param name="webDriver"></param>
        /// <param name="id"></param>
        /// <param name="viewText"></param>
        public static void SelectByTextById(IWebDriver webDriver, string id, string viewText)
        {
            SelectByText(webDriver, By.Id(id), viewText);
        }

        /// <summary>
        /// </summary>
        /// <param name="webDriver"></param>
        /// <param name="by"></param>
        /// <param name="viewText"></param>
        public static void SelectByText(IWebDriver webDriver, By by, string viewText)
        {
            var element = FindElement(webDriver, by);
            try
            {
                var selectElement = new SelectElement(element);
                selectElement.SelectByText(viewText);
            }
            catch (NoSuchElementException e)
            {
                throw new Exception($"Select要素への変換に失敗しました[{by}]", e);
            }
        }
        #endregion
    }
}

using System;
using OpenQA.Selenium;
using Selenium.Common;

namespace Selenium.PageObject
{
    public class NewTicketPage : AbstractPage
    {
        /// <summary>
        ///     コンストラクタ
        /// </summary>
        /// <param name="webDriver">ブラウザ操作用のWebDriver</param>
        public NewTicketPage(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        public void SelectTracker(Enums.Tracker tracker)
        {
            SeleniumHelper.SelectByTextById(webDriver, "issue_tracker_id", tracker.GetDescription());
        }

        public void EnterSubject(string subject) {SeleniumHelper.EnterTextById(webDriver, "issue_subject", subject);}

        public void EnterDescription(string description)
        {
            SeleniumHelper.EnterTextById(webDriver, "issue_description", description);
        }

        public void SelectPriority(Enums.Priority priority)
        {
            SeleniumHelper.SelectByTextById(webDriver, "issue_priority_id", priority.GetDescription());
        }

        public void SelectAssignedTo(Enums.AssignedTo assignedTo)
        {
            SeleniumHelper.SelectByTextById(webDriver, "issue_assigned_to_id", assignedTo.GetDescription());
        }

        public TicketCompletePage CreateClick()
        {
            SeleniumHelper.Click(webDriver, By.XPath("//input[@name='commit']"));

            // 検証エラーチェック
            try
            {
                var element = webDriver.FindElement(By.Id("errorExplanation"));
                throw new Exception($"チケット登録失敗！{element.Text}");
            }
            catch (NoSuchElementException)
            {
                // 検証エラー時にだけ存在するので無い場合に成功になる
                return new TicketCompletePage(webDriver);
            }
        }
    }
}

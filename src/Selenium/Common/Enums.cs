using System.ComponentModel;

namespace Selenium.Common
{
    public class Enums
    {
        public enum AssignedTo
        {
            [Description("")]
            Blank,

            [Description("<< 自分 >>")]
            Myself,

            [Description("ユーザー１ 一般")]
            User1,

            [Description("ユーザー２ 一般")]
            User2,

            [Description("ユーザー３ 一般")]
            User3
        }

        public enum Priority
        {
            [Description("低め")]
            Low,

            [Description("通常")]
            Normal,

            [Description("高め")]
            High,

            [Description("急いで")]
            Urgent,

            [Description("今すぐ")]
            Immediate
        }

        public enum Tracker
        {
            [Description("バグ")]
            Bug,

            [Description("機能")]
            Feature,

            [Description("サポート")]
            Support
        }
    }
}

using AudenQATest.Context;

namespace AudenUITest.Common
{
    public class CommonActions
    {
        readonly BrowserContext _browserContext;
        public CommonActions(BrowserContext browserContext)
        {
            _browserContext = browserContext;
        }
        public void NavigateToUrl(string url)
        {
            _browserContext.WebDriver.Navigate().GoToUrl(url);
            _browserContext.WebDriver.Manage().Window.Maximize();
        }
    }
}
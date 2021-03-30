using AudenQATest.Context;
using AudenUITest.PageObjects;
using OpenQA.Selenium;

namespace AudenQATest.Common.Components
{
    public class CookieBannerComponent
    {
        private BrowserContext _browserContext;
        public CookieBannerComponent(BrowserContext browserContext)
        {
            _browserContext = browserContext;
        }
        
        public void AcceptAll()
        {
            _browserContext.WebDriver.SwitchTo().Window("");
            _browserContext.WebDriver.FindElement(new CookieBannerPage().AcceptAllButton);
            _browserContext.WebDriver.SwitchTo().DefaultContent();
        }
    }
}
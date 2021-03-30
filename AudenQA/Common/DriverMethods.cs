using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AudenQATest.Common
{
    public static class DriverMethods
    {
        public static void WaitForPageToLoad(this IWebDriver webDriver, TimeSpan? timeout = null)
        {
            timeout = timeout ?? TimeSpan.FromSeconds(5);
            var waitForDocumentReady = new WebDriverWait(webDriver, timeout.GetValueOrDefault())
            {
                PollingInterval = TimeSpan.FromMilliseconds(200)
            };

        }
    }
}


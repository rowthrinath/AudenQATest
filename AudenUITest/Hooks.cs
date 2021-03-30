using AudenQATest.Common.Components;
using AudenQATest.Context;
using AudenUITest.PageObjects;
using TechTalk.SpecFlow;

namespace AudenUITest
{
    [Binding]
    public sealed class Hooks
    {
        private readonly BrowserContext _browserContext;
        
        public Hooks(BrowserContext browserContext)
        {
            _browserContext = browserContext;
            
        }

        [BeforeScenario(Order = 1)]
        public void BeforeScenario()
        {
            
        }

        [AfterScenario(Order = 1)]
        public void AfterScenario()
        {
            _browserContext.WebDriver.Quit();
        }
        
    }
}

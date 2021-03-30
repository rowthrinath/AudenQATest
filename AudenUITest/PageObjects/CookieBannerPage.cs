using OpenQA.Selenium;

namespace AudenUITest.PageObjects
{
    public class CookieBannerPage
    {
        public By AcceptAllButton => By.Id("consent_prompt_submit");
    }
}
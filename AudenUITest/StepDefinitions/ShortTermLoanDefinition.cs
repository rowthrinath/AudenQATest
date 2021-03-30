using System;
using System.Threading;
using AudenQATest.Common;
using AudenQATest.Common.Components;
using AudenQATest.Config;
using AudenQATest.Context;
using AudenUITest.Assertions;
using AudenUITest.Common;
using AudenUITest.Context;
using AudenUITest.Libraries;
using AudenUITest.PageObjects;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using TechTalk.SpecFlow;

namespace AudenQATest.StepDefinitions
{
    [Binding]
    public class ShortTermLoanDefinition
    {
        private readonly BrowserContext _browserContext;
        private readonly ShortTermLoanAmountPage _shortTermLoanAmountPage;
        private AssertShortTermLoan _assertShortTermLoan;
        private readonly CookieBannerComponent _cookieBannerComponent;
        private readonly ShortTermLoanActions _shortTermLoanActions;
        private readonly CommonActions _commonActions;
        private readonly CommonContext _commonContext;

        public ShortTermLoanDefinition(BrowserContext browserContext, CommonContext commonContext)
        {
            _browserContext = browserContext;
            _commonContext = commonContext;
            _shortTermLoanAmountPage = new ShortTermLoanAmountPage(_browserContext);
            _assertShortTermLoan = new AssertShortTermLoan(_browserContext);
            _cookieBannerComponent = new CookieBannerComponent(_browserContext);
            _shortTermLoanActions = new ShortTermLoanActions(_browserContext);
            _commonActions = new CommonActions(_browserContext);
        }
        
        
        [Given(@"I am able to launch credit short term loan portal")]
        public void GivenIAmAbleToLaunchCreditShortTermLoanPortal()
        {
            _commonActions.NavigateToUrl(_commonContext.url);
            _cookieBannerComponent.AcceptAll();
        }

        [Then(@"I can see the selected amount in the loan section")]
        public void ThenICanSeeTheSelectedAmountInTheLoanSection()
        {
            _assertShortTermLoan.CheckToSeeIfLoanSectionHasValue();
        }

        [Then(@"I can see that the select slider amount matches with the amount in the loan section")]
        public void ThenICanSeeThatTheSelectSliderAmountMatchesWithTheAmountInTheLoanSection()
        {
            _assertShortTermLoan.CompareSliderAmountAndLoanSectionAmount();
        }

        [When(@"I select (.*) loan amount using the slider")]
        public void WhenISelect200LoanAmountUsingTheSlider(decimal loanAmount)
        {
            _shortTermLoanActions.SelectLoanAmountInSlider(loanAmount);
        }

        [Then(@"I can see the minimum amount to be (.*)")]
        public void ThenICanSeeTheMinimumAmountToBe(decimal amountToCheck)
        {
            _assertShortTermLoan.VerifyIfTheSelectedAmountInSliderMatchesInSections(amountToCheck);
        }

        [When(@"I select a weekend as the repayment date (\d+)")]
        public void WhenISelectAWeekendAsTheRepaymentDate(int date)
        {
            _shortTermLoanAmountPage.SelectRepaymentDate(date);
        }

        [Then(@"I shouild see the first repayment day option be pushed back to the last working day")]
        public void ThenIShouildSeeTheFirstRepaymentDayOptionBePushedBackToTheLastWorkingDay()
        {
            _assertShortTermLoan.VerifyFirstRepaymentDate(9);
        }
    }
}
using System;
using System.Linq;
using System.Threading;
using AudenQATest.Common;
using AudenQATest.Context;
using AudenUITest.Enums;
using AudenUITest.PageObjects;
using Microsoft.VisualBasic;
using Xunit;

namespace AudenUITest.Assertions
{
    public class AssertShortTermLoan
    {
        private readonly BrowserContext _browserContext;
        private readonly ShortTermLoanAmountPage _shortTermLoanAmountPage;
        public AssertShortTermLoan(BrowserContext browserContext)
        {
            _browserContext = browserContext;
            _shortTermLoanAmountPage = new ShortTermLoanAmountPage(_browserContext);
        }
        
        public void CompareSliderAmountAndLoanSectionAmount()
        {
            DriverMethods.WaitForElementToBeVisible(_browserContext.WebDriver, _shortTermLoanAmountPage.LoanDisplaySection, (int) TimeOut.Duration);
            var loanAmountSection = $"{_browserContext.WebDriver.FindElement(_shortTermLoanAmountPage.LoanDisplaySection).Text}00";
            var sliderSelectionAmount = _browserContext.WebDriver
                .FindElement(_shortTermLoanAmountPage.SelectedSliderLoanAmount).Text;
            Assert.Equal(sliderSelectionAmount, loanAmountSection);
        }

        public void VerifyIfTheSelectedAmountInSliderMatchesInSections(decimal amountToCheck)
        {
            DriverMethods.WaitForElementToBeVisible(_browserContext.WebDriver, _shortTermLoanAmountPage.LoanDisplaySection, (int) TimeOut.Duration);
            var loanAmountSection = 
                Strings.Replace($"{_browserContext.WebDriver.FindElement(_shortTermLoanAmountPage.LoanDisplaySection).Text}00","£", "");
            var sliderSelectionAmount = _browserContext.WebDriver
                .FindElement(_shortTermLoanAmountPage.SelectedSliderLoanAmount).Text.Replace("£", "");
            amountToCheck = decimal.Parse($"{amountToCheck}.00");
                Assert.Equal(amountToCheck, decimal.Parse( loanAmountSection));
        }

        public void CheckToSeeIfLoanSectionHasValue()
        {
            DriverMethods.WaitForElementToBeVisible(_browserContext.WebDriver, _shortTermLoanAmountPage.LoanDisplaySection, (int) TimeOut.Duration);
            var loanAmount = _browserContext.WebDriver.FindElement(_shortTermLoanAmountPage.LoanDisplaySection).Text;
            Assert.NotEmpty(loanAmount);
        }

        public void VerifyFirstRepaymentDate(int dateToCheck)
        {
            DriverMethods.WaitForElementToBeVisible(_browserContext.WebDriver, _shortTermLoanAmountPage.LoanDisplaySection, (int) TimeOut.Duration);
            var firstRepayment = _browserContext.WebDriver.FindElement(_shortTermLoanAmountPage.RepaymentDate1).Text;
            var result =  firstRepayment.Contains( dateToCheck.ToString());
            Assert.True(result);
        }
    }
}
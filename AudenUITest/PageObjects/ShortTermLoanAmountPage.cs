using System;
using System.Threading;
using AudenQATest.Common.Components;
using AudenQATest.Context;
using Microsoft.VisualBasic.CompilerServices;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace AudenUITest.PageObjects
{
    public class ShortTermLoanAmountPage
    {
        private readonly BrowserContext _browserContext;
        private By LoanSelectorSlider => By.CssSelector("input[data-testid='loan-calculator-slider']");
        public By LoanDisplaySection => By.CssSelector("#root > div > div > div:nth-child(1) > div > div.right-wrapper > div.loan-calculator > section.loan-summary > div:nth-child(1) > div > span.loan-summary__column__amount__value");
        public By SelectedSliderLoanAmount => By.CssSelector("#root > div > div > div:nth-child(1) > div > div.right-wrapper > div.loan-calculator > section.loan-summary > div:nth-child(1) > div");

        public By RepaymentDayGrid =>
            By.CssSelector(
                "#root > div > div > div:nth-child(1) > div > div.right-wrapper > div.loan-calculator > section.loan-schedule.loan-schedule-wrapper-2VcYozN-sC > div > div.date-selector > span");

        public By RepaymentDate1 => By.CssSelector(
            "#root > div > div > div:nth-child(1) > div > div.right-wrapper > div.loan-calculator > section.loan-schedule.loan-schedule-wrapper-2VcYozN-sC > div > div.loan-schedule__tab__panel__detail > span.loan-schedule__tab__panel__detail__tag ");
        public By RepaymentDate2 => By.CssSelector(
            "#root > div > div > div:nth-child(1) > div > div.right-wrapper > div.loan-calculator > section.loan-schedule.loan-schedule-wrapper-2VcYozN-sC > div > div.loan-schedule__tab__panel__detail > span.loan-schedule__tab__panel__detail__tag > label:nth-child(2)");
        private IWebElement _loanSlider;
        
        public ShortTermLoanAmountPage(BrowserContext browserContext)
        {
            _browserContext = browserContext;
        }
        public void SelectLoanAmountInSlider(decimal loanAmount)
        {
            Thread.Sleep(5000);
            _loanSlider = _browserContext.WebDriver.FindElement(LoanSelectorSlider);
            var pixel = Slider.GetPixelsToMove(_loanSlider, loanAmount, 500, 200);
            Actions action = new Actions(_browserContext.WebDriver);
            action.ClickAndHold(_loanSlider)
                .MoveByOffset((-(int) _loanSlider.Size.Width /2), 0)
                .MoveByOffset(pixel, 0).Release().Perform();
        }

        public void SelectRepaymentDate(int dateToSelect)
        {
            var repaymentGrid = _browserContext.WebDriver.FindElements(RepaymentDayGrid);
            foreach (var date in repaymentGrid)
            {
                if (dateToSelect == Int32.Parse(date.Text))
                {
                    date.Click();
                    break;
                }
            }
        }
    }
}
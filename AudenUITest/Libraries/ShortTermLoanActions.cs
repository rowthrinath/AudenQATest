using AudenQATest.Context;
using AudenUITest.PageObjects;

namespace AudenUITest.Libraries
{
    public class ShortTermLoanActions
    {
        private BrowserContext _browserContext;
        private ShortTermLoanAmountPage _shortTermLoanAmountPage;
        public ShortTermLoanActions(BrowserContext browserContext)
        {
            _browserContext = browserContext;
            _shortTermLoanAmountPage = new ShortTermLoanAmountPage(_browserContext);
        }

        public void SelectLoanAmountInSlider(decimal loanAmount)
        {
            _shortTermLoanAmountPage.SelectLoanAmountInSlider(loanAmount);
        }
        
    }
}
using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using PayloadImport;

namespace ImportPayloads
{
    class PrintToHTML
    {
        private CXResponse _response;
        private CXResponse _responseOff;
        IWebDriver _driver;

        [Test, Category(Helpers.TestLevel.Consumer)]
        public void Print_To_HTML()
        {
            var payloadON = new CXPayload(Helpers.Payloads.MiniLoanLightsOn);
            var payloadOFF = new CXPayload(Helpers.Payloads.MiniLoanPrintToHTML);
            var apikey = TestContext.Parameters["apikey"];
            var secret = TestContext.Parameters["secret"];

            payloadON.InjectApiKey(apikey, secret);
            payloadOFF.InjectApiKey(apikey, secret);

            //Lights On Section
            _response = payloadON.Import(Helpers.Engines.Consumer_Stage);

            var url = _response.Uri;
            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl(url);

            _driver.FindElement(By.Id("ctl00_contentHolder_lnkInitials")).Click();

            try
            {
                _driver.FindElement(By.Id("ctl00_contentHolder_lnkYes")).Click();
            }
            catch (Exception e)
            {
                Console.WriteLine("No Existing Loan", e);
            }

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            _driver.FindElement(By.Id("ctl00_contentHolder_rptDocList_ctl00_hlWorksheet")).Click();
            _driver.SwitchTo().Window(_driver.WindowHandles[1]);

            bool check = _driver.FindElement(By.XPath("//*[@id=\"btnDisplayMode\"]")).Displayed;

            Assert.IsTrue(check);

            _driver.Quit();

            //Lights Off Section
            _responseOff = payloadOFF.Import(Helpers.Engines.Consumer_Stage);

            string docHTML = _responseOff.XmlResponse.ToString();

            Assert.IsTrue(docHTML.Contains("<DOCUMENT>"));
        }
    }
}

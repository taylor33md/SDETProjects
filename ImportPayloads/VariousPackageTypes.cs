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
    class VariousPackageTypes
    {
        private CXResponse _responseApplication;
        private CXResponse _responseClosing;
        private CXResponse _responseInitial;

        IWebDriver _driver;

        [Test, Category(Helpers.TestLevel.Consumer)]
        public void Various_Package_Types()
        {
            var payload = new CXPayload(Helpers.Payloads.MiniLoanLightsOn);
            var apikey = TestContext.Parameters["apikey"];
            var secret = TestContext.Parameters["secret"];

            payload.InjectApiKey(apikey, secret);

            //Application Package Type Test
            _responseApplication = payload.Import(Helpers.Engines.Consumer_Stage);

            var appUrl = _responseApplication.Uri;
            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl(appUrl);

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            _driver.FindElement(By.Id("ctl00_contentHolder_lnkApplication")).Click();

            try
            {
                _driver.FindElement(By.Id("ctl00_contentHolder_lnkYes")).Click();
            }
            catch (Exception e)
            {
                Console.WriteLine("No existing loan found.", e);
            }

            _driver.FindElement(By.Id("ctl00_contentHolder_DocProcessTop_lbEmail")).Click();
            _driver.FindElement(By.Id("txtTo")).SendKeys(Settings.Parameters.email);
            _driver.FindElement(By.Id("ctl00_contentHolder_lbEmailSend")).Click();
            string success = _driver.FindElement(By.Id("lblEmailInfo")).Text;
            Assert.IsTrue(success.Equals("Secure Email Link Sent Successfully"));

            _driver.Quit();

            //Closing Docs Package Type Test
            _responseClosing = payload.Import(Helpers.Engines.Consumer_Stage);

            var closingUrl = _responseClosing.Uri;
            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl(closingUrl);

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            _driver.FindElement(By.Id("ctl00_contentHolder_lnkClosing")).Click();

            try
            {
                _driver.FindElement(By.Id("ctl00_contentHolder_lnkYes")).Click();
            }
            catch (Exception e)
            {
                Console.WriteLine("No existing loan found.", e);
            }

            _driver.FindElement(By.Id("ctl00_contentHolder_DocProcessTop_lbEmail")).Click();
            _driver.FindElement(By.Id("txtTo")).SendKeys(Settings.Parameters.email);
            _driver.FindElement(By.Id("ctl00_contentHolder_lbEmailSend")).Click();
            string emailSuccess = _driver.FindElement(By.Id("lblEmailInfo")).Text;
            Assert.IsTrue(emailSuccess.Equals("Secure Email Link Sent Successfully"));

            _driver.Quit();

            //Initial Disclosures Package Type Test
            _responseInitial = payload.Import(Helpers.Engines.Consumer_Stage);

            var initialUrl = _responseInitial.Uri;
            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl(initialUrl);

            _driver.FindElement(By.Id("ctl00_contentHolder_lnkInitials")).Click();

            try
            {
                _driver.FindElement(By.Id("ctl00_contentHolder_lnkYes")).Click();
            }
            catch (Exception e)
            {
                Console.WriteLine("No existing loan found.", e);
            }

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            _driver.FindElement(By.Id("ctl00_contentHolder_DocProcessTop_lbESign")).Click();
            _driver.FindElement(By.Id("partyWizardStepBorrower")).Click();
            _driver.FindElement(By.Id("ctl00_contentHolder_partyWizardCtrl_txtNSEmailStep2party1")).SendKeys(Settings.Parameters.email);
            _driver.FindElement(By.Id("ctl00_contentHolder_partyWizardCtrl_partyWizardSend")).Click();

            try
            {
                _driver.FindElement(By.Id("ctl00_contentHolder_LinkPartyWizardYesSend")).Click();
            }
            catch (Exception e)
            {
                Console.WriteLine("No existing package found.", e);
            }

            string sentSuccess = _driver.FindElement(By.Id("ctl00_contentHolder_esignResultsSuccessHeader")).Text;
            Assert.IsTrue(sentSuccess.Contains("Packages have been successfully created for the signees listed below."));

            _driver.Quit();



        }
    }
}

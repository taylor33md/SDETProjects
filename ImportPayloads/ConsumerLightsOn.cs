﻿using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using PayloadImport;

namespace ImportPayloads
{
    class ConsumerLightsOn
    {
        private IWebDriver _driver;

        [Test, Category(Helpers.TestLevel.Consumer)]
        public void Lights_On()
        {
            Setup response = new Setup();
            string url = response.PayloadSetupURL(Helpers.Payloads.MiniLoanLightsOn);

            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl(url);

            _driver.FindElement(By.Id("ctl00_contentHolder_lnkInitials")).Click();

            try
            {
                _driver.FindElement(By.Id("ctl00_contentHolder_lnkYes")).Click();
            }
            catch (Exception e)
            {
                Console.WriteLine("No Existing Loan Found.", e);
            }

            var email = Settings.Parameters.email;

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            _driver.FindElement(By.Id("ctl00_contentHolder_DocProcessTop_lbESign")).Click();
            _driver.FindElement(By.Id("partyWizardStepBorrower")).Click();
            _driver.FindElement(By.Id("ctl00_contentHolder_partyWizardCtrl_txtNSEmailStep2party1"))
                .SendKeys(email);
            _driver.FindElement(By.Id("ctl00_contentHolder_partyWizardCtrl_partyWizardSend")).Click();


            try
            {
                _driver.FindElement(By.Id("ctl00_contentHolder_LinkPartyWizardYesSend")).Click();
            }
            catch (Exception e)
            {
                Console.WriteLine("Existing Packages not found", e);
            }

            string expectedResult = _driver.FindElement(By.Id("ctl00_contentHolder_esignResultsSuccessHeader")).Text;

            Assert.AreEqual(expectedResult, "Packages have been successfully created for the signees listed below.");
            _driver.Quit();
        }
    }
}

using System;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using PayloadImport;

namespace ImportPayloads
{
    class PrintToPDF
    {
        private CXResponse _response;
        private IWebDriver _driver;

        [Test, Category(Helpers.TestLevel.Consumer)]
        public void Print_To_PDF()
        {
            var payload = new CXPayload(Helpers.Payloads.MiniLoanLightsOn);
            var apikey = TestContext.Parameters["apikey"];
            var secret = TestContext.Parameters["secret"];

            payload.InjectApiKey(apikey, secret);

            _response = payload.Import(Helpers.Engines.Consumer_Stage);

            var url = _response.Uri;
            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl(url);

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));

            _driver.FindElement(By.Id("ctl00_contentHolder_lnkInitials")).Click();

            try
            {
                _driver.FindElement(By.Id("ctl00_contentHolder_lnkYes")).Click();
            }
            catch(Exception e)
            {
                Console.WriteLine("No existing loan available.", e);
            }

            //wait
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            _driver.FindElement(By.Id("ctl00_contentHolder_DocProcessTop_lbPDF")).Click();

            string expectedFilePath = @"C:\Users\mattaylor\Downloads\TESTTest1111_Name1.pdf";
            bool fileExists = false;

            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddUserProfilePreference("download.default_directory", @"C:\Users\mattaylor\Downloads\");

           
            wait.Until<bool>(x => fileExists = File.Exists(expectedFilePath));

            FileInfo fileInfo = new FileInfo(expectedFilePath);

            Assert.AreEqual(fileInfo.Name, "TESTTest1111_Name1.pdf");

            _driver.Quit();

        }
    }
}

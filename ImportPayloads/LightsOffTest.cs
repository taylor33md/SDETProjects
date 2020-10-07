using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PayloadImport;

namespace ImportPayloads
{
    [TestFixture]
    public class LightsOffTest
    {
        private CXResponse _response;
        private IWebDriver _driver;

        [Test, Category(Helpers.TestLevel.BasePass)]
        public void LightsOffTesting()
        {
            var payload = new CXPayload(Helpers.Payloads.MiniLoan);
            var apikey = TestContext.Parameters["apikey"];
            var secret = TestContext.Parameters["secret"];

            payload.InjectApiKey(apikey, secret);

            _response = payload.Import(Helpers.Engines.QA);

        }
        public void LightsOnTest()
        {
            var url = _response.Uri;
            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl(url);

            //Assert.IsFalse(string.IsNullOrEmpty(_response.PostId), "Iporting Lights Off Payload was unsuccessful");
        }
    }
}

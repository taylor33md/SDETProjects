using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using PayloadImport;

namespace ImportPayloads
{
    class ConsumerLightsOff
    {
        private CXResponse _response;

    [Test, Category(Helpers.TestLevel.Consumer)]
        public void Lights_Off()
        {
            var payload = new CXPayload(Helpers.Payloads.MiniLoan);
            var apikey = TestContext.Parameters["apikey"];
            var secret = TestContext.Parameters["secret"];

            payload.InjectApiKey(apikey, secret);

            _response = payload.Import(Helpers.Engines.Consumer_Stage);

            string Actual = _response.XmlResponse.ToString();

            Assert.IsTrue(Actual.Contains("<EMORTGAGE_PACKAGE"), "An error occurred when uploading the payload, test failed.");
        } 
    }
}

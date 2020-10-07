using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PayloadImport;

namespace ImportPayloads
{
    class AuthenticationRequest
    {
        private CXResponse _response;

        [Test, Category(Helpers.TestLevel.Consumer)]
        public void Authentication_Request()
        {
            var payload = new CXPayload(Helpers.Payloads.Authentication_Request);
            var apikey = TestContext.Parameters["apikey"];
            var secret = TestContext.Parameters["secret"];

            payload.InjectApiKey(apikey, secret);

            _response = payload.Import(Helpers.Engines.Consumer_Stage);

            string xmlResponse = _response.XmlResponse.ToString();
            Assert.AreEqual(xmlResponse, "<SUCCESS />", "Success not returned, test unsuccessful.");
        }
    }
}

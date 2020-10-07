using System;
using System.Security.Cryptography.X509Certificates;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PayloadImport;


namespace ImportPayloads
{
    class Loan_Program_Request
    {
        private CXResponse _response;
        public string XmlResponse;

        [Test, Category(Helpers.TestLevel.Consumer)]

        public void Loan_Import_Request()
        {
            var payload = new CXPayload(Helpers.Payloads.Loan_Program_Request);
            var apikey = TestContext.Parameters["apikey"];
            var secret = TestContext.Parameters["secret"];

            payload.InjectApiKey(apikey, secret);

            _response = payload.Import(Helpers.Engines.Consumer_Stage);
            XmlResponse = _response.XmlResponse.ToString();

            Assert.IsTrue(XmlResponse.Contains("<LOAN_PROGRAM_RESPONSE"), "Response not returned correctly, test failed.");
        }
    }
}

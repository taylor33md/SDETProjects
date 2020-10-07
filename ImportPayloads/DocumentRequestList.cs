using System;
using System.Security.Cryptography.X509Certificates;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PayloadImport;

namespace ImportPayloads
{
    class DocumentRequestList
    {
        private CXResponse _response;

        [Test, Category(Helpers.TestLevel.Consumer)]

        public void Document_Request_List()
        {
            var payload = new CXPayload(Helpers.Payloads.Document_Request_List);
            var apikey = TestContext.Parameters["apikey"];
            var secret = TestContext.Parameters["secret"];

            payload.InjectApiKey(apikey, secret);

            _response = payload.Import(Helpers.Engines.Consumer_Stage);

            string xmlResponse = _response.XmlResponse.ToString();
            Assert.IsTrue(xmlResponse.Contains("<EMORTGAGE_PACKAGE"));
        }
    }
}

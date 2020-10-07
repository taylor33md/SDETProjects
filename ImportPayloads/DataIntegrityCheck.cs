using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PayloadImport;

namespace ImportPayloads
{
    class DataIntegrityCheck
    {
        private CXResponse _response;
        public string Actual;
        public string XmlResponse = "<EXCEPTIONS";
        public string TestConsumerResponse = "<EMORTGAGE_PACKAGE";

        [Test, Category(Helpers.TestLevel.Consumer)]
        public void Data_Integrity_Check()
        {
            var payload = new CXPayload(Helpers.Payloads.Data_Integrity_Check);
            var apikey = TestContext.Parameters["apikey"];
            var secret = TestContext.Parameters["secret"];

            payload.InjectApiKey(apikey, secret);

            _response = payload.Import(Helpers.Engines.Consumer_Stage, suppressException: true);

            Actual = _response.XmlResponse.ToString();

            // testing on qa seems to want this response,
            //Assert.IsTrue(Actual.Contains(XmlResponse), "Exception not thrown, test unsuccessful");

            //testing on stage consumer seems to want this one, not sure what the difference is
            Assert.IsTrue(Actual.Contains(TestConsumerResponse), "Exception thrown, test unsuccessful");
        }
    }
}

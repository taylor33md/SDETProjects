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
    public class Setup
    {
        public CXResponse response;
        public string PayloadSetupString(string payloadLocation)
        {
            var payload = new CXPayload(payloadLocation);
            var apikey = TestContext.Parameters["apikey"];
            var secret = TestContext.Parameters["secret"];

            payload.InjectApiKey(apikey, secret);

            response = payload.Import(Helpers.Engines.Consumer_Stage);
            string xmlResponse = response.XmlResponse.ToString();

            return xmlResponse; 
        }

        public string PayloadSetupURL(string payloadLocation)
        {
            var payload = new CXPayload(payloadLocation);
            var apikey = TestContext.Parameters["apikey"];
            var secret = TestContext.Parameters["secret"];

            payload.InjectApiKey(apikey, secret);

            response = payload.Import(Helpers.Engines.Consumer_Stage);

            var url = response.Uri;

            return url;
        }

    }
}

using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PayloadImport;


namespace ImportPayloads
{
    class AuthenticationRequest
    {
        [Test, Category(Helpers.TestLevel.Consumer)]
        public void Authentication_Request()
        {
            Setup response = new Setup();
            string xml = response.PayloadSetupString(Helpers.Payloads.Authentication_Request);

            Assert.AreEqual(xml, "<SUCCESS />", "Success not returned, test unsuccessful.");
        }
    }
}

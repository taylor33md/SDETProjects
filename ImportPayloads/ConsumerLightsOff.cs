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
    [Test, Category(Helpers.TestLevel.Consumer)]
        public void Lights_Off()
        {
            Setup response = new Setup();
            string xml = response.PayloadSetupString(Helpers.Payloads.MiniLoan);

            Assert.IsTrue(xml.Contains("<EMORTGAGE_PACKAGE"), "An error occurred when uploading the payload, test failed.");
        } 
    }
}

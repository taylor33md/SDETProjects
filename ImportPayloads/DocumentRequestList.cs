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
        [Test, Category(Helpers.TestLevel.Consumer)]

        public void Document_Request_List()
        {
            Setup response = new Setup();
            string xml = response.PayloadSetupString(Helpers.Payloads.Document_Request_List);

            Assert.IsTrue(xml.Contains("<EMORTGAGE_PACKAGE"));
        }
    }
}

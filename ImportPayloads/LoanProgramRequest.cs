using System;
using System.Security.Cryptography.X509Certificates;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PayloadImport;


namespace ImportPayloads
{
    class LoanProgramRequest
    {
        [Test, Category(Helpers.TestLevel.Consumer)]

        public void Loan_Program_Request()
        {
            Setup response = new Setup();
            string xml = response.PayloadSetupString(Helpers.Payloads.Loan_Program_Request);

            Assert.IsTrue(xml.Contains("<LOAN_PROGRAM_RESPONSE"), "Response not returned correctly, test failed.");
        }
    }
}

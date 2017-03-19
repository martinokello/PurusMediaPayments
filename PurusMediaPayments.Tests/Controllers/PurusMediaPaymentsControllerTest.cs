using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;
using PurusMedia.Domain;
using PurusMedia.Services.Interfaces;
using PurusMedia.Services.Concretes;
using PurusMediaPayments.Controllers;
using Moq;
using System.Collections.Generic;

namespace PurusMediaPayments.Tests.Controllers
{
    /// <summary>
    /// Summary description for PurusDigitalControllerTest
    /// </summary>
    [TestClass]
    public class PurusMediaPaymentsControllerTest
    {
        private ICheapPaymentGateway _cheapPaymentGateway;
        private IExpensivePaymentGateway _expensivePaymentGateway;
        private IPremiumPaymentGateway _premiumPaymentGateway;
        private ValidationContext _validationContext;

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        [TestInitialize]
        public void Setup()
        {
            _cheapPaymentGateway = new CheapPaymentGateway();
            _expensivePaymentGateway = new ExpensivePaymentGateway();
            _premiumPaymentGateway = new PremiumPaymentGateway();
        }
        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void Test_BadRequest_Model_Object_Returns_403_StatusCode()
        {
            //
            // TODO: Add test logic here
            //
            var request = new RequestObject
            {
                Amount = null,
                CardHolder = "Mr. Joe Bloggs",
                CreditCardNumber = "1234567890123456",
                SecurityCode = "345",
                ExpirationDate = DateTime.Now.AddMonths(6)
            };

            var results = new List<ValidationResult>();

            _validationContext = new ValidationContext(request,
            serviceProvider: null, items: null);
            Validator.TryValidateObject(request, _validationContext, results, true);

            Assert.AreEqual("Bad Request",results[0].ErrorMessage);
        }


        [TestMethod]
        public void Test_BadRequest_Model_Object_Amount_Minus_50_Returns_403_StatusCode()
        {
            //
            // TODO: Add test logic here
            //
            var request = new RequestObject
            {
                Amount = -50,
                CardHolder = "Mr. Joe Bloggs",
                CreditCardNumber = "1234567890123456",
                SecurityCode = "345",
                ExpirationDate = DateTime.Now.AddMonths(6)
            };

            var results = new List<ValidationResult>();

            _validationContext = new ValidationContext(request,
            serviceProvider: null, items: null);
            Validator.TryValidateObject(request, _validationContext, results, true);

            Assert.AreEqual("Bad Request", results[0].ErrorMessage);
        }
        [TestMethod]
        public void Test_Valid_Model_Object_Cheap_Gateway_200_StatusCode()
        {
            //
            // TODO: Add test logic here
            // 
            //
            var request = new RequestObject
            {
                Amount = 15,
                CardHolder = "Mr. Joe Bloggs",
                CreditCardNumber = "1234567890123456",
                SecurityCode = "345",
                ExpirationDate = DateTime.Now.AddMonths(6)
            };

            var purusPayController = new PurusMediaPaymentsController(_cheapPaymentGateway, _expensivePaymentGateway, _premiumPaymentGateway);
            var response = purusPayController.ProcessPayment(request);
            Assert.AreEqual("200", response.StatusCode);
        }

        [TestMethod]
        public void Test_Valid_Model_Object_Expensive_Gateway_200_StatusCode()
        {
            //
            // TODO: Add test logic here
            //
            var request = new RequestObject
            {
                Amount = 135,
                CardHolder = "Mr. Joe Bloggs",
                CreditCardNumber = "1234567890123456",
                SecurityCode = "345",
                ExpirationDate = DateTime.Now.AddMonths(6)
            };

            var purusPayController = new PurusMediaPaymentsController(_cheapPaymentGateway, _expensivePaymentGateway, _premiumPaymentGateway);

            var response = purusPayController.ProcessPayment(request);
            Assert.AreEqual("200", response.StatusCode);
        }

        [TestMethod]
        public void Test_Valid_Model_Object_Premium_Gateway_200_StatusCode()
        {
            //
            // TODO: Add test logic here
            //
            var request = new RequestObject
            {
                Amount = 800,
                CardHolder = "Mr. Joe Bloggs",
                CreditCardNumber = "1234567890123456",
                SecurityCode = "345",
                ExpirationDate = DateTime.Now.AddMonths(6)
            };

            var purusPayController = new PurusMediaPaymentsController(_cheapPaymentGateway, _expensivePaymentGateway, _premiumPaymentGateway);

            var response = purusPayController.ProcessPayment(request);
            Assert.AreEqual("200", response.StatusCode);
        }

        [TestMethod]
        public void Test_InternalServerError_Model_Object_Expensive_NumberOfPremTries_When_4th_Reqest_Returns_500_StatusCode()
        {
            //
            // TODO: Add test logic here
            //
            var request = new RequestObject
            {
                Amount = 600,
                CardHolder = "Mr. Joe Bloggs",
                CreditCardNumber = "1234567890123456",
                SecurityCode = "345",
                ExpirationDate = DateTime.Now.AddMonths(6)
            };

            var purusPayController = new PurusMediaPaymentsController(_cheapPaymentGateway, _expensivePaymentGateway, _premiumPaymentGateway);
            purusPayController.NumberOfPremTries = 4;

            var response = purusPayController.ProcessPayment(request);
            Assert.AreEqual("500", response.StatusCode);
        }
    }
}

using System.Net;
using System.Net.Http;
using System.Web.Http;
using PurusMedia.Domain;
using PurusMedia.Services.Interfaces;

namespace PurusMediaPayments.Controllers
{
    public class PurusMediaPaymentsController : ApiController
    {
        private readonly ICheapPaymentGateway _cheapPaymentGateway;
        private readonly IExpensivePaymentGateway _expensivePaymentGateway;
        private readonly IPremiumPaymentGateway _premiumPaymentGateway;
        public int MaxNumberOfPremTries { get; set; }
        public int NumberOfPremTries { get; set; }
        public PurusMediaPaymentsController() { }
        public PurusMediaPaymentsController(ICheapPaymentGateway cheapPaymentGateway,
            IExpensivePaymentGateway expensivePaymentGateway, IPremiumPaymentGateway premiumPaymentGateway)
        {
            _cheapPaymentGateway = cheapPaymentGateway;
            _expensivePaymentGateway = expensivePaymentGateway;
            _premiumPaymentGateway = premiumPaymentGateway;
            MaxNumberOfPremTries = 3;
            NumberOfPremTries = 0;
        }
        // Post: PurusMediaPayments
        [System.Web.Http.HttpPost]
        public ResponseObject ProcessPayment(RequestObject request)
        {
            if (ModelState.IsValid)
            {
                _cheapPaymentGateway.MinValue = 0;
                _cheapPaymentGateway.MaxValue = 20;
                _expensivePaymentGateway.MinValue = 21;
                _expensivePaymentGateway.MaxValue = 500;
                _premiumPaymentGateway.MinValue = 501;
                _premiumPaymentGateway.MaxValue = decimal.MaxValue;

                ResponseObject responseObject  = _cheapPaymentGateway.ProcessPayment(request);
                if (responseObject.StatusCode != "200")
                    responseObject = _expensivePaymentGateway.ProcessPayment(request);
                if (responseObject.StatusCode != "200")
                {
                    responseObject = _cheapPaymentGateway.ProcessPayment(request);
                }

                while (responseObject.StatusCode != "200" && NumberOfPremTries < MaxNumberOfPremTries)
                {
                    responseObject = _premiumPaymentGateway.ProcessPayment(request);
                    NumberOfPremTries++;
                }
                return responseObject;
            }

            return new ResponseObject { StatusCode = "403", Message="Bad Request" };
        }
    }
}
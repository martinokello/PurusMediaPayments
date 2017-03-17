using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PurusMedia.Domain;
using PurusMedia.Services.Interfaces;

namespace PurusMedia.Services.Concretes
{
    public class ExpensivePaymentGateway:IExpensivePaymentGateway
    {
        public decimal MinValue { get; set; }
        public decimal MaxValue { get; set; }
        public ExpensivePaymentGateway()
        {
            MinValue = 21;
            MaxValue = 500;
        }
        public bool ProcessExpensivePayment(RequestObject requestObject)
        {
            return requestObject.Amount >= MinValue && requestObject.Amount< MaxValue;
        }

        public ResponseObject ProcessPayment(RequestObject requestObject)
        {            //Validate Request - if it fails: return 403: to do!!

            //otherwise:
            return ProcessExpensivePayment(requestObject)
                ? new ResponseObject
                {
                    StatusCode = "200",
                    Message = "OK"
                }
                : new ResponseObject
                {
                    StatusCode = "500",
                    Message = " internal server error"
                };
        }
    }
}

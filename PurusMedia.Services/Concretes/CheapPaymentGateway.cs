using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PurusMedia.Domain;
using PurusMedia.Services.Interfaces;

namespace PurusMedia.Services.Concretes
{
    public class CheapPaymentGateway:ICheapPaymentGateway
    {
        public CheapPaymentGateway()
        {
            MinValue = 0;
            MaxValue = 20;
        }
        public decimal MinValue { get; set; }
        public decimal MaxValue { get; set; }
        public bool ProcessCheapPayment(RequestObject requestObject)
        {
            return requestObject.Amount < MaxValue;
        }

        public ResponseObject ProcessPayment(RequestObject requestObject)
        {
            //Validate Request - if it fails: return 403: to do!!

            //otherwise:
            return ProcessCheapPayment(requestObject)
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

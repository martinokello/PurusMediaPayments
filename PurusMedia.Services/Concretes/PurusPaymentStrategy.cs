using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PurusMedia.Domain;
using PurusMedia.Services.Interfaces;

namespace PurusMedia.Services.Concretes
{
    public class PurusPaymentStrategyClient
    {
        private IPaymentGateWay _paymentGateWay;
        PurusPaymentStrategyClient(IPaymentGateWay paymentGateWay)
        {
            this._paymentGateWay = paymentGateWay;
        }

        public ResponseObject ExecutePaymentFunction(RequestObject requestObject) { 
            return  _paymentGateWay.ProcessPayment(requestObject);
        }
    }
}

using PurusMedia.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurusMedia.Services.Interfaces
{
    public interface ICheapPaymentGateway:IPaymentGateWay
    {
        bool ProcessCheapPayment(RequestObject requestObject);
    }
}

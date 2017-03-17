using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PurusMedia.Domain;

namespace PurusMedia.Services.Interfaces
{
    public interface IExpensivePaymentGateway:IPaymentGateWay
    {
        bool ProcessExpensivePayment(RequestObject requestObject);
    }
}

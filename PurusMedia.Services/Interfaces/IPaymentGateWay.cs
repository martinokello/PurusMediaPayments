using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PurusMedia.Domain;

namespace PurusMedia.Services.Interfaces
{
    public interface IPaymentGateWay
    {
        decimal MinValue { get; set; }
        decimal MaxValue { get; set; }
        ResponseObject ProcessPayment(RequestObject requestObject);
    }
}

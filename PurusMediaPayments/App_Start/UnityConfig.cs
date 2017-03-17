using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;
using System.Reflection;

namespace PurusMediaPayments
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<PurusMedia.Services.Interfaces.ICheapPaymentGateway, PurusMedia.Services.Concretes.CheapPaymentGateway>();
            container.RegisterType<PurusMedia.Services.Interfaces.IExpensivePaymentGateway, PurusMedia.Services.Concretes.ExpensivePaymentGateway>();
            container.RegisterType<PurusMedia.Services.Interfaces.IPremiumPaymentGateway, PurusMedia.Services.Concretes.PremiumPaymentGateway>();

            var types = Assembly.GetExecutingAssembly().GetTypes();

            foreach (var t in types)
            {
                if (t.IsAssignableFrom(typeof(System.Web.Http.Controllers.IHttpController)))
                {
                    container.RegisterType(t);
                }
            }
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}
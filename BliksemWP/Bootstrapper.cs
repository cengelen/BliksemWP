using BliksemWP.ViewModels;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BliksemWP
{
    public class Bootstrapper : PhoneBootstrapper
    {
        PhoneContainer container;

        protected override void Configure()
        {
            container = new PhoneContainer();

            container.RegisterPhoneServices(RootFrame);
            container.PerRequest<MainPageViewModel>();
            container.PerRequest<ResultPageViewModel>();

            AddCustomConventions();
        }

        static void AddCustomConventions()
        {
            //ellided  
        }

        protected override object GetInstance(Type service, string key)
        {
            return container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }
    }
}

using System.Collections.Generic;

namespace FBChecklist.Services
{
    interface IServicesRepository
    {
        void AddService(Service entity);

        IEnumerable<Service> GetAll();

       

        void AddServiceMonitor(ServiceMonitor entity);

    }
}

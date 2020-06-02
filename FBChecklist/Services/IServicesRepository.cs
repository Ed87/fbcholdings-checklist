using System.Collections.Generic;

namespace FBChecklist.Services
{
    interface IServicesRepository<TEntity>
    {
        void AddService(TEntity entity);

        IEnumerable<TEntity> GetAll();

    }
}

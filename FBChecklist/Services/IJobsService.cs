using System.Collections.Generic;
using System.Web.Mvc;

namespace FBChecklist.Services
{
    interface IJobsService<TEntity>
    {
        IEnumerable<TEntity> GetAll();

        void  AddJob(TEntity entity);

        void EditJob(TEntity dbentity, TEntity entity);

        TEntity GetServiceById(int id);

        IEnumerable<SelectListItem> GetFrequencies();
    }
}

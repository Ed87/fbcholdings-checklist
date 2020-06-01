using System.Collections.Generic;

namespace FBChecklist.Services
{
    public interface IDisksService<TEntity>
    {
        List<TEntity> GetEnvironmentStatistics();

        void SaveEnvironmentInfo(TEntity entity);
       
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FBChecklist.Services
{
   public interface IAsyncWebLogicService<TEntity>
    {
       Task <List<TEntity>> GetServerStatistics();
    }
}

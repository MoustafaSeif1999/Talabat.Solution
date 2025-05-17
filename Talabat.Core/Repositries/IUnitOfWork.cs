using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Repositries
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericReopositiory<TEntity> Repository<TEntity>() where TEntity : BaseEntity ;

        Task<int> Complate();

    }
}

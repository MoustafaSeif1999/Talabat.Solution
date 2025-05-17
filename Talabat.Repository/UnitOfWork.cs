using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositries;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _context;
        private Hashtable _Repositories;

        public UnitOfWork(StoreContext context)
        {
            _context = context;
        }


        public async Task<int> Complate()
            => await _context.SaveChangesAsync();


        public void Dispose()
            => _context.Dispose();

        public IGenericReopositiory<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_Repositories == null)
                _Repositories = new Hashtable();

            var Type = typeof(TEntity).Name;

            if (!_Repositories.ContainsKey(Type))
            {
                var Repo = new GenericRepository<TEntity>(_context);

                _Repositories.Add(Type, Repo);
            }

            return (IGenericReopositiory<TEntity>)_Repositories[Type];

        }
    }
}

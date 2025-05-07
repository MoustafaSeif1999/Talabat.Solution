using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Repositries;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class GenericRepository<T> : IGenericReopositiory<T> where T : BaseEntity
    {
        private readonly StoreContext _storeContext;

        public GenericRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        => await _storeContext.Set<T>().ToListAsync();

        public async Task<T> GetByIdAsync(int id)
         => await _storeContext.Set<T>().FindAsync(id);
         //=> await _storeContext.Set<T>().Where( Item => Item.Id == id).FirstOrDefaultAsync();
    }
}

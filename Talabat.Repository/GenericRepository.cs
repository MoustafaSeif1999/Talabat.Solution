using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Repositries;
using Talabat.Core.Specifications;
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


        //public async Task<IEnumerable<T>> GetAllAsync()
        //{
        //    if ( typeof(T) == typeof( Product ) )
        //    {
        //         return (IEnumerable<T>) await _storeContext.Set<Product>().Include(P=> P.ProductBrand).Include(P=> P.ProductType).ToListAsync();
        //    }
        //    else
        //    {
        //       return await _storeContext.Set<T>().ToListAsync();
        //    }
        //}


        public async Task<IReadOnlyList<T>> GetAllAsync()
        => await _storeContext.Set<T>().ToListAsync();

       

        public async Task<T> GetByIdAsync(int id)
         => await _storeContext.Set<T>().FindAsync(id);

        //=> await _storeContext.Set<T>().Where( Item => Item.Id == id).FirstOrDefaultAsync();





        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync( ISpecification<T> Spec )
        {
            return await ApplySpecifications(Spec).ToListAsync() ;
        }

        public async Task<T> GetByIdWithSpecAsync( ISpecification<T> Spec)
        {
            return await ApplySpecifications(Spec).FirstOrDefaultAsync();
        }
        
        public async Task<int> GetCountAsync(ISpecification<T> Spec)
        {
            return await ApplySpecifications(Spec).CountAsync();
        }

        private IQueryable<T> ApplySpecifications (ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_storeContext.Set<T>(),spec);
        }

    }
}

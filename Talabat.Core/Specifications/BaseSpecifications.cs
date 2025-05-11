using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class BaseSpecifications<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; set; }

        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>> ();
        public Expression<Func<T, object>> OrderByAs  {get ; set ; }
        public Expression<Func<T, object>> OrderByDes { get; set; }

        public BaseSpecifications()
        {
            
        }

        public BaseSpecifications(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }




        public void AddOrderByAs ( Expression<Func<T, object>> orderBy)
        {
            OrderByAs = orderBy;
        }
        
        
        public void AddOrderByDes ( Expression<Func<T, object>> orderByDes)
        {
            OrderByDes = orderByDes;
        }


    }
}

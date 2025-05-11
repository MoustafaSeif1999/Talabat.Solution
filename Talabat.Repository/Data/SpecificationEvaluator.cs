using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {

        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> InputQuery, ISpecification<TEntity> Spec)
        {
            var Query = InputQuery;

            if (Spec.Criteria != null)
            {
                Query = Query.Where(Spec.Criteria);
            }

            if (Spec.OrderByAs != null)
            {
                Query = Query.OrderBy(Spec.OrderByAs);
            }


            if (Spec.OrderByDes != null)
            {
                Query = Query.OrderByDescending(Spec.OrderByDes);
            }


            Query = Spec.Includes.Aggregate(Query, (CurrentQuery, IncloudExerpression)
                                           => CurrentQuery.Include(IncloudExerpression));


            return Query;
        }

    }
}

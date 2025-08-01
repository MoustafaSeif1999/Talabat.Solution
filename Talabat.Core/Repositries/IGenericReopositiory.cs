﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Core.Repositries
{
    public interface IGenericReopositiory<T> where T : BaseEntity
    {
        Task< IReadOnlyList<T> > GetAllAsync();

        Task<T> GetByIdAsync(int id);

        Task CreateAsync(T entity);

        void Update(T entity);
        void Delete(T entity);

        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> Spec);

        Task<T> GetByIdWithSpecAsync(ISpecification<T> Spec);


        Task<int> GetCountAsync(ISpecification<T> Spec);


    }
}

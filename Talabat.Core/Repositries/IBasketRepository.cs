﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Repositries
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetBasketAsync(string Basket_Id);


        Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);

        Task<bool> DeleteBasket(string Basket_Id);



    }
}

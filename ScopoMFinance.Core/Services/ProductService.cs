﻿using ScopoMFinance.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScopoMFinance.Core.Services
{
    public interface IProductService
    { 
    }

    public class ProductService : IProductService
    {
        private UnitOfWork _uow;

        public ProductService(UnitOfWork uow)
        {
            _uow = uow;
        }
    }
}
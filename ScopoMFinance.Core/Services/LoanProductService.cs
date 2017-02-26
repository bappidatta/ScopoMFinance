﻿using ScopoMFinance.Domain.Repositories;
using ScopoMFinance.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScopoMFinance.Core.Services
{
    public interface ILoanProductService
    {
        List<DropDownViewModel> GetLoanProductDropDown();
    }

    public class LoanProductService : ILoanProductService
    {
        private UnitOfWork _uow;

        public LoanProductService(UnitOfWork uow)
        {
            _uow = uow;
        }

        public List<DropDownViewModel> GetLoanProductDropDown()
        {
            var loanProductDropDown = from c in _uow.LoanProductRepository.Get(x => x.IsActive == true)
                                      select new DropDownViewModel()
                                      {
                                          Value = c.Id,
                                          Text = "(" + c.ProductCode + ") " + c.ProductName
                                      };

            return loanProductDropDown.ToList();
        }
    }
}
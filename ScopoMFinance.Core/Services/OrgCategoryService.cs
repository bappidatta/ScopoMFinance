using ScopoMFinance.Domain.Repositories;
using ScopoMFinance.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScopoMFinance.Core.Services
{
    public interface IOrgCategoryService
    {
        List<DropDownViewModel> GetOrgCategoryDropDown();
    }
    public class OrgCategoryService : IOrgCategoryService
    {
        private UnitOfWork _uow;

        public OrgCategoryService(UnitOfWork uow)
        {
            _uow = uow;
        }

        public List<DropDownViewModel> GetOrgCategoryDropDown()
        {
            var orgCategoryDropDown = from c in _uow.OrgCategoryRepository.Get()
                                 where c.IsActive == true
                                 select new DropDownViewModel()
                                 {
                                     Value = c.Id,
                                     Text = "(" + c.CategoryCode + ") " + c.CategoryName
                                 };

            return orgCategoryDropDown.ToList();
        }
    }
}

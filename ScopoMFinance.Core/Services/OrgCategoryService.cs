using ScopoMFinance.Core.Helpers;
using ScopoMFinance.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScopoMFinance.Core.Services
{
    public interface IOrgCategoryService
    {
        List<DropDownHelper> GetOrgCategoryDropDown();
    }
    public class OrgCategoryService : IOrgCategoryService
    {
        private UnitOfWork _uow;

        public OrgCategoryService(UnitOfWork uow)
        {
            _uow = uow;
        }

        public List<DropDownHelper> GetOrgCategoryDropDown()
        {
            var orgCategoryDropDown = from c in _uow.OrgCategoryRepository.Get()
                                 where c.IsActive == true && c.IsDeleted == false
                                 select new DropDownHelper()
                                 {
                                     Value = c.Id,
                                     Text = "(" + c.CategoryCode + ") " + c.CategoryName
                                 };

            return orgCategoryDropDown.ToList();
        }
    }
}

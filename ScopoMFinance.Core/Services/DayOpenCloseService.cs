using ScopoMFinance.Domain.Models;
using ScopoMFinance.Domain.Repositories;
using ScopoMFinance.Domain.ViewModels.Acc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScopoMFinance.Core.Services
{
    public interface IDayOpenCloseService 
    {
        DayOpenCloseViewModel GetDayOpenClose(int branchId);
        DayOpenCloseViewModel DayCloseRequest(int branchId, string userId);
    }

    public class DayOpenCloseService : IDayOpenCloseService
    {
        private UnitOfWork _uow;

        public DayOpenCloseService(UnitOfWork uow)
        {
            this._uow = uow;
        }

        public DayOpenCloseViewModel GetDayOpenClose(int branchId)
        {
            DayOpenCloseViewModel vm = (from c in _uow.DayOpenCloseRepository.Get()
                                        where c.BranchId == branchId
                                        orderby c.CurrentDate descending
                                        select new DayOpenCloseViewModel
                                        {
                                            Id = c.Id,
                                            ClosedAt = c.ClosedAt,
                                            CloseRequestAt = c.CloseRequestAt,
                                            IsClosed = c.IsClosed,
                                            IsCloseRequest = c.CloseRequest,
                                            OpenedAt = c.OpenedAt,
                                            SystemDate = c.CurrentDate
                                        }).FirstOrDefault();

            return vm;
        }

        public DayOpenCloseViewModel DayCloseRequest(int branchId, string userId)
        {
            DayOpenCloseViewModel vm = GetDayOpenClose(branchId);

            AccDayOpenClose model = _uow.DayOpenCloseRepository.GetById(vm.Id);
            model.CloseRequest = true;

            _uow.DayOpenCloseRepository.Update(model);
            _uow.Save();

            return GetDayOpenClose(branchId);
        }
    }
}

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
        void DayCloseRequest(int branchId, string userId);
        void CloseDay(int branchId);
        void OpenDay(int branchId);
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
                                            CloseRequestBy = c.CloseRequestBy,
                                            IsClosed = c.IsClosed,
                                            IsCloseRequest = c.CloseRequest,
                                            OpenedAt = c.OpenedAt,
                                            SystemDate = c.CurrentDate
                                        }).FirstOrDefault();

            return vm;
        }

        public void DayCloseRequest(int branchId, string userId)
        {
            DayOpenCloseViewModel vm = GetDayOpenClose(branchId);

            AccDayOpenClose model = _uow.DayOpenCloseRepository.GetById(vm.Id);
            model.CloseRequest = true;
            model.CloseRequestBy = userId;
            model.CloseRequestAt = DateTime.Now;

            _uow.DayOpenCloseRepository.Update(model);
            _uow.Save();
        }

        public void CloseDay(int branchId)
        {
            DayOpenCloseViewModel vm = GetDayOpenClose(branchId);

            AccDayOpenClose model = _uow.DayOpenCloseRepository.GetById(vm.Id);
            model.IsClosed = true;
            model.ClosedAt = DateTime.Now;

            _uow.DayOpenCloseRepository.Update(model);
            _uow.Save();
        }

        public void OpenDay(int branchId)
        {
            DayOpenCloseViewModel vm = GetDayOpenClose(branchId);

            AccDayOpenClose model = new AccDayOpenClose
            {
                BranchId = branchId,
                CurrentDate = vm.SystemDate.AddDays(1).Date,
                OpenedAt = DateTime.Now
            };

            _uow.DayOpenCloseRepository.Insert(model);
            _uow.Save();
        }
    }
}

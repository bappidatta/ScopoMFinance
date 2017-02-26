using ScopoMFinance.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScopoMFinance.Core.Services
{
    public interface IComponentService
    {

    }

    public class ComponentService : IComponentService
    {
        private UnitOfWork _uow;

        public ComponentService(UnitOfWork uow)
        {
            _uow = uow;
        }
    }
}

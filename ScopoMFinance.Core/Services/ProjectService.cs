﻿using ScopoMFinance.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScopoMFinance.Core.Services
{
    public interface IProjectService
    {

    }

    public class ProjectService : IProjectService
    {
        private UnitOfWork _uow;

        public ProjectService(UnitOfWork uow)
        {
            _uow = uow;
        }
    }
}

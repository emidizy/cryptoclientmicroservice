using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.UnitOfWork.Interfaces
{
    public interface IUnitOfWork
    {
        int SaveChanges();
        void Dispose();
    }
}

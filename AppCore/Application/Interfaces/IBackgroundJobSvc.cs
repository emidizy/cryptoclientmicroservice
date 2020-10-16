using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Application.Interfaces
{
    public interface IBackgroundJobSvc
    {
        Task CheckForTransactionUpdate();
    }
}

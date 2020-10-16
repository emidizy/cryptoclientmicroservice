using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Repository.Interfaces
{
    public interface IClientRepository
    {
        List<Clients> GetClients();
    }
}

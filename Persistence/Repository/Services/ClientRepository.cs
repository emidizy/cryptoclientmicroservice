using Persistence.Entities;
using Persistence.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Persistence.Repository.Services
{
    public class ClientRepository : Repository<Clients>, IClientRepository
    {
        public ClientRepository(DatabaseContext dbContext) : base(dbContext)
        {

        }


        public List<Clients> GetClients()
        {
            var clients = GetAllRecords().ToList();
            return clients;
        }
    }
}

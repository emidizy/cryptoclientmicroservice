using Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Application.Interfaces
{
    public interface IClientTransactions
    {
        Task<ResponseParam> UpdateTransactions(string clientId, string walletddress, string currencyType, string requestRef = null);
    }
}

using AppCore.Application.Interfaces;
using Domain.DTOs.UpdateTransactionReq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace AppCore.Application.Services
{
    public class BackgroundJobSvc : IBackgroundJobSvc
    {
        private readonly IClientTransactions _txnSvc;

        public BackgroundJobSvc(IClientTransactions clientTransactions)
        {
            _txnSvc = clientTransactions;
        }

        public async Task CheckForTransactionUpdate()
        {
            List<UpdateTransactionReq> clients = new List<UpdateTransactionReq>();
            var date = DateTime.Now;

            var clientId = $"{date.Year}{date.Month}";

            for(int i=1; i < 4; i++)
            {
                var uniqueId = Helper.GenerateUniqueId(7);

                var client = new UpdateTransactionReq()
                {
                    RequestId = uniqueId,
                    ClientId = $"UID{clientId}{i}",
                    WalletAddress = $"W{i}{date.Month}{date.Year}",
                    CurrencyType = "NGN"
                };
                clients.Add(client);

                //Poll Transaction Update for client
               var response = await _txnSvc.UpdateTransactions(client.ClientId, client.WalletAddress, client.CurrencyType, client.RequestId);

                //Do Logging....
            }
        }
    }
}

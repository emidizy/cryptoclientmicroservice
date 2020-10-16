using AppCore.Application.Interfaces;
using Broker.Clients.Interfaces;
using Broker.Events;
using Domain.Model;
using Domain.DTOs.UpdateTransactionReq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using AppCore.Shared.Interfaces;

namespace AppCore.Application.Services
{
    public class ClientTransactions : IClientTransactions
    {
        private readonly Logger _logger;
        private readonly IBroadcaster _eventPublisher;
        private readonly IResponseHandler _responseHandler;

        public ClientTransactions(Logger logger,
            IBroadcaster broadcaster,
            IResponseHandler responseHandler)
        {
            _logger = logger;
            _eventPublisher = broadcaster;
            _responseHandler = responseHandler;
        }

        public async Task<ResponseParam> UpdateTransactions(string clientId, string walletAddress, string currencyType, string requestRef)
        {
            var requestId = requestRef ?? Helper.GenerateUniqueId(7);
            try
            {
                var response = _responseHandler.InitializeResponse(requestId);

                var updateReq = new UpdateTransactionReq()
                {
                    RequestId = requestId,
                    ClientId = clientId,
                    WalletAddress = walletAddress,
                    CurrencyType = currencyType
                };

                var payload = JsonConvert.SerializeObject(updateReq);

                _logger.LogInfo($"[ClientTransactions][UpdateTransactions]=>{updateReq} | [requestId]=>{requestId}");

                await _eventPublisher.PublishPayload(payload, BrokerEvents.UpdateTransaction);

                response = _responseHandler.CommitResponse(requestId, ResponseCodes.SUCCESS, "Your request is being processed");
                return response;
            }
            catch(Exception ex)
            {
                _logger.LogError($"[ClientTransactions][UpdateTransactions]=>{ex.Message} | {JsonConvert.SerializeObject(ex.InnerException)} | [requestId]=>{requestId}");
                return _responseHandler.HandleException(requestId);
            }
           
        }
    }
}

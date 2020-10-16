using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppCore.Application.Interfaces;
using Domain.DTOs.UpdateTransactionReq;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClientMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : Controller
    {
        private readonly IClientTransactions _clientTxns;

        public TransactionsController(IClientTransactions clientTxs)
        {
            _clientTxns = clientTxs;
        }
        
        [HttpPost("update")]
        public async Task<IActionResult> CheckTransactionUpdate([FromBody] UpdateTransactionReq clientTx)
        {
            var response = await _clientTxns.UpdateTransactions(clientTx.ClientId, clientTx.WalletAddress, clientTx.CurrencyType, clientTx.RequestId);
            return Ok(response);
        }
    }
}

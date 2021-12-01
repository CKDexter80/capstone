using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TenmoServer.DAO;
using TenmoServer.Models;

namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class TransferController : ControllerBase
    {
        private readonly ITransferDao transferDao;
        private readonly IAccountDao accountDao;

        public TransferController(ITransferDao _transferDao, IAccountDao _accountDao)
        {
            transferDao = _transferDao;
            accountDao = _accountDao;
        }

        /*
         * Use case 5: View transfers
         */
        [HttpGet("view")]
        public ActionResult<IList<Transfer>> GetViewTransfers() {
            var accountId = accountDao.GetAccount(Convert.ToInt32(User.FindFirst("sub")?.Value)).AccountId;
            return Ok(transferDao.GetViewTransfers(accountId));
        }

        
        /*
         * Use case 6: Transfer details
         */
        [HttpGet("details/{transferId}")]
        public ActionResult<TransferDetails> GetTransferDetail(int transferId) {

            var accountId = accountDao.GetAccount(Convert.ToInt32(User.FindFirst("sub")?.Value)).AccountId;

            if (transferDao.GetTransferDetails(transferId, accountId) == null) {
                return BadRequest();
            } 

            return Ok(transferDao.GetTransferDetails(transferId, accountId));
        }

        [HttpPost]
        public ActionResult CreateTransfer(Transfer transfer) {
            var balance = accountDao.GetAccount(Convert.ToInt32(User.FindFirst("sub")?.Value)).Balance;
            if (transfer.Amount > balance) {
                return BadRequest();
            }

            var accountTo = accountDao.AccountChecker(transfer.AccountTo);
            if (accountTo == null) {
                return BadRequest();
            }

            var accountId = accountDao.GetAccount(Convert.ToInt32(User.FindFirst("sub")?.Value)).AccountId;
            transferDao.CreateTransfer(accountId, transfer);
            return NoContent();
        }
    }
}

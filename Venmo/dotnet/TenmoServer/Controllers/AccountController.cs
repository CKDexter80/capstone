using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TenmoServer.DAO;
using TenmoServer.Models;

namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IUserDao userDao;
        private readonly IAccountDao accountDao;

        public AccountController(IAccountDao _accountDao)
        {
            accountDao = _accountDao;
        }

        [HttpGet]
        public ActionResult<Account> GetAccount()
        {
            return Ok(accountDao.GetAccount(Convert.ToInt32(User.FindFirst("sub")?.Value)));
        }

        
        [HttpGet("balance")]
        public ActionResult<decimal> GetAccountBalance()
        {
            return Ok(accountDao.GetAccount(Convert.ToInt32(User.FindFirst("sub")?.Value)).Balance);            
        }

        [HttpPut("add")]
        public ActionResult AddToBalance(Transfer transfer)
        {
            accountDao.AddToBalance(transfer);
            return NoContent();
        }

        [HttpPut("subtract")]
        public ActionResult SubtractFromBalance(Transfer transfer)
        {
            accountDao.SubtractFromBalance(transfer);
            return NoContent();
        }
    }
}

using BankAccounts.Data;
using BankAccounts.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using BankAccounts.Models.BankAccountViewModels;
using BankAccounts.Models.TransactionViewModels;

namespace BankAccounts.Controllers {
    [Authorize]
    [Route("accounts")]
    public class BankAccountsController : Controller {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        private ApplicationDbContext _context;

        public UserManager<ApplicationUser> UserManager => _userManager;

        public BankAccountsController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context) {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [HttpGet("list")]
        // [Route("List")]
        // [RestoreModelStateFromTempData]
        public IActionResult List() {
            var userId = _userManager.GetUserId(User);
            ApplicationUser user = _context.Users.Where(u => u.Id == userId)
                                        .Include(u => u.BankAccounts)
                                        .ThenInclude(b => b.Transactions)
                                        .FirstOrDefault();
            // TempData["Review"] = model;d
            return View("List", user);
        }

        [HttpGet("{accountId:int}")]
        // [Route("{accountId: int}")]
        // [RestoreModelStateFromTempData]
        public IActionResult GetAccount(int accountId) {
            var userId = _userManager.GetUserId(User);

            // We could do this without getting the bankaccount first but eh...
            BankAccount bankAccount = _context.BankAccounts.Where(ba => ba.BankAccountId == accountId)
                                                            .Include(ba => ba.Transactions)
                                                            .FirstOrDefault();
            if (bankAccount.OwnerId == userId) {
                ViewBag.bankAccount = bankAccount;
                // TempData["Review"] = model;d
                return View("AccountDetails");
            }
            return RedirectToAction("Login", "Account", new { ReturnUrl = $@"/accounts/{accountId}" });
        }

        [HttpGet]

        public IActionResult Create() {
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateBankAccountViewModel model) {


            if (ModelState.IsValid) {
                // _userManager.GetUserName(User);
                var userId = _userManager.GetUserId(User);
                BankAccount review = new BankAccount {
                    Name = model.Name,
                    OwnerId = userId,
                    CreatedById = userId,
                    ModifiedById = userId
                };
                _context.BankAccounts.Add(review);
                _context.SaveChanges();
                System.Console.WriteLine("++++++++++++++++++++VALID+++++++++++++++++++");
                return RedirectToAction("List");
            }
            else {
                System.Console.WriteLine("--------------------NOT VALID--------------------");
                return View("Create", model);
            }
        }


        [HttpPost("{accountId:int}/transactions")]
        [ValidateAntiForgeryToken]

        public IActionResult CreateTransaction(CreateTransactionViewModel model, int accountId) {
            var userId = _userManager.GetUserId(User);

            // We could do this without getting the bankaccount first but eh...
            BankAccount bankAccount = _context.BankAccounts.Where(ba => ba.BankAccountId == accountId)
                                                            .Include(ba => ba.Transactions)
                                                            .FirstOrDefault();

            if (bankAccount.OwnerId == userId) {
                ViewBag.bankAccount = bankAccount;
                if (ModelState.IsValid) {
                    // _userManager.GetUserName(User);
                    if (model.Amount + bankAccount.Balance >= 0) {
                        Transaction trans = new Transaction {
                            Amount = model.Amount,
                            Description = model.Description,
                            BankAccountId = accountId,
                            CreatedById = userId,
                            ModifiedById = userId
                        };
                        _context.Transactions.Add(trans);
                        _context.SaveChanges();
                        System.Console.WriteLine("++++++++++++++++++++VALID+++++++++++++++++++");
                        return RedirectToAction("GetAccount", new { accountId = accountId });
                    }
                    else {
                        ModelState.AddModelError("Amount", "Transaction amount cannot make account balance less than $0.00");
                    }
                }
            }
            else {
                ModelState.AddModelError("", "You did something weird and are trying to post a transaction to an account you don't own");
            }

            System.Console.WriteLine("--------------------NOT VALID--------------------");
            return View("AccountDetails", model);

        }
    }
}

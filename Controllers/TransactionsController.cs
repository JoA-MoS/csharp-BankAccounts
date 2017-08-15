// using BankAccounts.Data;
// using BankAccounts.Models;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Mvc;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;

// using Microsoft.EntityFrameworkCore;
// using BankAccounts.Models.BankAccountViewModels;
// using BankAccounts.Models.TransactionViewModels;

// namespace BankAccounts.Controllers {
//     [Authorize]
//     [Route("accounts/{accountId:int}/transactions")]
//     public class TransactionsController : Controller {
//         private readonly UserManager<ApplicationUser> _userManager;
//         private readonly SignInManager<ApplicationUser> _signInManager;

//         private ApplicationDbContext _context;

//         public UserManager<ApplicationUser> UserManager => _userManager;

//         public TransactionsController(
//             UserManager<ApplicationUser> userManager,
//             SignInManager<ApplicationUser> signInManager,
//             ApplicationDbContext context) {
//             _userManager = userManager;
//             _signInManager = signInManager;
//             _context = context;
//         }


//         [HttpPost]
//         [ValidateAntiForgeryToken]

//         public IActionResult Create(CreateTransactionViewModel model, int accountId) {
//             var userId = _userManager.GetUserId(User);

//             // We could do this without getting the bankaccount first but eh...
//             BankAccount bankAccount = _context.BankAccounts.Where(ba => ba.BankAccountId == accountId)
//                                                             .Include(ba => ba.Transactions)
//                                                             .FirstOrDefault();

//             if (ModelState.IsValid) {
//                 // _userManager.GetUserName(User);
//                 if (model.Amount + bankAccount.Balance >= 0) {
//                     Transaction trans = new Transaction {
//                         Amount = model.Amount,
//                         Description = model.Description,
//                         BankAccountId = accountId,
//                         CreatedById = userId,
//                         ModifiedById = userId
//                     };
//                     _context.Transactions.Add(trans);
//                     _context.SaveChanges();
//                     System.Console.WriteLine("++++++++++++++++++++VALID+++++++++++++++++++");
//                     return RedirectToAction("GetAccount", "BankAccounts", new { accountId = accountId });
//                 }
//                 else {
//                     ModelState.AddModelError("Amount", "Transaction amount cannot make account balance less than $0.00");
//                 }
//             }

//             System.Console.WriteLine("--------------------NOT VALID--------------------");
//             return View("../BankAccounts/AccountDetails", model);

//         }
//     }
// }

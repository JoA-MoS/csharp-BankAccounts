using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;

namespace BankAccounts.Models {
    public class BankAccount : AuditEntity, IHaveAnOwner {
        public int BankAccountId { get; set; }
        public string Name { get; set; }
        public string OwnerId { get; set; }
        public ApplicationUser Owner { get; set; }
        public virtual List<Transaction> Transactions { get; set; } = new List<Transaction>();

        [NotMapped]
        public Decimal Balance {
            get { return Transactions.Sum(t => t.Amount); }
        }

    }
}

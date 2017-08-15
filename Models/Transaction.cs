using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;


namespace BankAccounts.Models {
    public class Transaction : AuditEntity {
        public int TransactionId { get; set; }
        public string Description { get; set; }
        public Decimal Amount { get; set; }
        public int BankAccountId { get; set; }
        public BankAccount BankAccount { get; set; }

    }
}

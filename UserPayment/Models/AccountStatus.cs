using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserPayment.Models
{
    public enum Status { New, Paid, Declined}

    public class AccountStatus
    {
        [Key]
        public int Id { get; set; }
        // id cчёта
        public int AccountId { get; set; }
        // статус счёта
        public Status Status { get; set; }
    }
}

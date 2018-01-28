namespace UserPayment.Models
{
    public enum Status { New, Paid, Declined}

    public class AccountStatus : BaseEntity
    {        
        // id cчёта
        public int AccountId { get; set; }
        // статус счёта
        public Status Status { get; set; }
    }
}

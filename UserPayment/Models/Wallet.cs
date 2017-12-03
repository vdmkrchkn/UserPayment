using System.ComponentModel.DataAnnotations;

namespace UserPayment.Models
{
    public class Wallet
    {
        public Wallet(int aUserId, double aBalance) : this()
        {
            UserId = aUserId;
            Balance = aBalance;
        }

        public Wallet(User aUser, double aBalance) : this(aUser.Id, aBalance)
        {
            User = aUser;            
        }

        public Wallet() { }

        public override string ToString()
        {
            return string.Format("[user={0}, balance={1}]", UserId, Balance);
        }

        // primary key
        [Key]
        public int Id { get; set; }
        // привязка кошелька к пользователю
        public int UserId { get; set; }
        public virtual User User { get; set; }
        // баланс
        public double Balance { get; set; }
    }
}

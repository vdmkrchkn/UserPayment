using System;
using System.ComponentModel.DataAnnotations;

namespace UserPayment.Models
{
    public class Account
    {
        public Account() { }
        //
        public Account(int aSrcWalletId, int aDstWalletId, double aPrice, DateTime aDate,
            string aComment = "") : this()
        {
            SrcWalletId = aSrcWalletId;
            DstWalletId = aDstWalletId;
            Date = aDate;
            Price = aPrice;
            Comment = aComment;
        }
        [Key]
        public int Id { get; set; }
        // id кошелька отправителя.
        [Display(Name = "Кошелёк-отправитель")]
        public int SrcWalletId { get; set; }
        // id кошелька получателя
        [Display(Name = "Кошелёк-получатель")]
        public int DstWalletId { get; set; }
        // дата
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        // сумма
        [Display(Name = "Сумма")]
        public double Price { get; set; }
        // комментарий
        public string Comment { get; set; }
        // статус
        public virtual AccountStatus Status { get; set; }
    }
}

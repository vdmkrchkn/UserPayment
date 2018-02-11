using System;
using System.ComponentModel.DataAnnotations;

namespace UserPayment.Models
{
    public class Account : BaseEntity
    {
        #region Ctors

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

        #endregion Ctors

        #region Fields		

        // id кошелька отправителя.
        [Display(Name = "Кошелёк-отправитель:"), Required]
        public int SrcWalletId { get; set; }
        // id кошелька получателя
        [Display(Name = "Кошелёк-получатель:"), Required]
        public int DstWalletId { get; set; }
        
        [DataType(DataType.Date), Display(Name = "Дата:")]
        public DateTime Date { get; set; }
        
        [Display(Name = "Сумма:"), Required, Range(.01, 10000)]
        public double Price { get; set; }
        
        [Display(Name = "Комментарий:"), MaxLength(40)]
        public string Comment { get; set; }
        
        [Display(Name = "Статус:")]
        public virtual AccountStatus Status { get; set; }
        
        #endregion Fields

        #region methods

        // валидация счёта
        public bool isValid()
		{ 
			return SrcWalletId != DstWalletId && Price > 0;
		}
#endregion
	}
}

using System;
using System.ComponentModel.DataAnnotations;

namespace UserPayment.Models
{
    public class Account : BaseEntity
    {
#region ctor
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
		#endregion
#region fields		
        // id кошелька отправителя.
        [Display(Name = "Кошелёк-отправитель"), Required]
        public int SrcWalletId { get; set; }
        // id кошелька получателя
        [Display(Name = "Кошелёк-получатель"), Required]
        public int DstWalletId { get; set; }
        // дата
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        
        [Display(Name = "Сумма"), Required]
        public double Price { get; set; }
        
		// комментарий
        public string Comment { get; set; }
        
		// статус
        public virtual AccountStatus Status { get; set; }
#endregion
#region methods
		// валидация счёта
		public bool isValid()
		{ 
			return SrcWalletId != DstWalletId && Price >= 0;
		}
#endregion
	}
}

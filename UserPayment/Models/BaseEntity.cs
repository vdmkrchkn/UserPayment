using System.ComponentModel.DataAnnotations;

namespace UserPayment.Models
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
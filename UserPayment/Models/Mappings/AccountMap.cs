using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace UserPayment.Models.Mappings
{
    public class AccountMap : EntityTypeConfiguration<Account>
    {
        public AccountMap()
        {
            ToTable("Accounts");
            HasKey(a => a.Id);

            Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(a => a.SrcWalletId).IsRequired();
            Property(a => a.DstWalletId).IsRequired();
            Property(a => a.Price).IsRequired();
        }
    }
}
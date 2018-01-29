using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace UserPayment.Models.Mappings
{
    public class AccountStatusMap : EntityTypeConfiguration<AccountStatus>
    {
        public AccountStatusMap()
        {
            ToTable("AccountStatuses");
            HasKey(@as => @as.Id);

            Property(@as => @as.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(@as => @as.AccountId).IsRequired();
            Property(@as => @as.Status).IsRequired();
        }    
    }
}
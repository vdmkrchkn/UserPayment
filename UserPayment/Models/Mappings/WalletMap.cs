using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace UserPayment.Models.Mappings
{
    public class WalletMap : EntityTypeConfiguration<Wallet>
    {
        public WalletMap()
        {
            ToTable("Wallets");
            HasKey(w => w.Id);

            Property(w => w.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(w => w.UserId).IsRequired();            
        }
    }    
}
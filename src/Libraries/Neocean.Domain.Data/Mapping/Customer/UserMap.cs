using System.Data.Entity.ModelConfiguration;

using  Neocean.Domain.Model.Customer;

namespace Neocean.Domain.Data.Mapping.Customer
{
    public partial class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            this.ToTable("User");
            this.HasKey(c => c.ID);
            this.Property(u => u.UserName).HasMaxLength(50);
            this.Property(u => u.Email).HasMaxLength(1000);
        }
    }
}

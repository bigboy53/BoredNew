using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using Bored.Model;

namespace DKD.Framework.Data.Configuration
{
    public class ManageUsersConfiguration : EntityTypeConfiguration<ManageUsers>
    {
        public ManageUsersConfiguration()
        {
            Property(t => t.UName).HasMaxLength(64).IsRequired();
            Property(t => t.Password).HasMaxLength(64).IsRequired();
            Property(t => t.AuthCode).HasMaxLength(32).IsRequired();
            Property(t => t.Email).HasMaxLength(128);
            Property(t => t.RelName).HasMaxLength(64);
            Property(t => t.Tel).HasMaxLength(64);
        }
    }
}

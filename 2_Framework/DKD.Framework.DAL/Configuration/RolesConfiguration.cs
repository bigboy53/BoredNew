using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using Bored.Model;

namespace DKD.Framework.Data.Configuration
{
    public class RolesConfiguration : EntityTypeConfiguration<Roles>
    {
        public RolesConfiguration()
        {
            Property(t => t.RoleName).HasMaxLength(64).IsRequired();
        }
    }
}

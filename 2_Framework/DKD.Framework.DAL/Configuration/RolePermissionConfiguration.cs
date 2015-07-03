using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using Bored.Model;

namespace DKD.Framework.Data.Configuration
{
    public class RolePermissionConfiguration : EntityTypeConfiguration<RolePermission>
    {
        public RolePermissionConfiguration()
        {

            Property(t => t.RPName).HasMaxLength(128).IsRequired();
            Property(t => t.RPUrl).HasMaxLength(128).IsRequired();
        }
    }
}

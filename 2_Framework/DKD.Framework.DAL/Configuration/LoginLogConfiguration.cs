using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using Bored.Model;

namespace DKD.Framework.Data.Configuration
{
    public class LoginLogConfiguration:EntityTypeConfiguration<LoginLog>
    {
        public LoginLogConfiguration()
        {
            Property(t => t.UName).HasMaxLength(128).IsRequired();
            Property(t => t.IP).HasMaxLength(128).IsRequired();
        }
    }
}

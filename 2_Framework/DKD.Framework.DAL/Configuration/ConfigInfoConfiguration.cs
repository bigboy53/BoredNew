using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using Bored.Model;

namespace DKD.Framework.Data.Configuration
{
    public class ConfigInfoConfiguration : EntityTypeConfiguration<ConfigInfo>
    {
        public ConfigInfoConfiguration()
        {
            Property(t => t.Name).HasMaxLength(250).IsRequired();
        }
    }
}

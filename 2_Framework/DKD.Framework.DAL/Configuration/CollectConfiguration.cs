using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using Bored.Model;

namespace DKD.Framework.Data.Configuration
{
    public class CollectConfiguration:EntityTypeConfiguration<Collect>
    {
        public CollectConfiguration()
        {
            Property(t => t.Title).HasMaxLength(200).IsRequired();
            Property(t => t.Url).HasMaxLength(200).IsRequired();
            Property(t => t.UserName).HasMaxLength(200).IsRequired();
            Property(t => t.Image).HasMaxLength(120);
        }
    }
}

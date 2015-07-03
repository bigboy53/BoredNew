using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using Bored.Model;

namespace DKD.Framework.Data.Configuration
{
    public class GameConfiguration : EntityTypeConfiguration<Game>
    {
        public GameConfiguration()
        {
            Property(t => t.Title).HasMaxLength(200).IsRequired();
            Property(t => t.Image).HasMaxLength(100);
            Property(t => t.Url).HasMaxLength(200).IsRequired();
            Property(t => t.Source).HasMaxLength(120);
            Property(t => t.Tags).HasMaxLength(250);
        }
    }
}

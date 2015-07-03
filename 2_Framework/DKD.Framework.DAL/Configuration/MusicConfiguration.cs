using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using Bored.Model;

namespace DKD.Framework.Data.Configuration
{
    public class MusicConfiguration : EntityTypeConfiguration<Music>
    {
        public MusicConfiguration()
        {
            Property(t => t.Song).HasMaxLength(60).IsRequired();
            Property(t => t.Songer).HasMaxLength(60).IsRequired();
            Property(t => t.Image).HasMaxLength(120);
            Property(t => t.Url).HasMaxLength(120).IsRequired();
            Property(t => t.Source).HasMaxLength(120);
            Property(t => t.Tags).HasMaxLength(250);
        }
    }
}

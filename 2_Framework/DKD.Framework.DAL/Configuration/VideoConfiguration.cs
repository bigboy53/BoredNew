using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using Bored.Model;

namespace DKD.Framework.Data.Configuration
{
    public class VideoConfiguration : EntityTypeConfiguration<Video>
    {
        public VideoConfiguration()
        {
            Property(t => t.Title).HasMaxLength(128).IsRequired();
            Property(t => t.Source).HasMaxLength(120);
            Property(t => t.Image).HasMaxLength(64);
            Property(t => t.Url).HasMaxLength(150).IsRequired();
            Property(t => t.Description).HasMaxLength(250);
            Property(t => t.Tags).HasMaxLength(250);
        }
    }
}

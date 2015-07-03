using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using Bored.Model;

namespace DKD.Framework.Data.Configuration
{
    public class ArticleConfiguration : EntityTypeConfiguration<Article>
    {
        public ArticleConfiguration()
        {
            Property(t => t.ID).IsRequired();
            Property(t => t.Title).HasMaxLength(120);
            Property(t => t.Content).HasColumnType("ntext");
            Property(t => t.Source).HasMaxLength(120);
            Property(t => t.Tags).HasMaxLength(250);
        }
    }
}

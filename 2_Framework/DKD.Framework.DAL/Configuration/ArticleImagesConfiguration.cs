using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using Bored.Model;

namespace DKD.Framework.Data.Configuration
{
    public class ArticleImagesConfiguration:EntityTypeConfiguration<ArticleImages>
    {
        public ArticleImagesConfiguration(DbModelBuilder modelBuilder)
        {
            Property(t => t.ID).IsRequired();
            modelBuilder.Entity<ArticleImages>().HasRequired(t => t.Article).WithMany(t => t.ArticleImages).HasForeignKey(t => t.AID);
            Property(t => t.URL).HasMaxLength(120).IsRequired();
        }
    }
}

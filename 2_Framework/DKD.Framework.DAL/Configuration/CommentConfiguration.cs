using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using Bored.Model;

namespace DKD.Framework.Data.Configuration
{
    public class CommentConfiguration:EntityTypeConfiguration<Comment>
    {
        public CommentConfiguration()
        {
            Property(t => t.Title).HasMaxLength(200).IsRequired();
            Property(t => t.Url).HasMaxLength(200);
            Property(t => t.Image).HasMaxLength(64);
            Property(t => t.UserName).HasMaxLength(64);
            Property(t => t.Content).HasColumnType("ntext");
        }
    }
}

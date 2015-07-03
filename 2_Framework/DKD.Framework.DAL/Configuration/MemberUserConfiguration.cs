using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using Bored.Model;

namespace DKD.Framework.Data.Configuration
{
    public class MemberUserConfiguration:EntityTypeConfiguration<MemberUser>
    {
        public MemberUserConfiguration()
        {
            Property(t => t.UName).HasMaxLength(64).IsRequired();
            Property(t => t.Password).HasMaxLength(64).IsRequired();
            Property(t => t.QQ).HasColumnType("char").HasMaxLength(20);
            Property(t => t.Email).HasMaxLength(128);
            Property(t => t.Image).HasMaxLength(128);
        }
    }
}

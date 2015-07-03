using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Bored.Model;
using DKD.Framework.Data.Configuration;
using DKD.Framework.Data.Infrastructure;

namespace DKD.Framework.Data
{
    public class BoredEntities : DbContextBase
    {
        public BoredEntities()
            : base("name=Entities")
        {

        }

        public virtual DbSet<Article> Article { get; set; }
        public virtual DbSet<ArticleImages> ArticleImages { get; set; }
        public virtual DbSet<Collect> Collect { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<ConfigInfo> ConfigInfo { get; set; }
        public virtual DbSet<Game> Game { get; set; }
        public virtual DbSet<LoginLog> LoginLog { get; set; }
        public virtual DbSet<ManageUsers> ManageUsers { get; set; }
        public virtual DbSet<MemberUser> MemberUser { get; set; }
        public virtual DbSet<Music> Music { get; set; }
        public virtual DbSet<Recommended> Recommended { get; set; }
        public virtual DbSet<RolePermission> RolePermission { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Video> Video { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<IncludeMetadataConvention>();

            modelBuilder.Configurations.Add(new ArticleConfiguration());
            modelBuilder.Configurations.Add(new ArticleImagesConfiguration(modelBuilder));
            modelBuilder.Configurations.Add(new CollectConfiguration());
            modelBuilder.Configurations.Add(new CommentConfiguration());
            modelBuilder.Configurations.Add(new ConfigInfoConfiguration());
            modelBuilder.Configurations.Add(new GameConfiguration());
            modelBuilder.Configurations.Add(new LoginLogConfiguration());
            modelBuilder.Configurations.Add(new ManageUsersConfiguration());
            modelBuilder.Configurations.Add(new MemberUserConfiguration());
            modelBuilder.Configurations.Add(new MusicConfiguration());
            modelBuilder.Configurations.Add(new RecommendedConfiguration());
            modelBuilder.Configurations.Add(new RolePermissionConfiguration());
            modelBuilder.Configurations.Add(new RolesConfiguration());
            modelBuilder.Configurations.Add(new VideoConfiguration());
        }

    }
}

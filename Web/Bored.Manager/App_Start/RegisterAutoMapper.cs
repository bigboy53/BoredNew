using AutoMapper;
using Bored.Model;
using Manage.ViewModel;

namespace Bored.Manager
{
    public static class RegisterAutoMapper
    {
        public static void Excute()
        {
            Mapper.CreateMap<ManageUsers, ManageUsersDto>();
            Mapper.CreateMap<ManageUsersDto, ManageUsers>();
            Mapper.CreateMap<Roles, RolesDto>();
            Mapper.CreateMap<RolesDto, Roles>();
            Mapper.CreateMap<RolePermission, RolePermissionDto>();
            Mapper.CreateMap<RolePermissionDto, RolePermission>();
            Mapper.CreateMap<Music, MusicDto>();
            Mapper.CreateMap<MusicDto, Music>();
            Mapper.CreateMap<Game, GameDto>();
            Mapper.CreateMap<GameDto, Game>();
            Mapper.CreateMap<ConfigInfo, ConfigInfoDto>();
            Mapper.CreateMap<ConfigInfoDto, ConfigInfo>();
            Mapper.CreateMap<Article, ArticleDto>();
            Mapper.CreateMap<ArticleDto, Article>();
            Mapper.CreateMap<ArticleImages, ArticleImagesDto>();
            Mapper.CreateMap<ArticleImagesDto, ArticleImages>();
            Mapper.CreateMap<Video, VideoDto>();
            Mapper.CreateMap<VideoDto, Video>();
        }
    }
}

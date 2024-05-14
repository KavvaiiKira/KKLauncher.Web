using AutoMapper;
using KKLauncher.DB.Entities;
using KKLauncher.Web.Contracts.Apps;

namespace KKLauncher.Web.Server.Mappers
{
    public class AppMapper : Profile
    {
        public AppMapper()
        {
            CreateMap<AppEntity, AppDto>();
            CreateMap<AppDto, AppEntity>();
        }
    }
}

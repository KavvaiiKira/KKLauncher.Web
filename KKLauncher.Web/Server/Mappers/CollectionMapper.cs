using AutoMapper;
using KKLauncher.DB.Entities;
using KKLauncher.Web.Contracts.Collections;

namespace KKLauncher.Web.Server.Mappers
{
    public class CollectionMapper : Profile
    {
        public CollectionMapper()
        {
            CreateMap<CollectionEntity, CollectionDto>();
            CreateMap<CollectionDto, CollectionEntity>();

            CreateMap<CollectionEntity, CollectionViewDto>();
        }
    }
}

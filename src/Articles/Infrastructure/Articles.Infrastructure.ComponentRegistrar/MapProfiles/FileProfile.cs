using Articles.Contracts.Files;
using AutoMapper;
using File = Articles.Domain.Entities.File;

namespace Articles.Infrastructure.ComponentRegistrar.MapProfiles
{
    public class FileProfile : Profile
    {
        public FileProfile()
        {
            CreateMap<File, FileInfoDto>(MemberList.None);

            CreateMap<File, FileDto>(MemberList.None);

            CreateMap<FileDto, File>(MemberList.None)
                .ForMember(s => s.Length, map => map.MapFrom(s => s.Content.Length))
                .ForMember(s => s.CreatedAt, map => map.MapFrom(s => DateTime.UtcNow));
        }
    }
}

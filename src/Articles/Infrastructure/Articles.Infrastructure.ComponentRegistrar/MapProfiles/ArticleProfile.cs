using Articles.Contracts.Articles;
using Articles.Domain.Entities;
using AutoMapper;

namespace Articles.Infrastructure.ComponentRegistrar.MapProfiles
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<CreateArticleDto, Article>(MemberList.None)
                .ForMember(s => s.CreatedAt, map => map.MapFrom(s => DateTime.UtcNow))
                .ForMember(s => s.User, map => map.MapFrom(s => s));

            CreateMap<CreateArticleDto, User>(MemberList.None)
                .ForMember(s => s.CreatedAt, map => map.MapFrom(s => DateTime.UtcNow))
                .ForMember(s => s.Name, map => map.MapFrom(s => s.UserName))
                .AfterMap((s, d, context) =>
                {
                    if (s.UserName == "Петя")
                    {
                        d.Name = "Петр";
                    }
                });

            CreateMap<Article, ArticleDto>(MemberList.None)
                .ForMember(s => s.UserName, map => map.MapFrom(s => s.User.Name));            
        }
    }
}

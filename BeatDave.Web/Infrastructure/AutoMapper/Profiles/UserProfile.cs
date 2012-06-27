using AutoMapper;
using BeatDave.Domain;
using BeatDave.Web.Areas.Api_v1.Models;

namespace BeatDave.Web.Infrastructure
{
    public class UserProfile : Profile
    {
        protected override void Configure()
        {
            //
            // UserInput -> User
            //
            Mapper.CreateMap<UserInput, User>()
                .ForMember( t=> t.Id, o => o.MapFrom(s => "users\\" + s.Username))
                .ForMember(t => t.AspNetId, o => o.Ignore())
                .ForMember(t => t.Friends, o => o.Ignore())
                .ForMember(t => t.SocialNetworkAccounts, o => o.Ignore());

            //
            // User -> UserView
            //
            Mapper.CreateMap<User, UserView>()
                .ForMember(t => t.Username, o => o.MapFrom(s => RavenIdResolver.ResolveToString("users", s.Id)))
                .ForMember(t => t.FullName, o => o.Ignore());

            Mapper.CreateMap<Friend, UserView.FriendView>()
                .ForMember(t => t.FriendUsername, o => o.MapFrom(s => RavenIdResolver.Resolve(s.FriendUsername)))
                .ForMember(t => t.FriendFullName, o => o.Ignore());
        }
    }
}

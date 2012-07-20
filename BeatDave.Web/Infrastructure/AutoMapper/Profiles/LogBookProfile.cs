
using System.Linq;
using System.Security.Principal;
using AutoMapper;
using BeatDave.Domain;
using BeatDave.Web.Areas.Api_v1.Models;

namespace BeatDave.Web.Infrastructure
{
    public class LogBookProfile : Profile
    {
        protected override void Configure()
        {
            // TODO: Find out how to inject IPrincipal into this

            //
            // LogBookInput -> Model
            //
            Mapper.CreateMap<LogBookInput, LogBook>()
                .ForMember(t => t.Id, o => o.Ignore())
                .ForMember(t => t.AutoShareOn, o => o.Ignore())
                .ForMember(t => t.OwnerId, o => o.MapFrom(s => ((IPrincipal)ShortConversation.Data[AppConstants.UserKey]).Identity.Name))
                .ForMember(t => t.AutoShareOn, o => o.Ignore());

            Mapper.CreateMap<LogBookInput.UnitsInput, Units>();

            Mapper.CreateMap<EntryInput, Entry>()
                .ForMember(t => t.Id, o => o.Ignore())
                .ForMember(t => t.OccurredOn, o => o.MapFrom(s => s.OccurredOn.ToUniversalTime()))                
                .ForMember(t => t.LogBook, o => o.Ignore());

            Mapper.CreateMap<CommentInput, Comment<Entry>>()
                .ForMember(t => t.Id, o => o.Ignore())
                .ForMember(t => t.CommentOn, o => o.Ignore())
                .ForMember(t => t.CreatedBy, o => o.MapFrom(s => ((IPrincipal)ShortConversation.Data[AppConstants.UserKey]).Identity.Name))
                .ForMember(t => t.CreatedOn, o => o.Ignore());

            //
            // Model -> LogBookView
            //
            Mapper.CreateMap<LogBook, LogBookView>()
                .ForMember(t => t.Id, o => o.MapFrom(s => RavenIdResolver.Resolve(s.Id)))
                .ForMember(t => t.Owner, o => o.Ignore());  // TODO: Ouput the appropriate value based on Visibility

            Mapper.CreateMap<Units, LogBookView.UnitsView>();

            Mapper.CreateMap<Entry, LogBookView.EntryView>()
                .ForMember(t => t.OccurredOn, o => o.MapFrom(s => DateTimeViewResolver.Resolve(s.OccurredOn)));

            Mapper.CreateMap<Comment, LogBookView.CommentView>()
                .ForMember(t => t.CreatedOn, o => o.MapFrom(s => DateTimeViewResolver.Resolve(s.CreatedOn)));

            Mapper.CreateMap<ISocialNetworkAccount, LogBookView.SocialNetworkAccountView>()
                .ForMember(t => t.NetworkName, o => o.MapFrom(s => s.SocialNetworkName));

            Mapper.CreateMap<User, LogBookView.OwnerView>()
                .ForMember(t => t.OwnerUsername, o => o.MapFrom(s => s.Id.Split('/').Last()))
                .ForMember(t => t.OwnerFullName, o => o.MapFrom(s => (s.FirstName + " " + s.LastName).Trim()));
        }
    }
}
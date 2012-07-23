
using System;
using System.Security.Principal;
using AutoMapper;
using BeatDave.Domain;
using BeatDave.Web.Areas.Api_v1.Models;
using BeatDave.Web.Infrastructure;

namespace BeatDave.Web.Areas.Api_v1.Mappings
{
    public class LogBookProfile : Profile
    {
        // Instance Variables
        private readonly Func<IPrincipal> _getUser;


        // C'tor
        public LogBookProfile(Func<IPrincipal> getUser)
        {
            _getUser = getUser;
        }


        // Profile Overrides
        protected override void Configure()
        {
            //
            // LogBookInput -> Model
            //
            Mapper.CreateMap<LogBookInput, LogBook>()
                .ForMember(t => t.Id, o => o.Ignore())
                .ForMember(t => t.AutoShareOn, o => o.Ignore())
                .ForMember(t => t.OwnerId, o => o.MapFrom(s => _getUser().Identity.Name))
                .ForMember(t => t.AutoShareOn, o => o.Ignore());

            Mapper.CreateMap<LogBookInput.UnitsInput, Units>();

            Mapper.CreateMap<EntryInput, Entry>()
                .ForMember(t => t.Id, o => o.Ignore())
                .ForMember(t => t.OccurredOn, o => o.MapFrom(s => s.OccurredOn.ToUniversalTime()))                
                .ForMember(t => t.LogBook, o => o.Ignore());

            Mapper.CreateMap<CommentInput, Comment<Entry>>()
                .ForMember(t => t.Id, o => o.Ignore())
                .ForMember(t => t.CommentOn, o => o.Ignore())
                .ForMember(t => t.CreatedBy, o => o.MapFrom(s => _getUser().Identity.Name))
                .ForMember(t => t.CreatedOn, o => o.Ignore());

            //
            // Model -> LogBookView
            //
            Mapper.CreateMap<LogBook, LogBookView>()
                .ForMember(t => t.Id, o => o.MapFrom(s => RavenIdResolver.Resolve(s.Id)))
                .ForMember(t => t.Owner, o => o.MapFrom(s => OwnerNameResolver.Resolve(s, _getUser)));

            Mapper.CreateMap<Units, LogBookView.UnitsView>();

            Mapper.CreateMap<Entry, LogBookView.EntryView>()
                .ForMember(t => t.OccurredOn, o => o.MapFrom(s => DateTimeViewResolver.Resolve(s.OccurredOn)));

            Mapper.CreateMap<Comment, LogBookView.CommentView>()
                .ForMember(t => t.CreatedOn, o => o.MapFrom(s => DateTimeViewResolver.Resolve(s.CreatedOn)));

            Mapper.CreateMap<ISocialNetworkAccount, LogBookView.SocialNetworkAccountView>()
                .ForMember(t => t.NetworkName, o => o.MapFrom(s => s.SocialNetworkName));
        }
    }
}
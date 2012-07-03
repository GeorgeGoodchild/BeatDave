
using System.Linq;
using System.Web;
using AutoMapper;
using BeatDave.Domain;
using BeatDave.Web.Areas.Api_v1.Models;

namespace BeatDave.Web.Infrastructure
{
    public class LogBookProfile : Profile
    {
        protected override void Configure()
        {
            //
            // LogBookInput -> Model
            //
            Mapper.CreateMap<LogBookInput, LogBook>()
                .ForMember(t => t.Id, o => o.Ignore())
                .ForMember(t => t.AutoShareOn, o => o.Ignore())
                .ForMember(t => t.OwnerId, o => o.MapFrom(s => HttpContext.Current.User.Identity.Name))
                .ForMember(t => t.AutoShareOn, o => o.Ignore());

            Mapper.CreateMap<LogBookInput.UnitsInput, Units>();

            Mapper.CreateMap<EntryInput, LogBookEntry>()
                .ForMember(t => t.Id, o => o.Ignore())
                .ForMember(t => t.LogBook, o => o.Ignore());

            //
            // Model -> LogBookView
            //
            Mapper.CreateMap<LogBook, LogBookView>()
                .ForMember(t => t.Id, o => o.MapFrom(s => RavenIdResolver.ResolveToInt(s.Id)))
                .ForMember(t => t.Owner, o => o.Ignore());

            Mapper.CreateMap<Units, LogBookView.UnitsView>();
            
            Mapper.CreateMap<LogBookEntry, LogBookView.EntryView>();

            Mapper.CreateMap<ISocialNetworkAccount, LogBookView.SocialNetworkAccountView>()
                .ForMember(t => t.NetworkName, o => o.MapFrom(s => s.SocialNetworkName));

            Mapper.CreateMap<User, LogBookView.OwnerView>()
                .ForMember(t => t.OwnerUsername, o => o.MapFrom(s => s.Id.Split('/').Last()))
                .ForMember(t => t.OwnerFullName, o => o.MapFrom(s => (s.FirstName + " " + s.LastName).Trim()));
        }
    }
}
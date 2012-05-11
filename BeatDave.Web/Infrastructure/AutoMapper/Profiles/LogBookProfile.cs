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
                .ForMember(t => t.OwnerId, o => o.Ignore()) // TODO: Create resolver to get value from the current auth user 
                .ForMember(t => t.AutoShareOn, o => o.Ignore());

            Mapper.CreateMap<LogBookInput.UnitsInput, Units>();

            Mapper.CreateMap<EntryInput, Entry>()
                .ForMember(t => t.Id, o => o.Ignore())
                .ForMember(t => t.LogBook, o => o.Ignore());

            //
            // Model -> LogBbokView
            //
            Mapper.CreateMap<LogBook, LogBookView>()
                .ForMember(t => t.Id, o => o.MapFrom(s => RavenIdResolver.Resolve(s.Id)))
                .ForMember(t => t.Owner, o => o.Ignore());

            Mapper.CreateMap<Units, LogBookView.UnitsView>();
            
            Mapper.CreateMap<Entry, LogBookView.EntryView>();
            
            Mapper.CreateMap<ISocialNetworkAccount, LogBookView.SocialNetworkAccountView>()
                .ForMember(t => t.NetworkName, o => o.MapFrom(s => s.SocialNetworkName))
                .ForMember(t => t.UserName, o => o.Ignore());   // TODO: This can't just be ignored

            Mapper.CreateMap<User, LogBookView.OwnerView>()
                .ForMember(t => t.OwnerUsername, o => o.MapFrom(s => RavenIdResolver.Resolve(s.Id)))
                .ForMember(t => t.OwnerFullName, o => o.MapFrom(s => (s.FirstName + " " + s.LastName).Trim()));
        }
    }
}
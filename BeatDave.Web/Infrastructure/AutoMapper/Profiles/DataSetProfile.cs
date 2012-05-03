using AutoMapper;
using BeatDave.Web.Areas.Api_v1.Models;
using BeatDave.Web.Models;

namespace BeatDave.Web.Infrastructure
{
    public class DataSetProfile : Profile
    {
        protected override void Configure()
        {
            //
            // DataSetInput -> Model
            //
            Mapper.CreateMap<DataSetInput, DataSet>()
                .ForMember(t => t.Id, o => o.Ignore())
                .ForMember(t => t.AutoShareOn, o => o.Ignore())
                .ForMember(t => t.OwnerId, o => o.Ignore()) // TODO: Create resolver to get value from the current auth user 
                .ForMember(t => t.AutoShareOn, o => o.Ignore());

            Mapper.CreateMap<DataSetInput.UnitsInput, Units>();

            Mapper.CreateMap<DataPointInput, DataPoint>()
                .ForMember(t => t.Id, o => o.Ignore())
                .ForMember(t => t.DataSet, o => o.Ignore());

            //
            // Model -> DataSetView
            //
            Mapper.CreateMap<DataSet, DataSetView>()
                .ForMember(t => t.Id, o => o.MapFrom(s => RavenIdResolver.Resolve(s.Id)))
                .ForMember(t => t.Owner, o => o.Ignore());

            Mapper.CreateMap<Units, DataSetView.UnitsView>();
            
            Mapper.CreateMap<DataPoint, DataSetView.DataPointView>();
            
            Mapper.CreateMap<ISocialNetworkAccount, DataSetView.SocialNetworkAccountView>()
                .ForMember(t => t.NetworkName, o => o.MapFrom(s => s.SocialNetworkName))
                .ForMember(t => t.UserName, o => o.Ignore());   // TODO: This can't just be ignored

            Mapper.CreateMap<User, DataSetView.OwnerView>()
                .ForMember(t => t.Id, o => o.MapFrom(s => RavenIdResolver.Resolve(s.Id)))
                .ForMember(t => t.OwnerName, o => o.MapFrom(s => s.FirstName + " " + s.LastName));
        }
    }
}
using AutoMapper;
using WeightWatchers.core.DTO;
using WeightWatchers.core.Response;
using WeightWatchers.data.Entities;

namespace WeightWatchers.Configurations
{
    public class WeightWatchersProfile: Profile
    {
        public WeightWatchersProfile()
        {
            CreateMap<Card, CardDTO>().ReverseMap();
            CreateMap<Subscriber, SubscriberDTO>().ReverseMap();
        }
    }
}

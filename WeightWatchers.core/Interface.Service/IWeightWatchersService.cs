using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeightWatchers.core.DTO;
using WeightWatchers.core.Response;
using WeightWatchers.data.Entities;

namespace WeightWatchers.core.Interface.Service
{
    public interface IWeightWatchersService
    {
        Task<BaseResponseGeneral<bool>> Post(SubscriberDTO subscriberDTO);
        Task<BaseResponseGeneral<int?>> Login(string email, string password);
        Task<BaseResponseGeneral<SubscriberCardResponse>> GetCardById(int id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeightWatchers.core.DTO;
using WeightWatchers.core.Response;
using WeightWatchers.data.Entities;

namespace WeightWatchers.core.Interface.DAL
{
    public interface IWeightWatchersRepository
    {
        Task<BaseResponseGeneral<bool>> Post(Subscriber subscriber, double height);
        bool IsExistEmail(string email);
        bool IsExistCard(int id);
        Task<BaseResponseGeneral<int?>> Login(string email, string password);
        Task<BaseResponseGeneral<SubscriberCardResponse>> GetCardById(int id);

    }
}

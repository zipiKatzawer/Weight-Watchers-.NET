using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeightWatchers.core.Interface.DAL;
using WeightWatchers.core.Response;
using WeightWatchers.data;
using WeightWatchers.data.Entities;

namespace WeightWatchers.DAL
{
    public class WeightWatchersRepository : IWeightWatchersRepository
    {
        readonly WeightWatchersContext _weightWatchersContext;
        public WeightWatchersRepository(WeightWatchersContext weightWatchersContext)
        {
            _weightWatchersContext = weightWatchersContext;
        }
        public async Task<BaseResponseGeneral<bool>> Post(Subscriber subscriber, double height)
        {
            try
            {
                BaseResponseGeneral<bool> response = new BaseResponseGeneral<bool>();
                var newSubscriber = _weightWatchersContext.Subscriber.Add(subscriber);
                _weightWatchersContext.SaveChanges();
                Card card = new Card()
                {
                    SubscriberId = newSubscriber.Entity.Id,
                    OpenDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    Height = height,
                    Weight = 0,
                    BMI = 0
                };
                _weightWatchersContext.Card.Add(card);
                _weightWatchersContext.SaveChanges();
                response.Succsed = true;
                response.message = "subscriber succeed";
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("subscriber failed");
            }

        }
        public bool IsExistEmail(string email)
        {
            if (_weightWatchersContext.Subscriber.Where(s => s.Email == email).FirstOrDefault() == null)
                return false;
            return true;
        }
        public bool IsExistCard(int id)
        {
            if (_weightWatchersContext.Card.Where(c => c.Id == id).FirstOrDefault() == null)
                return false;
            return true;
        }
        public async Task<BaseResponseGeneral<int?>> Login(string email, string password)
        {
            BaseResponseGeneral<int?> response = new BaseResponseGeneral<int?>();

            Subscriber subscriber = _weightWatchersContext.Subscriber.Where(s => s.Email == email && s.Password == password).FirstOrDefault();
            int id = 0;
            if (subscriber != null)
            {
                id = subscriber.Id;
            }

            Card card = _weightWatchersContext.Card.Where(c => c.SubscriberId == id).FirstOrDefault();
            if (card != null)
            {
                response.Data = card.Id;
                response.Succsed = true;
                response.message = "Card is exist";
            }
            else
            {
                response.Data = null;
                response.Succsed = false;
                response.message = "You are not available enter";
            }
            return response;


        }
        public async Task<BaseResponseGeneral<SubscriberCardResponse>> GetCardById(int id)
        {
            try
            {
                BaseResponseGeneral<SubscriberCardResponse> subscriberCardResponse = new BaseResponseGeneral<SubscriberCardResponse>();
                Card card = _weightWatchersContext.Card.Where(c => c.Id == id).FirstOrDefault();
                Subscriber subscriber = _weightWatchersContext.Subscriber.Where(s => s.Id == card.SubscriberId).FirstOrDefault();
                subscriberCardResponse.Succsed = true;
                subscriberCardResponse.message = "Card is exist";
                subscriberCardResponse.Data = new SubscriberCardResponse();
                subscriberCardResponse.Data.Id = id;
                subscriberCardResponse.Data.FirstName = subscriber.FirstName;
                subscriberCardResponse.Data.LastName = subscriber.LastName;
                subscriberCardResponse.Data.Height = card.Height;
                subscriberCardResponse.Data.Weight = card.Weight;
                subscriberCardResponse.Data.BMI = card.BMI;
                return subscriberCardResponse;
            }
            catch (Exception ex)
            {

                throw new Exception("Get card failed");
            }
        }
    }
}

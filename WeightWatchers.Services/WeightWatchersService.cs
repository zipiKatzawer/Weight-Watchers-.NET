using AutoMapper;
using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using WeightWatchers.core.DTO;
using WeightWatchers.core.Interface.DAL;
using WeightWatchers.core.Interface.Service;
using WeightWatchers.core.Response;
using WeightWatchers.data.Entities;

namespace WeightWatchers.Services
{
    public class WeightWatchersService : IWeightWatchersService
    {
        readonly IWeightWatchersRepository _weightWatchersRepository;
        readonly IMapper _mapper;
        public WeightWatchersService(IWeightWatchersRepository weightWatchersRepository, IMapper mapper)
        {
            _weightWatchersRepository = weightWatchersRepository;
            _mapper = mapper;
        }
        public async Task<BaseResponseGeneral<bool>> Post(SubscriberDTO subscriberDTO)
        {
            BaseResponseGeneral<bool> response = new BaseResponseGeneral<bool>();
            Subscriber subscriber = _mapper.Map<Subscriber>(subscriberDTO);
            double height = subscriberDTO.Height;
            try
            {
                if (_weightWatchersRepository.IsExistEmail(subscriber.Email))
                {
                    response.Succsed = false;
                    response.message = "Email is already exist";
                    return response;

                }
                return await _weightWatchersRepository.Post(subscriber, height);

            }
            catch (Exception ex)
            {

                throw new Exception("subscriber failed");
            }
        }
        public async Task<BaseResponseGeneral<int?>> Login(string email, string password)
        {
            BaseResponseGeneral<int?> response = new BaseResponseGeneral<int?>(); 
            if(!IsValidEmail(email)||!IsValidPassword(password))
            {
                response.Data = null;
                response.Succsed = false;
                response.message = "The email or password is not valid! ";
                return response;
            }
            return await _weightWatchersRepository.Login(email,password);

        }

        public async Task<BaseResponseGeneral<SubscriberCardResponse>> GetCardById(int id)
        {
            if (!_weightWatchersRepository.IsExistCard(id))
                return new BaseResponseGeneral<SubscriberCardResponse>() { Succsed = false, message = "Card isnt exist" };
            return await _weightWatchersRepository.GetCardById(id);
        }
        public bool IsValidEmail(string email)
        {
            try
            {
                MailAddress mailAddress = new MailAddress(email);
            }
            catch (FormatException)
            {
                return false;
            }
            return true;

        }
        public bool IsValidPassword(string password)
        {
            if(password.Length<6||password.Length>9)
                return false;
            return true;
        }
    }
}

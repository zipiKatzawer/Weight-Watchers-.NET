using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeightWatchers.core.DTO;
using WeightWatchers.core.Interface.Service;
using WeightWatchers.core.Response;
using WeightWatchers.data.Entities;

namespace WeightWatchers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeightWatchersController : ControllerBase
    {
        readonly IWeightWatchersService _weightWatchersService;
        public WeightWatchersController(IWeightWatchersService weightWatchersService)
        {
            _weightWatchersService=weightWatchersService;
        }
        [HttpPost]
        [Route("Subscriber")]
        public async Task<ActionResult<BaseResponseGeneral<bool>>> Post(SubscriberDTO subscriberDTO)
        {
            BaseResponseGeneral<bool> response = new BaseResponseGeneral<bool>();
            response= await _weightWatchersService.Post(subscriberDTO);
            if (response.Succsed == false)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<BaseResponseGeneral<int?>>>Login(string email,string password)
        {
            BaseResponseGeneral<int?>response=new BaseResponseGeneral<int?>();
            response = await _weightWatchersService.Login(email, password);
            if (response.Data == null)
                return Unauthorized(response);
            return Ok(response);
        }
        [HttpGet]
        [Route("GetCardById")]
        [Authorize]
        public async Task<ActionResult<BaseResponseGeneral<SubscriberCardResponse>>> GetCardById(int id)
        {
            BaseResponseGeneral < SubscriberCardResponse> response = new BaseResponseGeneral<SubscriberCardResponse>();
            response= await _weightWatchersService.GetCardById(id);
            if (response.Succsed==false)
            {
                return NotFound(response);
            }
            return Ok(response);
        }



    }
}

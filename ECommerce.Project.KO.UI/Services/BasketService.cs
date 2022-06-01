using ECommerce.Project.KO.Business.DTOs;
using ECommerce.Project.KO.UI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Project.KO.UI.Services
{
    public class BasketService : IBasketService
    {
        private readonly RedisService _redisService;

        public BasketService(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task<ResponseDto<BasketDto>> GetBasket(string userId)
        {
            var existBasket = await _redisService.GetDb().StringGetAsync(userId);

            if (string.IsNullOrEmpty(existBasket))
            {
                return ResponseDto<BasketDto>.Fail("Basket not found", 404, true);
            }

            return ResponseDto<BasketDto>.Success(JsonSerializer.Deserialize<BasketDto>(existBasket), 200);
        }

        public async Task<ResponseDto<BasketDto>> SaveOrUpdate(BasketDto basketDto)
        {
            var result = await GetBasket(basketDto.UserId);
            if (!result.IsSuccesful)
            {
                var status = await _redisService.GetDb().StringSetAsync(basketDto.UserId, JsonSerializer.Serialize(basketDto));

                return status ? ResponseDto<BasketDto>.Success(204) : ResponseDto<BasketDto>.Fail("Basket could not update or save", 500, true);
            }

            result.Data.basketItems.AddRange(basketDto.basketItems);
            var state = await _redisService.GetDb().StringSetAsync(basketDto.UserId, JsonSerializer.Serialize(result.Data));
            return state ? ResponseDto<BasketDto>.Success(204) : ResponseDto<BasketDto>.Fail("Basket could not update or save", 500, true);
        }
    }
}

using ECommerce.Project.KO.Business.DTOs;
using ECommerce.Project.KO.UI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Project.KO.UI.Services
{
    public interface IBasketService
    {
        Task<ResponseDto<BasketDto>> GetBasket(string UserId);
        Task<ResponseDto<BasketDto>> SaveOrUpdate(BasketDto basketDto);
    }
}

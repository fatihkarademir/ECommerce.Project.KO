using ECommerce.Project.KO.Business.Abstract;
using ECommerce.Project.KO.Business.DTOs;
using ECommerce.Project.KO.DataAccess.Abstract;
using ECommerce.Project.KO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Project.KO.Business.Concrete
{
    public class OrderService : IOrderService
    {
        IOrderDAL _orderDAL;

        public OrderService(IOrderDAL orderDAL)
        {
            _orderDAL = orderDAL;
        }

        public async Task<ResponseDto<OrderDto>> CreateAsync(OrderDto entity)
        {
            var newEntity = ECommerce.Project.KO.Business.Mapper.ObjectMapper.Mapper.Map<Order>(entity);
            await _orderDAL.Create(newEntity);
            var newDto = ECommerce.Project.KO.Business.Mapper.ObjectMapper.Mapper.Map<OrderDto>(newEntity);
            return ResponseDto<OrderDto>.Success(newDto, 200);
        }

        public async Task<ResponseDto<NoDataDto>> DeleteAsync(long id)
        {
            var isExistEntity = await _orderDAL.GetById(id);
            if (isExistEntity == null)
            {
                return ResponseDto<NoDataDto>.Fail("Id Not Found", 404, true);
            }

            await _orderDAL.Delete(id);
            return ResponseDto<NoDataDto>.Success(204);
        }

        public ResponseDto<IEnumerable<OrderDto>> GetAllAsync()
        {
            var results = Mapper.ObjectMapper.Mapper.Map<IEnumerable<OrderDto>>(_orderDAL.GetAllByFilterOrNotFiltered());

            return ResponseDto<IEnumerable<OrderDto>>.Success(results, 200);
        }

        public async Task<ResponseDto<OrderDto>> GetByIdAsync(long id)
        {
            var result = await _orderDAL.GetById(id);
            if (result == null)
            {
                return ResponseDto<OrderDto>.Fail("Id not found", 404, true);
            }

            return ResponseDto<OrderDto>.Success(Mapper.ObjectMapper.Mapper.Map<OrderDto>(result), 200);
        }

        public async Task<ResponseDto<NoDataDto>> UpdateAsync(OrderDto entity, long id)
        {
            var isExistEntity = await _orderDAL.GetById(id);
            if (isExistEntity == null)
            {
                return ResponseDto<NoDataDto>.Fail("Id not found", 404, true);
            }

            var updateEntity = Mapper.ObjectMapper.Mapper.Map<Order>(entity);
            await _orderDAL.Update(updateEntity);
            return ResponseDto<NoDataDto>.Success(204);
        }
    }
}

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
    public class OrderDetailService : IOrderDetailService
    {
        IOrderDetailDAL _orderDetailDAL;

        public OrderDetailService(IOrderDetailDAL orderDetailDAL)
        {
            _orderDetailDAL = orderDetailDAL;
        }

        public async Task<ResponseDto<OrderDetailDto>> CreateAsync(OrderDetailDto entity)
        {
            var newEntity = ECommerce.Project.KO.Business.Mapper.ObjectMapper.Mapper.Map<OrderDetail>(entity); 
            await _orderDetailDAL.Create(newEntity);
            var newDto = ECommerce.Project.KO.Business.Mapper.ObjectMapper.Mapper.Map<OrderDetailDto>(newEntity);
            return ResponseDto<OrderDetailDto>.Success(newDto, 200);
        }

        public async Task<ResponseDto<NoDataDto>> DeleteAsync(long id)
        {
            var isExistEntity = await _orderDetailDAL.GetById(id);
            if (isExistEntity == null)
            {
                return ResponseDto<NoDataDto>.Fail("Id Not Found", 404, true);
            }

            await _orderDetailDAL.Delete(id);
            return ResponseDto<NoDataDto>.Success(204);
        }

        public ResponseDto<IEnumerable<OrderDetailDto>> GetAllAsync()
        {
            var results = Mapper.ObjectMapper.Mapper.Map<IEnumerable<OrderDetailDto>>(_orderDetailDAL.GetAllByFilterOrNotFiltered());

            return ResponseDto<IEnumerable<OrderDetailDto>>.Success(results, 200);
        }

        public async Task<ResponseDto<OrderDetailDto>> GetByIdAsync(long id)
        {
            var result = await _orderDetailDAL.GetById(id);
            if (result == null)
            {
                return ResponseDto<OrderDetailDto>.Fail("Id not found", 404, true);
            }

            return ResponseDto<OrderDetailDto>.Success(Mapper.ObjectMapper.Mapper.Map<OrderDetailDto>(result), 200);
        }

        public async Task<ResponseDto<NoDataDto>> UpdateAsync(OrderDetailDto entity, long id)
        {
            var isExistEntity = await _orderDetailDAL.GetById(id);
            if (isExistEntity == null)
            {
                return ResponseDto<NoDataDto>.Fail("Id not found", 404, true);
            }

            var updateEntity = Mapper.ObjectMapper.Mapper.Map<OrderDetail>(entity);
            await _orderDetailDAL.Update(updateEntity);
            return ResponseDto<NoDataDto>.Success(204);
        }
    }
}

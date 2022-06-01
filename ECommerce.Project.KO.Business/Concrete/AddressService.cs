using AutoMapper;
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
    public class AddressService : IAddressService
    {
        IAddressDAL _addressDAL;

        public AddressService(IAddressDAL addressDAL)
        {
            _addressDAL = addressDAL;
        }

        public async Task<ResponseDto<AddressDto>> CreateAsync(AddressDto entity)
        {
            var newEntity = ECommerce.Project.KO.Business.Mapper.ObjectMapper.Mapper.Map<Address>(entity);
            await _addressDAL.Create(newEntity);
            var newDto = ECommerce.Project.KO.Business.Mapper.ObjectMapper.Mapper.Map<AddressDto>(newEntity);
            return ResponseDto<AddressDto>.Success(newDto, 200);
        }

        public async Task<ResponseDto<NoDataDto>> DeleteAsync(long id)
        {
            var isExistEntity = await _addressDAL.GetById(id);
            if (isExistEntity == null)
            {
                return ResponseDto<NoDataDto>.Fail("Id Not Found", 404, true);
            }

            await _addressDAL.Delete(id);
            return ResponseDto<NoDataDto>.Success(204);
        }

        public ResponseDto<IEnumerable<AddressDto>> GetAllAsync()
        {
            var results = Mapper.ObjectMapper.Mapper.Map<IEnumerable<AddressDto>>(_addressDAL.GetAllByFilterOrNotFiltered());

            return ResponseDto<IEnumerable<AddressDto>>.Success(results, 200);
        }

        public async Task<ResponseDto<AddressDto>> GetByIdAsync(long id)
        {
            var result = await _addressDAL.GetById(id);
            if (result == null)
            {
                return ResponseDto<AddressDto>.Fail("Id not found", 404, true);
            }

            return ResponseDto<AddressDto>.Success(Mapper.ObjectMapper.Mapper.Map<AddressDto>(result), 200);
        }

        public async Task<ResponseDto<NoDataDto>> UpdateAsync(AddressDto entity, long id)
        {
            var isExistEntity = await _addressDAL.GetById(id);
            if (isExistEntity == null)
            {
                return ResponseDto<NoDataDto>.Fail("Id not found", 404, true);
            }

            var updateEntity = Mapper.ObjectMapper.Mapper.Map<Address>(entity);
            await _addressDAL.Update(updateEntity);          
            return ResponseDto<NoDataDto>.Success(204);
        }

    }
}

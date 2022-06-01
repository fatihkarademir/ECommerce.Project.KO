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
    public class ProductService : IProductService
    {
        IProductDAL _productDAL;

        public ProductService(IProductDAL productDAL)
        {
            _productDAL = productDAL;
        }

        public async Task<ResponseDto<ProductDto>> GetDetailByIdAsync(long id)
        {
            var result = await _productDAL.GetDetailById(id);
            if (result == null)
            {
                return ResponseDto<ProductDto>.Fail("Id Not Found", 404, true);
            }
            var newDto = ECommerce.Project.KO.Business.Mapper.ObjectMapper.Mapper.Map<ProductDto>(result);
            return ResponseDto<ProductDto>.Success(newDto, 200);

        }

        public async Task<ResponseDto<ProductDto>> CreateAsync(ProductDto entity)
        {
            var newEntity = ECommerce.Project.KO.Business.Mapper.ObjectMapper.Mapper.Map<Product>(entity);
            await _productDAL.Create(newEntity);
            var newDto = ECommerce.Project.KO.Business.Mapper.ObjectMapper.Mapper.Map<ProductDto>(newEntity);
            return ResponseDto<ProductDto>.Success(newDto, 200);
        }

        public async Task<ResponseDto<NoDataDto>> DeleteAsync(long id)
        {
            var isExistEntity = await _productDAL.GetById(id);
            if (isExistEntity == null)
            {
                return ResponseDto<NoDataDto>.Fail("Id Not Found", 404, true);
            }

            await _productDAL.Delete(id);
            return ResponseDto<NoDataDto>.Success(204);
        }

        public ResponseDto<IEnumerable<ProductDto>> GetAllAsync()
        {
            var results = Mapper.ObjectMapper.Mapper.Map<IEnumerable<ProductDto>>(_productDAL.GetAllByFilterOrNotFiltered());

            return ResponseDto<IEnumerable<ProductDto>>.Success(results, 200);
        }

        public async Task<ResponseDto<ProductDto>> GetByIdAsync(long id)
        {
            var result = await _productDAL.GetById(id);
            if (result == null)
            {
                return ResponseDto<ProductDto>.Fail("Id not found", 404, true);
            }

            return ResponseDto<ProductDto>.Success(Mapper.ObjectMapper.Mapper.Map<ProductDto>(result), 200);
        }



        public async Task<ResponseDto<NoDataDto>> UpdateAsync(ProductDto entity, long id)
        {
            var isExistEntity = await _productDAL.GetById(id);
            if (isExistEntity == null)
            {
                return ResponseDto<NoDataDto>.Fail("Id not found", 404, true);
            }

            var updateEntity = Mapper.ObjectMapper.Mapper.Map<Product>(entity);
            await _productDAL.Update(updateEntity);
            return ResponseDto<NoDataDto>.Success(204);
        }
    }
}

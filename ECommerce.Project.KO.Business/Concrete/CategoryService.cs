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
    public class CategoryService : ICategoryService
    {
        ICategoryDAL _categoryDAL;

        public CategoryService(ICategoryDAL categoryDAL)
        {
            _categoryDAL = categoryDAL;
        }

        public async Task<ResponseDto<CategoryDto>> CreateAsync(CategoryDto entity)
        {
            var newEntity = ECommerce.Project.KO.Business.Mapper.ObjectMapper.Mapper.Map<Category>(entity);
            await _categoryDAL.Create(newEntity);
            var newDto = ECommerce.Project.KO.Business.Mapper.ObjectMapper.Mapper.Map<CategoryDto>(newEntity);
            return ResponseDto<CategoryDto>.Success(newDto, 200);
        }

        public async Task<ResponseDto<NoDataDto>> DeleteAsync(long id)
        {
            var isExistEntity = await _categoryDAL.GetById(id);
            if (isExistEntity == null)
            {
                return ResponseDto<NoDataDto>.Fail("Id Not Found", 404, true);
            }

            await _categoryDAL.Delete(id);
            return ResponseDto<NoDataDto>.Success(204);
        }

        public ResponseDto<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var results = Mapper.ObjectMapper.Mapper.Map<IEnumerable<CategoryDto>>(_categoryDAL.GetAllByFilterOrNotFiltered());

            return ResponseDto<IEnumerable<CategoryDto>>.Success(results, 200);
        }

        public async Task<ResponseDto<CategoryDto>> GetByIdAsync(long id)
        {
            var product = await _categoryDAL.GetById(id);
            if (product == null)
            {
                return ResponseDto<CategoryDto>.Fail("Id not found", 404, true);
            }

            return ResponseDto<CategoryDto>.Success(Mapper.ObjectMapper.Mapper.Map<CategoryDto>(product), 200);
        }

        public async Task<ResponseDto<NoDataDto>> UpdateAsync(CategoryDto entity, long id)
        {
            var isExistEntity = await _categoryDAL.GetById(id);
            if (isExistEntity == null)
            {
                return ResponseDto<NoDataDto>.Fail("Id not found", 404, true);
            }

            var updateEntity = Mapper.ObjectMapper.Mapper.Map<Category>(entity);
            await _categoryDAL.Update(updateEntity);
            //204 durum kodu => No Content => Response body'sinde hiçbir data olamayacak
            return ResponseDto<NoDataDto>.Success(204);
        }
    }
}

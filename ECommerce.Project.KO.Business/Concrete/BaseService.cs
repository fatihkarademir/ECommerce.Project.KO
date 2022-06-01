using AutoMapper;
using ECommerce.Project.KO.Business.Abstract;
using ECommerce.Project.KO.Business.DTOs;
using ECommerce.Project.KO.Business.Mapper;
using ECommerce.Project.KO.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Project.KO.Business.Concrete
{
    public class BaseService<TDto, TEntity> : IBaseService<TDto> 
        where TEntity : class
        where TDto : class
    {
        private readonly IRepository<TEntity> _genericRepository;

        public BaseService(IRepository<TEntity> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<ResponseDto<TDto>> CreateAsync(TDto entity)
        {
            var newEntity = ObjectMapper.Mapper.Map<TEntity>(entity);
            await _genericRepository.Create(newEntity);
            var newDto = ObjectMapper.Mapper.Map<TDto>(newEntity);
            return ResponseDto<TDto>.Success(newDto, 200);
        }

        public async Task<ResponseDto<NoDataDto>> DeleteAsync(long id)
        {
            var isExistEntity = await _genericRepository.GetById(id);
            if (isExistEntity == null)
            {
                return ResponseDto<NoDataDto>.Fail("Id Not Found", 404, true);
            }

            await _genericRepository.Delete(id);
            return ResponseDto<NoDataDto>.Success(204);
        }

        public ResponseDto<IEnumerable<TDto>> GetAllAsync()
        {
            var results = ObjectMapper.Mapper.Map<List<TDto>>(_genericRepository.GetAllByFilterOrNotFiltered());

            return ResponseDto<IEnumerable<TDto>>.Success(results, 200);
        }

        public async Task<ResponseDto<TDto>> GetByIdAsync(long id)
        {
            var product = await _genericRepository.GetById(id);
            if (product == null)
            {
                return ResponseDto<TDto>.Fail("Id not found", 404, true);
            }

            return ResponseDto<TDto>.Success(ObjectMapper.Mapper.Map<TDto>(product), 200);
        }

        public async Task<ResponseDto<NoDataDto>> UpdateAsync(TDto entity,long id)
        {
            var isExistEntity = await _genericRepository.GetById(id);
            if (isExistEntity == null)
            {
                return ResponseDto<NoDataDto>.Fail("Id not found", 404, true);
            }

            var updateEntity = ObjectMapper.Mapper.Map<TEntity>(entity);
            await _genericRepository.Update(updateEntity);
            //204 durum kodu => No Content => Response body'sinde hiçbir data olamayacak
            return ResponseDto<NoDataDto>.Success(204);
        }


    }
}

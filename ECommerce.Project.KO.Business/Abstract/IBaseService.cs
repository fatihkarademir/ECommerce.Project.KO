using ECommerce.Project.KO.Business.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Project.KO.Business.Abstract
{
    public interface IBaseService<TDto>
        where TDto : class
    {
        Task<ResponseDto<TDto>> CreateAsync(TDto entity);
        Task<ResponseDto<NoDataDto>> UpdateAsync(TDto entity, long id);
        Task<ResponseDto<NoDataDto>> DeleteAsync(long id);
        ResponseDto<IEnumerable<TDto>> GetAllAsync();
        Task<ResponseDto<TDto>> GetByIdAsync(long id);

        //Task<TDto> CreateAsync(TDto entity);
        //Task<NoDataDto> UpdateAsync(TDto entity, int id);
        //Task<NoDataDto> DeleteAsync(int id);
        //IEnumerable<TDto> GetAllAsync();
        //ResponseDto<TDto> GetByIdAsync(int id);


    }
}

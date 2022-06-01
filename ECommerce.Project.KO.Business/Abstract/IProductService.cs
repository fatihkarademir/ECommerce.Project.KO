using ECommerce.Project.KO.Business.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Project.KO.Business.Abstract
{
    public interface IProductService : IBaseService<ProductDto>
    {
        Task<ResponseDto<ProductDto>> GetDetailByIdAsync(long id);
    }
}

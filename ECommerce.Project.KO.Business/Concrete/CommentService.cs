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
    public class CommentService : ICommentService
    {
        ICommentDAL _commentDAL;

        public CommentService(ICommentDAL commentDAL)
        {
            _commentDAL = commentDAL;
        }

        public async Task<ResponseDto<CommentDto>> CreateAsync(CommentDto entity)
        {
            var newEntity = ECommerce.Project.KO.Business.Mapper.ObjectMapper.Mapper.Map<Comment>(entity);
            await _commentDAL.Create(newEntity);
            var newDto = ECommerce.Project.KO.Business.Mapper.ObjectMapper.Mapper.Map<CommentDto>(newEntity);
            return ResponseDto<CommentDto>.Success(newDto, 200);
        }

        public async Task<ResponseDto<NoDataDto>> DeleteAsync(long id)
        {
            var isExistEntity = await _commentDAL.GetById(id);
            if (isExistEntity == null)
            {
                return ResponseDto<NoDataDto>.Fail("Id Not Found", 404, true);
            }

            await _commentDAL.Delete(id);
            return ResponseDto<NoDataDto>.Success(204);
        }

        public ResponseDto<IEnumerable<CommentDto>> GetAllAsync()
        {
            var results = Mapper.ObjectMapper.Mapper.Map<IEnumerable<CommentDto>>(_commentDAL.GetAllByFilterOrNotFiltered());

            return ResponseDto<IEnumerable<CommentDto>>.Success(results, 200);
        }

        public async Task<ResponseDto<CommentDto>> GetByIdAsync(long id)
        {
            var result = await _commentDAL.GetById(id);
            if (result == null)
            {
                return ResponseDto<CommentDto>.Fail("Id not found", 404, true);
            }

            return ResponseDto<CommentDto>.Success(Mapper.ObjectMapper.Mapper.Map<CommentDto>(result), 200);
        }

        public async Task<ResponseDto<List<CommentDto>>> GetCommentsByProductId(long productId)
        {
            var result = await _commentDAL.GetCommentsByProductId(productId);
            if (result == null)
            {
                return ResponseDto<List<CommentDto>>.Fail("not found", 404, true);
            }

            return ResponseDto<List<CommentDto>>.Success(Mapper.ObjectMapper.Mapper.Map<List<CommentDto>>(result), 200);
        }

        public async Task<ResponseDto<NoDataDto>> UpdateAsync(CommentDto entity, long id)
        {
            var isExistEntity = await _commentDAL.GetById(id);
            if (isExistEntity == null)
            {
                return ResponseDto<NoDataDto>.Fail("Id not found", 404, true);
            }

            var updateEntity = Mapper.ObjectMapper.Mapper.Map<Comment>(entity);
            await _commentDAL.Update(updateEntity);
            return ResponseDto<NoDataDto>.Success(204);
        }
    }
}

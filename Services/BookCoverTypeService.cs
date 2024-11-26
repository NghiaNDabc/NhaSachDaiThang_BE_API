using AutoMapper;
using NhaSachDaiThang_BE_API.Helper;
using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Services.IServices;
using NhaSachDaiThang_BE_API.UnitOfWork;

namespace NhaSachDaiThang_BE_API.Services
{
    public class BookCoverTypeService : IBookCoverTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BookCoverTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }
        public async Task<ServiceResult> AddAsync(BookCoverTypeDto model)
        {
            var find = await _unitOfWork.BookCoverTypeRepository.GetByNameAsync(model.Name);
            if (find.Count() > 0)
            {
                return ServiceResultFactory.BadRequest(model.Name + " đã tồn tại");
            }
            var bookcv = _mapper.Map<BookCoverType>(model);
            await _unitOfWork.BookCoverTypeRepository.AddAsync(bookcv);
            await _unitOfWork.SaveChangeAsync();
            return ServiceResultFactory.Created("Thêm bìa mới thành công!");
        }
        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var bookcv = await _unitOfWork.BookCoverTypeRepository.GetByIdAsync(id);
            if (bookcv != null)
            {
                await _unitOfWork.BookCoverTypeRepository.DeleteAsync(id);
                await _unitOfWork.SaveChangeAsync();
                return ServiceResultFactory.Ok("Xóa thành công");
            }
            return ServiceResultFactory.NotFound("Không tìm thấy cần xóa");
        }
        public async Task<ServiceResult> GetAllAsync()
        {
            var rs = await _unitOfWork.BookCoverTypeRepository.GetAllAsync();
            if (rs == null || rs.Count() == 0)
            {
                return ServiceResultFactory.NoContent();
            }
            return ServiceResultFactory.Ok(data: _mapper.Map<IEnumerable<BookCoverTypeDto>>(rs));
        }
        public async Task<ServiceResult> GetByIdAsync(int id)
        {
            var bookcv = await _unitOfWork.BookCoverTypeRepository.GetByIdAsync(id);
            if (bookcv != null)
            {
                return ServiceResultFactory.Ok(data: _mapper.Map<BookCoverTypeDto>(bookcv));

            }
            return ServiceResultFactory.NotFound();
        }
        public async Task<ServiceResult> UpdateAsync(BookCoverTypeDto model)
        {
            var bookcv = await _unitOfWork.BookCoverTypeRepository.GetByIdAsync(model.BookCoverTypeId);
            if (bookcv == null)
            {
                return ServiceResultFactory.NotFound("Không tìm thấy bìa cần update");

            }
            bookcv.Name = model.Name;
            _unitOfWork.BookCoverTypeRepository.Update(bookcv);
            await _unitOfWork.SaveChangeAsync();
            return ServiceResultFactory.Ok("\"Update bìa thành công\"");

        }

    }

}


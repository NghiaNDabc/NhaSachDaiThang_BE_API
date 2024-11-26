using AutoMapper;
using Microsoft.CodeAnalysis.Host;
using NhaSachDaiThang_BE_API.Helper;
using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Services.IServices;
using NhaSachDaiThang_BE_API.UnitOfWork;

namespace NhaSachDaiThang_BE_API.Services
{
    public class LanguageService : NhaSachDaiThang_BE_API.Services.IServices.ILanguageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public LanguageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }
        public async Task<ServiceResult> AddAsync(LanguageDto model)
        {
            var find = await _unitOfWork.LanguageRepository.GetByNameAsync(model.Name);
            if (find.Count() > 0)
            {
                return ServiceResultFactory.BadRequest(model.Name + " đã tồn tại");
            }
            var language = _mapper.Map<Language>(model);
            await _unitOfWork.LanguageRepository.AddAsync(language);
            await _unitOfWork.SaveChangeAsync();
            return ServiceResultFactory.Created("Thêm nhà ngôn ngữ mới thành công!");
        }
        public  async Task<ServiceResult> DeleteAsync(int id)
        {
            var language = await _unitOfWork.LanguageRepository.GetByIdAsync(id);
            if (language != null)
            {
                await _unitOfWork.LanguageRepository.DeleteAsync(id);
                await _unitOfWork.SaveChangeAsync();
                return ServiceResultFactory.Ok("Xóa thành công");
            }
            return ServiceResultFactory.NotFound("Không tìm thấy cần xóa");
        }
        public async Task<ServiceResult> GetAllAsync()
        {
            var rs =  await _unitOfWork.LanguageRepository.GetAllAsync();
            if(rs ==null || rs.Count() == 0)
            {
                return ServiceResultFactory.NoContent();
            }
            return ServiceResultFactory.Ok(data: _mapper.Map<IEnumerable<LanguageDto>>(rs));
        }
        public async Task <ServiceResult> GetByIdAsync(int id)
        {
            var language = await _unitOfWork.LanguageRepository.GetByIdAsync(id);
            if (language != null)
            {
                return ServiceResultFactory.Ok(data: _mapper.Map<LanguageDto>(language));
               
            }
            return ServiceResultFactory.NotFound();
        }
        public async Task<ServiceResult> UpdateAsync(LanguageDto model)
        {
            var language = await _unitOfWork.LanguageRepository.GetByIdAsync(model.LanguageId);
            if (language == null)
            {
                return ServiceResultFactory.NotFound("Không tìm thấy ngôn ngữ cần update");

            }
            language.Name = model.Name;
             _unitOfWork.LanguageRepository.Update(language);
            await _unitOfWork.SaveChangeAsync();
            return ServiceResultFactory.Ok("\"Update ngôn ngữ thành công\"");

        }
    }
}

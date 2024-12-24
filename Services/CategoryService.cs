using AutoMapper;
using NhaSachDaiThang_BE_API.Helper;
using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Services.IServices;
using NhaSachDaiThang_BE_API.UnitOfWork;

namespace NhaSachDaiThang_BE_API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ServiceResult> Add(CategoryDto model)
        {
            var item = await _unitOfWork.CategoryRepository.GetByNameAsync(model.Name);
            if (item!=null && item.Count()>0)
            {

                return ServiceResultFactory.BadRequest(model.Name + " đã tồn tại");
            }
            var category = _mapper.Map<Category>(model);
            await _unitOfWork.CategoryRepository.AddAsync(category);
            await _unitOfWork.SaveChangeAsync();
            return ServiceResultFactory.Created("Thêm danh mục mới thành công!");

        }

        public async Task<ServiceResult> Delete(int id)
        {
            if (await _unitOfWork.CategoryRepository.GetByIdAsync(id) == null)
            {
                return ServiceResultFactory.NotFound("Không tìm thấy danh mục cần xóa");
            }
            await _unitOfWork.CategoryRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangeAsync();
            return ServiceResultFactory.Ok("Xóa danh mục thành công!");
        }

        public async Task<ServiceResult> GetActiveById(int id)
        {
            var categorie = await _unitOfWork.CategoryRepository.GetCategoriesByLevel(id);
            int statusCode = 200;
            bool success = true;
            if (categorie == null)
            {
                statusCode = 404;
                success = false;
            }
            return new ServiceResult
            {
                StatusCode = statusCode,
                ApiResult = new ApiResult
                {
                    Success = success,
                    Data = _mapper.Map<CategoryDto>(categorie)
                }
            };
        }

        public async Task<ServiceResult> GetActiveByName(string name, int? pageNumber = null, int? pageSize = null)
        {
            var categorie = await _unitOfWork.CategoryRepository.GetActiveByNameAsync(name, pageNumber, pageSize);
            int statusCode = 200;
            bool success = true;
            if (categorie == null)
            {
                statusCode = 404;
                success = false;
            }
            return new ServiceResult
            {
                StatusCode = statusCode,
                ApiResult = new ApiResult
                {
                    Success = success,
                    Data = _mapper.Map<CategoryDto>(categorie)
                }
            };
        }

        public async Task<ServiceResult> GetAll(int? pageNumber = null, int? pageSize = null)
        {
            var categories = await _unitOfWork.CategoryRepository.GetCategoriesByLevel();

            return ServiceResultFactory.Ok(data: categories);
        }

        public async Task<ServiceResult> GetAllActive(int? pageNumber = null, int? pageSize = null)
        {
            var categories = await _unitOfWork.CategoryRepository.GetCategoriesByLevelActive();

            return new ServiceResult
            {
                StatusCode = 200,
                ApiResult = new ApiResult
                {
                    Success = true,
                    Data = categories
                }
            };
        }

        public async Task<ServiceResult> GetById(int id)
        {
            var categorie = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            int statusCode = 200;
            bool success = true;
            if (categorie == null)
            {
                statusCode = 404;
                success = false;
            }
            return new ServiceResult
            {
                StatusCode = statusCode,
                ApiResult = new ApiResult
                {
                    Success = success,
                    Data = _mapper.Map<CategoryDto>(categorie)
                }
            };
        }

        public async Task<ServiceResult> GetByNameAsync(string name, int? pageNumber = null, int? pageSize = null)
        {
            var cates = await _unitOfWork.CategoryRepository.GetByNameAsync(name, pageNumber, pageSize);

            if (cates == null || !cates.Any())
            {
                return new ServiceResult
                {
                    StatusCode = 404,
                    ApiResult = new ApiResult { Success = false, Message = "No books found." }
                };
            }

            // Chuyển đổi dữ liệu sách sang DTO
            var catesDtos = cates.Select(x => _mapper.Map<CategoryDto>(x)).ToList();

            return ServiceResultFactory.Ok(data: catesDtos);
        }

        public async Task<ServiceResult> SoftDelete(int id)
        {
            if (await _unitOfWork.CategoryRepository.GetByIdAsync(id) == null)
            {
                return ServiceResultFactory.NotFound("Không tìm thấy danh mục cần xóa");
            }
            await _unitOfWork.CategoryRepository.SoftDelete(id);
            await _unitOfWork.SaveChangeAsync();
            return ServiceResultFactory.Ok("Thay đổi trạng thái thành công!");
        }

        public async Task<ServiceResult> Update(CategoryDto model)
        {
            var cate = await _unitOfWork.CategoryRepository.GetByIdAsync(model.CategoryId);
            if (cate == null)
            {
                return new ServiceResult
                {
                    StatusCode = 400,
                    ApiResult = new ApiResult
                    {
                        Success = false,
                        ErrMessage = "Không tìm thấy danh mục cần update"
                    }
                };
            }
            cate.Name = model.Name;
            cate.ModifyBy = model.ModifyBy;
            cate.ModifyDate = DateTime.Now;
            cate.Description = model.Description;
            
            cate.ParentCategoryID = model.ParentCategoryID;
            await _unitOfWork.CategoryRepository.UpdateAsync(cate);
            await _unitOfWork.SaveChangeAsync();
            return new ServiceResult
            {
                StatusCode = 200,
                ApiResult = new ApiResult
                {
                    Success = true,
                    Message = "Update danh mục thành công!"
                }
            };
        }
        public async Task<ServiceResult> Count()
        {
            var activeCat = await _unitOfWork.CategoryRepository.CountActive();
            var deactiveCat = await _unitOfWork.CategoryRepository.CountDeactive();

            return new ServiceResult
            {
                StatusCode = 200,
                ApiResult = new ApiResult
                {
                    Data = new
                    {
                        ActiveBoo = activeCat,
                        DeactiveBook = deactiveCat
                    }
                }
            };
        }
    }
}

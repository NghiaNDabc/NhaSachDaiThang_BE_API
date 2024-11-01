using AutoMapper;
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
            if ( await _unitOfWork.CategoryRepository.GetByNameAsync(model.Name) != null)
            {
               
                return new ServiceResult
                {
                    StatusCode = 400,
                    ApiResult = new ApiResult
                    {
                        Success = false,
                        ErrMessage = model.Name + " đã tồn tại"
                    }
                };
            }
            var category = _mapper.Map<Category>(model);
            await _unitOfWork.CategoryRepository.AddAsync(category);
            await _unitOfWork.SaveChangeAsync();
            return new ServiceResult
            {
                StatusCode = 200,
                ApiResult = new ApiResult
                {
                    Success = true,
                    Message = "Thêm danh mục mới thành công!"
                }
            };

        }

        public async Task<ServiceResult> Delete(int id)
        {
            if (await _unitOfWork.CategoryRepository.GetByIdAsync(id) != null)
            {
                return new ServiceResult
                {
                    StatusCode = 400,
                    ApiResult = new ApiResult
                    {
                        Success = false,
                        ErrMessage =  "Không tìm thấy danh mục cần xóa"
                    }
                };
            }
            await _unitOfWork.CategoryRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangeAsync();
            return new ServiceResult
            {
                StatusCode = 200,
                ApiResult = new ApiResult
                {
                    Success = true,
                    Message = "Xóa danh mục thành công!"
                }
            };
        }

        public async Task<ServiceResult> GetAll()
        {
            var  categories = await _unitOfWork.CategoryRepository.GetCategoriesByLevel();

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

        public async Task<ServiceResult> GetAllActive()
        {
            var categories =  await _unitOfWork.CategoryRepository.GetCategoriesByLevelActive();

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
            bool success  = true;
            if(categorie == null)
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

        public async Task<ServiceResult> SoftDelete(int id)
        {
            if (await _unitOfWork.CategoryRepository.GetByIdAsync(id) != null)
            {
                return new ServiceResult
                {
                    StatusCode = 400,
                    ApiResult = new ApiResult
                    {
                        Success = false,
                        ErrMessage = "Không tìm thấy danh mục cần xóa"
                    }
                };
            }
            await _unitOfWork.CategoryRepository.SoftDelete(id);
            await _unitOfWork.SaveChangeAsync();
            return new ServiceResult
            {
                StatusCode = 200,
                ApiResult = new ApiResult
                {
                    Success = true,
                    Message = "Xóa danh mục thành công!"
                }
            };
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
            await _unitOfWork.CategoryRepository.UpdateAsync(cate);
            await _unitOfWork.SaveChangeAsync();
            return new ServiceResult
            {
                StatusCode = 200,
                ApiResult = new ApiResult
                {
                    Success = true,
                    Message = "Update danh mục thành công!",
                    Data = cate
                }
            };
        }
    }
}

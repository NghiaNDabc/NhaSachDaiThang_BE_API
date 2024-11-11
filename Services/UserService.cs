using AutoMapper;
using Microsoft.AspNetCore.Http;
using NhaSachDaiThang_BE_API.Helper.Enum;
using NhaSachDaiThang_BE_API.Helper.GlobalVar;
using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Repositories.IRepositories;
using NhaSachDaiThang_BE_API.Services.IServices;
using NhaSachDaiThang_BE_API.UnitOfWork;

namespace NhaSachDaiThang_BE_API.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private IUploadFile _uploadFile;
        private IHttpContextAccessor _httpContextAccessor;
        public UserService(IUnitOfWork IUnitOfWork, IMapper mapper, IUploadFile uploadFile, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = IUnitOfWork;
            _mapper = mapper;
            _uploadFile = uploadFile;
            _httpContextAccessor = httpContextAccessor;
        }
        public Task<ServiceResult> Add(UserDTO model)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult> Add(UserDTO model, IFormFile formFile)
        {
            var user = _mapper.Map<User>(model);

            if (formFile == null)
            {
                user.Image = "defaul-avatar.jpg";
            }
            else
            {
                var rs = await _uploadFile.UploadImage(formFile, GlobalConst.UserImagePhysicalPath);
                user.Image = rs.ApiResult.Data.ToString();
            }

            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.SaveChangeAsync();
            return new ServiceResult
            {
                StatusCode = 201,
                ApiResult = new ApiResult
                {
                    Success = true,
                    Data = model
                }
            };

        }

        public async Task<ServiceResult> Delete(int id)
        {

            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
            if (user == null)
            {
                return new ServiceResult
                {
                    StatusCode = 404,
                    ApiResult = new ApiResult
                    {
                        Success = false,
                        ErrMessage = "Không tìm thấy người cần xóa"

                    }
                };
            }

            await _unitOfWork.UserRepository.DeleteAsync(id);
            return new ServiceResult
            {
                StatusCode = 201,
                ApiResult = new ApiResult
                {
                    Success = true,
                    Message = "Xóa thành công"
                }
            };
        }

        public Task<ServiceResult> GetActiveById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> GetActiveByName(string name, int? pageNumber = null, int? pageSize = null)
        {
            throw new NotImplementedException();
        }
        //public async Task<ServiceResult> GetAll(AccountType accountType, int? pageNumber = null, int? pageSize = null)
        //{
        //    var users = await _unitOfWork.UserRepository.GetAllAsync(accountType, pageNumber, pageSize);

        //    if (users == null || !users.Any())
        //    {
        //        return new ServiceResult
        //        {
        //            StatusCode = 404,
        //            ApiResult = new ApiResult { Success = false, Message = "No user found." }
        //        };
        //    }

        //    // Chuyển đổi dữ liệu sách sang DTO
        //    var userDtos = MapList(users);

        //    return new ServiceResult
        //    {
        //        StatusCode = 200,
        //        ApiResult = new ApiResult
        //        {
        //            Success = true,
        //            Data = userDtos
        //        }
        //    };
        //}
        public async Task<ServiceResult> GetAll(int? pageNumber = null, int? pageSize = null)
        {
            var users = await _unitOfWork.UserRepository.GetAllAsync(pageNumber, pageSize);

            if (users == null || !users.Any())
            {
                return new ServiceResult
                {
                    StatusCode = 404,
                    ApiResult = new ApiResult { Success = false, Message = "No user found." }
                };
            }

            // Chuyển đổi dữ liệu sách sang DTO
            var userDtos = MapList(users);

            return new ServiceResult
            {
                StatusCode = 200,
                ApiResult = new ApiResult
                {
                    Success = true,
                    Data = userDtos
                }
            };
        }
        public async Task<ServiceResult> GetAll(AccountType accountType, int? pageNumber = null, int? pageSize = null)
        {
            IEnumerable<User> users;
                users =  await _unitOfWork.UserRepository.GetAllAsync(accountType, pageNumber, pageSize);
           

            if (users == null || !users.Any())
            {
                return new ServiceResult
                {
                    StatusCode = 404,
                    ApiResult = new ApiResult { Success = false, Message = "No user found." }
                };
            }

            // Chuyển đổi dữ liệu sách sang DTO
            var userDtos = MapList(users);

            return new ServiceResult
            {
                StatusCode = 200,
                ApiResult = new ApiResult
                {
                    Success = true,
                    Data = userDtos
                }
            };
        }

        public Task<ServiceResult> GetAllActive(int? pageNumber = null, int? pageSize = null)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult> GetById(int id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
            if (user == null )
            {
                return new ServiceResult
                {
                    StatusCode = 404,
                    ApiResult = new ApiResult { Success = false, Message = "No user found." }
                };
            }

            // Chuyển đổi dữ liệu sách sang DTO
            var userDtos = MapOne(user);

            return new ServiceResult
            {
                StatusCode = 200,
                ApiResult = new ApiResult
                {
                    Success = true,
                    Data = userDtos
                }
            };
        }

        List<UserDTO> MapList(IEnumerable<User> users)
        {
            return users.Select(user =>
            {
                var userDto = _mapper.Map<UserDTO>(user);

                // Lấy base URL từ HttpContext
                var request = _httpContextAccessor.HttpContext?.Request;
                var baseUrl = $"{request?.Scheme}://{request?.Host}";

                userDto.Image = $"{baseUrl}/{GlobalConst.UserImageRelativePath}/{userDto.Image}";

                return userDto;
            }).ToList();
        }

        UserDTO MapOne(User user)
        {
            var userDto = _mapper.Map<UserDTO>(user );
            var request = _httpContextAccessor.HttpContext?.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}";
            userDto.Image = $"{baseUrl}/{GlobalConst.UserImageRelativePath}/{userDto.Image}";
            return userDto; 
        }
        public async Task<ServiceResult> GetByNameAsync(string name, int? pageNumber = null, int? pageSize = null)
        {
            var users = await _unitOfWork.UserRepository.GetByNameAsync(name, pageNumber, pageSize);
            if (users == null || !users.Any())
            {
                return new ServiceResult
                {
                    StatusCode = 404,
                    ApiResult = new ApiResult { Success = false, Message = "No user found." }
                };
            }

            // Chuyển đổi dữ liệu sách sang DTO
            var userDtos = MapList(users);

            return new ServiceResult
            {
                StatusCode = 200,
                ApiResult = new ApiResult
                {
                    Success = true,
                    Data = userDtos
                }
            };
        }
        public async Task<ServiceResult> GetByNameAsync(string name, AccountType accountType, int? pageNumber = null, int? pageSize = null)
        {
            var users = await _unitOfWork.UserRepository.GetByName(name,accountType, pageNumber, pageSize);
            if (users == null || !users.Any())
            {
                return new ServiceResult
                {
                    StatusCode = 404,
                    ApiResult = new ApiResult { Success = false, Message = "No user found." }
                };
            }

            // Chuyển đổi dữ liệu sách sang DTO
            var userDtos = MapList(users);

            return new ServiceResult
            {
                StatusCode = 200,
                ApiResult = new ApiResult
                {
                    Success = true,
                    Data = userDtos
                }
            };
        }

        public Task<ServiceResult> SoftDelete(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<ServiceResult> Update(UserDTO model)
        {
            throw new NotImplementedException();
        }
        public async Task<ServiceResult> Update(UserDTO model, IFormFile formFile)
        {
            var exisitngUser = await _unitOfWork.UserRepository.GetByIdAsync(model.Id);

            if (exisitngUser == null)
            {
                return new ServiceResult
                {
                    StatusCode = 404,
                    ApiResult = new ApiResult
                    {
                        Success = false,
                        ErrMessage = "Không tìm thấy User cần cập nhật"
                    }
                };
            }

            if (formFile == null)
            {
                model.Image = exisitngUser.Image;
            }
            else
            {
                var uploadImageRs = await _uploadFile.UploadImage(formFile, GlobalConst.UserImagePhysicalPath);
                if (uploadImageRs.ApiResult.Success == false) { return uploadImageRs; }

                model.Image = uploadImageRs.ApiResult.Data.ToString();
            }
            UpdateUserFromDto(exisitngUser,model);
            await _unitOfWork.UserRepository.UpdateAsync(exisitngUser);
            await _unitOfWork.SaveChangeAsync();
            return new ServiceResult
            {
                StatusCode = 200,
                ApiResult = new ApiResult
                {
                    Success = true,
                    Message = "Cập nhật user thành công",
                    Data = model
                }
            };

        }

        void UpdateUserFromDto(User user, UserDTO userDTO)
        {
            var properties = userDTO.GetType().GetProperties();

            foreach (var property in properties)
            {
                if (property.Name == "UserId") continue;
                var value = property.GetValue(userDTO);
                if ((value!=null))
                {
                    property.SetValue(user, value);
                    
                }
            }
        }
    }
}

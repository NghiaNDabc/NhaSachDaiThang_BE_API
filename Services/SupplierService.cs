﻿using AutoMapper;
using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Services.IServices;


using NhaSachDaiThang_BE_API.UnitOfWork;



namespace NhaSachDaiThang_BE_API.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SupplierService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult> Add(SupplierDto model)
        {
            if (await _unitOfWork.SupplierRepository.GetByNameAsync(model.Name) != null)
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
            var supplier = _mapper.Map<Supplier>(model);
            await _unitOfWork.SupplierRepository.AddAsync(supplier);
            await _unitOfWork.SaveChangeAsync();
            return new ServiceResult
            {
                StatusCode = 200,
                ApiResult = new ApiResult
                {
                    Success = true,
                    Message = "Thêm nhà cung cấp mới thành công!"
                }
            };
        }

        public async Task<ServiceResult> Delete(int id)
        {
            if (await _unitOfWork.SupplierRepository.GetByIdAsync(id) != null)
            {
                return new ServiceResult
                {
                    StatusCode = 404,
                    ApiResult = new ApiResult
                    {
                        Success = false,
                        ErrMessage = "Không tìm thấy nhà cung cấp cần xóa"
                    }
                };
            }
            await _unitOfWork.SupplierRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangeAsync();
            return new ServiceResult
            {
                StatusCode = 200,
                ApiResult = new ApiResult
                {
                    Success = true,
                    Message = "Xóa nhà cung cấp thành công!"
                }
            };
        }
     

        public async Task<ServiceResult> GetAll(int? pageNumber = null, int? pageSize = null)
        {
            var suppliers = await _unitOfWork.SupplierRepository.GetAllAsync(pageNumber, pageSize);

            if (suppliers == null || !suppliers.Any())
            {
                return new ServiceResult
                {
                    StatusCode = 404,
                    ApiResult = new ApiResult { Success = false, Message = "No books found." }
                };
            }
            var supplierDtos = suppliers.Select(s =>  _mapper.Map<SupplierDto>(s)).ToList();

            return new ServiceResult
            {
                StatusCode = 200,
                ApiResult = new ApiResult
                {
                    Success = true,
                    Data = supplierDtos
                }
            };
        }

        public async Task<ServiceResult> GetById(int id)
        {
            var supplier = await _unitOfWork.SupplierRepository.GetByIdAsync(id);
            if (supplier == null)
                return new ServiceResult
                {
                    StatusCode = 404,
                    ApiResult = new ApiResult
                    {
                        Success = false,
                        ErrMessage = "Không tìm thấy nhà cung cấp có id " + id

                    }
                };
            var supplierDto = _mapper.Map<SupplierDto>(supplier);


            return new ServiceResult
            {
                StatusCode = 404,
                ApiResult = new ApiResult
                {
                    Success = false,
                    Data = supplierDto

                }
            };
        }

        public async Task<ServiceResult> GetByNameAsync(string name, int? pageNumber = null, int? pageSize = null)
        {
            var suppliers = await _unitOfWork.SupplierRepository.GetByNameAsync(name);

            if (suppliers == null || !suppliers.Any())
            {
                return new ServiceResult
                {
                    StatusCode = 404,
                    ApiResult = new ApiResult { Success = false, Message = "No books found." }
                };
            }

            var suppliersDto = suppliers.Select(s => _mapper.Map<SupplierDto>(s)).ToList();

            return new ServiceResult
            {
                StatusCode = 200,
                ApiResult = new ApiResult
                {
                    Success = true,
                    Data = suppliersDto
                }
            };
        }

        public async Task<ServiceResult> SoftDelete(int id)
        {
            if (await _unitOfWork.SupplierRepository.GetByIdAsync(id) != null)
            {
                return new ServiceResult
                {
                    StatusCode = 404,
                    ApiResult = new ApiResult
                    {
                        Success = false,
                        ErrMessage = "Không tìm thấy nhà cung cấp"
                    }
                };
            }
            await _unitOfWork.SupplierRepository.SoftDelete(id);
            await _unitOfWork.SaveChangeAsync();
            return new ServiceResult
            {
                StatusCode = 200,
                ApiResult = new ApiResult
                {
                    Success = true,
                    Message = "Ẩn nhà cung cấp thành công!",
              
                }
            };
        }
        public Task<ServiceResult> GetAllActive(int? pageNumber = null, int? pageSize = null)
        {
            throw new NotImplementedException();
        }
        public Task<ServiceResult> Update(SupplierDto model)
        {
            throw new NotImplementedException();
        }
        public Task<ServiceResult> GetActiveById(int id)
        {
            throw new NotImplementedException();
        }
        public Task<ServiceResult> GetActiveByName(string name, int? pageNumber = null, int? pageSize=null)
        {
            throw new NotImplementedException();
        }
    }
}

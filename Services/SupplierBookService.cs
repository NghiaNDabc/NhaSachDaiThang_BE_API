using AutoMapper;
using NhaSachDaiThang_BE_API.Helper;
using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Services.IServices;
using NhaSachDaiThang_BE_API.UnitOfWork;

namespace NhaSachDaiThang_BE_API.Services
{
    public class SupplierBookService : ISupplierBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SupplierBookService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResult> Add(SupplierBookDto model)
        {
           
            var supplier = _mapper.Map<SupplierBook>(model);
            await _unitOfWork.SupplierBookRepository.AddAsync(supplier);
            var book =  await _unitOfWork.BookRepository.GetByIdAsync(supplier.BookId);
            book.Quantity += supplier.Quanlity;
            await _unitOfWork.BookRepository.UpdateAsync(book);
            await _unitOfWork.SaveChangeAsync();
            return new ServiceResult
            {
                StatusCode = 200,
                ApiResult = new ApiResult
                {
                    Success = true,
                    Message = "Nhập hàng mới thành công!"
                }
            };
        }

        public async Task<ServiceResult> Delete(int id)
        {
            var item = (await _unitOfWork.SupplierBookRepository.GetByIdAsync(id)).FirstOrDefault();
            if (item == null)
            {
                return new ServiceResult
                {
                    StatusCode = 404,
                    ApiResult = new ApiResult
                    {
                        Success = false,
                        ErrMessage = "Không đơn nhập hàng cấp cần xóa"
                    }
                };
            }
            await _unitOfWork.SupplierRepository.DeleteAsync(id);
            var book = await _unitOfWork.BookRepository.GetByIdAsync(item.BookId);
            book.Quantity -= item.Quanlity;
            await _unitOfWork.BookRepository.UpdateAsync(book);
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
            var supplierBooks = await _unitOfWork.SupplierBookRepository.GetAllAsync(pageNumber, pageSize);

            if (supplierBooks == null || !supplierBooks.Any())
            {
                return new ServiceResult
                {
                    StatusCode = 404,
                    ApiResult = new ApiResult { Success = false, ErrMessage = "Không tim thấy đơn nhập hàng nào" }
                };
            }

            // Chuyển đổi dữ liệu sách sang DTO
            var supplierBooksDto = _mapper.Map<SupplierBookDto>(supplierBooks);

            return new ServiceResult
            {
                StatusCode = 200,
                ApiResult = new ApiResult
                {
                    Success = true,
                    Data = supplierBooksDto
                }
            };
        }

        public async Task<ServiceResult> GetByIdAsync(int? bookId = null, int? supplierid = null, int? pageNumber = null, int? pageSize = null)
        {
            IEnumerable<SupplierBook> query = await _unitOfWork.SupplierBookRepository.GetAllAsync();
            if (bookId.HasValue)
            {
                query = query.Where(x => x.BookId==bookId);
            }

            if (supplierid.HasValue)
            {
                query = query.Where(x => x.SupplierId == supplierid);
            }
            if (query == null || !query.Any())
            {
                return new ServiceResult
                {
                    StatusCode = 404,
                    ApiResult = new ApiResult { Success = false, ErrMessage = "Không tìm thấy đơn nhập hàng nào" }
                };
            }
            if (pageNumber == null || pageNumber <= 0 || pageSize == null || pageSize <= 0)
            {
            }
            else
            {
                int defaultPageSize = 10;
                int pageNum = pageNumber ?? 1;
                int size = pageSize ?? defaultPageSize;

                int skip = (pageNum - 1) * size;
                query = query.Skip(skip).Take(size);
            }
            // Chuyển đổi dữ liệu SupplierBook sang DTO
            var supplierBookDtos = _mapper.Map<IEnumerable<SupplierBookDto>>(query);

            return new ServiceResult
            {
                StatusCode = 200,
                ApiResult = new ApiResult
                {
                    Success = true,
                    Data = supplierBookDtos
                }
            };
        }
        public async Task<ServiceResult> GetByNameAsync(string bookName = null, string supplierName = null, int? pageNumber = null, int? pageSize = null)
        {
            IEnumerable<SupplierBook> query = await _unitOfWork.SupplierBookRepository.GetAllAsync();
            if (!string.IsNullOrEmpty(bookName))
            {
                query = query.Where(x => x.Book.Title.Contains(bookName));
            }

            if (!string.IsNullOrEmpty(supplierName))
            {
                query = query.Where(x => x.Supplier.Name.Contains(supplierName));
            }
            if (query == null || !query.Any())
            {
                return new ServiceResult
                {
                    StatusCode = 404,
                    ApiResult = new ApiResult { Success = false, ErrMessage = "Không tìm thấy đơn nhập hàng nào" }
                };
            }
            if (pageNumber == null || pageNumber <= 0 || pageSize == null || pageSize <= 0)
            {
            }
            else
            {
                int defaultPageSize = 10;
                int pageNum = pageNumber ?? 1;
                int size = pageSize ?? defaultPageSize;

                int skip = (pageNum - 1) * size;
                query =  query.Skip(skip).Take(size);
            }
            // Chuyển đổi dữ liệu SupplierBook sang DTO
            var supplierBookDtos = _mapper.Map<IEnumerable<SupplierBookDto>>(query);

            return new ServiceResult
            {
                StatusCode = 200,
                ApiResult = new ApiResult
                {
                    Success = true,
                    Data = supplierBookDtos
                }
            };
        }

        public async Task<ServiceResult> Update(SupplierBookDto model)
        {
            var supplier = (await _unitOfWork.SupplierBookRepository.GetByIdAsync(model.SupplierId, model.BookId)).FirstOrDefault();
            if (supplier == null)
            {
                return new ServiceResult
                {
                    StatusCode = 400,
                    ApiResult = new ApiResult
                    {
                        Success = false,
                        ErrMessage = "Không tìm thấy nhà cung cấp cần update"
                    }
                };
            }
            int preQuality = supplier.Quanlity;
            int afterQuality = model.Quanlity;
            var book = await _unitOfWork.BookRepository.GetByIdAsync(supplier.BookId);
            book.Quantity-= (preQuality - afterQuality);
            UpdateSupplierBookFromDto(supplier, model);
            await _unitOfWork.SupplierBookRepository.UpdateAsync(supplier);
            await _unitOfWork.BookRepository.UpdateAsync(book);
            await _unitOfWork.SaveChangeAsync();
            return new ServiceResult
            {
                StatusCode = 200,
                ApiResult = new ApiResult
                {
                    Success = true,
                    Message = "Update nhà cung cấp thành công!",
                    Data = model
                }
            };
        }
        void UpdateSupplierBookFromDto(SupplierBook supplierbook, SupplierBookDto supplierbookDto)
        {
            var properties = typeof(SupplierBookDto).GetProperties();
            foreach (var property in properties)
            {
                if (property.Name == "SupplierId" || property.Name == "BookId") continue;
                var value = property.GetValue(supplierbookDto);
                if (value != null)
                {
                    var bookProperty = typeof(SupplierBook).GetProperty(property.Name);
                    if (bookProperty != null)
                    {
                        bookProperty.SetValue(supplierbook, value);
                    }
                }
            }
        }
    }
}

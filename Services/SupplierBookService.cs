using AutoMapper;
using NhaSachDaiThang_BE_API.Helper;
using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Services.IServices;
using NhaSachDaiThang_BE_API.UnitOfWork;
using System.Drawing.Printing;
using System.Net;
using System.Text;

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

        IEnumerable<object> GroupSuppierBook(IEnumerable<SupplierBookDto> suppliersBook)
        {
            return suppliersBook.GroupBy(sb => new { sb.SupplierBookId, sb.SupplierId, sb.SupplierName, sb.SupplyDate }).Select(

                g => new
                {
                    g.Key.SupplierBookId,
                    g.Key.SupplierId,
                    g.Key.SupplierName,
                    g.Key.SupplyDate,
                    Books = g.Select(x => new
                    {
                        x.BookId,
                        x.Quantity,

                        x.SupplyPrice
                    }).ToList()
                });
        }
        public string Validate(SupplierBookDto model)
        {
            StringBuilder errorMessages = new StringBuilder();


            if (model.SupplierId <= 0)
            {
                errorMessages.AppendLine("SupplierId phải là số dương.");
            }

            if (model.BookId <= 0)
            {
                errorMessages.AppendLine("BookId phải là số dương.");
            }

            if (model.Quantity < 0)
            {
                errorMessages.AppendLine("Số lượng không được nhỏ hơn 0.");
            }
            return errorMessages.ToString().Trim();
        }
        public async Task<ServiceResult> AddRangeAsync(IEnumerable<SupplierBookDto> models)
        {
            foreach (var model in models)
            {
                string errmes = Validate(model);
                if (!string.IsNullOrEmpty(errmes))
                    return ServiceResultFactory.BadRequest(errmes);
            }
            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                int nextid = await _unitOfWork.SupplierBookRepository.GetNextSupplierBookIdAsync();
                try
                {
                    foreach (var model in models)
                    {
                        var supplierbook = _mapper.Map<SupplierBook>(model);
                        supplierbook.SupplierBookId = nextid;
                        await _unitOfWork.SupplierBookRepository.AddAsync(supplierbook);
                        var book = await _unitOfWork.BookRepository.GetByIdAsync(supplierbook.BookId);
                        book.Quantity = (book.Quantity ?? 0) + supplierbook.Quantity;
                        await _unitOfWork.BookRepository.UpdateAsync(book);
                    }

                    await _unitOfWork.SaveChangeAsync();
                    await transaction.CommitAsync();
                    return ServiceResultFactory.Created("Nhập hàng mới thành công!");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return ServiceResultFactory.BadRequest();
                }
            }
        }

        public async Task<ServiceResult> Delete(int id)
        {
            var item = (await _unitOfWork.SupplierBookRepository.GetByIdAsync(id));
            if (item == null || item.Count() == 0)
            {
                ServiceResultFactory.NotFound("Không đơn nhập hàng cấp cần xóa");
            }
            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    
                    foreach (var suply in item)
                    {
                        var book = await _unitOfWork.BookRepository.GetByIdAsync(suply.BookId);
                        book.Quantity -= suply.Quantity;
                        if (book.Quantity < 0) throw new Exception();
                        await _unitOfWork.BookRepository.UpdateAsync(book);
                    }
                    await _unitOfWork.SupplierBookRepository.DeleteAsync(id);
                    await _unitOfWork.SaveChangeAsync();
                    await transaction.CommitAsync();
                    return ServiceResultFactory.Ok("Xóa đơn nhập hàng!");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return new ServiceResult
                    {
                        StatusCode = 500,
                        ApiResult = new ApiResult
                        {
                            Success = false,
                            ErrMessage = "Lỗi khi xóa đơn nhập hàng: " + ex.Message
                        }
                    };
                }
            }
        }

        public async Task<ServiceResult> GetAll(int? pageNumber = null, int? pageSize = null)
        {
            var supplierBooks = await _unitOfWork.SupplierBookRepository.GetAllAsync();

            if (supplierBooks == null || !supplierBooks.Any())
            {
                return new ServiceResult
                {
                    StatusCode = 204,
                    ApiResult = new ApiResult { Success = false, ErrMessage = "Không tim thấy đơn nhập hàng nào" }
                };
            }

            var supplierBookDtos = _mapper.Map<IEnumerable<SupplierBookDto>>(supplierBooks);

            var groupData = supplierBookDtos.GroupBy(sb => new { sb.SupplierBookId, sb.SupplierId, sb.SupplierName, sb.SupplyDate }).Select(

                g => new
                {
                    g.Key.SupplierBookId,
                    g.Key.SupplierId,
                    g.Key.SupplierName,
                    g.Key.SupplyDate,
                    Books = g.Select(x => new
                    {
                        x.BookId,
                        x.Quantity,

                        x.SupplyPrice
                    }).ToList()
                });
            var paged = PaginationHelper.Paginate(groupData, pageNumber, pageSize);
            return new ServiceResult
            {
                StatusCode = 200,
                ApiResult = new ApiResult
                {
                    Success = true,
                    Count = groupData.Count(),
                    Data = paged
                }
            };
        }

        public async Task<ServiceResult> GetBySuppierIdAsync(int supplierBookId, int? pageNumber = null, int? pageSize = null)
        {
            var supplierBooks = await _unitOfWork.SupplierBookRepository.GetByIdAsync(supplierBookId);
            if (supplierBooks == null)
            {
                return new ServiceResult
                {
                    StatusCode = 404,
                    ApiResult = new ApiResult { Success = false, ErrMessage = "Không tìm thấy đơn nhập hàng nào" }
                };
            }
            // Chuyển đổi dữ liệu SupplierBook sang DTO
            //var supplierBookDtos = _mapper.Map<SupplierBookDto>(query);

            //return new ServiceResult
            //{
            //    StatusCode = 200,
            //    ApiResult = new ApiResult
            //    {
            //        Success = true,
            //        Data = supplierBookDtos
            //    }
            //};
            var supplierBookDtos = _mapper.Map<IEnumerable<SupplierBookDto>>(supplierBooks);

            var groupData = supplierBookDtos.GroupBy(sb => new { sb.SupplierBookId, sb.SupplierId, sb.SupplierName, sb.SupplyDate }).Select(

                g => new
                {
                    g.Key.SupplierBookId,
                    g.Key.SupplierId,
                    g.Key.SupplierName,
                    g.Key.SupplyDate,
                    Books = g.Select(x => new
                    {
                        x.BookId,
                        x.Quantity,

                        x.SupplyPrice
                    }).ToList()
                });
            var paged = PaginationHelper.Paginate(groupData, pageNumber, pageSize);
            return new ServiceResult
            {
                StatusCode = 200,
                ApiResult = new ApiResult
                {
                    Success = true,
                    Count = groupData.Count(),
                    Data = paged
                }
            };
        }
        public async Task<ServiceResult> GetByFilterAsync(int? supplierId = null, int? bookid = null, string? bookName = null, string? supplierName = null, DateTime? minDate = null, DateTime? maxDate = null, int? pageNumber = null, int? pageSize = null)
        {
            IEnumerable<SupplierBook> supplierBook = await _unitOfWork.SupplierBookRepository.GetByFilterAsync(supplierId, bookid, bookName, supplierName, minDate, maxDate);
            // Chuyển đổi dữ liệu SupplierBook sang DTO
            if (!supplierBook.Any())
                return ServiceResultFactory.NoContent();

            var supplierBookDtos = _mapper.Map<IEnumerable<SupplierBookDto>>(supplierBook);

            var groupData = GroupSuppierBook(supplierBookDtos);
            var paged = PaginationHelper.Paginate(groupData, pageNumber, pageSize);
            return new ServiceResult
            {
                StatusCode = 200,
                ApiResult = new ApiResult
                {
                    Count = groupData.Count(),
                    Data = paged,
                    Success = true
                }
            };
        }

        //public async Task<ServiceResult> Update(SupplierBookDto model)
        //{
        //    var supplier = (await _unitOfWork.SupplierBookRepository.GetByIdAsync(model.SupplierBookId));
        //    if (supplier == null)
        //    {
        //        return ServiceResultFactory.NotFound("Không tìm thấy nhà cung cấp cần update");
        //    }
        //    int preQuality = supplier.Quantity;
        //    int afterQuality = model.Quantity;
        //    var book = await _unitOfWork.BookRepository.GetByIdAsync(supplier.BookId);
        //    book.Quantity -= (preQuality - afterQuality);
        //    UpdateSupplierBookFromDto(supplier, model);
        //    await _unitOfWork.SupplierBookRepository.UpdateAsync(supplier);
        //    await _unitOfWork.BookRepository.UpdateAsync(book);
        //    await _unitOfWork.SaveChangeAsync();
        //    return new ServiceResult
        //    {
        //        StatusCode = 200,
        //        ApiResult = new ApiResult
        //        {
        //            Success = true,
        //            Message = "Update nhà cung cấp thành công!",
        //            Data = model
        //        }
        //    };
        //}
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

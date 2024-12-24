using AutoMapper;
using NhaSachDaiThang_BE_API.Data;
using NhaSachDaiThang_BE_API.Helper;
using NhaSachDaiThang_BE_API.Helper.GlobalVar;
using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Services.IServices;
using NhaSachDaiThang_BE_API.UnitOfWork;
using System.Text.RegularExpressions;

namespace NhaSachDaiThang_BE_API.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public OrderService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResult> AddAsync(OrderDto model)
        {
            string phonePattern = @"^(84|0)[1-9][0-9]{8,9}$";
            //string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (!Regex.IsMatch(model.Phone, phonePattern, RegexOptions.IgnoreCase))
            {
                return ServiceResultFactory.BadRequest("Số điện thoại không hợp lệ");
            }

            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    var orderDetails = model.OrderDetails;
                    model.OrderDetails = new List<OrderDetailDto>();
                    var orderInsert = _mapper.Map<Order>(model);
                    orderInsert.CreatedDate = DateTime.Now;
                    var obj = await _unitOfWork.OrderRepository.AddAsync(orderInsert);
                    await _unitOfWork.SaveChangeAsync();
                    int id = orderInsert.OrderId;

                    foreach (var i in orderDetails)
                    {
                        if (i.Quantity < 0)
                        {
                            throw new InvalidOperationException($"Số lượng sách  không được < 0.");
                        }
                        var book = await _unitOfWork.BookRepository.GetByIdAsync(i.BookId.Value);
                        book.Quantity -= i.Quantity;
                        if (book.Quantity < 0)
                        {
                            throw new InvalidOperationException($"Số lượng sách {book.Title} không đủ.");
                        }
                        var maped = _mapper.Map<OrderDetail>(i);
                        maped.OrderId = id;
                        await _unitOfWork.OrderDetailRepository.AddAsync(maped);


                    }
                    await _unitOfWork.SaveChangeAsync();
                    await transaction.CommitAsync();
                    var data = new
                    {
                        orderId = id
                    };
                    return ServiceResultFactory.Create(statusCode: 201, message: "Thêm đơn hàng thành công", success: true, data: data);

                }
                catch (InvalidOperationException ex1)
                {
                    await transaction.RollbackAsync();
                    return ServiceResultFactory.BadRequest(ex1.Message);
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
                            ErrMessage = "Lỗi khi thêm đơn hàng: " + ex.Message
                        }
                    };
                }
            }
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
            if (order != null)
            {
                var orderDetails = await _unitOfWork.OrderDetailRepository.GetByOrderIdAsync(id);
                foreach (var i in orderDetails)
                {
                    var maped = _mapper.Map<OrderDetail>(i);
                    maped.OrderId = id;
                    await _unitOfWork.OrderDetailRepository.AddAsync(maped);

                    var book = await _unitOfWork.BookRepository.GetByIdAsync(i.BookId.Value);
                    book.Quantity += i.Quantity;
                }
                _unitOfWork.OrderDetailRepository.RemoveRange(orderDetails);

                await _unitOfWork.OrderRepository.DeleteAsync(id);
                await _unitOfWork.SaveChangeAsync();
                return ServiceResultFactory.Ok("Xóa đơn hàng thành công");
            }
            return ServiceResultFactory.NotFound("Không tìm thấy đơn hàng cần xóa");

        }

        public async Task<ServiceResult> GetByIdAsync(int id)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
            var request = _httpContextAccessor.HttpContext?.Request;
            var baseUrl = $"{request?.Scheme}://{request?.Host}";
            var mappedData = _mapper.Map<OrderDto>(order);
            foreach (var item in mappedData.OrderDetails)
            {
                item.MainImage = $"{baseUrl}/{GlobalConst.BookImageRelativePath}/{item.MainImage}";
            }
            if (order != null)
            {
                return new ServiceResult
                {
                    StatusCode = 200,
                    ApiResult = new ApiResult
                    {
                        Success = true,
                        Data = mappedData
                    }
                };
            }
            return new ServiceResult
            {
                StatusCode = 404,
                ApiResult = new ApiResult
                {
                    Success = false,
                    Data = mappedData
                }
            };
        }

        public async Task<ServiceResult> GetFilteredAsync(DateTime? minorderDate = null, DateTime? maxorderDate = null, DateTime? deliverdDate = null, string? customerName = null, string? status = null, int? userId = null, string? phoneNumber = null, int? pageNumber = null, int? pageSize = null)
        {
            var rs = await _unitOfWork.OrderRepository.GetFilteredAsync(minorderDate, maxorderDate, deliverdDate, customerName, status, userId, phoneNumber);
            var count = rs.Count();
            var paged = PaginationHelper.Paginate(rs, pageNumber, pageSize);
            var request = _httpContextAccessor.HttpContext?.Request;
            var baseUrl = $"{request?.Scheme}://{request?.Host}";
            if (rs != null)
            {
                var mapped = _mapper.Map<IEnumerable<OrderDto>>(paged);
                foreach (var item in mapped)
                {
                    foreach (var itemDto in item.OrderDetails)
                    {
                        itemDto.MainImage = $"{baseUrl}/{GlobalConst.BookImageRelativePath}/{itemDto.MainImage}";
                    }
                }
                return new ServiceResult
                {

                    StatusCode = 200,
                    ApiResult = new ApiResult
                    {
                        Count = count,
                        Success = true,
                        Data = mapped
                    }
                };
            }
            return ServiceResultFactory.NotFound("Không tìm thấy đơn hàng nào");
        }

        public async Task<ServiceResult> UpdateAsync(OrderDto model)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(model.OrderId);
            if (order == null) return ServiceResultFactory.NotFound();
            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {

                    //UpdateOrderFromDto(order, model);
                    order.RecipientName = model.RecipientName;
                    order.ShippingAddress = model.ShippingAddress;
                    order.ModifyBy = model.ModifyBy;
                    order.ModifyDate = DateTime.Now;
                    order.Phone = model.Phone;
                    order.Email = model.Email;  
                    order.Status = model.Status;
                    await _unitOfWork.SaveChangeAsync();
                    if (order.Status.ToLower() == OrderStatus.Cancelled.ToLower() || order.Status.ToLower() == OrderStatus.Returned.ToLower())
                    {
                        var orderdetail = order.OrderDetails;
                        foreach (var detail in orderdetail)
                        {
                            var book = await _unitOfWork.BookRepository.GetByIdAsync(detail.BookId.Value);
                            if (book != null)
                            {
                                book.Quantity += detail.Quantity;
                            }
                            else
                            {
                                throw new Exception("Lỗi server");
                            }
                        }
                    }
                    await _unitOfWork.SaveChangeAsync();
                    await transaction.CommitAsync();
                    return ServiceResultFactory.Ok("Cập nhất thông tin đơn hàng thành công");
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
                            ErrMessage = "Lỗi khi thêm đơn hàng: " + ex.Message
                        }
                    };
                }

            }

        }
        public async Task<ServiceResult> UpdateStauaAsync(int? userId, int id, string status)
        {
            BookStoreContext db = new BookStoreContext();
            var order = await db.Order.FindAsync(id);
            if (order == null) return ServiceResultFactory.NotFound();
            if (userId.HasValue)
            {
                if (order.UserId != userId) return ServiceResultFactory.Unauthorized();
            }
            using (var transaction = await db.Database.BeginTransactionAsync())
            {
                try
                {

                    order.Status = status;
                    if (status.ToLower() == OrderStatus.DeliveredToCustomer) {
                        order.DeliveredDate = DateTime.Now;
                    }
                    db.SaveChanges();

                    if (order.Status.ToLower() == OrderStatus.Cancelled.ToLower() || order.Status.ToLower() == OrderStatus.Returned.ToLower())
                    {
                        var orderdetail = order.OrderDetails;
                        foreach (var detail in orderdetail)
                        {
                            var book = await db.Book.FindAsync(detail.BookId.Value);
                            if (book != null)
                            {
                                book.Quantity += detail.Quantity;
                            }
                            else
                            {
                                throw new Exception("Lỗi server");
                            }
                        }
                    }
                    await db.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return ServiceResultFactory.Ok("Cập nhất thông tin đơn hàng thành công", order);
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
                            ErrMessage = "Lỗi khi thêm đơn hàng: " + ex.Message
                        }
                    };
                }

            }

        }
        public void UpdateOrderFromDto(Order order, OrderDto orderDto)
        {
            var properties = typeof(OrderDto).GetProperties();
            foreach (var property in properties)
            {
                if (property.Name == "OrderId" || property.Name == "OrderDetails") continue;
                var value = property.GetValue(orderDto);
                if (value != null)
                {
                    var orderProperty = typeof(Order).GetProperty(property.Name);
                    if (orderProperty != null)
                    {
                        orderProperty.SetValue(order, value);
                    }
                }
            }
        }
    }
}

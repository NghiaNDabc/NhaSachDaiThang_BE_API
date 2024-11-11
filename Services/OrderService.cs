using AutoMapper;
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

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
                    var obj = await _unitOfWork.OrderRepository.AddAsync(orderInsert);
                    await _unitOfWork.SaveChangeAsync();
                    int id = orderInsert.OrderId;

                    foreach (var i in orderDetails)
                    {
                        var book = await _unitOfWork.BookRepository.GetByIdAsync(i.BookId.Value);
                        book.Quantity -= i.Quantity;
                        if (book.Quantity < 0)
                        {
                            throw new InvalidOperationException("Số lượng sách không đủ.");
                        }
                        var maped = _mapper.Map<OrderDetail>(i);
                        maped.OrderId = id;
                        await _unitOfWork.OrderDetailRepository.AddAsync(maped);


                    }
                    await _unitOfWork.SaveChangeAsync();
                    await transaction.CommitAsync();
                    return ServiceResultFactory.Create(statusCode: 201, message: "Thêm đơn hàng thành công", success: true);

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

        public async Task<ServiceResult> Delete(int id)
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
                return new ServiceResult
                {
                    StatusCode = 200,
                    ApiResult = new ApiResult
                    {
                        Success = true,
                        Message = "Xóa đơn hàng thành công",
                        Data = order
                    }
                };
            }
            return new ServiceResult
            {
                StatusCode = 404,
                ApiResult = new ApiResult
                {
                    Success = false,
                    ErrMessage = "Không tìm thấy đơn hàng cần xóa",
                    Data = order
                }
            };

        }

        public async Task<ServiceResult> GetByIdAsync(int id)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
            if (order != null)
            {
                return new ServiceResult
                {
                    StatusCode = 200,
                    ApiResult = new ApiResult
                    {
                        Success = true,
                        Data = _mapper.Map<OrderDto>(order)
                    }
                };
            }
            return new ServiceResult
            {
                StatusCode = 404,
                ApiResult = new ApiResult
                {
                    Success = false,
                    Data = order
                }
            };
        }

        public async Task<ServiceResult> GetFilteredAsync(DateTime? orderDate = null, DateTime? deliverdDate = null, string? customerName = null, string? status = null, int? pageNumber = null, int? pageSize = null)
        {
            var rs = _unitOfWork.OrderRepository.GetFilteredAsync(orderDate, deliverdDate, customerName, status, pageNumber, pageSize);

            if (rs != null)
            {
                var mapped = _mapper.Map<OrderDto>(rs);
                return new ServiceResult
                {
                    StatusCode = 200,
                    ApiResult = new ApiResult
                    {
                        Success = true,
                        Data = mapped
                    }
                };
            }
            return ServiceResultFactory.NotFound("Không tìm thấy đơn hàng nào");
        }

        public async Task<ServiceResult> Update(OrderDto model)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(model.OrderId);
            if (order == null) return ServiceResultFactory.NotFound();


            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {

                    UpdateOrderFromDto(order, model);
                    _unitOfWork.OrderRepository.Update(order);
                    await _unitOfWork.SaveChangeAsync();
                    if (order.Status == OrderStatus.Cancelled)
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

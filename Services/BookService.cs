using AutoMapper;
using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Services.IServices;
using NhaSachDaiThang_BE_API.UnitOfWork;

namespace NhaSachDaiThang_BE_API.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BookService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ServiceResult> Add(BookDto model, List<IFormFile> imageFiles)
        {

            if (imageFiles == null || imageFiles.Count == 0)
            {
                return new ServiceResult
                {
                    StatusCode = 400,
                    ApiResult = new ApiResult
                    {
                        Success = false,
                        ErrMessage = "Không có hình ảnh nào được gửi."
                    }
                };
            }


            var mainImageResult = await UploadImage(imageFiles[0]); // Lưu ảnh
            if (!mainImageResult.ApiResult.Success)
            {
                return mainImageResult;
            }

            model.MainImage = mainImageResult.ApiResult.Data.ToString(); // Gán tên file cho mainImage

            // Xử lý các hình ảnh bổ sung
            var additionalImages = new List<string>();
            for (int i = 1; i < imageFiles.Count; i++)
            {
                var additionalImageResult = await UploadImage(imageFiles[i]);
                if (!additionalImageResult.ApiResult.Success)
                {
                    return additionalImageResult;
                }
                additionalImages.Add(additionalImageResult.ApiResult.Data.ToString()); // Gán tên file 
            }

            model.AdditionalImages = string.Join(";", additionalImages); 


            var book = _mapper.Map<Book>(model);
            await _unitOfWork.BookRepository.AddAsync(book);
            await _unitOfWork.SaveChangeAsync();

            return new ServiceResult
            {
                StatusCode = 201,
                ApiResult = new ApiResult
                {
                    Success = true,
                    Message = "Sách đã được thêm thành công."
                }
            };
        }

        public async Task<ServiceResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return new ServiceResult
                {
                    StatusCode = 400,
                    ApiResult = new ApiResult { Success = false, Message = "File không hợp lệ." }
                };
            }

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

            // Tạo thư mục nếu không tồn tại
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileName = Path.GetFileName(file.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            // Lưu file vào thư mục
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return new ServiceResult
            {
                StatusCode = 200,
                ApiResult = new ApiResult
                {
                    Success = true,
                    Message = "File đã được tải lên thành công.",
                    Data = fileName
                }
            };
        }

        public async Task<ServiceResult> Add(BookDto model)
        {
            var book = _mapper.Map<Book>(model);
            await _unitOfWork.BookRepository.AddAsync(book);
            await _unitOfWork.SaveChangeAsync(); // Lưu thay đổi vào cơ sở dữ liệu
            return new ServiceResult();
        }

        public async Task<ServiceResult> Delete(int id)
        {
            if (await _unitOfWork.BookRepository.GetByIdAsync(id) != null)
            {
                return new ServiceResult
                {
                    StatusCode = 400,
                    ApiResult = new ApiResult
                    {
                        Success = false,
                        ErrMessage = "Không tìm thấy sách cần xóa"
                    }
                };
            }
            await _unitOfWork.BookRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangeAsync();
            return new ServiceResult
            {
                StatusCode = 200,
                ApiResult = new ApiResult
                {
                    Success = true,
                    Message = "Xóa sách thành công!"
                }
            };
        }

        public async Task<ServiceResult> GetAll()
        {
            var books = await _unitOfWork.BookRepository.GetAllAsync();

            if (books == null || !books.Any())
            {
                return new ServiceResult
                {
                    StatusCode = 404,
                    ApiResult = new ApiResult { Success = false, Message = "No books found." }
                };
            }

            // Chuyển đổi dữ liệu sách sang DTO
            var bookDtos = books.Select(book =>
            {
                var bookDto = _mapper.Map<BookDto>(book);

                // Lấy base URL từ HttpContext
                var request = _httpContextAccessor.HttpContext?.Request;
                var baseUrl = $"{request?.Scheme}://{request?.Host}";

                bookDto.MainImage = $"{baseUrl}/images/{bookDto.MainImage}";

                return bookDto;
            }).ToList();

            return new ServiceResult
            {
                StatusCode = 200,
                ApiResult = new ApiResult
                {
                    Success = true,
                    Data = bookDtos
                }
            };
        }

        public async Task<ServiceResult> GetAllActive()
        {
            var books = await _unitOfWork.BookRepository.GetAllActiveAsync();

            if (books == null || !books.Any())
            {
                return new ServiceResult
                {
                    StatusCode = 404,
                    ApiResult = new ApiResult { Success = false, Message = "No books found." }
                };
            }

            // Chuyển đổi dữ liệu sách sang DTO
            var bookDtos = books.Select(book =>
            {
                var bookDto = _mapper.Map<BookDto>(book);

                // Lấy base URL từ HttpContext
                var request = _httpContextAccessor.HttpContext?.Request;
                var baseUrl = $"{request?.Scheme}://{request?.Host}";

                bookDto.MainImage = $"{baseUrl}/images/{bookDto.MainImage}";

                return bookDto;
            }).ToList();

            return new ServiceResult
            {
                StatusCode = 200,
                ApiResult = new ApiResult
                {
                    Success = true,
                    Data = bookDtos
                }
            };
        }


        public async Task<ServiceResult> GetById(int id)
        {
            var book = await _unitOfWork.BookRepository.GetByIdAsync(id);
            if (book == null)
                return new ServiceResult
                {
                    StatusCode = 404,
                    ApiResult = new ApiResult
                    {
                        Success = false,
                        ErrMessage = "Không tìm thấy sách có id " + id

                    }
                };
            var bookDto = _mapper.Map<BookDto>(book);
            var requst = _httpContextAccessor.HttpContext?.Request;
            var baseUrl = $"{requst?.Scheme}://{requst?.Host}";
            bookDto.MainImage = $"{baseUrl}/images/{bookDto.MainImage}";
            if (!String.IsNullOrEmpty(bookDto.AdditionalImages))
            {
                 var imgUrl = bookDto.AdditionalImages.Split(';', StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => $"{baseUrl}/images/{x.Trim()}" ).ToArray();
                bookDto.AdditionalImages = string.Join(";", imgUrl);
            }

            
            return new ServiceResult
            {
                StatusCode = 404,
                ApiResult = new ApiResult
                {
                    Success = false,
                    Data = bookDto

                }
            };
        }

        public async Task<ServiceResult> GetByNameAsync(string name)
        {
            var books = await _unitOfWork.BookRepository.GetByNameAsync(name);

            if (books == null || !books.Any())
            {
                return new ServiceResult
                {
                    StatusCode = 404,
                    ApiResult = new ApiResult { Success = false, Message = "No books found." }
                };
            }

            // Chuyển đổi dữ liệu sách sang DTO
            var bookDtos = books.Select(book =>
            {
                var bookDto = _mapper.Map<BookDto>(book);

                // Lấy base URL từ HttpContext
                var request = _httpContextAccessor.HttpContext?.Request;
                var baseUrl = $"{request?.Scheme}://{request?.Host}";

                bookDto.MainImage = $"{baseUrl}/images/{bookDto.MainImage}";

                return bookDto;
            }).ToList();

            return new ServiceResult
            {
                StatusCode = 200,
                ApiResult = new ApiResult
                {
                    Success = true,
                    Data = bookDtos
                }
            };
        }

        public async Task<ServiceResult> SoftDelete(int id)
        {
            if (await _unitOfWork.BookRepository.GetByIdAsync(id) != null)
            {
                return new ServiceResult
                {
                    StatusCode = 404,
                    ApiResult = new ApiResult
                    {
                        Success = false,
                        ErrMessage = "Không tìm thấy sach"
                    }
                };
            }
            await _unitOfWork.BookRepository.SoftDelete(id);
            await _unitOfWork.SaveChangeAsync();
            return new ServiceResult
            {
                StatusCode = 200,
                ApiResult = new ApiResult
                {
                    Success = true,
                    Message = "Ẩn sách thành công!"
                }
            };
        }

        public Task<ServiceResult> Update(BookDto model)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult> GetBooksByCategoryId(int id)
        {
            var books = await _unitOfWork.BookRepository.GetBooksByCategoryId(id);
            if (books == null || !books.Any())
            {
                return new ServiceResult
                {
                    StatusCode = 404,
                    ApiResult = new ApiResult { Success = false, Message = "No books found." }
                };
            }

            // Chuyển đổi dữ liệu sách sang DTO
            var bookDtos = books.Select(book =>
            {
                var bookDto = _mapper.Map<BookDto>(book);

                // Lấy base URL từ HttpContext
                var request = _httpContextAccessor.HttpContext?.Request;
                var baseUrl = $"{request?.Scheme}://{request?.Host}";

                bookDto.MainImage = $"{baseUrl}/images/{bookDto.MainImage}";

                return bookDto;
            }).ToList();

            return new ServiceResult
            {
                StatusCode = 200,
                ApiResult = new ApiResult
                {
                    Success = true,
                    Data = bookDtos
                }
            };
        }

        public async Task<ServiceResult> GetActiveById(int id)
        {
            var book = await _unitOfWork.BookRepository.GetActiveByIdAsync(id);
            if (book == null)
                return new ServiceResult
                {
                    StatusCode = 404,
                    ApiResult = new ApiResult
                    {
                        Success = false,
                        ErrMessage = "Không tìm thấy sách có id " + id

                    }
                };
            var bookDto = _mapper.Map<BookDto>(book);
            var requst = _httpContextAccessor.HttpContext?.Request;
            var baseUrl = $"{requst?.Scheme}://{requst?.Host}";
            bookDto.MainImage = $"{baseUrl}/images/{bookDto.MainImage}";
            return new ServiceResult
            {
                StatusCode = 200,
                ApiResult = new ApiResult
                {
                    Success = false,
                    Data = bookDto

                }
            };
        }

        public async Task<ServiceResult> GetActiveByName(string name)
        {
            var book = _unitOfWork.BookRepository.GetActiveByNameAsync(name);
            if (book == null)
                return new ServiceResult
                {
                    StatusCode = 404,
                    ApiResult = new ApiResult
                    {
                        Success = false,
                        ErrMessage = "Không tìm thấy sách có tên: " + name

                    }
                };
            var bookDto = _mapper.Map<BookDto>(book);
            var requst = _httpContextAccessor.HttpContext?.Request;
            var baseUrl = $"{requst?.Scheme}://{requst?.Host}";
            bookDto.MainImage = $"{baseUrl}/images/{bookDto.MainImage}";
            return new ServiceResult
            {
                StatusCode = 200,
                ApiResult = new ApiResult
                {
                    Success = false,
                    Data = bookDto

                }
            };
        }
    }
}

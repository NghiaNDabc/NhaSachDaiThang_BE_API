using AutoMapper;
using NhaSachDaiThang_BE_API.Helper;
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
        private readonly IUploadFile _uploadFile;
        public BookService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, IUploadFile uploadFile)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _uploadFile = uploadFile;
        }
        public async Task<ServiceResult> Add(BookDto model, List<IFormFile> imageFiles)
        {

            //if (imageFiles == null || imageFiles.Count == 0)
            //{
            //    return new ServiceResult
            //    {
            //        StatusCode = 400,
            //        ApiResult = new ApiResult
            //        {
            //            Success = false,
            //            ErrMessage = "Không có hình ảnh nào được gửi."
            //        }
            //    };
            //}


            var mainImageResult = await _uploadFile.UploadImage(imageFiles[0], GlobalConst.BookImagePhysicalPath); // Lưu ảnh
            if (!mainImageResult.ApiResult.Success)
            {
                return mainImageResult;
            }

            model.MainImage = mainImageResult.ApiResult.Data.ToString(); // Gán tên file cho mainImage

            // Xử lý các hình ảnh bổ sung
            var additionalImages = new List<string>();
            for (int i = 1; i < imageFiles.Count; i++)
            {
                var additionalImageResult = await _uploadFile.UploadImage(imageFiles[i], GlobalConst.BookImagePhysicalPath);
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



        public async Task<ServiceResult> Add(BookDto model)
        {
            var book = _mapper.Map<Book>(model);
            await _unitOfWork.BookRepository.AddAsync(book);
            await _unitOfWork.SaveChangeAsync();
            return new ServiceResult
            {
                StatusCode = 201,
                ApiResult = new ApiResult
                {
                    Success = true,
                    Message = "Thêm mới sách thành công",
                    Data = model
                }
            };
        }

        public async Task<ServiceResult> Delete(int id)
        {
            if (await _unitOfWork.BookRepository.GetByIdAsync(id) != null)
            {
                return new ServiceResult
                {
                    StatusCode = 404,
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
            var bookDtos = MapList(books);

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
            var bookDtos = MapList(books);

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
            var bookDto = MapOne(book);


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
            var bookDtos = MapList(books);

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
        public async Task<ServiceResult> Update(BookDto model, List<IFormFile> imageFiles)
        {
            var exisitngBook = await _unitOfWork.BookRepository.GetByIdAsync(model.BookId);

            if (exisitngBook == null)
            {
                return new ServiceResult
                {
                    StatusCode = 404,
                    ApiResult = new ApiResult
                    {
                        Success = false,
                        ErrMessage = "Không tìm thấy sách cần cập nhật"
                    }
                };
            }

            if (imageFiles == null || imageFiles.Count() == 0)
            {
                model.MainImage = exisitngBook.MainImage;
                model.AdditionalImages = exisitngBook.AdditionalImages;
            }
            else
            {
                var uploadMainImageResult = await _uploadFile.UploadImage(imageFiles[0], GlobalConst.BookImagePhysicalPath);
                if (uploadMainImageResult.ApiResult.Success == false)
                {
                    return uploadMainImageResult;
                }
                model.MainImage = uploadMainImageResult.ApiResult.Data.ToString();
            }
            var imgCount = imageFiles.Count();
            if (imgCount > 0)
            {
                var listAdditionalImage = new List<string>();
                for (int i = 1; i < imgCount; i++)
                {
                    var uploadMainImageResult = await _uploadFile.UploadImage(imageFiles[i], GlobalConst.BookImagePhysicalPath);
                    if (uploadMainImageResult.ApiResult.Success == false)
                    {
                        return uploadMainImageResult;
                    }
                    listAdditionalImage.Add(uploadMainImageResult.ApiResult.Data.ToString());
                }
                model.AdditionalImages = string.Join(';', listAdditionalImage);
            }
            UpdateBookFromDto(exisitngBook, model);

            await _unitOfWork.BookRepository.UpdateAsync(exisitngBook);

            return new ServiceResult
            {
                StatusCode = 200,
                ApiResult = new ApiResult
                {
                    Success = true,
                    Message = "Cập nhật sách thành công",
                    Data = model
                }
            };
        }

        public void UpdateBookFromDto(Book book, BookDto bookDto)
        {
            var properties = typeof(BookDto).GetProperties();
            foreach (var property in properties)
            {
                if (property.Name == "BookId") continue;
                var value = property.GetValue(bookDto);
                if (value != null)
                {
                    var bookProperty = typeof(Book).GetProperty(property.Name);
                    if (bookProperty != null)
                    {
                        bookProperty.SetValue(book, value);
                    }
                }
            }
        }
        List<BookDto> MapList(IEnumerable<Book> books)
        {
            return books.Select(book =>
            {
                var bookDto = _mapper.Map<BookDto>(book);

                // Lấy base URL từ HttpContext
                var request = _httpContextAccessor.HttpContext?.Request;
                var baseUrl = $"{request?.Scheme}://{request?.Host}";

                bookDto.MainImage = $"{baseUrl}/{GlobalConst.BookImageRelativePath}/{bookDto.MainImage}";
                if (!string.IsNullOrEmpty(bookDto.AdditionalImages))
                {

                    var imgUrls = bookDto.AdditionalImages.Contains(";")
                        ? bookDto.AdditionalImages.Split(';', StringSplitOptions.RemoveEmptyEntries)
                            .Select(x => $"{baseUrl}/{GlobalConst.BookImageRelativePath}/{x.Trim()}")
                        : new[] { $"{baseUrl}/{GlobalConst.BookImageRelativePath}/{bookDto.AdditionalImages.Trim()}" };

                    bookDto.AdditionalImages = string.Join(";", imgUrls);
                }
                return bookDto;
            }).ToList();
        }
        BookDto MapOne(Book book)
        {
            var bookDto = _mapper.Map<BookDto>(book);
            var requst = _httpContextAccessor.HttpContext?.Request;
            var baseUrl = $"{requst?.Scheme}://{requst?.Host}";
            bookDto.MainImage = $"{baseUrl}/{GlobalConst.BookImageRelativePath}/{bookDto.MainImage}";
            if (!String.IsNullOrEmpty(bookDto.AdditionalImages))
            {
                var imgUrls = bookDto.AdditionalImages.Contains(";")
                        ? bookDto.AdditionalImages.Split(';', StringSplitOptions.RemoveEmptyEntries)
                            .Select(x => $"{baseUrl}/{GlobalConst.BookImageRelativePath}/{x.Trim()}")
                        : new[] { $"{baseUrl}/{GlobalConst.BookImageRelativePath}/{bookDto.AdditionalImages.Trim()}" };

                bookDto.AdditionalImages = string.Join(";", imgUrls);
            }
            return bookDto;
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
            var bookDtos = MapList(books);

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
            var bookDto = MapOne(book);
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
            var book = await _unitOfWork.BookRepository.GetActiveByNameAsync(name);
            if (book == null || book.Count() <= 0)
                return new ServiceResult
                {
                    StatusCode = 404,
                    ApiResult = new ApiResult
                    {
                        Success = false,
                        ErrMessage = "Không tìm thấy sách có tên: " + name

                    }
                };
            var bookDto = MapList(book);
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

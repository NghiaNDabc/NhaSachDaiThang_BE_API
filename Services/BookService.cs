using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NhaSachDaiThang_BE_API.Helper;
using NhaSachDaiThang_BE_API.Helper.GlobalVar;
using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Services.IServices;
using NhaSachDaiThang_BE_API.UnitOfWork;
using System.Text;
using System.Text.RegularExpressions;

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
        string checkBook(BookDto book)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (string.IsNullOrEmpty(book.Title)) stringBuilder.AppendLine("Tiêu đề không được để trống");
            if (string.IsNullOrEmpty(book.Author)) stringBuilder.AppendLine("Tác giả không được để trống");
            if (string.IsNullOrEmpty(book.Publisher)) stringBuilder.AppendLine("Nhà xuất bản không được để trống");
            if (book.PageCount.Value < 0) stringBuilder.AppendLine("Số trang không được nhỏ hơn 0");
            //if (book.PromotionEndDate != null && book.PromotionEndDate < DateTime.Now)
            //    stringBuilder.AppendLine("Ngày kết thúc khuyến mãi không hợp lệ");
            if (book.Weight < 0)
                stringBuilder.AppendLine("Ngày cân nặng không được < 0");
            if (book.Price < 0)
                stringBuilder.AppendLine("Giá không được < 0");
            return stringBuilder.ToString();

        }
        public async Task<ServiceResult> AddAsync(BookDto model, List<IFormFile> imageFiles)
        {
            string errmes = checkBook(model);
            if (!string.IsNullOrEmpty(errmes))
            {
                return ServiceResultFactory.BadRequest(errmes);
            }

            // Tạo thực thể Book từ DTO và lưu tạm để lấy ID
            var book = _mapper.Map<Book>(model);
            book.CreatedDate = DateTime.Now;

            await _unitOfWork.BookRepository.AddAsync(book);
            await _unitOfWork.SaveChangeAsync();

            // Lấy ID của sách vừa tạo
            var bookId = book.BookId;

            // Tạo đường dẫn thư mục dựa trên ID
            string bookImageFolder = Path.Combine(GlobalConst.BookImagePhysicalPath, bookId.ToString());
            if (!Directory.Exists(bookImageFolder))
            {
                Directory.CreateDirectory(bookImageFolder);
            }

            // Xử lý các tệp ảnh
            if (imageFiles != null && imageFiles.Count > 0)
            {
                // Lưu ảnh chính
                var mainImageResult = await _uploadFile.UploadImageAsync(imageFiles[0], bookImageFolder);
                if (!mainImageResult.ApiResult.Success)
                {
                    return mainImageResult;
                }

                model.MainImage = Path.Combine(bookId.ToString(), mainImageResult.ApiResult.Data.ToString());

                // Xử lý các hình ảnh bổ sung
                var additionalImages = new List<string>();
                for (int i = 1; i < imageFiles.Count; i++)
                {
                    var additionalImageResult = await _uploadFile.UploadImageAsync(imageFiles[i], bookImageFolder);
                    if (!additionalImageResult.ApiResult.Success)
                    {
                        return additionalImageResult;
                    }
                    additionalImages.Add(Path.Combine(bookId.ToString(), additionalImageResult.ApiResult.Data.ToString()));
                }

                model.AdditionalImages = string.Join(";", additionalImages);
            }

            // Cập nhật lại thông tin ảnh của sách vào cơ sở dữ liệu
            book.MainImage = model.MainImage;
            book.AdditionalImages = model.AdditionalImages;
           await _unitOfWork.BookRepository.UpdateAsync(book);
            await _unitOfWork.SaveChangeAsync();

            return ServiceResultFactory.Created("Sách đã được thêm thành công.");
        }

        //public async Task<ServiceResult> AddAsync(BookDto model, List<IFormFile> imageFiles)
        //{

        //    string errmes = checkBook(model);
        //    if (!string.IsNullOrEmpty(errmes))
        //    {
        //        return ServiceResultFactory.BadRequest(errmes);
        //    }

        //    if (imageFiles != null && imageFiles.Count() > 0)
        //    {
        //        var mainImageResult = await _uploadFile.UploadImage(imageFiles[0], GlobalConst.BookImagePhysicalPath); // Lưu ảnh
        //        if (!mainImageResult.ApiResult.Success)
        //        {
        //            return mainImageResult;
        //        }

        //        model.MainImage = mainImageResult.ApiResult.Data.ToString(); // Gán tên file cho mainImage

        //        // Xử lý các hình ảnh bổ sung
        //        var additionalImages = new List<string>();
        //        for (int i = 1; i < imageFiles.Count; i++)
        //        {
        //            var additionalImageResult = await _uploadFile.UploadImage(imageFiles[i], GlobalConst.BookImagePhysicalPath);
        //            if (!additionalImageResult.ApiResult.Success)
        //            {
        //                return additionalImageResult;
        //            }
        //            additionalImages.Add(additionalImageResult.ApiResult.Data.ToString()); // Gán tên file 
        //        }

        //        model.AdditionalImages = string.Join(";", additionalImages);
        //    }


        //    var book = _mapper.Map<Book>(model);
        //    book.CreatedDate = DateTime.Now;
        //    await _unitOfWork.BookRepository.AddAsync(book);
        //    await _unitOfWork.SaveChangeAsync();

        //    return ServiceResultFactory.Created("Sách đã được thêm thành công.");
        //}



        public async Task<ServiceResult> AddAsync(BookDto model)
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

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var item = await _unitOfWork.BookRepository.GetByIdAsync(id);
            if (item == null)
            {
                return ServiceResultFactory.NotFound("Không tìm thấy sách cần xóa");
            }
            if (!string.IsNullOrEmpty(item.MainImage))
            {
                _uploadFile.DeleteFile(item.MainImage, GlobalConst.BookImagePhysicalPath);
            }
            if (item.AdditionalImages != null && item.AdditionalImages.Count() > 0)
            {
                foreach (var i in item.AdditionalImages.Split(";"))
                {
                    _uploadFile.DeleteFile(i, GlobalConst.BookImagePhysicalPath);
                }
            }
            await _unitOfWork.BookRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangeAsync();
            return ServiceResultFactory.Ok("Xóa sách thành công!");
        }

        public async Task<ServiceResult> GetAllAsync(int? pageNumber = null, int? pageSize = null)
        {
            var books = await _unitOfWork.BookRepository.GetAllAsync(pageNumber, pageSize);
            var count = await _unitOfWork.BookRepository.CountByFillterAsync();

            if (books == null || !books.Any())
            {
                return new ServiceResult
                {
                    StatusCode = 204,
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
                    Count = count,
                    Data = bookDtos
                }
            };
        }

        public async Task<ServiceResult> GetAllActiveAsync(int? pageNumber = null, int? pageSize = null)
        {
            var books = await _unitOfWork.BookRepository.GetAllActiveAsync(pageNumber, pageSize);

            if (books == null || !books.Any())
            {
                return new ServiceResult
                {
                    StatusCode = 204,
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
                    Data = bookDtos,
                    Count = bookDtos.Count()
                }
            };
        }


        public async Task<ServiceResult> GetByIdAsync(int id)
        {
            var book = await _unitOfWork.BookRepository.GetByIdAsync(id);
            if (book == null)
                return new ServiceResult
                {
                    StatusCode = 204,
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



        public async Task<ServiceResult> ChangStatusAsync(int id)
        {
            if (await _unitOfWork.BookRepository.GetByIdAsync(id) == null)
            {
                return new ServiceResult
                {
                    StatusCode = 204,
                    ApiResult = new ApiResult
                    {
                        Success = false,
                        ErrMessage = "Không tìm thấy sach"
                    }
                };
            }
            await _unitOfWork.BookRepository.SoftDeleteAsync(id);
            await _unitOfWork.SaveChangeAsync();
            return new ServiceResult
            {
                StatusCode = 200,
                ApiResult = new ApiResult
                {
                    Success = true,
                    Message = "Thay đổi trạng thái thành công"
                }
            };
        }

        public Task<ServiceResult> UpdateAsync(BookDto model)
        {
            throw new NotImplementedException();
        }
        public async Task<ServiceResult> UpdateAync(BookDto model, List<IFormFile> imageFiles)
        {
            var exisitngBook = await _unitOfWork.BookRepository.GetByIdAsync(model.BookId);

            if (exisitngBook == null)
            {
                return ServiceResultFactory.NotFound("Không tìm thấy sách cần cập nhật");
            }
            var request = _httpContextAccessor.HttpContext?.Request;
            var baseUrl = $"{request?.Scheme}://{request?.Host}/";
            //if (imageFiles == null || imageFiles.Count() == 0)
            //{
            //    model.MainImage = exisitngBook.MainImage;
            //    model.AdditionalImages = exisitngBook.AdditionalImages;
            //}
            //else
            var pattern = baseUrl + GlobalConst.BookImageRelativePath + "/";

            List<string> additionalImages;
            if (!string.IsNullOrEmpty(model.MainImage))
            {
                model.MainImage = Regex.Replace(model.MainImage, pattern, "", RegexOptions.IgnoreCase);

            }
            else
            {
                model.MainImage = null;
            }
            if (string.IsNullOrEmpty(model.AdditionalImages)) additionalImages = new List<string>();
            else
            {
                //bỏ url ra khỏi file ảnh

                model.AdditionalImages = Regex.Replace(model.AdditionalImages, pattern, "", RegexOptions.IgnoreCase);
                additionalImages = model.AdditionalImages.Split(";").ToList();
            }

            if (imageFiles != null && imageFiles.Count() > 0)
            {
                int i = 0;
                if (string.IsNullOrEmpty(model.MainImage))
                {
                    var mainImageResult = await _uploadFile.UploadImageAsync(imageFiles[0], GlobalConst.BookImagePhysicalPath); // Lưu ảnh
                    if (!mainImageResult.ApiResult.Success)
                    {
                        return mainImageResult;
                    }

                    model.MainImage = mainImageResult.ApiResult.Data.ToString();
                    i++;
                }


                for (; i < imageFiles.Count; i++)
                {
                    var additionalImageResult = await _uploadFile.UploadImageAsync(imageFiles[i], GlobalConst.BookImagePhysicalPath);
                    if (!additionalImageResult.ApiResult.Success)
                    {
                        return additionalImageResult;
                    }
                    additionalImages.Add(additionalImageResult.ApiResult.Data.ToString());
                }

                model.AdditionalImages = string.Join(";", additionalImages);
            }
            //else
            //{
            //    var uploadMainImageResult = await _uploadFile.UploadImage(imageFiles[0], GlobalConst.BookImagePhysicalPath);
            //    if (uploadMainImageResult.ApiResult.Success == false)
            //    {
            //        return uploadMainImageResult;
            //    }
            //    model.MainImage = uploadMainImageResult.ApiResult.Data.ToString();
            //}
            //var imgCount = imageFiles.Count();
            //if (imgCount > 0)
            //{
            //    var listAdditionalImage = new List<string>();
            //    for (int i = 1; i < imgCount; i++)
            //    {
            //        var uploadMainImageResult = await _uploadFile.UploadImage(imageFiles[i], GlobalConst.BookImagePhysicalPath);
            //        if (uploadMainImageResult.ApiResult.Success == false)
            //        {
            //            return uploadMainImageResult;
            //        }
            //        listAdditionalImage.Add(uploadMainImageResult.ApiResult.Data.ToString());
            //    }
            //    model.AdditionalImages = string.Join(';', listAdditionalImage);
            //}
            UpdateBookFromDto(exisitngBook, model);
            exisitngBook.ModifyDate = DateTime.Now;
            await _unitOfWork.BookRepository.UpdateAsync(exisitngBook);
            await _unitOfWork.SaveChangeAsync();

            return ServiceResultFactory.Ok("Cập nhật sách thành công");
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
                bookDto.CategoryName = book.Category?.Name;
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


        public async Task<ServiceResult> GetActiveByIdAsync(int id)
        {
            var book = await _unitOfWork.BookRepository.GetActiveByIdAsync(id);
            if (book == null)
                return new ServiceResult
                {
                    StatusCode = 204,
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


        public async Task<ServiceResult> GetByFilterAsync(int? categoryid = null,
            string? categoryName = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            string? bookName = null,
            int? minQuality = null,
            int? maxQuanlity = null,
            bool? isPromotion = null,
            int? languageId = null,
            int? bookCoverTypeId = null,
            int? pageNumber = null,
            int? pageSize = null)
        {
            var book = await _unitOfWork.BookRepository.GetByFilterAsync(categoryid, categoryName, minPrice, maxPrice, bookName, minQuality, maxQuanlity, isPromotion, languageId, bookCoverTypeId, pageNumber, pageSize);
            if (book == null || book.Count() <= 0)
                return new ServiceResult
                {
                    StatusCode = 204,
                    ApiResult = new ApiResult
                    {
                        Success = false,
                        ErrMessage = "Không tìm thấy sách"

                    }
                };
            var count = await _unitOfWork.BookRepository.CountByFillterAsync(categoryid, categoryName, minPrice, maxPrice, bookName, minQuality, maxQuanlity, isPromotion, languageId, bookCoverTypeId);
            var bookDto = MapList(book);
            return new ServiceResult
            {
                StatusCode = 200,
                ApiResult = new ApiResult
                {
                    Success = true,
                    Count = count,
                    Data = bookDto
                
                }
            };
        }
        public async Task<ServiceResult> GetActiveByFilterAsync(
            int? categoryid = null,
            string? categoryName = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            string? bookName = null,
            int? minQuality = null,
            int? maxQuanlity = null,
            bool? isPromotion = null,
            int? languageId = null,
            int? bookCoverTypeId = null,
            int? pageNumber = null,
            int? pageSize = null)
        {
            var book = await _unitOfWork.BookRepository.GetActiveByFilterAsync(categoryid, categoryName, minPrice, maxPrice, bookName, minQuality, maxQuanlity, isPromotion, languageId, bookCoverTypeId, pageNumber, pageSize);
            var count = await _unitOfWork.BookRepository.CountByFillterAsync(categoryid, categoryName, minPrice, maxPrice, bookName, minQuality, maxQuanlity, isPromotion, languageId, bookCoverTypeId);
            if (book == null || book.Count() <= 0)
                return new ServiceResult
                {
                    StatusCode = 204,
                    ApiResult = new ApiResult
                    {
                        Success = false,
                        ErrMessage = "Không tìm thấy sách"

                    }
                };
            var bookDto = MapList(book);
            return new ServiceResult
            {
                StatusCode = 200,
                ApiResult = new ApiResult
                {
                    Success = false,
                    Data = bookDto,
                    Count = count
                }
            };
        }

        public async Task<ServiceResult> CountAsync()
        {
            var activebook = await _unitOfWork.BookRepository.CountActive();
            var deactivebook = await _unitOfWork.BookRepository.CountDeactive();

            return new ServiceResult
            {
                StatusCode = 200,
                ApiResult = new ApiResult
                {
                    Data = new
                    {
                        ActiveBook = activebook,
                        DeactiveBook = deactivebook

                    },
                    Count = activebook + deactivebook
                }
            };
        }
    }
}

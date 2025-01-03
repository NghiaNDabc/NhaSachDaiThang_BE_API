﻿using NhaSachDaiThang_BE_API.Models.Dtos;

namespace NhaSachDaiThang_BE_API.Services.CommonInterface
{
    public interface IActiveService
    {
        Task<ServiceResult> GetAllActive(int? pageNumber = null, int? pageSize = null);
        Task<ServiceResult> GetActiveById(int id);
        Task<ServiceResult> ChangStatus(int id);
        Task<ServiceResult> GetActiveByName(string name, int? pageNumber = null, int? pageSize = null);
    }
}

﻿using Microsoft.EntityFrameworkCore.Storage;
using NhaSachDaiThang_BE_API.Repositories.IRepositories;

namespace NhaSachDaiThang_BE_API.UnitOfWork
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IBookRepository BookRepository { get; }
        ISupplierRepository SupplierRepository { get; }
        ISupplierBookRepository SupplierBookRepository { get; }
        IOrderRepository OrderRepository { get; }
        IOrderDetailRepository OrderDetailRepository { get; }
        IBookCoverTypeRepository BookCoverTypeRepository { get; }
        ILanguageRepository LanguageRepository { get; }
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task<int> SaveChangeAsync();
    }
}

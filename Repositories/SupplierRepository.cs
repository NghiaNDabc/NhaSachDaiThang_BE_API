﻿using Microsoft.EntityFrameworkCore;
using NhaSachDaiThang_BE_API.Data;
using NhaSachDaiThang_BE_API.Helper;
using NhaSachDaiThang_BE_API.Models.Entities;
using NhaSachDaiThang_BE_API.Repositories.IRepositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace NhaSachDaiThang_BE_API.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly BookStoreContext _bookStoreContext;
        private readonly DbSet<Supplier> _supplierSet;
        public SupplierRepository(BookStoreContext bookStoreContext)
        {
            _bookStoreContext = bookStoreContext;
            _supplierSet = _bookStoreContext.Supplier;
        }

        public async Task AddAsync(Supplier entity)
        {
            await _supplierSet.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var rs =  await _supplierSet.FindAsync(id);
            if (rs != null) {
                 _supplierSet.Remove(rs);
            }
            
        }

        public async Task<IEnumerable<Supplier>> GetAllAsync(int? pageNumber = null, int? pageSize = null)
        {
            var query =  _supplierSet;
            return await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
        }

        public async Task<Supplier> GetByIdAsync(int id)
        {
            var rs = await _supplierSet.FirstOrDefaultAsync(x=> x.SupplierId==id);
            return rs;
        }

        public async Task<IEnumerable<Supplier>> GetByNameAsync(string name, int? pageNumber = null, int? pageSize = null)
        {
            var query =  _supplierSet.Where(x => x.Name.Contains(name));
            return await PaginationHelper.PaginateAsync(query, pageNumber, pageSize);
        }

        public async Task SoftDelete(int id)
        {
            var rs = await _supplierSet.FirstOrDefaultAsync(x => x.SupplierId == id);
            if (rs != null) {
                rs.IsDel = true;
            }
        }

        public  Task UpdateAsync(Supplier entity)
        {
            _supplierSet.Update(entity);
            return Task.CompletedTask;
        }
    }
}
using System;
using System.Text;
using System.Collections.Generic;
using The25th.Business.Services.IServices;
using The25th.Models;
using The25th.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace The25th.Business.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _db;

        public OrderService(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<OrderHeader> CreteOrderAsync(OrderHeader orderHeader)
        {
            _db.OrderHeaders.Add(orderHeader);
            await _db.SaveChangesAsync();

            return orderHeader;
        }

        public async Task<IEnumerable<OrderHeader?>> GetAllOredrAsync(string? userId = null, string? status = null, bool includeUser = false, bool includeDetail = false)
        {
            var query = _db.OrderHeaders.AsQueryable();

            if (includeUser)
            {
                query = query.Include(u => u.ApplicationUser);
            }
            if (includeDetail)
            {
                query = query.Include(u => u.OrderDetails).ThenInclude(u => u.Product);
            }
            if (!string.IsNullOrEmpty(userId))
            {
                query = query.Where(u => u.ApplicationUserId == userId);
            }
            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(u => u.OrderStatus == status);
            }
            return await query.ToListAsync();
        }

        public async Task<OrderHeader?> GetOredrByIdAsync(int id, bool includeUser = false, bool includeDetail = false)
        {
            var query = _db.OrderHeaders.AsQueryable();

            if (includeUser)
            {
                query = query.Include(u => u.ApplicationUser);
            }
            if (includeDetail)
            {
                query = query.Include(u => u.OrderDetails).ThenInclude(u => u.Product);
            }
            return await query.FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
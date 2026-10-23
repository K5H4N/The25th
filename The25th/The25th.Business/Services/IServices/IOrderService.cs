using System;
using System.Collections.Generic;
using System.Text;
using The25th.Models;

namespace The25th.Business.Services.IServices
{
    public interface IOrderService
    {
        Task<OrderHeader> CreteOrderAsync(OrderHeader orderHeader);
        Task<OrderHeader?> GetOredrByIdAsync(int id, bool includeUser = false, bool includeDetail = false);
        Task<IEnumerable<OrderHeader>> GetAllOredrAsync(string? userId = null, string? status = null, bool includeUser = false, bool includeDetail = false);
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using The25th.Business.Services.IServices;
using The25th.DataAccess.Data;
using The25th.Models;

namespace The25th.Business.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly ApplicationDbContext _context;
        public ApplicationUserService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ApplicationUser?> GetUserByIdAsynce(string userId)
        {
            return await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == userId);
        }
    }
}
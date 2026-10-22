using System;
using System.Collections.Generic;
using System.Text;
using The25th.Models;

namespace The25th.Business.Services.IServices
{
    public interface IApplicationUserService
    {
        Task<ApplicationUser?> GetUserByIdAsynce(string userId);
    }
}
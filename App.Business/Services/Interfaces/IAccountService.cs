using App.Business.ViewModels.AccountVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Services.Interfaces
{
    public interface IAccountService
    {
        Task Register(RegisterVM register);
        Task Login(LoginVM login);
        Task CreateRoles();
        Task Logout();
    }
}

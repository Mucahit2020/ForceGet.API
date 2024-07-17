using ForceGetCase.Application.Models;
using ForceGetCase.Application.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForceGetCase.Application.Services.Abstracts
{
    public interface IUserService
    {

        Task<UserCreateResponseModel> CreateAsync(UserCreateModel userCreateModel);

        Task<UserLoginResponseModel> LoginAsync(UserLoginModel userLoginModel);
    }
}
using AutoMapper;
using ForceGetCase.Application.Models.User;
using ForceGetCase.Application.Services.Abstracts;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ForceGetCase.Application.Services.Concrates
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;

        private readonly IMapper _mapper;


        public async Task<UserCreateResponseModel> CreateAsync(UserCreateModel userCreateModel)
        {
            //var user = _mapper.Map<ApplicationUser>(createUserModel);

            //var result = await _userManager.CreateAsync(user, userCreateModel.Password);

            //if (!result.Succeeded) throw new BadRequestException(result.Errors.FirstOrDefault()?.Description);

            //var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

           

            return new UserCreateResponseModel
            {
                //Id = Guid.Parse((await _userManager.FindByNameAsync(user.UserName)).Id)
            };
        }

        public Task<UserLoginResponseModel> LoginAsync(UserLoginModel userLoginModel)
        {
            throw new NotImplementedException();
        }
    }
}

using AutoMapper;
using BaseAuthApp_BAL.Models;
using BaseAuthApp_DAL.Contracts;
using BaseAuthApp_DAL.Entities;
using System.ComponentModel.DataAnnotations;
using System.Web.Helpers;
using System.Web.Mvc;

namespace BaseAuthApp_BAL.Services
{
    public class ServiceUser
    {
        private readonly IRepositoryUser _repositoryUser;
        private readonly IMapper _mapper;

        public ServiceUser(IRepositoryUser repositoryUser, IMapper mapper)
        {
            _repositoryUser = repositoryUser;
            _mapper = mapper;
        }

        public async Task<Result<UserModel, Error>> RegisterUserAsync(UserCreateModel userCreateModel)
        {
            if (await _repositoryUser.UserExistsByUsernameAsync(userCreateModel.Username))
            {
                return UserError.UserExists;
            }
                        
            userCreateModel.Password = hashPassword(userCreateModel.Password);          

            var newUser = _mapper.Map<UserCreateModel, User>(userCreateModel);
            await _repositoryUser.Add(newUser);

            var userModel = _mapper.Map<User, UserModel>(newUser);

            return userModel;
        }

        public async Task<bool> ValidateUserAsync(string username, string password)
        {
            var user = await _repositoryUser.GetUserByUsernameAsync(username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return false;
            }

            return true;           
        }        

        public async Task<Result<UserModel, Error>> ValidateUserWithResultAsync(string username, string password)
        {
            var user = await _repositoryUser.GetUserByUsernameAsync(username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return AuthenticationError.InvalidCredentials;
            }

            var userModel = _mapper.Map<User, UserModel>(user);
            return userModel;
        }

        private string hashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}

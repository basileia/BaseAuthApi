﻿using AutoMapper;
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

        public async Task<bool> ValidateUserAsync(string username, string password)
        {
            return await _repositoryUser.UserExistsAsync(username, password);
        }

        public async Task<Result<UserModel, Error>> RegisterUserAsync(UserCreateModel userCreateModel)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(userCreateModel, serviceProvider: null, items: null);
            bool isValid = Validator.TryValidateObject(userCreateModel, validationContext, validationResults, validateAllProperties: true);

            if (!isValid)
            {
                var errorDetails = validationResults.Select(vr => vr.ErrorMessage).ToList();
                var error = new Error("ValidationError", "Model validation failed", errorDetails);
                return error;
            }

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

        private string hashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }


    }
}

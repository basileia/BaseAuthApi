using BaseAuthApp_BAL.Models;
using BaseAuthApp_DAL.Contracts;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BaseAuthApp_BAL.Services
{
    public class ServiceUser
    {
        private readonly IRepositoryUser _repositoryUser;

        public ServiceUser(IRepositoryUser repositoryUser)
        {
            _repositoryUser = repositoryUser;
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



        }

    }
}

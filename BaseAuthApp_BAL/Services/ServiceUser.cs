using BaseAuthApp_DAL.Contracts;

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

    }
}

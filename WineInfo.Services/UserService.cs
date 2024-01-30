using System.Threading.Tasks;
using WineInfo.DAL.Infrastructure;
using WineInfo.DAL.Repositories;
using WineInfo.Entities;
using WineInfo.Services.Communication;
using WineInfo.Services.Security;

namespace WineInfo.Services
{
    public class UserService : IUserService
    {
        private IUserRepository repository;
        private IUnitOfWork unitOfWork;
        private readonly IPasswordHasher passwordHasher;

        public UserService(IUserRepository repository, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this.passwordHasher = passwordHasher;
        }

        public async Task<UserResponse> AddUserAsync(User user)
        {
            var existingUser = await repository.FindByUserNameAsync(user.UserName);

            if (existingUser != null)
            {
                return new UserResponse(false, "UserName already exists.", null);
            }

            user.Password = passwordHasher.HashPassword(user.Password);

            await repository.AddAsync(user);
            SaveChanges();

            return new UserResponse(true, null, user);
        }

        public void SaveChanges()
        {
            unitOfWork.SaveChanges();
        }

        public async Task<UserResponse> FindUserByNameAndPasswordAsync(string userName, string password)
        {
            var user = await repository.FindByUserNameAsync(userName);

            if (user == null || !passwordHasher.PasswordMatches(password, user.Password))
            {
                return new UserResponse(false, "Invalid credentials.", null);
            }

            return new UserResponse(true, null, user);
        }
    }
}

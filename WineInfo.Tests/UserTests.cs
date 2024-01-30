using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WineInfo.DAL.Infrastructure;
using WineInfo.DAL.Repositories;
using WineInfo.Entities;
using WineInfo.Services;
using WineInfo.Services.Security;

namespace WineInfo.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        Mock<IUserRepository> repositoryMock;
        Mock<IUnitOfWork> unitOfWorkMock;
        Mock<IPasswordHasher> passwordHasherMock;
        UserService service;

        [SetUp]
        public void Init()
        {
            // Arrange
            repositoryMock = new Mock<IUserRepository>();
            unitOfWorkMock = new Mock<IUnitOfWork>();
            passwordHasherMock = new Mock<IPasswordHasher>();
            service = new UserService(repositoryMock.Object, unitOfWorkMock.Object, passwordHasherMock.Object);
        }

        [Test]
        public async Task AddUserAsyncWithNonExistingUserShouldReturnSuccess()
        {
            // Arrange
            var newUser = new User
            {
                UserName = "newuser",
                Password = "password123"
            };

            repositoryMock.Setup(r => r.FindByUserNameAsync(newUser.UserName))
                .ReturnsAsync((User)null); // Simulate non-existing user

            passwordHasherMock.Setup(p => p.HashPassword(newUser.Password))
                .Returns("hashed_password"); // Simulate hashed password

            // Act
            var result = await service.AddUserAsync(newUser);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.IsNull(result.Message);
            Assert.AreEqual(newUser, result.User);
            repositoryMock.Verify(r => r.FindByUserNameAsync(newUser.UserName), Times.Once);
            repositoryMock.Verify(r => r.AddAsync(newUser), Times.Once);
            unitOfWorkMock.Verify(u => u.SaveChanges(), Times.Once);
        }

        [Test]
        public async Task AddUserAsyncWithExistingUserShouldReturnError()
        {
            // Arrange
            var existingUser = new User
            {
                UserName = "existinguser"
            };

            var newUser = new User
            {
                UserName = "existinguser", 
                Password = "password123"
            };

            repositoryMock.Setup(r => r.FindByUserNameAsync(newUser.UserName))
                .ReturnsAsync(existingUser); // Simulate existing user

            // Act
            var result = await service.AddUserAsync(newUser);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual("UserName already exists.", result.Message);
            Assert.IsNull(result.User);
            repositoryMock.Verify(r => r.FindByUserNameAsync(newUser.UserName), Times.Once);
            repositoryMock.Verify(r => r.AddAsync(It.IsAny<User>()), Times.Never);
            unitOfWorkMock.Verify(u => u.SaveChanges(), Times.Never);
        }

        [Test]
        public async Task FindUserByNameAndPasswordAsyncWithValidCredentialsShouldReturnUser()
        {
            // Arrange
            string userName = "existinguser";
            string password = "password123";

            var existingUser = new User
            {
                UserName = userName,
                Password = "hashed_password"
            };

            repositoryMock.Setup(r => r.FindByUserNameAsync(userName))
                .ReturnsAsync(existingUser); // Simulate existing user

            passwordHasherMock.Setup(p => p.PasswordMatches(password, existingUser.Password))
                .Returns(true); // Simulate password match

            // Act
            var result = await service.FindUserByNameAndPasswordAsync(userName, password);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.IsNull(result.Message);
            Assert.AreEqual(existingUser.UserName, result.User.UserName);
            repositoryMock.Verify(r => r.FindByUserNameAsync(userName), Times.Once);
            passwordHasherMock.Verify(p => p.PasswordMatches(password, existingUser.Password), Times.Once);
        }

        [Test]
        public async Task FindUserByNameAndPasswordAsyncWithInvalidCredentialsShouldReturnError()
        {
            // Arrange
            var userName = "existinguser";
            var password = "incorrect_password";

            var existingUser = new User
            {
                UserName = userName,
                Password = "hashed_password" // Simulate hashed password
            };

            repositoryMock.Setup(r => r.FindByUserNameAsync(userName))
                .ReturnsAsync(existingUser); // Simulate existing user

            passwordHasherMock.Setup(p => p.PasswordMatches(password, existingUser.Password))
                .Returns(false); // Simulate password mismatch

            // Act
            var result = await service.FindUserByNameAndPasswordAsync(userName, password);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual("Invalid credentials.", result.Message);
            Assert.IsNull(result.User);
            repositoryMock.Verify(r => r.FindByUserNameAsync(userName), Times.Once);
            passwordHasherMock.Verify(p => p.PasswordMatches(password, existingUser.Password), Times.Once);
        }
    }
}

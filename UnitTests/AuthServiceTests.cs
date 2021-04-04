using Microsoft.Extensions.Options;
using Moq;
using Sunioj.Core.Contracts.Repositories;
using Sunioj.Core.Contracts.Services;
using Sunioj.Core.Contracts.UnitsOfWork;
using Sunioj.Core.Exceptions;
using Sunioj.Core.Resources.Auth;
using Sunioj.Core.Services;
using Sunioj.Data.Entities;
using Sunioj.Options;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class AuthServiceTests
    {
        [Fact]
        public async void LoginAsync_ValidUser_ReturnsToken()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var usersRepositoryMock = new Mock<IUsersRepository>();
            var hashServiceMock = new Mock<IHashService>();
            var jwtOptionsMock = new Mock<IOptionsMonitor<JwtOptions>>();
            var user = GetUser();
            var userForAuth = GetUserForAuth();

            unitOfWorkMock.Setup(x => x.Users).Returns(usersRepositoryMock.Object);
            usersRepositoryMock.Setup(x => x.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
            hashServiceMock.Setup(x => x.ValidatePassword(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            jwtOptionsMock.Setup(x => x.CurrentValue).Returns(GetJwtOptions());

            var service = new AuthService(unitOfWorkMock.Object, hashServiceMock.Object, jwtOptionsMock.Object);

            // Act
            var result = await service.LoginAsync(userForAuth);

            // Assert
            Assert.NotNull(result);
            Assert.True(!string.IsNullOrEmpty(result.AccessToken));
        }

        [Fact]
        public async void LoginAsync_InvalidUser_ThrowsException()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var usersRepositoryMock = new Mock<IUsersRepository>();
            var hashServiceMock = new Mock<IHashService>();
            var jwtOptionsMock = new Mock<IOptionsMonitor<JwtOptions>>();
            var user = GetNullUser();
            var userForAuth = GetUserForAuth();

            unitOfWorkMock.Setup(x => x.Users).Returns(usersRepositoryMock.Object);
            usersRepositoryMock.Setup(x => x.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);

            var service = new AuthService(unitOfWorkMock.Object, hashServiceMock.Object, jwtOptionsMock.Object);

            // Act
            Task login() => service.LoginAsync(userForAuth);

            // Assert
            await Assert.ThrowsAsync<BadRequestException>(login);
        }

        #region Helpers
        private static User GetUser()
        {
            return new User
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@sunioj.com",
                PasswordHash = "1000:RIGtm0DVzuZ4dEtiQncCm4LfujPS9iud:utqaw7AM6GNd4VcbGrplshEbAlE="
            };
        }

        private static User GetNullUser()
        {
            return null;
        }

        private static UserForAuthDTO GetUserForAuth()
        {
            return new UserForAuthDTO("john.doe@sunioj.com", "123456");
        }

        private static JwtOptions GetJwtOptions()
        {
            return new JwtOptions
            {
                Secret = "Zebras are African equines with black-and-white striped coats and share the genus Equus with horses and asses."
            };
        }
        #endregion
    }
}

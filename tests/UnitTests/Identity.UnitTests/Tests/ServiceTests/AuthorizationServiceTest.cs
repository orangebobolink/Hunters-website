using Moq;
using Identity.Services.Services;
using Identity.Domain.Entities;
using Identity.Services.Dtos.RequestDtos;
using Identity.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using FluentAssertions;
using Identity.Services.Utilities;
using Mapster;
using Identity.Services.Dtos.ResponseDtos;
using Bogus;
using Identity.Domain.Exceptions;
using Identity.UnitTests.Helpers.BogusFaker;

namespace Identity.UnitTests.Tests.ServiceTests
{
    public class AuthorizationServiceTest
    {
        private readonly Mock<UserManager<User>> _userManagerMock;
        private readonly Mock<ITokenService> _tokenServiceMock = new();
        private readonly Mock<IUserService> _userServiceMock = new();
        private readonly Mock<ILogger<AuthorizationService>> _loggerMock = new();
        private readonly AuthorizationService _authorizationService;
        private readonly RequestRegistrationUserDtoFakerHelper _fakerRequestRegistrationUserDto = new();
        private readonly Faker _faker = new();

        public AuthorizationServiceTest()
        {
            _userManagerMock = new Mock<UserManager<User>>(
                Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
            _authorizationService = new AuthorizationService(
                _userManagerMock.Object,
                _tokenServiceMock.Object,
                _userServiceMock.Object,
                _loggerMock.Object);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnAuthenticatedResponse_WhenLoginIsValid()
        {
            // Arrange
            var user = new User { Id = Guid.NewGuid(), UserName = _faker.Internet.UserName() };
            var loginUserDto = new RequestLoginUserDto
            {
                Email = _faker.Internet.Email(),
                Password = _faker.Internet.Password()
            };
            var accessToken = _faker.Random.Guid().ToString();
            var refreshToken = _faker.Random.String(32);

            _userManagerMock.Setup(
                um => um.FindByEmailAsync(loginUserDto.Email))
                .ReturnsAsync(user);
            _userManagerMock.Setup(
                um => um.CheckPasswordAsync(user, loginUserDto.Password))
                .ReturnsAsync(true);
            _userManagerMock.Setup(
                um => um.GetRolesAsync(user))
                .ReturnsAsync(new List<string> { "User" });
            _tokenServiceMock.Setup(
                ts => ts.GenerateAccessTokenAsync(user, It.IsAny<CancellationToken>()))
                .ReturnsAsync(accessToken);
            _tokenServiceMock.Setup(
                ts => ts.GenerateRefreshToken())
                .Returns(refreshToken);

            // Act
            var result = await _authorizationService.LoginAsync(loginUserDto, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(user.Id);
            result.UserName.Should().Be(user.UserName);
            result.Roles.Should().Contain("User");
            result.AccessToken.Should().Be(accessToken);
        }

        [Fact]
        public async Task RegistrationAsync_ShouldReturnTrue_WhenRegistrationIsSuccessful()
        {
            // Arrange
            var registrationUserDto = _fakerRequestRegistrationUserDto
                .GenerateFakeOneValidRequestRegistrationUserDto();
            var userRequestDto = registrationUserDto.Adapt<RequestUserDto>();
            userRequestDto.UserName = RandomUsernameGeneratorUtility.GenerateRandomUsername();
            userRequestDto.RoleNames = new List<string> { Role.User };

            var responseCreateUserDto = new ResponseCreateUserDto
            {
                Id = Guid.NewGuid(),
                UserName = userRequestDto.UserName,
                Email = userRequestDto.Email,
                FirstName = userRequestDto.FirstName,
                MiddleName = userRequestDto.MiddleName,
                LastName = userRequestDto.LastName,
                RoleNames = userRequestDto.RoleNames
            };

            _userServiceMock.Setup(
                us => us.CreateAsync(
                It.IsAny<RequestUserDto>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(responseCreateUserDto);

            // Act
            var result = await _authorizationService.RegistrationAsync(
                registrationUserDto,
                CancellationToken.None);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task LoginAsync_ShouldTrowExeption_WhenEmailNotFound()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = _faker.Internet.UserName()
            };
            var loginUserDto = new RequestLoginUserDto
            {
                Email = _faker.Internet.Email(),
                Password = _faker.Internet.Password()
            };
            var accessToken = _faker.Random.Guid().ToString();
            var refreshToken = _faker.Random.String(32);

            _userManagerMock.Setup(um => um.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((User)null);

            // Act
            Func<Task> act =
                async () => await _authorizationService
                .LoginAsync(loginUserDto, CancellationToken.None);

            // Assert
            await act
                .Should()
                .ThrowAsync<Exception>();
        }

        [Fact]
        public async Task LoginAsync_ShouldTrowExeption_WhenPasswordIsIncorrect()
        {
            // Arrange
            var user = new User { Id = Guid.NewGuid(), UserName = _faker.Internet.UserName() };
            var loginUserDto = new RequestLoginUserDto
            {
                Email = _faker.Internet.Email(),
                Password = _faker.Internet.Password()
            };
            var accessToken = _faker.Random.Guid().ToString();
            var refreshToken = _faker.Random.String(32);

            _userManagerMock.Setup(um => um.FindByEmailAsync(loginUserDto.Email))
                 .ReturnsAsync(user);
            _userManagerMock.Setup(um => um.CheckPasswordAsync(user, loginUserDto.Password))
                .ReturnsAsync(false);

            // Act
            Func<Task> act =
                async () => await _authorizationService
                .LoginAsync(loginUserDto, CancellationToken.None);

            // Assert
            await act
                .Should()
                .ThrowAsync<InvalidPasswordException>();
        }
    }
}

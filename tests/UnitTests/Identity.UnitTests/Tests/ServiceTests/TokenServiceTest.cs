using Bogus;
using FluentAssertions;
using Identity.Domain.Entities;
using Identity.Services.Interfaces;
using Identity.Services.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using Identity.UnitTests.Helpers.MockSetupsHelpers;

namespace Identity.UnitTests.Tests.ServiceTests
{
    public class TokenServiceTest
    {
        private readonly MockUserManagerSetupHelper _userManagerMock;
        private readonly Mock<ILogger<TokenService>> _loggerMock;
        private readonly MockHyntingLicenseRepositorySetupHelper _hyntingLicenseRepositoryMock;
        private readonly Mock<IAccessTokenUtilities> _accessTokenUtilitiesMock;
        private readonly Mock<IRefreshTokenUtilities> _refreshTokenUtilitiesMock;
        private readonly Mock<IRefreshTokenCookie> _refreshTokenCookieUtilitiesMock;
        private readonly TokenService _tokenService;
        private readonly Faker _faker;

        public TokenServiceTest()
        {
            _userManagerMock = new MockUserManagerSetupHelper();
            _loggerMock = new Mock<ILogger<TokenService>>();
            _hyntingLicenseRepositoryMock = new MockHyntingLicenseRepositorySetupHelper();
            _accessTokenUtilitiesMock = new Mock<IAccessTokenUtilities>();
            _refreshTokenUtilitiesMock = new Mock<IRefreshTokenUtilities>();
            _refreshTokenCookieUtilitiesMock = new Mock<IRefreshTokenCookie>();
            _tokenService = new TokenService(
                _userManagerMock.Object,
                _loggerMock.Object,
                _hyntingLicenseRepositoryMock.Object,
                _accessTokenUtilitiesMock.Object,
                _refreshTokenUtilitiesMock.Object,
                _refreshTokenCookieUtilitiesMock.Object);
            _faker = new Faker();
        }

        [Fact]
        public async Task GenerateAccessTokenAsync_ShouldReturnTokenString()
        {
            // Arrange
            var user = new User { Id = Guid.NewGuid(), UserName = _faker.Internet.UserName() };
            var tokenOptions = new JwtSecurityToken();
            _accessTokenUtilitiesMock.Setup(atu => atu.GetTokenOptionsAsync(user, It.IsAny<CancellationToken>()))
                .ReturnsAsync(tokenOptions);

            // Act
            var result = await _tokenService.GenerateAccessTokenAsync(user, CancellationToken.None);

            // Assert
            result.Should().NotBeNullOrEmpty();
            _accessTokenUtilitiesMock.Verify(atu => atu.GetTokenOptionsAsync(user, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public void GenerateRefreshToken_ShouldReturnTokenString()
        {
            // Act
            var result = _tokenService.GenerateRefreshToken();

            // Assert
            result.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task RefreshAsync_ShouldReturnNewTokens_WhenValid()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = _faker.Internet.UserName(),
                RefreshToken = "validToken",
                RefreshTokenExpiryTime = DateTime.Now.AddDays(10)
            };
            var newAccessToken = _faker.Random.String2(32);
            var newRefreshToken = _faker.Random.String2(32);
            var roles = new List<string> { "User" };
            var huntingLicense = new HuntingLicense { UserId = user.Id, IsPaid = true, Id = Guid.NewGuid() };

            _accessTokenUtilitiesMock
                .Setup(atu => atu.GetUsernameFromAccessToken())
                .Returns(user.UserName);
            _refreshTokenCookieUtilitiesMock
                .Setup(rtcu => rtcu.ReadRefreshTokenCookie())
                .Returns("validToken");
            _userManagerMock
                .MockFindByNameAsync(user.UserName, user)
                .MockGetRolesAsync(user, roles)
                .MockUpdateAsync(user, IdentityResult.Success);
            _accessTokenUtilitiesMock
                .Setup(atu => atu.GetTokenOptionsAsync(user, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new JwtSecurityToken());
            _hyntingLicenseRepositoryMock
                .MockGetByPredicate(huntingLicense);

            // Act
            var result = await _tokenService.RefreshAsync(CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.UserName.Should().Be(user.UserName);
            result.IsPaid.Should().Be(huntingLicense.IsPaid);
            result.AccessToken.Should().NotBeNullOrEmpty();

            _userManagerMock.VerifyAll();
            _refreshTokenCookieUtilitiesMock.Verify(rtcu => rtcu.ReadRefreshTokenCookie(), Times.Once);
        }

        [Fact]
        public async Task RefreshAsync_ShouldThrowException_WhenUserNotFound()
        {
            // Arrange
            _accessTokenUtilitiesMock.Setup(atu => atu.GetUsernameFromAccessToken()).Returns(_faker.Internet.UserName());

            // Act
            Func<Task> act = async () => await _tokenService.RefreshAsync(CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task RefreshAsync_ShouldThrowException_WhenRefreshTokenIsInvalid()
        {
            // Arrange
            var user = new User { Id = Guid.NewGuid(), UserName = _faker.Internet.UserName(), RefreshToken = "validToken" };

            _accessTokenUtilitiesMock.Setup(atu => atu.GetUsernameFromAccessToken()).Returns(user.UserName);
            _refreshTokenCookieUtilitiesMock.Setup(rtcu => rtcu.ReadRefreshTokenCookie()).Returns("invalidToken");

            _userManagerMock
                .MockFindByNameAsync(user.UserName, user);

            // Act
            Func<Task> act = async () => await _tokenService.RefreshAsync(CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<Exception>();
            _userManagerMock.VerifyAll();
        }

        [Fact]
        public async Task RevokeAsync_ShouldRevokeTokens_WhenValid()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = _faker.Internet.UserName(),
                RefreshToken = "validToken",
                RefreshTokenExpiryTime = DateTime.Now.AddDays(10)
            };

            _accessTokenUtilitiesMock.Setup(atu => atu.GetUsernameFromAccessToken()).Returns(user.UserName);
            _userManagerMock
                .MockFindByNameAsync(user.UserName, user)
                .MockUpdateAsync(user, IdentityResult.Success);

            // Act
            await _tokenService.RevokeAsync(CancellationToken.None);

            // Assert
            _userManagerMock.VerifyAll();
            _refreshTokenCookieUtilitiesMock.Verify(rtcu => rtcu.DeleteRefreshTokenCookie(), Times.Once);
        }

        [Fact]
        public async Task RevokeAsync_ShouldThrowException_WhenUserNotFound()
        {
            // Arrange
            _accessTokenUtilitiesMock.Setup(atu => atu.GetUsernameFromAccessToken()).Returns(_faker.Internet.UserName());

            // Act
            Func<Task> act = async () => await _tokenService.RevokeAsync(CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<Exception>();
        }
    }
}

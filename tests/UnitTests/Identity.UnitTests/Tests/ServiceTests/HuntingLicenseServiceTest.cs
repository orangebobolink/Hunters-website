using Bogus;
using FluentAssertions;
using Identity.Domain.Entities;
using Identity.Domain.Interfaces;
using Identity.Services.Dtos.RequestDtos;
using Identity.Services.Services;
using MassTransit;
using Moq;
using Shared.Messages.HunterLicenseMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Identity.UnitTests.Tests.ServiceTests
{
    public class HuntingLicenseServiceTest
    {
        private readonly Mock<IHyntingLicenseRepository> _hyntingLicenseRepositoryMock;
        private readonly Mock<IBus> _busMock;
        private readonly HuntingLicenseService _huntingLicenseService;
        private readonly Faker _faker;

        public HuntingLicenseServiceTest()
        {
            _hyntingLicenseRepositoryMock = new Mock<IHyntingLicenseRepository>();
            _busMock = new Mock<IBus>();
            _huntingLicenseService = new HuntingLicenseService(_hyntingLicenseRepositoryMock.Object, _busMock.Object);
            _faker = new Faker();
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateHuntingLicense_WhenLicenseDoesNotExist()
        {
            // Arrange
            var request = new HuntingLicenseRequestDto
            {
                LicenseNumber = _faker.Random.String2(10),
                UserId = Guid.NewGuid()
            };

            _hyntingLicenseRepositoryMock.Setup(repo => repo.GetByPredicate(It.IsAny<Expression<Func<HuntingLicense, bool>>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((HuntingLicense)null);

            // Act
            var result = await _huntingLicenseService.CreateAsync(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.LicenseNumber.Should().Be(request.LicenseNumber);

            _hyntingLicenseRepositoryMock.Verify(repo => repo.Create(It.IsAny<HuntingLicense>()), Times.Once);
            _hyntingLicenseRepositoryMock.Verify(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            _busMock.Verify(bus => bus.Publish(It.IsAny<CreateHuntingLicenseMessage>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_ShouldThrowException_WhenLicenseAlreadyExists()
        {
            // Arrange
            var request = new HuntingLicenseRequestDto
            {
                LicenseNumber = _faker.Random.String2(10),
                UserId = Guid.NewGuid()
            };

            var existingLicense = new HuntingLicense
            {
                Id = Guid.NewGuid(),
                LicenseNumber = request.LicenseNumber
            };

            _hyntingLicenseRepositoryMock.Setup(repo => repo.GetByPredicate(
                It.IsAny<Expression<Func<HuntingLicense, bool>>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingLicense);

            // Act
            Func<Task> act = async () => await _huntingLicenseService.CreateAsync(request, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<Exception>();

            _hyntingLicenseRepositoryMock.Verify(repo => repo.Create(It.IsAny<HuntingLicense>()), Times.Never);
            _hyntingLicenseRepositoryMock.Verify(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
            _busMock.Verify(bus => bus.Publish(It.IsAny<CreateHuntingLicenseMessage>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task GetByLicenseNumberAsync_ShouldReturnHuntingLicense_WhenLicenseExists()
        {
            // Arrange
            var licenseNumber = _faker.Random.String2(10);
            var existingLicense = new HuntingLicense
            {
                Id = Guid.NewGuid(),
                LicenseNumber = licenseNumber
            };

            _hyntingLicenseRepositoryMock.Setup(repo => repo.GetByPredicate(It.IsAny<Expression<Func<HuntingLicense, bool>>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingLicense);

            // Act
            var result = await _huntingLicenseService.GetByLicenseNumberAsync(licenseNumber, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.LicenseNumber.Should().Be(licenseNumber);

            _hyntingLicenseRepositoryMock.Verify(repo => repo.GetByPredicate(
                It.IsAny<Expression<Func<HuntingLicense, bool>>>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetByLicenseNumberAsync_ShouldThrowException_WhenLicenseDoesNotExist()
        {
            // Arrange
            var licenseNumber = _faker.Random.String2(10);

            _hyntingLicenseRepositoryMock.Setup(repo => repo.GetByPredicate(
                 It.IsAny<Expression<Func<HuntingLicense, bool>>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((HuntingLicense)null);

            // Act
            Func<Task> act = async () => await _huntingLicenseService.GetByLicenseNumberAsync(licenseNumber, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>();

            _hyntingLicenseRepositoryMock.Verify(repo => repo.GetByPredicate(
                It.IsAny<Expression<Func<HuntingLicense, bool>>>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetByUserIdAsync_ShouldReturnHuntingLicense_WhenLicenseExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var existingLicense = new HuntingLicense
            {
                Id = Guid.NewGuid(),
                UserId = userId
            };

            _hyntingLicenseRepositoryMock.Setup(repo => repo.GetByPredicate(It.IsAny<Expression<Func<HuntingLicense, bool>>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingLicense);

            // Act
            var result = await _huntingLicenseService.GetByUserIdAsync(userId, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.UserId.Should().Be(userId);

            _hyntingLicenseRepositoryMock.Verify(repo => repo.GetByPredicate(It.IsAny<Expression<Func<HuntingLicense, bool>>>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetByUserIdAsync_ShouldThrowException_WhenLicenseDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();

            _hyntingLicenseRepositoryMock.Setup(repo => repo.GetByPredicate(It.IsAny<Expression<Func<HuntingLicense, bool>>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((HuntingLicense)null);

            // Act
            Func<Task> act = async () => await _huntingLicenseService.GetByUserIdAsync(userId, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>();

            _hyntingLicenseRepositoryMock.Verify(repo => repo.GetByPredicate(It.IsAny<Expression<Func<HuntingLicense, bool>>>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}

using Identity.Domain.Entities;
using Identity.Domain.Interfaces;
using Moq;
using System.Linq.Expressions;

namespace Identity.UnitTests.Helpers.MockSetupsHelpers
{
    internal class MockHyntingLicenseRepositorySetupHelper
        : Mock<IHyntingLicenseRepository>
    {
        public MockHyntingLicenseRepositorySetupHelper MockGetByPredicate(
            HuntingLicense output)
        {
            Setup(repo => repo.GetByPredicate(
                It.IsAny<Expression<Func<HuntingLicense, bool>>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(output);
            return this;
        }
    }
}

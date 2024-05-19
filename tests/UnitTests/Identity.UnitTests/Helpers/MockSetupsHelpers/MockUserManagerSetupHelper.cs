using Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace Identity.UnitTests.Helpers.MockSetupsHelpers
{
    internal class MockUserManagerSetupHelper
        : Mock<UserManager<User>>
    {
        public MockUserManagerSetupHelper()
            : base(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null)
        {
        }

        public MockUserManagerSetupHelper MockFindByNameAsync(
            string username,
            User output)
        {
            Setup(
                um => um.FindByNameAsync(username))
            .ReturnsAsync(output);

            return this;
        }

        public MockUserManagerSetupHelper MockGetRolesAsync(
           User user,
           List<string> output)
        {
            Setup(um => um.GetRolesAsync(user))
             .ReturnsAsync(output);

            return this;
        }

        public MockUserManagerSetupHelper MockUpdateAsync(
          User user,
          IdentityResult output)
        {
            Setup(um => um.UpdateAsync(user))
             .ReturnsAsync(output);

            return this;
        }
    }
}

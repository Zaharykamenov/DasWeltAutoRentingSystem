using CarRentingSystem.Core.Contracts;
using CarRentingSystem.Core.Services;

namespace CarRentingSystem.UnitTest.UnitTests
{
    public class UserServiceTests : UnitTestsBase
    {
        private IUserService userService;

        [OneTimeSetUp]
        public void SetUp() => this.userService = new UserService(this.repository);

        [Test]
        public async Task UserFullName_ShouldReturnCorrectUserFullName()
        {
            var result = await this.userService.UserFullName(this.User.Id);
            var expected = $"{this.User.FirstName} {this.User.LastName}";
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public async Task All_ShouldReturnCorrectListOfModel()
        {
            var result = await this.userService.All();

            var expectedCount = this.context.Agents.Count() + this.context.Users.Count(u=>this.context.Agents.Any(a=>a.UserId!=u.Id));

            Assert.IsNotNull(result);

            Assert.That(result.Count(), Is.EqualTo(expectedCount));
        }

        [Test]
        public async Task UserHasRents_ShouldReturnTrue_WithCorrectUserId()
        {
            var result = await this.userService.UserHasRents(this.Renter.Id);

            Assert.That(result, Is.True);
        }

        [Test]
        public async Task UserHasRents_ShouldReturnFalse_WithWrongUserId()
        {
            var result = await this.userService.UserHasRents(this.User.Id);

            Assert.That(result, Is.False);
        }
    }
}

using CarRentingSystem.Core.Contracts;
using CarRentingSystem.Core.Services;

namespace CarRentingSystem.UnitTest.UnitTests
{
    [TestFixture]
    public class AgentServiceTests : UnitTestsBase
    {
        private IAgentService agentService;

        [OneTimeSetUp]
        public void SetUp() => this.agentService = new AgentService(this.repository);

        [Test]
        public async Task GetAgentId_ShouldReturnCorrectUserId()
        {
            var result = await this.agentService.GetAgentId(this.User.Id);

            Assert.That(result, Is.EqualTo(this.Agent.Id));
        }

        [Test]
        public async Task GetAgentId_ShouldReturnNotEqual_WithInCorrectUserId()
        {
            var result = await this.agentService.GetAgentId(this.Renter.Id);

            Assert.That(result, Is.Not.EqualTo(this.Agent.Id));
        }

        [Test]
        public async Task GetAgentId_ShouldReturnZero_WithInCorrectUserId()
        {
            var result = await this.agentService.GetAgentId(this.Renter.Id);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public async Task ExistsById_ShouldReturnTrue_WithValidId()
        {
            var result = await this.agentService.ExistById(this.Agent.UserId);

            Assert.That(result, Is.True);
        }

        [Test]
        public async Task ExistsById_ShouldReturnFalse_WithInValidId()
        {
            var result = await this.agentService.ExistById(this.Renter.Id);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task AgentWithPhoneNumberExist_ShouldReturnTrue_WithValidId()
        {
            var result = await this.agentService.AgentWithPhoneNumberExist(this.Agent.PhoneNumber);

            Assert.That(result, Is.True);
        }

        [Test]
        public async Task AgentWithPhoneNumberExist_ShouldReturnFasle_WithInValidId()
        {
            var result = await this.agentService.AgentWithPhoneNumberExist(this.User.PhoneNumber);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task Create_ShouldWorkCorrectly()
        {
            var agentsCountBefore = this.context.Agents.Count();

            await this.agentService.Create(this.Agent.UserId, this.Agent.PhoneNumber);

            var agentsCountAfter = this.context.Agents.Count();

        }

    }
}

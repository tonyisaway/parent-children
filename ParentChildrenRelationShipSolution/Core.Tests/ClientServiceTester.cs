namespace Core.Tests
{
    using Interfaces.Services;
    using Interfaces.Services.Requests;

    using NUnit.Framework;

    [TestFixture]
    public class ClientTaskServiceTester : ClientTaskServiceBase
    {
        [Test]
        public void CanCreateClientTaskServiceType()
        {
            var service = GetServiceMock();

            Assert.That(service, Is.InstanceOf<IClientTaskService>());
        }

        [Test]
        public void CanCreateCreateTaskRequestType()
        {
            var request = GetCreateTaskRequestMock();

            Assert.That(request, Is.InstanceOf<ICreateTaskRequest>());
        }

        [Test]
        public void CanCreateCreateServiceResponseType()
        {
            var response = GetServiceResponseMock();
            Assert.That(response, Is.InstanceOf<IServiceResponse>());
        }

        [Test]
        public void GivenCreateRequestCanCallCreateOnService()
        {
            var service = GetServiceMock();
            var request = GetCreateTaskRequestMock();

            service.CreateTask(request);
        }

        [Test]
        public void GivenCreateRequestWhenCallCreateOnServiceThenResponseReturned()
        {
            var service = GetServiceMock();

            var request = GetCreateTaskRequestMock();
            var response = service.CreateTask(request);
        }

        [Test]
        public void CanCreateClientTaskService()
        {
            var service = GetClientTaskService();
            Assert.That(service, Is.Not.Null);
        }

        [Test]
        public void ClientTaskServiceImplementsInterfaceClientTaskService()
        {
            var service = GetClientTaskService();
            Assert.That(service, Is.InstanceOf<IClientTaskService>());
        }

        [Test]
        public void GivenCreateRequestWhenCallCreateOnServiceInstanceThenResponseIsNotNull()
        {
            var service = GetClientTaskService();
            var request = GetCreateTaskRequestMock();

            var response = service.CreateTask(request);

            Assert.That(response, Is.Not.Null);
        }

        [Test]
        public void CanCreateCreateServiceResponse()
        {
            var response = GetServiceResponse();
            Assert.That(response, Is.Not.Null);
        }

        [Test]
        public void ServiceResponseTypeHasStatus()
        {
            var response = GetServiceResponseMock();
            var status = response.Ok;
        }

        [Test]
        public void ServiceResponseTypeHasStatusAndItIsNotNull()
        {
            var response = GetServiceResponse();
            var status = response.Ok;
            Assert.That(status, Is.Not.Null);
        }

        [Test]
        public void ServiceResponseIsPessimisticByDefault()
        {
            var response = GetServiceResponse();
            Assert.That(response.Ok, Is.EqualTo(false));
        }

        [Test]
        public void GivenNullRequestWhenCallingCreateThenResponseIsFailure()
        {
            var service = GetClientTaskService();
            var response = service.CreateTask(null);
            Assert.That(response.Ok, Is.EqualTo(false));
        }

        [Test]
        public void GivenClientExistsGivenValidCreateTaskRequestWhenCallingCreateThenResponseIsTrue()
        {
            //var client = 
        }
    }
}

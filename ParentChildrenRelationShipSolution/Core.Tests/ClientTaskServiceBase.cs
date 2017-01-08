namespace Core.Tests
{
    using Domain;

    using Interfaces.Domain;
    using Interfaces.Services;
    using Interfaces.Services.Requests;

    using Moq;

    using Services;

    public class ClientTaskServiceBase
    {

        public static IClient GetClientMock()
        {
            return new Mock<IClient>().Object;
        }

        public static Client GetClient(int id)
        {
            return new Client(id);
        }

        public static ITask GetTaskMock()
        {
            return new Mock<ITask>().Object;
        }

        public static IClientTaskService GetServiceMock()
        {
            return new Mock<IClientTaskService>().Object;
        }

        public static ICreateTaskRequest GetCreateTaskRequestMock()
        {
            return new Mock<ICreateTaskRequest>().Object;
        }

        public static ClientTaskService GetClientTaskService()
        {
            return new ClientTaskService();
        }
        public static ServiceResponse GetServiceResponse()
        {
            return new ServiceResponse();
        }
        public static IServiceResponse GetServiceResponseMock()
        {
            return new Mock<IServiceResponse>().Object;
        }
    }
}

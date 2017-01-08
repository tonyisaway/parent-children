namespace Core.Services
{
    using System;

    using Interfaces.Services;
    using Interfaces.Services.Requests;

    public class ClientTaskService : IClientTaskService
    {
        public IServiceResponse CreateTask(ICreateTaskRequest request)
        {
            var response = new ServiceResponse();
            return response;
        }
    }
}

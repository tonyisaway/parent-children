namespace Core.Interfaces.Services
{
    using Requests;

    public interface IClientTaskService
    {
        IServiceResponse CreateTask(ICreateTaskRequest request);
    }
}

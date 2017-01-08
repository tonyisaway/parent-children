namespace Core.Interfaces.Domain
{
    using System.Collections.Generic;

    public interface IClient
    {
        IReadOnlyCollection<ITask> Tasks { get; }

        int Id { get; }

        ITask CreateTask();
    }
}

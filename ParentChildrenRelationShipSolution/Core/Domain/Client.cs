namespace Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Interfaces.Domain;

    public class Client : IClient
    {
        private readonly IList<ITask> tasks = new List<ITask>();

        public Client(int id)
        {
            this.Id = id;
        }

        public IReadOnlyCollection<ITask> Tasks => new ReadOnlyCollection<ITask>(this.tasks);

        public int Id { get; }

        public ITask CreateTask()
        {
            var name = this.tasks.Count.ToString();
            var task = this.CreateTask(name);
            return task;
        }

        private void IntendedTaskNameChange(ITask task, string name)
        {
            var otherTasks = this.tasks.Where(x => x != task);
            if (otherTasks.Any(x => x.Name == name))
            {
                throw new Exception();
            }
        }

        private ITask CreateTask(string name)
        {
            var task = new Task(name, this.Id, this.IntendedTaskNameChange);
            this.tasks.Add(task);
            return task;
        }
    }
}

namespace Core.Domain
{
    using System;

    using Interfaces.Domain;

    public class Task : ITask
    {
        private readonly Action<ITask, string> intendedNameChange;

        public Task(string name, int clientId, Action<ITask, string> intendedNameChange)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name cannot be null or empty string", nameof(name));
            }

            if (intendedNameChange == null)
            {
                throw new ArgumentNullException(nameof(intendedNameChange));
            }

            this.Name = name;
            this.ClientId = clientId;
            this.intendedNameChange = intendedNameChange;
        }

        public string Name { get; private set; }

        public int ClientId { get; }

        public void SetName(string name)
        {
            this.intendedNameChange?.Invoke(this, name);
            this.Name = name;
        }
    }
}

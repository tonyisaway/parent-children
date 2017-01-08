namespace Core.Interfaces.Domain
{
    public interface ITask
    {
        string Name { get; }

        void SetName(string name);

        int ClientId { get; }
    }
}

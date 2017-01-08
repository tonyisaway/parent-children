namespace Core.Tests
{
    using System.Collections.Generic;
    using System.Linq;

    using Interfaces.Domain;

    using NUnit.Framework;

    [TestFixture]
    public class ClientTasksRelationshipTester : ClientTaskServiceBase
    {
        [Test]
        public void CanCreateClient()
        {
            var client = GetClientMock();
        }

        [Test]
        public void CanCreateTask()
        {
            var task = GetTaskMock();
        }

        [Test]
        public void CanGetTasksFromClient()
        {
            var client = GetClientMock();
            var tasks = client.Tasks;
        }

        [Test]
        public void CanCallCreateTaskOnClientType()
        {
            var client = GetClientMock();
            var task = client.CreateTask();
        }

        [Test]
        public void CanCreateInstanceOfClient()
        {
            var client = GetClient(0);
        }

        [Test]
        public void ClientInstanceImplementsIClient()
        {
            var client = GetClient(0);
            Assert.That(client, Is.InstanceOf<IClient>());
        }

        [Test]
        public void GivenTaskWhenCallingAddThenTaskIsInClientsTasks()
        {
            var client = GetClient(0);
            var task = client.CreateTask();
            var tasks = client.Tasks;

            Assert.That(tasks.Contains(task), Is.True);
        }

        [Test]
        public void ClientTasksIsNotNull()
        {
            var client = GetClient(0);
            var tasks = client.Tasks;

            Assert.That(tasks, Is.Not.Null);
        }

        [Test]
        public void TasksOfClientsShouldBeReadOnly()
        {
            var client = GetClient(0);
            var tasks = client.Tasks;
            Assert.That(tasks, Is.InstanceOf<IReadOnlyCollection<ITask>>());
        }

        [Test]
        public void TaskHasName()
        {
            var task = GetTaskMock();
            var taskName = task.Name;
        }

        [Test]
        public void CreatedTaskHasName()
        {
            var client = GetClient(0);
            var task = client.CreateTask();
            Assert.That(task.Name, Is.Not.Null);
        }

        [Test]
        public void GivenClientWhenCreatingTwoTasksThenTasksHaveUniqueNames()
        {
            var client = GetClient(0);
            var taskOne = client.CreateTask();
            var taskTwo = client.CreateTask();

            Assert.That(taskOne.Name, Is.Not.EqualTo(taskTwo.Name));
        }

        [Test]
        public void GivenNameWhenUpdateTaskNameThenTaskNameIsCorrect()
        {
            var client = GetClient(0);
            var task = client.CreateTask();
            var newName = task.Name + "Updated";
            task.SetName(newName);

            Assert.That(task.Name, Is.EqualTo(newName));
        }

        [Test]
        public void GivenDuplicateNameCannotSetTaskNameToDuplicate()
        {
            var client = GetClient(0);
            var taskOne = client.CreateTask();
            var duplicateName = taskOne.Name;
            var taskTwo = client.CreateTask();
            TestDelegate testDelegate = () => taskTwo.SetName(duplicateName);

            Assert.That(testDelegate, Throws.Exception);
        }

        [Test]
        public void GivenNameWhenUpdateTaskNameThenTaskWithOldNameNoLongerFoundInTasks()
        {
            var client = GetClient(0);
            var task = client.CreateTask();
            var originalName = task.Name;
            var newName = task.Name + "Updated";
            task.SetName(newName);

            Assert.That(client.Tasks.Any(x => x.Name == originalName), Is.False);
        }

        [Test]
        public void ClientHasIdentifier()
        {
            var client = GetClientMock();
            int id = client.Id;
        }

        [Test]
        public void TaskHasClientId()
        {
            ITask task = GetTaskMock();
            int clientId = task.ClientId;
        }

        [Test]
        public void CreatedTaskHasClientId()
        {
            var client = GetClient(1);
            var task = client.CreateTask();

            Assert.That(task.ClientId, Is.EqualTo(client.Id));
        }
    }
}
namespace Core.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class ParentChildrenRelationshipTester
    {
        public void CanCreateParent()
        {
            var parent = new Mock<IParent>();
        }

        [Test]
        public void CanCreateChild()
        {
            var child = new Mock<IChild>();
        }

        [Test]
        public void CanGetChildrenFromParent()
        {
            var parent = new Mock<IParent>().Object;
            var children = parent.Children;
        }

        [Test]
        public void CanCreateInstanceOfParent()
        {
            var parent = new Parent(0);
        }

        [Test]
        public void ParentInstanceImplementsIParent()
        {
            var parent = new Parent(0);
            Assert.That(parent, Is.InstanceOf<IParent>());
        }

        [Test]
        public void GivenChildWhenCallingAddThenChildIsInParentsChildren()
        {
            var parent = new Parent(0);
            var child = parent.CreateChild();
            var children = parent.Children;

            Assert.That(children.Contains(child), Is.True);
        }

        [Test]
        public void ParentChildrenIsNotNull()
        {
            var parent = new Parent(0);
            var children = parent.Children;

            Assert.That(children, Is.Not.Null);
        }

        [Test]
        public void ChildrenOfParentsShouldBeReadOnly()
        {
            var parent = new Parent(0);
            var children = parent.Children;
            Assert.That(children, Is.InstanceOf<IReadOnlyCollection<IChild>>());
        }

        [Test]
        public void ChildHasName()
        {
            var child = new Mock<IChild>().Object;
            var childName = child.Name;
        }

        [Test]
        public void CreatedChildHasName()
        {
            var child = new Parent(0).CreateChild();
            Assert.That(child.Name, Is.Not.Null);
        }

        [Test]
        public void GivenParentWhenCreatingTwoChildrenThenChildrenHaveUniqueNames()
        {
            var parent = new Parent(0);
            var childOne = parent.CreateChild();
            var childTwo = parent.CreateChild();

            Assert.That(childOne.Name, Is.Not.EqualTo(childTwo.Name));
        }

        [Test]
        public void GivenNameWhenUpdateChildNameThenChildNameIsCorrect()
        {
            var parent = new Parent(0);
            var child = parent.CreateChild();
            var newName = child.Name + "Updated";
            child.SetName(newName);

            Assert.That(child.Name, Is.EqualTo(newName));
        }

        [Test]
        public void GivenDuplicateNameCannotSetChildNameToDuplicate()
        {
            var parent = new Parent(0);
            var childOne = parent.CreateChild();
            var duplicateName = childOne.Name;
            var childTwo = parent.CreateChild();
            TestDelegate testDelegate = () => childTwo.SetName(duplicateName);

            Assert.That(testDelegate, Throws.Exception);
        }

        [Test]
        public void GivenNameWhenUpdateChildNameThenChildWithOldNameNoLongerFoundInChildren()
        {
            var parent = new Parent(0);
            var child = parent.CreateChild();
            var originalName = child.Name;
            var newName = child.Name + "Updated";
            child.SetName(newName);

            Assert.That(parent.Children.Any(x => x.Name == originalName), Is.False);
        }

        [Test]
        public void ParentHasIdentifier()
        {
            IParent parent = new Mock<IParent>().Object;
            int id = parent.Id;
        }

        [Test]
        public void ChildHasParentId()
        {
            IChild child = new Mock<IChild>().Object;
            int parentId = child.ParentId;
        }

        [Test]
        public void CreatedChildHasParentId()
        {
            var parent = new Parent(1);
            var child = parent.CreateChild();

            Assert.That(child.ParentId, Is.EqualTo(parent.Id));
        }
    }

    public class Parent : IParent
    {
        private readonly IList<IChild> children = new List<IChild>();

        public Parent(int id)
        {
            this.Id = id;
        }

        public IReadOnlyCollection<IChild> Children => new ReadOnlyCollection<IChild>(this.children);

        public int Id { get; }

        public IChild CreateChild()
        {
            var name = this.children.Count.ToString();
            var child = this.CreateChild(name);
            return child;
        }

        private void IntendedChildNameChange(IChild child, string name)
        {
            var otherChildren = this.children.Where(x => x != child);
            if (otherChildren.Any(x => x.Name == name))
            {
                throw new Exception();
            }
        }

        private IChild CreateChild(string name)
        {
            var child = new Child(name, this.Id, this.IntendedChildNameChange);
            this.children.Add(child);
            return child;
        }

        private class Child : IChild
        {
            private readonly Action<IChild, string> intendedNameChange;

            public Child(string name, int parentId, Action<IChild, string> intendedNameChange)
            {
                if (string.IsNullOrEmpty(name))
                {
                    throw new ArgumentException("name cannot be null or empty string", nameof(name));
                }

                if (intendedNameChange == null)
                {
                    throw new ArgumentNullException(nameof(intendedNameChange));
                }

                this.Name = name;
                this.ParentId = parentId;
                this.intendedNameChange = intendedNameChange;
            }

            public string Name { get; private set; }

            public int ParentId { get; }

            public void SetName(string name)
            {
                this.intendedNameChange?.Invoke(this, name);
                this.Name = name;
            }
        }
    }

    public interface IChild
    {
        string Name { get;}

        void SetName(string name);

        int ParentId { get; }
    }

    public interface IParent
    {
        IReadOnlyCollection<IChild> Children { get; }

        int Id { get; }

        IChild CreateChild();
    }
}
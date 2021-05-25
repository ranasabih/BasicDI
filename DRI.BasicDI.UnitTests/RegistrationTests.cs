using System;
using Xunit;
using DRI.BasicDI.UnitTests.TestClasses;
using DRI.BasicDI.Exceptions;

namespace DRI.BasicDI.UnitTests
{
    public class RegistrationTests
    {
        [Fact]
        public void Should_register_concrete_types()
        {
          var container = new Container();
          container.Register<TestClassC>();
          
          Assert.Equal(1, container.Registrations());
        }

        [Fact]
        public void Should_throw_AlreadyRegisteredException_when_registering_dependency_more_than_once()
        {
            var container = new Container();
            container.Register<TestClassC>();
            Assert.Throws<AlreadyRegisteredException>(() => container.Register<TestClassC>());            
        }

        [Fact]
        public void Should_register_find_number_of_registered_types()
        {
          var container = new Container();
          container.Register<TestClassC>();
          container.Register<TestClassB>();
          container.Register<TestClassA>();

          Assert.Equal(3, container.Registrations());
        }
  }
}
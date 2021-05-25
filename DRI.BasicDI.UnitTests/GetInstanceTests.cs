using DRI.BasicDI.Exceptions;
using DRI.BasicDI.UnitTests.TestClasses;
using Xunit;

namespace DRI.BasicDI.UnitTests
{
  public class GetInstanceTests
    {
        [Fact]
        public void Should_instantiate_concrete_type_with_no_dependencies()
        {
            var container = new Container();
            container.Register<TestClassC>();
            var testClassC = container.GetInstance<TestClassC>();
            Assert.NotNull(testClassC);
        }

        [Fact]
        public void Should_instantiate_concrete_type_with_concrete_dependency()
        {
            var container = new Container();
            container.Register<TestClassC>();
            container.Register<TestClassB>();
            var testClassB = container.GetInstance<TestClassB>();
            Assert.NotNull(testClassB);            
        }

        [Fact]
        public void Should_instantiate_concrete_type_with_multiple_concrete_dependencies()
        {
          var container = new Container();
          container.Register<TestClassC>();
          container.Register<TestClassB>();
          container.Register<TestClassA>();
          var testClassA = container.GetInstance<TestClassA>();     
          Assert.NotNull(testClassA);
        }

        [Fact]
        public void Should_throw_UnregisteredDependencyException_when_instantiating_unregistered_type()
        {
            var container = new Container();
            Assert.Throws<UnregisteredDependencyException>(() => container.GetInstance<TestClassC>());
        }

        [Fact]
        public void Should_throw_UnregisteredDependencyException_when_instantiating_type_with_unregistered_dependency()
        {
          var container = new Container();          
          container.Register<TestClassA>();
          Assert.Throws<UnregisteredDependencyException>(() => container.GetInstance<TestClassA>());
        }

        [Fact]
        public void Should_throw_CircularDependencyException_when_instantiating_type_with_circular_dependency()
        {
          var container = new Container();
          container.Register<CircularClassA>();
          container.Register<CircularClassB>();
          container.Register<CircularClassC>();          
          Assert.Throws<CircularDependencyException>(() => container.GetInstance<CircularClassC>());
        }
    }
}
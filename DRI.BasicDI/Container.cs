using DRI.BasicDI.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
namespace DRI.BasicDI
{
    public sealed class Container
    {
        
        private readonly IDictionary<Type, Dependency> RegisteredTypes = new Dictionary<Type, Dependency>();
        
        public void Register<T>()
        {            
            var type = typeof(T);
            if (RegisteredTypes.ContainsKey(type))
            {
              throw new AlreadyRegisteredException();
            }            
            
            RegisteredTypes.Add(type, new Dependency(type));            
        }

        public int Registrations()
        {
            return RegisteredTypes.Count;
        }

        private object GetInstance(Type type)
        {
          if (!RegisteredTypes.ContainsKey(type)) 
            throw new UnregisteredDependencyException();
          
          var circularDependency = DependencyHelper.FindCircularDependency(type, RegisteredTypes);
          if (circularDependency != null)
            throw new CircularDependencyException();
          
          var registeredType = RegisteredTypes[type];
          
          var parameters = registeredType.ConstructorDepencencies
                                       .Select(dep => GetInstance(dep))
                                       .ToArray();

          return Activator.CreateInstance(type, parameters);           
        }   

        public T GetInstance<T>()
        {
            var type = typeof(T);          
            var instance = GetInstance(type);
            return (T)instance;            
        }
    }
}
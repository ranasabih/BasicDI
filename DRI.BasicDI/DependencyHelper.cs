using System;
using System.Collections.Generic;
using System.Linq;
namespace DRI.BasicDI
{
  public static class DependencyHelper
  {
    public static Type FindCircularDependency(Type start, IDictionary<Type, Dependency> registeredTypes)
    {
      var dependencies = new HashSet<Type>();
      Type current = start;
      while (current != null)
      {
        dependencies.Add(current);
        registeredTypes.TryGetValue(current, out Dependency dependency);
        current = dependency?.ConstructorDepencencies.FirstOrDefault();

        if (dependencies.Contains(current))
        {          
          return current;
        }
      }

      return null;
    }
  }
}

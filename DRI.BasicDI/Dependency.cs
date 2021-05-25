using System;
using System.Linq;
namespace DRI.BasicDI
{
  public class Dependency
  {
      public Dependency(Type T){
        var type = T;
        var constructor = type.GetConstructors().FirstOrDefault();
        ConstructorDepencencies = constructor == null ? new Type[0] 
                                  : constructor.GetParameters().Select(p => p.ParameterType).ToArray();
      }
      public Type[] ConstructorDepencencies { get; private set; }
  }
}

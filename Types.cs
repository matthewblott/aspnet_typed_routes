using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace aspnet_typed_routes
{
  public class Id1
  {
    private int _value;

    public Id1() { }
    
    // use with ...
    // int id1 = 7
    // Id0 id2 = id1 
    public static implicit operator Id1(int value) => new Id1 {_value = value};

    // use with ...
    // Id0 id1 = 7
    // int id2 = id1
    public static implicit operator int(Id1 value) => value._value;

  }

  public class Id2
  {
    private int _value;
    public Id2() { }
    public static implicit operator Id2(int value) => new Id2 {_value = value};
    public static implicit operator int(Id2 value) => value._value;
  }

  [ModelBinder(BinderType = typeof(Id3ModelBinder))]
  public class Id3
  {
    private int _value;
    public Id3() { }
    public static implicit operator Id3(int value) => new Id3 {_value = value};
    public static implicit operator int(Id3 value) => value._value;
  }

  [ModelBinder(BinderType = typeof(Id4ModelBinder))]
  public class Id4
  {
    private int _value;
    public Id4() { }
    public static implicit operator Id4(int value) => new Id4 {_value = value};
    public static implicit operator int(Id4 value) => value._value;
  }

  // Only required if it's been registered in Startup
  public class Id3BinderProvider : IModelBinderProvider  
  {  
    public IModelBinder GetBinder(ModelBinderProviderContext context) => 
      context.Metadata.ModelType == typeof(Id3) ? new Id3ModelBinder() : null;
  }

  public class Id3ModelBinder : IModelBinder
  {
    public Task BindModelAsync(ModelBindingContext context)  
    { 
      var name = nameof(Id3).ToLower();
      var result = context.ValueProvider.GetValue(name);
      var x = (Id3)Convert.ToInt32(result.FirstValue);
      var id3 = x;
      
      context.Result = ModelBindingResult.Success(id3);  
  
      return Task.CompletedTask;  
    
    }

  }
  
  public class Id4ModelBinder : IModelBinder
  {
    public Task BindModelAsync(ModelBindingContext context)  
    { 
      var name = nameof(Id4).ToLower();
      var result = context.ValueProvider.GetValue(name);
      var x = (Id4)Convert.ToInt32(result.FirstValue);
      var id3 = x;
      
      context.Result = ModelBindingResult.Success(id3);  
  
      return Task.CompletedTask;  
    
    }

  }

  
  public class BaseType<T>
  {
    private T _value;
    
    public static implicit operator BaseType<T>(T value)
    {
      return new BaseType<T> {_value = value};
    }

    public static implicit operator T(BaseType<T> value)
    {
      return value._value;
    }
    
  }

  public class Id : BaseType<int> { }
  
}
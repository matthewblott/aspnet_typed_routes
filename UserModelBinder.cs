using Microsoft.AspNetCore.Mvc.ModelBinding;  
using System;
using System.IO;
using System.Threading.Tasks;
using aspnet_typed_routes.models;

namespace aspnet_typed_routes
{  
  public class UserModelBinder : IModelBinder
  {  
    public Task BindModelAsync(ModelBindingContext context)  
    {  
      if (context == null)  
        throw new ArgumentNullException(nameof(context));  

      var valueFromBody = string.Empty;  
  
      // InvalidOperationException:
      // Synchronous operations are disallowed. Call ReadAsync or set AllowSynchronousIO to true instead.
      
//      using (var sr = new StreamReader(context.HttpContext.Request.Body))  
//      {  
//        valueFromBody = sr.ReadToEnd();  
//      }  
//  
//      if (string.IsNullOrEmpty(valueFromBody))  
//      {  
//        return Task.CompletedTask;  
//      }  
//  
      var values = "a|b";
      
      var splitData = values.Split(new char[] { '|' });
      
      if (splitData.Length >= 2)
      {
        var result = new UserViewModel();
          context.Result = ModelBindingResult.Success(result);  
      }  
  
      return Task.CompletedTask;  
      
    }
    
  }
  
}  
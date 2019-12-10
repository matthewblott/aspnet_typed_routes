using Microsoft.AspNetCore.Mvc.ModelBinding;
using aspnet_typed_routes.models;

namespace aspnet_typed_routes  
{
  public class UserModelBinderProvider : IModelBinderProvider  
  {  
    public IModelBinder GetBinder(ModelBinderProviderContext context) => 
      context.Metadata.ModelType == typeof(UserViewModel) ? new UserModelBinder() : null;

  }
}
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

// http://www.prideparrot.com/blog/archive/2012/3/change_the_input_parameters_using_action_filters
// https://stackoverflow.com/questions/10416951/change-the-model-in-onactionexecuting-event
// https://www.strathweb.com/2017/07/customizing-query-string-parameter-binding-in-asp-net-core-mvc
// https://www.ryadel.com/en/asp-net-mvc-fix-ambiguous-action-methods-errors-multiple-action-methods-action-name-c-sharp-core
// https://www.c-sharpcorner.com/article/custom-model-binding-in-asp-net-core-mvc

namespace aspnet_typed_routes
{
  public class MyActionFilterAttribute : ActionFilterAttribute
  {
    public override void OnActionExecuting(ActionExecutingContext context)
    {
      var (key1, value1) = context.ActionArguments.SingleOrDefault(pair => pair.Value is Id1);

      if (!string.IsNullOrWhiteSpace(key1))
      {
        SetId1(context);
        
        return;
      }

      var (key2, value2) = context.ActionArguments.SingleOrDefault(pair => pair.Value is Id2);

      if (!string.IsNullOrWhiteSpace(key2))
      {
        SetId2(context);
        
        return;
      }
      
    }

    private void SetId1(ActionExecutingContext context)
    {
      var name = nameof(Id1).ToLower();
      var ctl = context.Controller as Controller;
      var ctx = ctl?.ControllerContext;
      var provider = CompositeValueProvider.CreateAsync(ctx);
      var result = provider.Result.GetValue(name);
      
      context.ActionArguments[name] = (Id1)Convert.ToInt32(result.FirstValue);
      
    }
    
    private void SetId2(ActionExecutingContext context)
    {
      var name = nameof(Id2).ToLower();
      var ctl = context.Controller as Controller;
      var ctx = ctl?.ControllerContext;
      var provider = CompositeValueProvider.CreateAsync(ctx);
      var result = provider.Result.GetValue(name);
      
      context.ActionArguments[name] = (Id2)Convert.ToInt32(result.FirstValue);
      
    }

  }
  
}
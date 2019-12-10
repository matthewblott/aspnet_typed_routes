using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Routing;

namespace aspnet_typed_routes
{
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
  public class RequireParameterAttribute : ActionMethodSelectorAttribute 
  {
    public RequireParameterAttribute(string valueName) => ValueName = valueName;

    public override bool IsValidForRequest(RouteContext context, ActionDescriptor action) => 
      context.HttpContext.Request.Query.Keys.Contains(ValueName, StringComparer.OrdinalIgnoreCase);

    public string ValueName { get; }
    
  }
  
  public class RequireParametersAttribute : ActionMethodSelectorAttribute 
  {
    public RequireParametersAttribute(string parameterName) : this(new[] { parameterName }) { }
    public RequireParametersAttribute(params string[] parameterNames) => ParameterNames = parameterNames;
    
    public override bool IsValidForRequest(RouteContext context, ActionDescriptor action) => 
      ParameterNames.All(parameterName => context.HttpContext.Request.Query.Keys.Contains(ValueName, StringComparer.OrdinalIgnoreCase));

    public string[] ParameterNames { get; private set; }

    public string ValueName { get; }
    
  }

  
  public class RequiredParameterAttribute1 : ActionMethodSelectorAttribute
  {
    public RequiredParameterAttribute1(string parameterName) : this(new[] { parameterName }) { }
    public RequiredParameterAttribute1(params string[] parameterNames) => ParameterNames = parameterNames;

    public override bool IsValidForRequest(RouteContext routeContext, ActionDescriptor action)
    {
      var request = routeContext.HttpContext.Request;
      
      switch (Mode)
      {
        case MatchMode.All:
        default:
          return IncludeGet &&
                 ParameterNames.All(p => request.Query.Keys.Contains(p))
                 || IncludePost && ParameterNames.All(p => request.Form.Keys.Contains(p))
                 || IncludeCookies && ParameterNames.All(p => request.Cookies.Keys.Contains(p));
        case MatchMode.Any:
          return IncludeGet &&
                 ParameterNames.Any(p => request.Query.Keys.Contains(p))
                 || IncludePost && ParameterNames.Any(p => request.Form.Keys.Contains(p))
                 || IncludeCookies && ParameterNames.Any(p => request.Cookies.Keys.Contains(p));
        case MatchMode.None:
          return (!IncludeGet ||
                  !ParameterNames.Any(p => request.Query.Keys.Contains(p)))
                 && (!IncludePost || !ParameterNames.Any(p => request.Form.Keys.Contains(p)))
                 && (!IncludeCookies || !ParameterNames.Any(p => request.Cookies.Keys.Contains(p)));
      }

    }

    public string[] ParameterNames { get; private set; }
    public bool IncludeGet { get; set; } = true;
    public bool IncludePost { get; set; } = true;
    public bool IncludeCookies { get; set; } = false;
    public MatchMode Mode { get; set; } = MatchMode.All;
    public enum MatchMode
    {
      All,
      Any,
      None
    }
    
  }

}
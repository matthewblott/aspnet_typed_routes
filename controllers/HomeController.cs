using Microsoft.AspNetCore.Mvc;

namespace aspnet_typed_routes.controllers
{
  public class HomeController : Controller
  {
    [MyActionFilter]
    public IActionResult Index() => View();

    [MyActionFilter]
    [RequireParameter(nameof(Id1))]
    public int Index(Id1 id1) => id1;

    [MyActionFilter]
    [RequireParameter(nameof(Id2))]
    public int Index(Id2 id2) => id2;

    [RequireParameter(nameof(Id3))]
    public int Index(Id3 id3) => id3;

    [RequireParameter(nameof(Id4))]
    public int Index(Id4 id4) => id4;

    // [RequireParameterAttribute(nameof(Id3))]
    // [RequireParameterAttribute(nameof(Id4))]
    //[RequireParametersAttribute(nameof(Id3), nameof(Id4))]
    public int Index(Id3 id3, Id4 id4) => id3 + id4;
    
    [MyActionFilter]
    public IActionResult About() => View();
    
  }
    
}

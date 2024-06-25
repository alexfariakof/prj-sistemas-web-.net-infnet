using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace LiteStreaming.AdministrativeApp.Controllers.Abstractions;

public abstract class BaseController : Controller
{
    protected string CREATE { get;  } = "Create";
    protected string INDEX { get; }  = "Index";
    protected string EDIT { get; }  = "Edit";

    protected BaseController() { }

    public Guid UserId
    {
        get
        {
            if (User.Identity.IsAuthenticated && Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId))
            {
                return userId;
            }
            throw new ArgumentNullException();
        }
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);

        try
        {
            var user = UserId.ToString(); 
        }
        catch
        {
            ViewBag.LoginError = "Usuário sem permissão de acesso.";
            HttpContext.SignOutAsync();
        }
    }

    protected IActionResult CreateView()
    {
        return View(CREATE);
    }

    protected IActionResult EditView()
    {
        return View(EDIT);
    }

    protected IActionResult RedirectToIndexView()
    {
        return RedirectToAction(INDEX);
    }
}

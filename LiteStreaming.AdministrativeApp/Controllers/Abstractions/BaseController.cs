using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AdministrativeApp.Controllers.Abastractions;
public abstract class BaseController : Controller
{
    public Guid? UserId;

    public string? UserName;
    protected BaseController() { }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);

        try
        {
            var userId = HttpContext.Session.GetString("UserId");
            this.UserId = userId != null ? Guid.Parse(userId) : null;
            if (this.UserId is null)
            {
                ViewBag.LoginError = "Usuário sem permissão de acesso.";
            }

            var userName = HttpContext.Session.GetString("UserName");
            this.UserName = !String.IsNullOrEmpty(userName) ? userName : null;
            if (this.UserName is null)  throw new();
        }
        catch
        {
            this.ClaerSession();
        }
    }

    protected void ClaerSession()
    {
        HttpContext.Session.Clear();
        this.UserId = null;
        this.UserName = null;
    }
}

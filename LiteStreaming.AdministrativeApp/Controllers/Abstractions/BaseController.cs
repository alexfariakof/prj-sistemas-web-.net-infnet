using LiteStreaming.Application.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlClient;
using System.Security.Claims;

namespace LiteStreaming.AdministrativeApp.Controllers.Abstractions;

public abstract class BaseController<T> : Controller where T : class, new()
{
    protected string INDEX { get; } = "Index";
    protected string CREATE { get;  } = "Create";    
    protected string EDIT { get; }  = "Edit";
    protected IService<T> Services { get; private set; }

    protected BaseController(IService<T> service) 
    {
        this.Services = service;    
    }

    public virtual IActionResult Index(string sortExpression = null)
    {
        ViewData["SortParamName"] = "";
        SortOrder sortOrder;
        string? sortProperty = sortExpression?.Replace("_desc", "").ToLower();

        if (sortProperty is not null && sortExpression.Contains("_desc"))
        {
            sortOrder = SortOrder.Descending;
            ViewData["SortParamName"] = $"{sortProperty}";
        }
        else if (sortProperty is not null && !sortExpression.Contains("_desc"))
        {
            sortOrder = SortOrder.Ascending;
            ViewData["SortParamName"] = $"{sortProperty}_desc";
        }
        else
        {
            sortOrder = SortOrder.Ascending;
            ViewData["SortParamName"] = "name";
        }
        return View(this.Services.FindAll(sortProperty, sortOrder));
    }

    protected IActionResult IndexView()
    {
        return View(INDEX, this.Services.FindAll());
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
}
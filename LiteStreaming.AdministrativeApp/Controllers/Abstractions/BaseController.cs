using LiteStreaming.Application.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlClient;
using System.Security.Claims;

namespace LiteStreaming.AdministrativeApp.Controllers.Abstractions;

public abstract class BaseController<T> : Controller where T : class, new()
{
    const string SORT_PARAM_NAME = "SortParamName"; 
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
        ViewData[SORT_PARAM_NAME] = "";
        SortOrder sortOrder;
        string? sortProperty = sortExpression?.Replace("_desc", "").ToLower();

        if (sortProperty is not null && (sortExpression.Contains("_desc") || sortExpression.Contains("_init")))
        {
            sortOrder = SortOrder.Descending;
            ViewData[SORT_PARAM_NAME] = $"{sortProperty}";
        }
        else if (sortProperty is not null && !sortExpression.Contains("_desc"))
        {
            sortOrder = SortOrder.Ascending;
            ViewData[SORT_PARAM_NAME] = $"{sortProperty}_desc";
        }
        else
        {
            sortOrder = SortOrder.Ascending;
            ViewData[SORT_PARAM_NAME] = "_init";
        }
        return View(this.Services.FindAllSorted(sortProperty, sortOrder));
    }

    [Authorize]
    public virtual IActionResult Create()
    {
        return View(CREATE);
    }

    protected IActionResult IndexView()
    {
        return View(INDEX, this.Services.FindAll());
    }
    protected virtual IActionResult CreateView()
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
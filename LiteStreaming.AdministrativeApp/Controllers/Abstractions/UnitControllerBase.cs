using LiteStreaming.AdministrativeApp.Models;
using LiteStreaming.Application.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlClient;
using System.Security.Claims;

namespace LiteStreaming.AdministrativeApp.Controllers.Abstractions;

public abstract class UnitControllerBase<T> : Controller where T : class, new()
{
    const string SORT_PARAM_NAME = "SortParamName";
    const string SORT_ICONS = "SortIcon";
    const string SORT_ICON_ASC = "";
    const string SORT_ICON_DESC = "";
    protected string INDEX { get; } = "Index";
    protected string CREATE { get;  } = "Create";    
    protected string EDIT { get; }  = "Edit";
    protected IService<T> Services { get; private set; }

    protected UnitControllerBase(IService<T> service) 
    {
        this.Services = service;    
    }

    private SortModel ApllySortOrder(string sortExpression)
    {
        SortModel sortModel = new();
        if (sortExpression is not null && !sortExpression.Contains("_desc"))
        {
            sortModel.SortOrder= SortOrder.Ascending;
            sortModel.SortParamName = $"{sortExpression}_desc";
        }
        else if (sortExpression is not null)
        {
            sortModel.SortOrder  = SortOrder.Descending;
            sortModel.SortParamName  = $"{sortExpression.Replace("_desc", "").ToLower()}";
            sortModel.SortIcon = SortIcons.SORT_ICON_DESC;
        }
        return sortModel;
    }

    public virtual IActionResult Index(string sortExpression = null, string SearchText = "")
    {
        var pagerModel = new PagerModel();                
        pagerModel.SortModel = ApllySortOrder(sortExpression);
        pagerModel.SearchText = SearchText;
        pagerModel.SetItems<T>(this.Services.FindAllSorted(SearchText, sortExpression, pagerModel.SortModel.SortOrder));
        return View(pagerModel);
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

    protected AlertViewModel SuccessMessage(string message)
    {
        return new AlertViewModel(AlertViewModel.AlertType.Success, message);
    }

    protected AlertViewModel WarningMessage(string message)
    {
        return new AlertViewModel(AlertViewModel.AlertType.Warning, message);
    }

    protected AlertViewModel ErrorMessage(string message)
    {
        return new AlertViewModel(AlertViewModel.AlertType.Danger, message);
    }
}
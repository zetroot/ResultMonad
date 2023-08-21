using Abstractions.Models;
using Microsoft.AspNetCore.Mvc;
using ServerApi.Services;

namespace ServerApi.Controllers;

[ApiController, Route("api/v1/[controller]")]
public class MainController : ControllerBase
{
    private readonly MainService _service;

    public MainController(MainService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<MyDataObject> GetData([FromQuery]bool fail)
    {
        var serviceResult = _service.GetData(fail);
        return WrapResult(serviceResult);
    }

    private UnprocessableEntityObjectResult BusinessProblem(Problem problem) => new(new ProblemDetails
        { Title = problem.Title, Detail = problem.Detail });
    
    private ActionResult<T> WrapResult<T>(MaybeResult<T> result) => result.Match<ActionResult>(x => Ok(x), BusinessProblem);
}

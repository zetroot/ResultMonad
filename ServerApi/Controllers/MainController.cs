using Abstractions.Models;
using Microsoft.AspNetCore.Mvc;
using ServerApi.Services;

namespace ServerApi.Controllers;

[ApiController, Route("api/v1/[controller]")]
public class MainController : ControllerBase
{
    private readonly MainService _service;
    private readonly Transformer _transformer;
    
    public MainController(MainService service, Transformer transformer)
    {
        _service = service;
        _transformer = transformer;
    }

    [HttpGet]
    public ActionResult<MyDataObject> GetData([FromQuery]int input)
    {
        var serviceResult = _service.GetData(input);
        var transformed = _transformer.Transform(serviceResult);
        return WrapResult(transformed);
    }

    private UnprocessableEntityObjectResult BusinessProblem(Problem problem) => new(new ProblemDetails
        { Title = problem.Title, Detail = problem.Detail });
    
    private ActionResult<T> WrapResult<T>(MaybeResult<T> result) => result.Match<ActionResult>(x => Ok(x), BusinessProblem);
}

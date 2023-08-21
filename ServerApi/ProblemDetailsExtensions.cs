using Abstractions.Models;
using Microsoft.AspNetCore.Mvc;

namespace ServerApi;

public static class ProblemDetailsExtensions
{
    public static ProblemDetails ToProblemDetails(this Problem problem) => new ProblemDetails
        { Title = problem.Title, Detail = problem.Detail };
}

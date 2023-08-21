using OneOf;

namespace Abstractions.Models;

public class MaybeResult<T> : OneOfBase<T, Problem>
{
    protected MaybeResult(OneOf<T, Problem> input) : base(input)
    {
    }

    public static MaybeResult<T> Ok(T result) => new(result);
    public static MaybeResult<T> Fail(Problem problem) => new(problem);

    public static implicit operator MaybeResult<T> (T result) => new (result);
    public static implicit operator MaybeResult<T>(Problem problem) => new(problem);
}

public record Problem(string Title, string Detail);

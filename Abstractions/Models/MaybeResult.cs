using OneOf;

namespace Abstractions.Models;

public class MaybeResult<T> : OneOfBase<T, Problem>
{
    protected MaybeResult(OneOf<T, Problem> input) : base(input)
    {
    }

    public MaybeResult<U> Bind<U>(Func<T, MaybeResult<U>> func, Func<Problem, Problem> problemFunc) => 
        Match<MaybeResult<U>>(func, problem => problemFunc(problem));

    public MaybeResult<U> Bind<U>(Func<T, MaybeResult<U>> func) => Bind(func, x => x);
    
    public static MaybeResult<T> Ok(T result) => new(result);
    public static MaybeResult<T> Fail(Problem problem) => new(problem);

    public static implicit operator MaybeResult<T> (T result) => new (result);
    public static implicit operator MaybeResult<T>(Problem problem) => new(problem);
}

public record Problem(string Title, string Detail);

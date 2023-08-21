using Abstractions.Models;

namespace ServerApi.Services;

public class MainService
{
    public MaybeResult<int> GetData(int input) =>
        input switch
        {
            < 0 => new Problem("No negatives allowed", "Input must be non negative"),
            _ => input
        };
}

public class Transformer
{
    public MaybeResult<MyDataObject> Transform(MaybeResult<int> src) => 
        src.Bind(x => x switch
        {
            < 42 => MaybeResult<MyDataObject>.Fail(new ("Too little", "Value must be greater than 42")),
            _ => new MyDataObject(x * 2)
        } );
}

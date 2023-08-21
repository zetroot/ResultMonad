using Abstractions.Models;

namespace ServerApi.Services;

public class MainService
{
    public MaybeResult<MyDataObject> GetData(bool fail)
    {
        return fail ? new Problem("Fail requested", "User requested failure") : new MyDataObject(42);
    }
}

using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Abstractions.Models;
using Refit;

namespace Abstractions.Providers;

public interface IMainClient
{
    [Get("/api/v1/main")]
    public Task<IApiResponse<MyDataObject>> GetData([Query]bool fail);
}

public interface IMainProvider
{
    public Task<MaybeResult<MyDataObject>> GetData(bool fail);
}

public class MainProvider : IMainProvider
{
    private readonly IMainClient _client;

    public MainProvider(IMainClient client)
    {
        _client = client;
    }


    private async Task<MaybeResult<T>> ProcessResponse<T>(Func<Task<IApiResponse<T>>> call)
    {
        var response = await call();
        if (response.IsSuccessStatusCode)
        {
            return response.Content ?? throw new InvalidDataException("Response does not contain body");
        }

        if (response.StatusCode == HttpStatusCode.UnprocessableEntity)
        {
            var problemBody = response.Error.Content ??
                              throw new InvalidDataException("Response body does not contain problem details");
            return JsonSerializer.Deserialize<Problem>(problemBody) ?? throw new InvalidDataException("Response body is not problem details body");
        }

        throw response.Error;
    }

    public Task<MaybeResult<MyDataObject>> GetData(bool fail) => ProcessResponse(() => _client.GetData(fail));
}

using System.Net.Http.Json;
using Core.Requests;
using Core.Responses;
using Util.Exceptions;
using Util.Services.Abstract;

namespace Util.Services;

public class RemoteScannerService : IRemoteScannerService
{
    private const string ServiceHostName = "localhost:5000";

    public async Task<StartScanningResponse> StartScanningAsync(StartScanningRequest request)
    {
        var response = await PostAsync(
            $"http://{ServiceHostName}/Scanner/StartScanning",
            JsonContent.Create(request)
        );

        var startScanningResponse = await response.Content.ReadFromJsonAsync<StartScanningResponse>();
        if (startScanningResponse is null)
        {
            throw new RemoteServiceCallException("Could not deserialize response body.");
        }

        return startScanningResponse;
    }

    public async Task<StatusResponse> GetScanningStatus(StatusRequest request)
    {
        var response = await PostAsync(
            $"http://{ServiceHostName}/Scanner/Status",
            JsonContent.Create(request)
        );

        var statusResponse = await response.Content.ReadFromJsonAsync<StatusResponse>();
        if (statusResponse is null)
        {
            throw new RemoteServiceCallException("Could not deserialize response body.");
        }

        return statusResponse;
    }

    private static async Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content)
    {
        using var client = new HttpClient();

        try
        {
            return await client.PostAsync(requestUri, content);
        }
        catch (HttpRequestException e)
        {
            throw new RemoteServiceCallException("Error on http request to remote service.", e);
        }
        catch (TaskCanceledException e)
        {
            throw new RemoteServiceCallException("Error on http request to remote service.", e);
        }
    }
}
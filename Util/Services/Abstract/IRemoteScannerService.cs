using Core.Requests;
using Core.Responses;

namespace Util.Services.Abstract;

public interface IRemoteScannerService
{
    Task<StartScanningResponse> StartScanningAsync(StartScanningRequest request);

    Task<StatusResponse> GetScanningStatus(StatusRequest request);
}
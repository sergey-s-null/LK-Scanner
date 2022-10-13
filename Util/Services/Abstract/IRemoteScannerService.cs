using Core.Requests;
using Core.Responses;
using Util.Exceptions;

namespace Util.Services.Abstract;

public interface IRemoteScannerService
{
    /// <exception cref="RemoteServiceCallException">Error on remote service calling</exception>
    Task<StartScanningResponse> StartScanningAsync(StartScanningRequest request);

    /// <exception cref="RemoteServiceCallException">Error on remote service calling</exception>
    Task<StatusResponse> GetScanningStatus(StatusRequest request);
}
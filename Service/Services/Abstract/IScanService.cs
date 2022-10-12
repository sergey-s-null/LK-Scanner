using Service.Entities.Abstract;
using Service.Exceptions;

namespace Service.Services.Abstract;

public interface IScanService
{
    /// <exception cref="DirectoryScanException">Error on scan directory.</exception>
    Task<IScanResult> ScanDirectoryAsync(DirectoryInfo directory);
}
using Service.Entities;
using Service.Exceptions;

namespace Service.Services.Abstract;

public interface IFileScanner
{
    /// <summary>
    /// Scan file for suspicious content
    /// </summary>
    /// <param name="file"></param>
    /// <returns>null - there is not suspicious content, suspicion type otherwise.</returns>
    /// <exception cref="FileScanException">Error on scan file.</exception>
    Task<SuspicionType?> ScanFileAsync(FileInfo file);
}
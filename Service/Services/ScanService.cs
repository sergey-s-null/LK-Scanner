using System.Diagnostics;
using System.Security;
using Service.Entities;
using Service.Entities.Abstract;
using Service.Exceptions;
using Service.Services.Abstract;

namespace Service.Services;

public class ScanService : IScanService
{
    private readonly IFileScanner _fileScanner;

    public ScanService(IFileScanner fileScanner)
    {
        _fileScanner = fileScanner;
    }

    public async Task<IScanResult> ScanDirectoryAsync(DirectoryInfo directory)
    {
        var fileCount = 0;
        var detections = new Dictionary<SuspicionType, int>();
        var errorCount = 0;
        var stopwatch = Stopwatch.StartNew();

        foreach (var file in EnumerateFilesRecursively(directory))
        {
            fileCount++;

            try
            {
                var suspicionType = await _fileScanner.ScanFileAsync(file);
                if (suspicionType is not null)
                {
                    if (detections.ContainsKey(suspicionType))
                    {
                        detections[suspicionType] += 1;
                    }
                    else
                    {
                        detections[suspicionType] = 1;
                    }
                }
            }
            catch (FileScanException)
            {
                errorCount++;
            }
        }

        stopwatch.Stop();

        return new ScanResult(directory.FullName, fileCount, detections, errorCount, stopwatch.Elapsed);
    }

    private static IEnumerable<FileInfo> EnumerateFilesRecursively(DirectoryInfo directory)
    {
        foreach (var file in GetFiles(directory))
        {
            yield return file;
        }

        foreach (var subDirectory in GetDirectories(directory))
        {
            foreach (var file in EnumerateFilesRecursively(subDirectory))
            {
                yield return file;
            }
        }
    }

    private static IEnumerable<FileInfo> GetFiles(DirectoryInfo directory)
    {
        try
        {
            return directory.GetFiles();
        }
        catch (IOException e)
        {
            throw new DirectoryScanException("Error on list files in directory.", e);
        }
    }

    private static IEnumerable<DirectoryInfo> GetDirectories(DirectoryInfo directory)
    {
        try
        {
            return directory.GetDirectories();
        }
        catch (IOException e)
        {
            throw new DirectoryScanException("Error on list subdirectories.", e);
        }
        catch (SecurityException e)
        {
            throw new DirectoryScanException("Error on list subdirectories.", e);
        }
        catch (UnauthorizedAccessException e)
        {
            throw new DirectoryScanException("Error on list subdirectories.", e);
        }
    }
}
﻿using System.Diagnostics;
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
        var detects = new Dictionary<SuspicionType, int>();
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
                    if (detects.ContainsKey(suspicionType))
                    {
                        detects[suspicionType] += 1;
                    }
                    else
                    {
                        detects[suspicionType] = 1;
                    }
                }
            }
            catch (FileScanException e)
            {
                errorCount++;
            }
        }

        stopwatch.Stop();

        return new ScanResult(fileCount, detects, errorCount, stopwatch.Elapsed);
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
        catch (DirectoryNotFoundException e)
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
        catch (DirectoryNotFoundException e)
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
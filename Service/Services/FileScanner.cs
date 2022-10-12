using Service.Entities;
using Service.Exceptions;
using Service.Services.Abstract;

namespace Service.Services;

public class FileScanner : IFileScanner
{
    private readonly ISuspiciousContentCheckersProvider _suspiciousContentCheckersProvider;

    public FileScanner(ISuspiciousContentCheckersProvider suspiciousContentCheckersProvider)
    {
        _suspiciousContentCheckersProvider = suspiciousContentCheckersProvider;
    }

    public async Task<SuspicionType?> ScanFileAsync(FileInfo file)
    {
        var checkers = _suspiciousContentCheckersProvider.GetCheckers(file);

        try
        {
            using var reader = new StreamReader(file.OpenRead());
            while (true)
            {
                var line = await reader.ReadLineAsync();
                if (line is null)
                {
                    break;
                }

                var suspicionType = checkers.FirstOrDefault(x => x.CheckLine(line))?.SuspicionType;
                if (suspicionType is not null)
                {
                    return suspicionType;
                }
            }
        }
        catch (ArgumentOutOfRangeException e)
        {
            throw new FileScanException("Error on read file.", e);
        }
        catch (UnauthorizedAccessException e)
        {
            throw new FileScanException("Error on read file.", e);
        }
        catch (IOException e)
        {
            throw new FileScanException("Error on read file.", e);
        }

        return null;
    }
}
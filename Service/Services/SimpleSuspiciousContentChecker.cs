using Service.Entities;
using Service.Services.Abstract;

namespace Service.Services;

public class SimpleSuspiciousContentChecker : ISuspiciousContentChecker
{
    public SuspicionType SuspicionType { get; }

    private readonly string _suspiciousLine;
    private readonly Func<FileInfo, bool>? _isForCheck;

    public SimpleSuspiciousContentChecker(
        SuspicionType suspicionType,
        string suspiciousLine,
        Func<FileInfo, bool>? isForCheck = null)
    {
        SuspicionType = suspicionType;
        _suspiciousLine = suspiciousLine;
        _isForCheck = isForCheck;
    }

    public bool IsForCheck(FileInfo file)
    {
        return _isForCheck is null || _isForCheck(file);
    }

    public bool CheckLine(string line)
    {
        return line == _suspiciousLine;
    }
}
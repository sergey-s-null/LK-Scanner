using Service.Entities;

namespace Service.Services.Abstract;

public interface ISuspiciousContentChecker
{
    SuspicionType SuspicionType { get; }

    bool IsForCheck(FileInfo file);

    /// <returns>true - line is suspicious, false - line is not suspicious.</returns>
    bool CheckLine(string line);
}
using Service.Entities;
using Service.Services.Abstract;

namespace Service.Services;

public class SuspiciousContentCheckersProvider : ISuspiciousContentCheckersProvider
{
    private readonly IReadOnlyCollection<ISuspiciousContentChecker> _checkers;

    public SuspiciousContentCheckersProvider()
    {
        _checkers = new[]
        {
            new SimpleSuspiciousContentChecker(
                SuspicionType.Js,
                "<script>evil_script()</script>",
                x => x.Extension.ToLower() == ".js"
            ),
            new SimpleSuspiciousContentChecker(SuspicionType.RmRf, @"rm -rf %userprofile%\Documents"),
            new SimpleSuspiciousContentChecker(SuspicionType.Rundll32, "Rundll32 sus.dll SusEntry")
        };
    }

    public IReadOnlyCollection<ISuspiciousContentChecker> GetCheckers(FileInfo file)
    {
        return _checkers
            .Where(x => x.IsForCheck(file))
            .ToList();
    }
}
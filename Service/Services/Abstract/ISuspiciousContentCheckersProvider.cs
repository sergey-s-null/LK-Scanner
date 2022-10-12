namespace Service.Services.Abstract;

public interface ISuspiciousContentCheckersProvider
{
    IReadOnlyCollection<ISuspiciousContentChecker> GetCheckers(FileInfo file);
}
using Autofac;
using Service.Services;
using Service.Services.Abstract;

namespace Service;

public class ServiceModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterType<ScanTasksManager>()
            .As<IScanTasksManager>()
            .SingleInstance();
        builder
            .RegisterType<ScanService>()
            .As<IScanService>()
            .SingleInstance();
        builder
            .RegisterType<FileScanner>()
            .As<IFileScanner>()
            .SingleInstance();
        builder
            .RegisterType<SuspiciousContentCheckersProvider>()
            .As<ISuspiciousContentCheckersProvider>()
            .SingleInstance();
    }
}
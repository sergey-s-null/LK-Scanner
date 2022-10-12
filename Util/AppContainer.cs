using Autofac;
using Util.Services;
using Util.Services.Abstract;

namespace Util;

public static class AppContainer
{
    public static IContainer Build()
    {
        var builder = new ContainerBuilder();

        builder
            .RegisterType<CommandExecutor>()
            .As<ICommandExecutor>()
            .SingleInstance();
        builder
            .RegisterType<RemoteScannerService>()
            .As<IRemoteScannerService>()
            .SingleInstance();

        return builder.Build();
    }
}
using Autofac;

namespace Util;

public static class AppContainer
{
    public static IContainer Build()
    {
        var builder = new ContainerBuilder();

        return builder.Build();
    }
}
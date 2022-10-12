using Autofac;
using Util;
using Util.Services.Abstract;

if (args.Length != 2)
{
    Console.WriteLine("Invalid count of arguments. You must specify command and argument.");
    return;
}

var container = AppContainer.Build();

var commandExecutor = container.Resolve<ICommandExecutor>();

await commandExecutor.ExecuteAsync(args[0], args[1]);
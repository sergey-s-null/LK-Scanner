using Autofac;
using Autofac.Builder;
using Autofac.Extensions.DependencyInjection;
using Service;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory(
    ContainerBuildOptions.None,
    x => x.RegisterModule<ServiceModule>())
);

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run("http://localhost:5000");
using FrontServer;
using MockDataSource.Gateways;
using YamatoDaiwa.CSharpExtensions.DataMocking;


FrontServerDependencies.Injector.SetDependencies(
  new FrontServerDependencies
  {
    gateways = new FrontServerDependencies.Gateways
    {
      Person = new PersonMockGateway(),
      Task = new TaskMockGateway()
    }
  }
);

MockGatewayHelper.SetLogger(Console.WriteLine);

WebApplicationBuilder webApplicationBuilder = WebApplication.CreateBuilder(args);


webApplicationBuilder.Services.
    AddControllers().
    AddJsonOptions(
      (Microsoft.AspNetCore.Mvc.JsonOptions options) => options.JsonSerializerOptions.PropertyNamingPolicy = null
    );


WebApplication webApplication = webApplicationBuilder.Build();

webApplication.MapControllers();

webApplication.Run();

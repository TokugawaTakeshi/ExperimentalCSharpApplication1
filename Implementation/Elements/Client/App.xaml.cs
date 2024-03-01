using System.Diagnostics;
using Client.LocalDataBase.Gateways;
using MockDataSource.Gateways;
using YamatoDaiwa.CSharpExtensions.DataMocking;


namespace Client;


public partial class App : Application
{
  public App()
  {

    ClientDependencies.Injector.SetDependencies(
      new ClientDependencies
      {
        gateways = new ClientDependencies.Gateways
        {
          Person = new PersonMockGateway(),
          Task = new TaskEntityFrameworkSQLiteGateway()
          // Task = new TaskMockGateway()
        }
      }
    );
    
    MockGatewayHelper.SetLogger((string message) => { Debug.WriteLine(message); });

    InitializeComponent();

    MainPage = new MainPage();
    
  }
}
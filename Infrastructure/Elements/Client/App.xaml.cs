using MockDataSource.Gateways;


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
          Task = new TaskMockGateway()
        }
      }
    );
    
    // TODO 新規修飾的変形等を追加
    // FrontEndFramework.Components.Badge.Badge.defineNewDecorativeVariations(
    //   Client.Components.SharedReusable.Badge.Badge.DecorativeVariations
    // );
    
    InitializeComponent();

    MainPage = new MainPage();
    
  }
}
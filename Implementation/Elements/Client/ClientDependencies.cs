using CommonSolution.Gateways;


namespace Client;


internal class ClientDependencies {

  public required Gateways gateways { get; init; }

  public struct Gateways {
    public required IPersonGateway Person { get; init; }
    public required TaskGateway Task { get; init; }
  }
  
  public abstract class Injector {

    private static ClientDependencies? _dependencies;

    public static void SetDependencies(ClientDependencies dependencies) {
      _dependencies = dependencies;
    }


    private static ClientDependencies getDependencies() {

      if (_dependencies == null) {
        throw new NullReferenceException("ClientDependenciesは初期化されなかった。");
      }


      return _dependencies;
      
    }

    public static Gateways gateways() {
      return getDependencies().gateways;
    }
    
  }
  
}
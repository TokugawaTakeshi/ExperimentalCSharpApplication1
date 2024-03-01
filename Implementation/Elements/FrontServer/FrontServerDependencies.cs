using CommonSolution.Gateways;


namespace FrontServer;


internal class FrontServerDependencies {

  public required Gateways gateways { get; init; }

  public struct Gateways {
    public required IPersonGateway Person { get; init; }
    public required TaskGateway Task { get; init; }
  }
  
  public abstract class Injector {

    private static FrontServerDependencies? _dependencies;

    public static void SetDependencies(FrontServerDependencies dependencies) {
      _dependencies = dependencies;
    }


    private static FrontServerDependencies getDependencies() {

      if (_dependencies == null) {
        throw new NullReferenceException("FrontServerDependenciesは初期化されなかった。");
      }


      return _dependencies;
      
    }

    public static Gateways gateways() {
      return getDependencies().gateways;
    }
    
  }
  
}
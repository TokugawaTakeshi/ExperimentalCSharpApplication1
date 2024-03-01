using CommonSolution.Entities;
using Task = CommonSolution.Entities.Task;

using MockDataSource.Entities;
using YamatoDaiwa.CSharpExtensions.DataMocking;


namespace MockDataSource.Collections;


internal abstract class TasksCollectionsMocker
{

  public struct Dependencies
  {
    internal Location[] Locations { get; init; }
  }
  
  
  public static IEnumerable<Task> Generate(IEnumerable<Subset> order, Dependencies dependencies)
  {

    List<Task> accumulatingCollection = new();

    foreach (Subset subset in order)
    {
      for (uint itemNumber = 0; itemNumber < subset.Quantity; itemNumber++)
      {
        accumulatingCollection.Add(TaskMocker.Generate(
          preDefines: null,
          new TaskMocker.Dependencies { Locations = dependencies.Locations },
          new TaskMocker.Options
          {
            NullablePropertiesDecisionStrategy = subset.NullablePropertiesDecisionStrategy
          }
        ));
      }
    }

    return accumulatingCollection.ToArray();

  }

  
  public struct Subset
  {
    internal required uint Quantity { get; init; }
    internal required DataMocking.NullablePropertiesDecisionStrategies NullablePropertiesDecisionStrategy { get; init; }
  }
  
}
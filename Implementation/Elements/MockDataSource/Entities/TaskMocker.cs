using CommonSolution.Entities;
using Task = CommonSolution.Entities.Task;

using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;

using YamatoDaiwa.CSharpExtensions.DataMocking;
using Utils;


namespace MockDataSource.Entities;


internal abstract class TaskMocker
{

  public struct PreDefines
  {
    internal string? ID { get; init; }
    internal string? title { get; init; }
    internal string? description { get; init; }
    internal bool? isComplete { get; init; }
    internal List<Task>? subtasks { get; init; }
    internal DateTime? associatedDateTime { get; init; }
    internal DateOnly? associatedDate { get; init; }
    internal Location? associatedLocation { get; init; }
  }

  public struct Dependencies
  {
    internal Location[] Locations { get; init; }
  }
  
  public struct Options
  {
    internal required DataMocking.NullablePropertiesDecisionStrategies NullablePropertiesDecisionStrategy { get; init; }
  }

  
  public static Task Generate(PreDefines? preDefines, Dependencies dependencies, Options options)
  {

    string ID = preDefines?.ID ?? GenerateID();

    
    string title =
        preDefines?.title ??
        RandomizerFactory.GetRandomizer(new FieldOptionsTextWords { Min = 1, Max = 10 }).Generate() + "";
    
    
    string? description = DataMocking.DecideOptionalValue(
      new DataMocking.NullablePropertiesDecisionSourceDataAndOptions<string?>
      {
        PreDefinedValue = preDefines?.description,
        RandomValueGenerator = () => RandomizerFactory.GetRandomizer(new FieldOptionsTextLipsum()).Generate(),
        Strategy = options.NullablePropertiesDecisionStrategy
      }
    );

    
    DateOnly today = DateOnly.FromDateTime(DateTime.UtcNow.Date);
    DateOnly oneYearLater = DateOnly.FromDateTime(DateTime.Today).AddYears(1);

    bool isComplete = preDefines?.isComplete ?? RandomValuesGenerator.GetRandomBoolean();

    
    DateTime? associatedDateTime = null; 
    DateOnly? associatedDate = null;
    
    switch (options.NullablePropertiesDecisionStrategy)
    {

      case DataMocking.NullablePropertiesDecisionStrategies.mustGenerateAll:
      {
        
        associatedDateTime = DataMocking.DecideOptionalValue(
          new DataMocking.NullablePropertiesDecisionSourceDataAndOptions<DateTime?>
          {
            PreDefinedValue = preDefines?.associatedDateTime,
            RandomValueGenerator = () => RandomValuesGenerator.GetRandomDateTime(
              earliestDate: today, latestDate: oneYearLater
            ),
            Strategy = DataMocking.NullablePropertiesDecisionStrategies.
              mustGenerateWith50PercentageProbabilityIfHasNotBeenPreDefined
          }
        );

        if (associatedDateTime is null)
        {
            
          associatedDate = DataMocking.DecideOptionalValue(
            new DataMocking.NullablePropertiesDecisionSourceDataAndOptions<DateOnly?>
            {
              PreDefinedValue = preDefines?.associatedDate,
              RandomValueGenerator = () => RandomValuesGenerator.GetRandomDate(
                earliestDate: today, latestDate: oneYearLater
              ),
              Strategy = DataMocking.NullablePropertiesDecisionStrategies.mustGenerateAll
            }
          );
      
        }
        
        break;
        
      }

      case DataMocking.NullablePropertiesDecisionStrategies.mustGenerateWith50PercentageProbabilityIfHasNotBeenPreDefined:
      {

        bool mustGenerateEitherAssociatedDateTimeOrDateOnly = RandomValuesGenerator.GetRandomBoolean();
        
        associatedDateTime = DataMocking.DecideOptionalValue(
          new DataMocking.NullablePropertiesDecisionSourceDataAndOptions<DateTime?>
          {
            PreDefinedValue = preDefines?.associatedDateTime,
            RandomValueGenerator = () => RandomValuesGenerator.GetRandomDateTime(
              earliestDate: today, latestDate: oneYearLater
            ),
            Strategy = DataMocking.NullablePropertiesDecisionStrategies.
                mustGenerateWith50PercentageProbabilityIfHasNotBeenPreDefined
          }
        );

        if (associatedDateTime is null)
        {
    
          associatedDate = DataMocking.DecideOptionalValue(
            new DataMocking.NullablePropertiesDecisionSourceDataAndOptions<DateOnly?>
            {
              PreDefinedValue = preDefines?.associatedDate,
              RandomValueGenerator = () => RandomValuesGenerator.GetRandomDate(
                earliestDate: today, latestDate: oneYearLater
              ),
              Strategy = mustGenerateEitherAssociatedDateTimeOrDateOnly ? 
                  DataMocking.NullablePropertiesDecisionStrategies.mustGenerateAll :
                  DataMocking.NullablePropertiesDecisionStrategies.
                      mustGenerateWith50PercentageProbabilityIfHasNotBeenPreDefined
            }
          );
      
        }
       
        break;
        
      }


      case DataMocking.NullablePropertiesDecisionStrategies.mustSkipIfHasNotBeenPreDefined:
      {
        
        associatedDateTime = preDefines?.associatedDateTime;
        associatedDate = preDefines?.associatedDate;
        
        break;
        
      }
      
    }
    
    
    Location? associatedLocation = DataMocking.DecideOptionalValue(
      new DataMocking.NullablePropertiesDecisionSourceDataAndOptions<Location?>
      {
        PreDefinedValue = preDefines?.associatedLocation,
        RandomValueGenerator = () => RandomValuesGenerator.GetRandomArrayElement(dependencies.Locations),
        Strategy = options.NullablePropertiesDecisionStrategy
      }
    ); 
    
    
    return new Task
    {
      ID = ID,
      title = title,
      description = description,
      isComplete = isComplete,
      subtasks = preDefines?.subtasks ?? new List<Task>(),
      associatedDateTime = associatedDateTime,
      associatedDate = associatedDate,
      associatedLocation = associatedLocation
    };

  }
  
  
  private static uint counterForID_Generating;

  private static string GenerateID()
  {
    counterForID_Generating++;
    return $"TASK-{counterForID_Generating}__GENERATED";
  }
  
}
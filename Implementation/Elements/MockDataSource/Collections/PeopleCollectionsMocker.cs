using CommonSolution.Entities;
using CommonSolution.Fundamentals;
using MockDataSource.AdditionalStructures;
using MockDataSource.Entities;
using YamatoDaiwa.CSharpExtensions.DataMocking;


namespace MockDataSource.Collections;


internal abstract class PeopleCollectionsMocker
{

  public static IEnumerable<Person> Generate(IEnumerable<Subset> order)
  {

    List<Person> accumulatingCollection = new();

    foreach (Subset subset in order)
    {

      uint itemsQuantity = subset.WithNames is not null ? (uint)subset.WithNames.Length : subset.Quantity;
      
      for (uint itemNumber = 1; itemNumber <= itemsQuantity; itemNumber++)
      {
        
        PersonNameData? name = subset.WithNames?[itemNumber - 1]; 
        
        accumulatingCollection.Add(PersonMocker.Generate(
          preDefines: name is not null ? 
            new PersonMocker.PreDefines
            {
              familyName = name.Value.FamilyName,
              familyNameSpell = name.Value.FamilyNameSpell, 
              givenName = name.Value.GivenName,
              givenNameSpell = name.Value.GivenNameSpell,
              gender = name.Value.IsGivenNameForMales ? Genders.Male : Genders.Female
            } : null,
          new PersonMocker.Options
          {
            FamilyNamePrefix = subset.FamilyNamePrefix,
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
    internal string? FamilyNamePrefix { get; init; }
    internal PersonNameData[]? WithNames { get; init; }
  }
  
}
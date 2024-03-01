using CommonSolution.Entities;
using CommonSolution.Fundamentals;

using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;

using YamatoDaiwa.CSharpExtensions.DataMocking;
using Utils;


namespace MockDataSource.Entities;


internal abstract class PersonMocker
{
  
  public struct PreDefines
  {
    internal string? ID { get; init; }
    internal string? familyName { get; init; }
    internal string? givenName { get; init; }
    internal string? familyNameSpell { get; init; }
    internal string? givenNameSpell { get; init; }
    internal Genders? gender { get; init; }
    internal string? avatarURI { get; init; }
    internal ushort? birthYear { get; init; }
    internal byte? birthMonthNumber__numerationFrom1 { get; init; }
    internal byte? birthDayOfMonth__numerationFrom1 { get; init; }
    internal string? emailAddress { get; init; }
    internal string? phoneNumber;
  }
  
  public struct Options
  {
    internal required DataMocking.NullablePropertiesDecisionStrategies NullablePropertiesDecisionStrategy { get; init; }
    internal string? FamilyNamePrefix { get; init; }
  }
  
  
  public static Person Generate(PreDefines? preDefines, Options options)
  {

    string ID = preDefines?.ID ?? GenerateID();

    string familyName =
        preDefines?.familyName ??
        (options.FamilyNamePrefix ?? "") + RandomizerFactory.GetRandomizer(new FieldOptionsLastName()).Generate();

    string? givenName = DataMocking.DecideOptionalValue(
      new DataMocking.NullablePropertiesDecisionSourceDataAndOptions<string>
      {
        PreDefinedValue = preDefines?.givenName,
        RandomValueGenerator = () => RandomizerFactory.GetRandomizer(new FieldOptionsFirstName()).Generate() + "",
        Strategy = options.NullablePropertiesDecisionStrategy
      }
    ); 
      
    string? familyNameSpell = DataMocking.DecideOptionalValue(
      new DataMocking.NullablePropertiesDecisionSourceDataAndOptions<string>
      {
        PreDefinedValue = preDefines?.familyNameSpell,
        RandomValueGenerator = () => RandomizerFactory.GetRandomizer(new FieldOptionsLastName()).Generate() + "",
        Strategy = options.NullablePropertiesDecisionStrategy
      }
    ); 
    
    string? givenNameSpell = DataMocking.DecideOptionalValue(
      new DataMocking.NullablePropertiesDecisionSourceDataAndOptions<string>
      {
        PreDefinedValue = preDefines?.givenNameSpell,
        RandomValueGenerator = () => RandomizerFactory.GetRandomizer(new FieldOptionsFirstName()).Generate() + "",
        Strategy = options.NullablePropertiesDecisionStrategy
      }
    ); 

    Genders? gender = DataMocking.DecideOptionalValue(
      new DataMocking.NullablePropertiesDecisionSourceDataAndOptions<Genders?>
      {
        PreDefinedValue = preDefines?.gender,
        RandomValueGenerator = () => RandomValuesGenerator.GetRandomBoolean() ? Genders.Male : Genders.Female,
        Strategy = options.NullablePropertiesDecisionStrategy
      }
    );
    
    string? avatarURI = DataMocking.DecideOptionalValue(
      new DataMocking.NullablePropertiesDecisionSourceDataAndOptions<string>
      {
        PreDefinedValue = preDefines?.avatarURI,
        RandomValueGenerator = () => gender == Genders.Male ? 
            "https://images.pexels.com/photos/2379004/pexels-photo-2379004.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500" : 
            "https://img.freepik.com/free-photo/young-beautiful-woman-pink-warm-sweater-natural-look-smiling-portrait-isolated-long-hair_285396-896.jpg",
        Strategy = options.NullablePropertiesDecisionStrategy
      }
    );

    ushort? birthYear = DataMocking.DecideOptionalValue(
      new DataMocking.NullablePropertiesDecisionSourceDataAndOptions<ushort?>
      {
        PreDefinedValue = preDefines?.birthYear,
        RandomValueGenerator = () => RandomValuesGenerator.GetRandomUShort(
          minimalValue: Person.BirthYear.MINIMAL_VALUE,
          maximalValue: Person.BirthYear.MAXIMAL_VALUE
        ),
        Strategy = options.NullablePropertiesDecisionStrategy
      }
    );
    
    byte? birthMonthNumber__numerationFrom1 = DataMocking.DecideOptionalValue(
      new DataMocking.NullablePropertiesDecisionSourceDataAndOptions<byte?>
      {
        PreDefinedValue = preDefines?.birthMonthNumber__numerationFrom1,
        RandomValueGenerator = () => RandomValuesGenerator.GetRandomByte(
          minimalValue: Person.BirthMonthNumber__NumerationFrom1.MINIMAL_VALUE,
          maximalValue: Person.BirthMonthNumber__NumerationFrom1.MAXIMAL_VALUE
        ),
        Strategy = options.NullablePropertiesDecisionStrategy
      }
    );

    byte? birthDayOfMonth__numerationFrom1 = null;

    if (birthMonthNumber__numerationFrom1 is not null)
    {
      birthDayOfMonth__numerationFrom1 =
        preDefines?.birthDayOfMonth__numerationFrom1 ??
        RandomValuesGenerator.GetRandomByte(
          minimalValue: Person.BirthDayOfMonth__NumerationFrom1.MINIMAL_VALUE,
          maximalValue: Person.BirthDayOfMonth__NumerationFrom1.MAXIMAL_VALUE
        );
    }
    
    string? emailAddress = DataMocking.DecideOptionalValue(
      new DataMocking.NullablePropertiesDecisionSourceDataAndOptions<string?>
      {
        PreDefinedValue = preDefines?.emailAddress,
        RandomValueGenerator = RandomValuesGenerator.GetRandomEmailAddress,
        Strategy = options.NullablePropertiesDecisionStrategy,
      }
    );

    string? phoneNumber = DataMocking.DecideOptionalValue(
      new DataMocking.NullablePropertiesDecisionSourceDataAndOptions<string?>
      {
        PreDefinedValue = preDefines?.phoneNumber,
        RandomValueGenerator = RandomValuesGenerator.GenerateRandomJapanesePhoneNumber,
        Strategy = options.NullablePropertiesDecisionStrategy
      }
    );
    
    return new Person
    {
      ID = ID,
      familyName = familyName,
      givenName = givenName,
      familyNameSpell = familyNameSpell,
      givenNameSpell = givenNameSpell,
      gender = gender,
      birthYear = birthYear,
      birthMonthNumber__numerationFrom1 = birthMonthNumber__numerationFrom1,
      birthDayOfMonth__numerationFrom1 = birthDayOfMonth__numerationFrom1,
      emailAddress = emailAddress,
      phoneNumber__digitsOnly = phoneNumber
    };
    
  }

  
  private static uint counterForID_Generating;

  private static string GenerateID()
  {
    counterForID_Generating++;
    return $"PERSON-{ counterForID_Generating }__GENERATED";
  }
  
}

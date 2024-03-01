namespace MockDataSource.AdditionalStructures;


public struct PersonNameData
{
  public required string FamilyName { get; init; }
  public required string FamilyNameSpell { get; init; }
  public required string GivenName { get; init; }
  public required string GivenNameSpell { get; init; }
  public required bool IsGivenNameForMales { get; init;  }
}
using CommonSolution.Fundamentals;
using Person = CommonSolution.Entities.Person;


namespace CommonSolution.Gateways;


public interface IPersonGateway
{

  /* ━━━ 取得 ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  Task<Person[]> RetrieveAll();
  
  Task<SelectionRetrieving.ResponseData> RetrieveSelection(SelectionRetrieving.RequestParameters? requestParameters);
  
  public abstract class SelectionRetrieving
  {
    
    public struct RequestParameters
    {
      public string? SearchingByFullOrPartialNameOrItsSpell { get; init; }
    }

    public struct ResponseData
    {
      public required uint TotalItemsCount;
      public required uint TotalItemsCountInSelection;
      public required Person[] Items;
    }
    
  }

  
  /* ━━━ 追加 ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  Task<Adding.ResponseData> Add(Adding.RequestData requestData);

  public abstract class Adding
  {
    
    public struct RequestData
    {
      public required string FamilyName { get; init; }
      public string? GivenName { get; init; }
      public string? FamilyNameSpell { get; init; }
      public string? GivenNameSpell { get; init; }
      public Genders? Gender { get; init; }
      public string? AvatarURI { get; init; }
      public ushort? BirthYear { get; set; }
      public byte? BirthMonthNumber__NumerationFrom1 { get; set; }
      public byte? BirthDayOfMonth__NumerationFrom1 { get; set; }
      public string? EmailAddress { get; init; }
      public string? PhoneNumber { get; init; }
    }

    public struct ResponseData
    {
      public required string AddedPersonID { get; init; }
    }
    
  }
  
  
  /* ━━━ 更新 ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  Task Update(Updating.RequestData requestData);

  public abstract class Updating
  {
    public struct RequestData
    {
      public required string ID { get; set; }
      public required string FamilyName { get; init; }
      public string? GivenName { get; init; }
      public string? FamilyNameSpell { get; init; }
      public string? GivenNameSpell { get; init; }
      public Genders? Gender { get; init; }
      public string? AvatarURI { get; init; }
      public ushort? BirthYear { get; set; }
      public byte? BirthMonthNumber__NumerationFrom1 { get; set; }
      public byte? BirthDayOfMonth__NumerationFrom1 { get; set; }
      public string? EmailAddress { get; init; }
      public string? PhoneNumber { get; init; }
    }
  }
  
  
  /* ━━━ 削除 ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  Task Delete(string targetPersonID);
  
}

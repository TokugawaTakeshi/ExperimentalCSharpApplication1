using CommonSolution.Fundamentals;


namespace CommonSolution.Entities;


public class Person
{

  public required string ID { get; init; }

  
  /* ━━━ Name ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  public required string familyName { get; set; }

  public abstract class FamilyName
  {
    public const bool IS_REQUIRED = true;
    public const byte MINIMAL_CHARACTERS_COUNT = 1;
    public const byte MAXIMAL_CHARACTERS_COUNT = Byte.MaxValue;
  }


  public string? givenName { get; set; }

  public abstract class GivenName
  {
    public const bool IS_REQUIRED = false;
    public const byte MINIMAL_CHARACTERS_COUNT = 1;
    public const byte MAXIMAL_CHARACTERS_COUNT = Byte.MaxValue;
  }

  
  public string fullName => this.familyName + (this.givenName ?? "");
  
  
  public string? familyNameSpell { get; set; }
  
  public abstract class FamilyNameSpell
  {
    public const bool IS_REQUIRED = false;
    public const byte MINIMAL_CHARACTERS_COUNT = 1;
    public const byte MAXIMAL_CHARACTERS_COUNT = Byte.MaxValue;
  }
  
  
  public string? givenNameSpell { get; set; }
  
  public abstract class GivenNameSpell
  {
    public const bool IS_REQUIRED = false;
    public const byte MINIMAL_CHARACTERS_COUNT = 1;
    public const byte MAXIMAL_CHARACTERS_COUNT = Byte.MaxValue;
  }


  public string? fullNameSpell
  {
    get
    {

      if (this.familyNameSpell is not null && this.givenNameSpell is not null)
      {
        return this.familyNameSpell + this.givenNameSpell;
      } 
      
      
      if (this.familyNameSpell is null && this.givenNameSpell is null)
      {
        return null;
      }


      return (this.familyNameSpell ?? "") + (this.givenNameSpell ?? "");

    }
  }
  
  
  /* ━━━ Gender ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  public Genders? gender { get; set; }

  public abstract class Gender
  {
    public const bool IS_REQUIRED = false;
  }


  /* ━━━ アバター ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  public string? avatarURI { get; set; }

  public abstract class AvatarURI
  {
    public const bool IS_REQUIRED = false;
    public const byte MINIMAL_CHARACTERS_COUNT = 10;
  }

  
  /* ━━━ 誕生 ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  /* 【 理論 】 生年を知らなくても、生月日を知っている事が多いが、年齢を算出するには年迄知る事が必要。 */
  public ushort? birthYear { get; set; }

  public abstract class BirthYear
  {
    public const bool IS_REQUIRED = false;
    public const ushort MINIMAL_VALUE = 1900;
    public static readonly ushort MAXIMAL_VALUE = (ushort) DateTime.Now.Year;
  }

  
  public byte? birthMonthNumber__numerationFrom1 { get; set; }

  public abstract class BirthMonthNumber__NumerationFrom1
  {
    public const bool IS_REQUIRED = false;
    public const byte MINIMAL_VALUE = 1;
    public const byte MAXIMAL_VALUE = 12;
  }

  
  public byte? birthDayOfMonth__numerationFrom1 { get; set; }
  
  public abstract class BirthDayOfMonth__NumerationFrom1
  {
    public const bool IS_REQUIRED = false;
    public const byte MINIMAL_VALUE = 1;
    public const byte MAXIMAL_VALUE = 31;
  }

  
  public DateOnly? birthDate =>
    this.birthYear is not null &&
    this.birthMonthNumber__numerationFrom1 is not null &&
    this.birthDayOfMonth__numerationFrom1 is not null ? 
        new DateOnly(
          (int)this.birthYear,
          (int)this.birthMonthNumber__numerationFrom1,
          (int)this.birthDayOfMonth__numerationFrom1
        )
        : null;

  
  public byte? age
  {
    get
    {

      if (this.birthDate is null)
      {
        return null;
      }
      
      
      DateOnly currentDate = DateOnly.FromDateTime(DateTime.UtcNow.Date);
      DateOnly birthDate = this.birthDate.Value;

      byte age = (byte) (currentDate.Year - this.birthDate.Value.Year);

      if (currentDate < birthDate.AddYears(age))
      {
        age--;
      }

      return age;

    }
  }

  
  /* ━━━ メールアドレス ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  public string? emailAddress { get; set; }
  
  public abstract class EmailAddress
  {
    public const bool IS_REQUIRED = false;
    public const ushort MAXIMAL_CHARACTERS_COUNT = 320;
  }


  /* ━━━ 電話番号 ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  public string? phoneNumber__digitsOnly { get; set; }
  
  public abstract class PhoneNumber__DigitsOnly
  {
    public const bool IS_REQUIRED = false;
  }
  
  
  /* ━━━ 其の他 ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  public Person clone()
  {
    return new Person
    {
      ID = this.ID,
      familyName = this.familyName,
      givenName = this.givenName,
      familyNameSpell = this.familyNameSpell,
      givenNameSpell = this.givenNameSpell,
      gender = this.gender,
      avatarURI = this.avatarURI,
      birthYear = this.birthYear,
      birthMonthNumber__numerationFrom1 = this.birthMonthNumber__numerationFrom1,
      birthDayOfMonth__numerationFrom1 = this.birthDayOfMonth__numerationFrom1,
      emailAddress = this.emailAddress,
      phoneNumber__digitsOnly = this.phoneNumber__digitsOnly
    };
  }

}
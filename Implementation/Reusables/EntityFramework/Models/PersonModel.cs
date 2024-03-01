using CommonSolution.Fundamentals;
using Microsoft.EntityFrameworkCore;


namespace EntityFramework.Models;

// TODO 要らないと確認したら削除
// [System.ComponentModel.DataAnnotations.Schema.Table(PersonModel.TABLE_NAME)]
[Microsoft.EntityFrameworkCore.EntityTypeConfiguration(typeof(PersonModel.Configuration))]
public class PersonModel
{

  /* [ Theory ] `Guid.NewGuid()` return the string of 36 characters. See https://stackoverflow.com/a/4458925/4818123 */
  [System.ComponentModel.DataAnnotations.Key]
  [System.ComponentModel.DataAnnotations.MaxLength(36)]
  public string ID { get; set; } = Guid.NewGuid().ToString();
  
  
  /* ━━━ Name ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  [System.ComponentModel.DataAnnotations.MaxLength(CommonSolution.Entities.Person.FamilyName.MAXIMAL_CHARACTERS_COUNT)]
  public string FamilyName { get; set; } = null!;
  
  [System.ComponentModel.DataAnnotations.MaxLength(CommonSolution.Entities.Person.GivenName.MAXIMAL_CHARACTERS_COUNT)]
  public string GivenName { get; set; } = null!;
  
  [System.ComponentModel.DataAnnotations.MaxLength(CommonSolution.Entities.Person.FamilyNameSpell.MAXIMAL_CHARACTERS_COUNT)]
  public string FamilyNameSpell { get; set; } = null!;
  
  [System.ComponentModel.DataAnnotations.MaxLength(CommonSolution.Entities.Person.GivenNameSpell.MAXIMAL_CHARACTERS_COUNT)]
  public string GivenNameSpell { get; set; } = null!;
  
  
  /* ━━━ Gender ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  public Genders? Gender { get; set; }
  
  
  /* ━━━ Birth Date / Age ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  public ushort? BirthYear { get; set; } = null!;
  
  [
    System.ComponentModel.DataAnnotations.Range(
      CommonSolution.Entities.Person.BirthMonthNumber__NumerationFrom1.MINIMAL_VALUE,
      CommonSolution.Entities.Person.BirthMonthNumber__NumerationFrom1.MAXIMAL_VALUE
    )
  ]
  public byte? BirthMonthNumber__NumerationFrom1 { get; set; } = null!;
  
  [
    System.ComponentModel.DataAnnotations.Range(
      CommonSolution.Entities.Person.BirthDayOfMonth__NumerationFrom1.MINIMAL_VALUE,
      CommonSolution.Entities.Person.BirthDayOfMonth__NumerationFrom1.MAXIMAL_VALUE
    )
  ]
  public byte? BirthDayOfMonth__NumerationFrom1 { get; set; } = null!;
  
  
  /* ━━━ Email Address ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  [System.ComponentModel.DataAnnotations.MaxLength(CommonSolution.Entities.Person.EmailAddress.MAXIMAL_CHARACTERS_COUNT)]
  public string EmailAddress { get; set; } = null!;
  
  public string PhoneNumber__DigitsOnly { get; set; } = null!;


  public class Configuration : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<PersonModel>
  {
    
    private const string TABLE_NAME = "People";
    
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<PersonModel> builder)
    {

      builder.ToTable(Configuration.TABLE_NAME);

      builder.
          Property(personModel => personModel.FamilyName).
          IsRequired(CommonSolution.Entities.Person.FamilyName.IS_REQUIRED);

      builder.
          Property(personModel => personModel.GivenName).
          IsRequired(CommonSolution.Entities.Person.GivenName.IS_REQUIRED);
      
      builder.
          Property(personModel => personModel.FamilyNameSpell).
          IsRequired(CommonSolution.Entities.Person.FamilyNameSpell.IS_REQUIRED);

      builder.
          Property(personModel => personModel.GivenNameSpell).
          IsRequired(CommonSolution.Entities.Person.GivenNameSpell.IS_REQUIRED);
      
      builder.
          Property(personModel => personModel.Gender).
          IsRequired(CommonSolution.Entities.Person.Gender.IS_REQUIRED);
      
      builder.
          Property(personModel => personModel.BirthYear).
          IsRequired(CommonSolution.Entities.Person.BirthYear.IS_REQUIRED);
      
      builder.
          Property(personModel => personModel.BirthMonthNumber__NumerationFrom1).
          IsRequired(CommonSolution.Entities.Person.BirthMonthNumber__NumerationFrom1.IS_REQUIRED);
      
      builder.
          Property(personModel => personModel.BirthDayOfMonth__NumerationFrom1).
          IsRequired(CommonSolution.Entities.Person.BirthDayOfMonth__NumerationFrom1.IS_REQUIRED);

      builder.
          Property(personModel => personModel.EmailAddress).
          IsRequired(CommonSolution.Entities.Person.EmailAddress.IS_REQUIRED);
      
      builder.
          Property(personModel => personModel.PhoneNumber__DigitsOnly).
          IsRequired(CommonSolution.Entities.Person.PhoneNumber__DigitsOnly.IS_REQUIRED);

    }
  }

}
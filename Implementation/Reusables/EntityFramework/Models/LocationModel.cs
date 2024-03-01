using Microsoft.EntityFrameworkCore;


namespace EntityFramework.Models;

[System.ComponentModel.DataAnnotations.Schema.Table("Locations")]

// [System.ComponentModel.DataAnnotations.Schema.Table("Locations")]
public class LocationModel
{
  
  /* [ Theory ] `Guid.NewGuid()` return the string of 36 characters. See https://stackoverflow.com/a/4458925/4818123 */
  [System.ComponentModel.DataAnnotations.Key]
  [System.ComponentModel.DataAnnotations.MaxLength(36)]
  public string ID { get; set; } = Guid.NewGuid().ToString();
  
  [System.ComponentModel.DataAnnotations.MinLength(CommonSolution.Entities.Location.LocalizedName.MINIMAL_CHARACTERS_COUNT)]
  public string LocalizedName { get; set; } = null!;
 
  [System.ComponentModel.DataAnnotations.Required]
  [
    System.ComponentModel.DataAnnotations.Range(
      CommonSolution.Entities.Location.Latitude.MINIMAL_VALUE, 
      CommonSolution.Entities.Location.Latitude.MAXIMAL_VALUE
    )
  ]
  public double Latitude { get; set; } = default!;
  
  [
    System.ComponentModel.DataAnnotations.Range(
      CommonSolution.Entities.Location.Longitude.MINIMAL_VALUE, 
      CommonSolution.Entities.Location.Longitude.MAXIMAL_VALUE
    )
  ]
  public double Longitude { get; set; } = default!;
  
  public CommonSolution.Entities.Location.Categories Category { get; set; } = default!;
  
  
  public class Configuration : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<LocationModel>
  {

    private const string TABLE_NAME = "Locatioins";
    
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<LocationModel> builder)
    {

      builder.ToTable(Configuration.TABLE_NAME);

      builder.
          Property(taskModel => taskModel.LocalizedName).
          IsRequired(CommonSolution.Entities.Location.LocalizedName.IS_REQUIRED);
      
      builder.
          Property(taskModel => taskModel.Latitude).
          IsRequired(CommonSolution.Entities.Location.Latitude.IS_REQUIRED);
      
      builder.
          Property(taskModel => taskModel.Longitude).
          IsRequired(CommonSolution.Entities.Location.Longitude.IS_REQUIRED);
      
      builder.
          Property(taskModel => taskModel.Category).
          IsRequired(CommonSolution.Entities.Location.Category.IS_REQUIRED);

    }
 
  }
    
}
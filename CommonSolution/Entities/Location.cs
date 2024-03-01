namespace CommonSolution.Entities;


public class Location
{
  
  public required string ID { get; init; }
  
  
  public required string localizedName { get; set; }
  
  public abstract class LocalizedName
  {
    public const bool IS_REQUIRED = true;
    public const byte MINIMAL_CHARACTERS_COUNT = 1;
  }
  
  
  public required double latitude { get; set; }
  public abstract class Latitude
  {
    public const bool IS_REQUIRED = true;
    public const double MINIMAL_VALUE = -90;
    public const double MAXIMAL_VALUE = 90;
  }
  
  
  public required double longitude { get; set; }
  public abstract class Longitude
  {
    public const bool IS_REQUIRED = true;
    public const double MINIMAL_VALUE = -180;
    public const double MAXIMAL_VALUE = 180;
  }

  
  public Categories category { get; set; } 
  
  public abstract class Category
  {
    public const bool IS_REQUIRED = true;
  }
  
  public enum Categories
  {
    RailwayStation,
    MedicalFacility,
    Mall,
    Office,
    ApartmentHouse
  }
  
}
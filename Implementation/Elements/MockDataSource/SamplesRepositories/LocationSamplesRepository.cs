using CommonSolution.Entities;

namespace MockDataSource.SamplesRepositories;


public class LocationSamplesRepository
{

  public static Location[] Locactions => LocationSamplesRepository.RailwayStations.
      Concat(LocationSamplesRepository.MedicalFacilities).
      ToArray();


  public static Location[] RailwayStations => new[]
  {
    new Location
    {
      ID = LocationSamplesRepository.GenerateID(),
      localizedName = "品川駅",
      latitude = 35.62858992904989,
      longitude = 139.7387655351599,
      category = Location.Categories.RailwayStation
    },
    new Location {
      ID = LocationSamplesRepository.GenerateID(),
      localizedName = "新宿",
      latitude = 35.689874643347416,
      longitude = 139.70055927939123,
      category = Location.Categories.RailwayStation
    },
    new Location {
      ID = LocationSamplesRepository.GenerateID(),
      localizedName = "渋谷",
      latitude = 35.6581731588672,
      longitude = 139.70185782659462,
      category = Location.Categories.RailwayStation
    }
  };
  
  public static Location[] MedicalFacilities => new[]
  {
    new Location
    {
      ID = LocationSamplesRepository.GenerateID(),
      localizedName = "JCHO東京高輪病院", 
      latitude = 35.63126696439641,
      longitude = 139.73224789220157,
      category = Location.Categories.MedicalFacility
    },
    new Location {
      ID = LocationSamplesRepository.GenerateID(),
      localizedName = "東京医療センター",
      latitude = 35.62632644237783, 
      longitude = 139.66722613882337,
      category = Location.Categories.MedicalFacility
    },
    new Location {
      ID = LocationSamplesRepository.GenerateID(),
      localizedName = "ＪＲ東京総合病院",
      latitude = 35.68561973571344, 
      longitude = 139.69986084671515,
      category = Location.Categories.MedicalFacility
    }
  };
  
  
  private static uint counterForID_Generating;

  private static string GenerateID()
  {
    counterForID_Generating++;
    return $"LOCATION-{ counterForID_Generating }__LOCATION_SAMPLES_REPOSITORY";
  }

}
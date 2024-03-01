using Client.LocalDataBase.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;


namespace Client.LocalDataBase;


public class LocalDatabaseContext : DbContext
{

  private static LocalDatabaseContext? selfSoleInstance = null;
  
  public static LocalDatabaseContext GetInstance()
  {
    return selfSoleInstance ?? (LocalDatabaseContext.selfSoleInstance = new LocalDatabaseContext());
  }
  
  
  public DbSet<TaskModel> TasksModels { get; internal set; } = null!;
  public DbSet<LocationModel> LocationModels { get; internal set; } = null!;
  public DbSet<PersonModel> PeopleModels { get; internal set; } = null!; 

  
  protected LocalDatabaseContext()
  {
    base.Database.EnsureDeleted();
    base.Database.EnsureCreated();
  }
  

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    
    base.OnConfiguring(optionsBuilder);

    string stringifiedConnectionData = new SqliteConnectionStringBuilder
    {
      /* 〔 実例 〕 C:\Users\Takeshi Tokugawa\AppData\Local\Packages\2302D388(中略)yvjzm\LocalState\TestTest.sq3 */
      DataSource = Path.Combine(FileSystem.AppDataDirectory, "LocalDatabase.sq3")
    }.ToString();
    
    optionsBuilder.UseSqlite(new SqliteConnection(stringifiedConnectionData));
    
  }
  
}

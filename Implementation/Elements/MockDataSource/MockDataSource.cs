using CommonSolution.Entities;
using CommonSolution.Gateways;

using MockDataSource.Entities;
using MockDataSource.Collections;
using MockDataSource.SamplesRepositories;
using MockDataSource.AdditionalStructures;

using System.Diagnostics;
using YamatoDaiwa.CSharpExtensions;
using YamatoDaiwa.CSharpExtensions.DataMocking;


namespace MockDataSource;


public class MockDataSource
{

  /* ━━━ Data ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  public readonly List<Person> People;
  public readonly List<CommonSolution.Entities.Task> Tasks;
  public readonly List<Location> Locations;


  /* ━━━ Initialization ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  private static MockDataSource? selfSoleInstance;

  public static MockDataSource GetInstance()
  {

    if (MockDataSource.selfSoleInstance == null)
    {
      MockDataSource.selfSoleInstance = new MockDataSource();
      Debug.WriteLine("Mock data source has been initialized.");
    }

    return selfSoleInstance;
    
  }
  
  private MockDataSource()
  {
    
    this.People = PeopleCollectionsMocker.Generate(new PeopleCollectionsMocker.Subset[] {
      new ()
      {
        NullablePropertiesDecisionStrategy = DataMocking.NullablePropertiesDecisionStrategies.
            mustGenerateWith50PercentageProbabilityIfHasNotBeenPreDefined,
        WithNames = PeopleNamesRepository.PeopleNames,
        Quantity = (uint)PeopleNamesRepository.PeopleNames.Length
      },
      new()
      {
        NullablePropertiesDecisionStrategy = DataMocking.NullablePropertiesDecisionStrategies.mustGenerateAll, 
        Quantity = 5
      },
      new()
      {
        NullablePropertiesDecisionStrategy = DataMocking.NullablePropertiesDecisionStrategies.
            mustGenerateWith50PercentageProbabilityIfHasNotBeenPreDefined,
        Quantity = 5
      },
      new()
      {
        FamilyNamePrefix = "SEARCH_TEST-", 
        NullablePropertiesDecisionStrategy = DataMocking.NullablePropertiesDecisionStrategies.
            mustGenerateWith50PercentageProbabilityIfHasNotBeenPreDefined,
        Quantity = 5
      },
      new ()
      {
        NullablePropertiesDecisionStrategy = DataMocking.NullablePropertiesDecisionStrategies.
            mustGenerateWith50PercentageProbabilityIfHasNotBeenPreDefined,
        WithNames = new []
        {
          new PersonNameData
          {
            FamilyName = "OverflowTest:ÀÇĤfhjgpjklbĜiEstosTreMalfacileEnvolverLaVicoAbcdefghijklmnopqrstuvwxyza",
            FamilyNameSpell = "OverflowTest:ÀÇĤfhjgpjklbĜiEstosTreMalfacileEnvolverLaVicoAbcdefghijklmnopqrstuvwxyza",
            GivenName = "OverflowTest:ÀÇĤfhjgpjklbĜiEstosTreMalfacileEnvolverLaVicoAbcdefghijklmnopqrstuvwxyza",
            GivenNameSpell = "OverflowTest:ÀÇĤfhjgpjklbĜiEstosTreMalfacileEnvolverLaVicoAbcdefghijklmnopqrstuvwxyza",
            IsGivenNameForMales = true
          }
        },
        Quantity = 1
      }
    }).ToList();

    this.Locations = LocationSamplesRepository.Locactions.ToList();
    
    this.Tasks = TasksCollectionsMocker.Generate(
      new TasksCollectionsMocker.Subset[] {
        new()
        {
          NullablePropertiesDecisionStrategy = DataMocking.NullablePropertiesDecisionStrategies.mustGenerateAll,
          Quantity = 10
        },
        new()
        {
          NullablePropertiesDecisionStrategy = DataMocking.NullablePropertiesDecisionStrategies.mustGenerateWith50PercentageProbabilityIfHasNotBeenPreDefined,
          Quantity = 5
        }
      },
      new TasksCollectionsMocker.Dependencies
      {
        Locations = this.Locations.ToArray()
      }
    ).ToList();

  }


  /* ━━━ 人 ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  public Person[] RetrieveAllPeople()
  {
    return People.Select(person => person.clone()).ToArray();
  }

  public IPersonGateway.Adding.ResponseData AddPerson(IPersonGateway.Adding.RequestData requestData)
  {

    Person newPerson = PersonMocker.Generate(
      new PersonMocker.PreDefines 
      {
        familyName = requestData.FamilyName,
        givenName = requestData.GivenName,
        familyNameSpell = requestData.FamilyNameSpell,
        givenNameSpell = requestData.GivenNameSpell,
        gender = requestData.Gender,
        avatarURI = requestData.AvatarURI,
        birthYear = requestData.BirthYear, 
        birthMonthNumber__numerationFrom1 = requestData.BirthMonthNumber__NumerationFrom1,
        birthDayOfMonth__numerationFrom1 = requestData.BirthDayOfMonth__NumerationFrom1,
        emailAddress = requestData.EmailAddress,
        phoneNumber = requestData.PhoneNumber
      },
      new PersonMocker.Options
      {
        NullablePropertiesDecisionStrategy = DataMocking.NullablePropertiesDecisionStrategies.mustSkipIfHasNotBeenPreDefined
      }
    );

    People.Insert(0, newPerson);

    return new IPersonGateway.Adding.ResponseData { AddedPersonID = newPerson.ID };
    
  }

  public void UpdatePerson(IPersonGateway.Updating.RequestData requestData)
  {

    Person? targetPerson = People.Find(person => person.ID == requestData.ID);

    if (targetPerson == null)
    {
      throw new InvalidDataException($"ID「{ requestData.ID }」の人が発見されず。");
    }
    
    
    targetPerson.familyName = requestData.FamilyName;
    targetPerson.givenName = requestData.GivenName;
    targetPerson.familyNameSpell = requestData.FamilyNameSpell;
    targetPerson.givenNameSpell = requestData.GivenNameSpell;
    targetPerson.gender = requestData.Gender;
    targetPerson.birthYear = requestData.BirthYear;
    targetPerson.birthMonthNumber__numerationFrom1 = requestData.BirthMonthNumber__NumerationFrom1;
    targetPerson.birthDayOfMonth__numerationFrom1 = requestData.BirthDayOfMonth__NumerationFrom1;
    targetPerson.emailAddress = requestData.EmailAddress;
    targetPerson.phoneNumber__digitsOnly = requestData.PhoneNumber;
    
  }

  public void DeletePerson(string targetPersonID)
  {
    People.RemoveAll(person => person.ID == targetPersonID);
  }
  
  
  /* ━━━ 課題 ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  public CommonSolution.Entities.Task[] RetrieveAllTasks()
  {
    return Tasks.Select(task => task.clone()).ToArray();
  }

  public TaskGateway.Adding.ResponseData AddTask(TaskGateway.Adding.RequestData requestData)
  {

    CommonSolution.Entities.Task newTask = TaskMocker.Generate(
      new TaskMocker.PreDefines
      {
        title = requestData.Title,
        description = requestData.Description,
        isComplete = requestData.IsComplete,
        subtasks = requestData.SubtasksIDs == null ? 
          null : Tasks.FindAll(task => requestData.SubtasksIDs.Any(task.ID.Contains)),
        associatedDateTime = requestData.AssociatedDate__ISO8601 is not null ? 
            DateTimeExtensions.CreateDateTimeFromISO8601_String(requestData.AssociatedDate__ISO8601) : null,
        associatedDate = requestData.AssociatedDate__ISO8601 is not null ?
            DateOnlyExtensions.CreateDateOnlyFromISO8601_String(requestData.AssociatedDate__ISO8601) : null,
        associatedLocation = requestData.AssociatedLocation
      },
      new TaskMocker.Dependencies
      {
        Locations = this.Locations.ToArray()
      },
      new TaskMocker.Options
      {
        NullablePropertiesDecisionStrategy =
          DataMocking.NullablePropertiesDecisionStrategies.mustSkipIfHasNotBeenPreDefined
      }
    );
    
    Tasks.Insert(0, newTask);

    return new TaskGateway.Adding.ResponseData() { AddedTaskID = newTask.ID };

  }

  public void UpdateTask(TaskGateway.Updating.RequestData requestData)
  {

    CommonSolution.Entities.Task? targetTask = Tasks.Find(task => task.ID == requestData.ID);

    if (targetTask == null)
    {
      throw new InvalidDataException($"ID「{ requestData.ID }」の課題が発見されず。");
    }

    targetTask.title = requestData.Title;
    targetTask.description = requestData.Description;
    targetTask.isComplete = requestData.IsComplete;
    
    if (requestData.SubtasksIDs != null)
    {
      targetTask.subtasks = Tasks.FindAll(task => requestData.SubtasksIDs.Any(task.ID.Contains));
    }

    targetTask.associatedDateTime = targetTask.associatedDateTime;
    targetTask.associatedDate = targetTask.associatedDate;

    targetTask.associatedLocation = targetTask.associatedLocation;

  }

  public void DeleteTask(string targetTaskID)
  {
    Tasks.RemoveAll(tasks => tasks.ID == targetTaskID);
  }

}
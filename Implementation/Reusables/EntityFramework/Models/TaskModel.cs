using Microsoft.EntityFrameworkCore;


namespace EntityFramework.Models;

// TODO 要らないと確認したら削除
// [System.ComponentModel.DataAnnotations.Schema.Table("Tasks")]
[Microsoft.EntityFrameworkCore.EntityTypeConfiguration(typeof(TaskModel.Configuration))]
public class TaskModel
{

  /* [ Theory ] `Guid.NewGuid()` return the string of 36 characters. See https://stackoverflow.com/a/4458925/4818123 */
  [System.ComponentModel.DataAnnotations.Key]
  [System.ComponentModel.DataAnnotations.MaxLength(36)]
  public string ID { get; set; } = Guid.NewGuid().ToString();

  [System.ComponentModel.DataAnnotations.MaxLength(CommonSolution.Entities.Task.Title.MAXIMAL_CHARACTERS_COUNT)]
  public string Title { get; set; } = null!;

  public string? Description { get; set; }

  public bool IsComplete { get; set; }

  /* ━━━ < TODO Next versions (GUI is aspirational) ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  // [System.ComponentModel.DataAnnotations.Required]
  // public List<TaskModel> Subtasks { get; set; } = null!;
  
  // public DateTime? AssociatedDateTime { get; set; }
  // public DateOnly? AssociatedDate { get; set; }
  
  // public Location? Location { get; set; }
  /* ━━━ TODO > ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  
  
  public CommonSolution.Entities.Task ToEntity()
  {
    return new CommonSolution.Entities.Task
    {
      ID = this.ID,
      title = this.Title,
      description = this.Description,
      isComplete = this.IsComplete
    };
  }


  public class Configuration : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<TaskModel>
  {

    private const string TABLE_NAME = "Tasks";
    
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TaskModel> builder)
    {

      builder.ToTable(Configuration.TABLE_NAME);

      builder.
          Property(taskModel => taskModel.Title).
          IsRequired(CommonSolution.Entities.Task.Title.IS_REQUIRED);
      
      builder.
          Property(taskModel => taskModel.Description).
          IsRequired(CommonSolution.Entities.Task.Description.IS_REQUIRED);
      
      builder.
          Property(taskModel => taskModel.IsComplete).
          IsRequired(CommonSolution.Entities.Task.IsComplete.IS_REQUIRED);

    }
    
  }

}
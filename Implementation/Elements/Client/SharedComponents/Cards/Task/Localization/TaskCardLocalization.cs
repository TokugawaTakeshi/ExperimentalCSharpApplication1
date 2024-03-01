namespace Client.SharedComponents.Cards.Task.Localization;


internal abstract record TaskCardLocalization
{

  internal record Badges
  {
    internal required string date { get; init; }
    internal required string dateTime { get; init; }
  }
  
  internal abstract Badges badges { get; init; }
  
}

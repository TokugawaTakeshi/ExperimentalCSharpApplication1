namespace Client.SharedComponents.Cards.Task.Localization;


internal record TaskCardEnglishLocalization : TaskCardLocalization
{

  internal override Badges badges { get; init; } = new()
  {
    date = "Date",
    dateTime = "Date and Time"
  };

}

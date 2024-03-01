namespace Client.Resources.Localizations;


internal class SharedStaticEnglishStrings: SharedStaticStrings
{

  internal override string buildDataRetrievingOrSubmittingFailedPoliteMessage(string dynamicPart)
  {
    return "We sincerely apologize for the inconvenience, but it seems a challenge has arisen in processing your request. " +
        $"{ dynamicPart } " +
        "Our team has been alerted to this matter, and they are currently working diligently to investigate and " +
          "resolve it as swiftly as possible. " +
        "Thank you for your patience and understanding. We deeply regret any disruption this may have caused.";
  }
  
}
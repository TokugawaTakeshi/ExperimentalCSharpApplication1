namespace Utils;


public static class ListExtensions
{

  public static List<TElement> AddElementsToStart<TElement> (
    this List<TElement> self,
    params TElement[] newElements
  )
  {
    
    foreach (TElement element in newElements)
    {
      self.Insert(0, element);   
    }
    
    return self;
    
  }
  
}
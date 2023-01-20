namespace LocalBusiness.Models;

  public class PagingParameters
{
  const int maxElementPerPage = 10; // max amount of element per page
  public int PageNumber { get; set; } = 1; // by default set to the first page, how many pages you will have ( Number of element / maxPageSize)
  private int _elementsPerPage = 5; // works in relation with public PageSize, if not specified default 3 elements will populate
  public int ElementsPerPage // this property value represents how many elements you want to show in a Get
  {
    get
    {
      return _elementsPerPage;
    }
    set
    {
      _elementsPerPage = (value > maxElementPerPage) ? maxElementPerPage : value;
    }
  }
}

using Microsoft.EntityFrameworkCore;
namespace LocalBusiness.Models;

public class PagedList<T> : List<T>
{
  public int CurrentPage { get; private set; }
  public int TotalPages { get; private set; }
  public int ElementsPerPage { get; private set; }
  public int TotalCount { get; private set; }
  public bool HasPrevious => CurrentPage > 1;
  public bool HasNext => CurrentPage < TotalPages;
  public PagedList(List<T> items, int count, int pageNumber, int elementsPerPage)
  {
    TotalCount = count;
    ElementsPerPage = elementsPerPage;
    CurrentPage = pageNumber;
    TotalPages = (int)Math.Ceiling(count / (double)elementsPerPage);
    AddRange(items);
  }
  public static async Task<PagedList<T>> ToPagedListAsync(IQueryable<T> source, int pageNumber, int elementsPerPage)
  {
    var count = source.Count();
    var items = await source.Skip((pageNumber - 1) * elementsPerPage).Take(elementsPerPage).ToListAsync();
    return new PagedList<T>(items, count, pageNumber, elementsPerPage);
  }
}

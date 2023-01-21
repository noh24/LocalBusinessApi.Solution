using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LocalBusiness.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace LocalBusiness.Controllers.v2;
[Authorize]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("2.0")]
public class BusinessController : ControllerBase
{
  private readonly ApplicationContext _db;

  public BusinessController(ApplicationContext db)
  {
    _db = db;
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<Business>>> Get([FromQuery] PagingParameters parameters, string name, string description, int review, bool random)
  {
    IQueryable<Business> query = _db.Businesses.AsQueryable();
    if (name != null)
    {
      query = query.Where(e => e.Name.Contains(name));
    }
    if (description != null)
    {
      query = query.Where(entry => entry.Description.Contains(description));
    }
    if (review > 0)
    {
      query = query.Where(e => e.Review == review);
    }
    if (random)
    {
      Random randomNum = new Random();
      int ToThisIndex = randomNum.Next(0, query.Count());
      Business randomBusiness = query.Skip(ToThisIndex).FirstOrDefault();
      return new ActionResult<IEnumerable<Business>>(new List<Business> { randomBusiness });
    }
    var businesses = await PagedList<Business>.ToPagedListAsync(query.OrderBy(e => e.Name), parameters.PageNumber, parameters.ElementsPerPage);
    var metadata = new
    {
      businesses.TotalPages,
      businesses.CurrentPage,
      businesses.ElementsPerPage,
      businesses.TotalCount,
      businesses.HasPrevious,
      businesses.HasNext
    };
    Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

    return new ActionResult<IEnumerable<Business>>(businesses);
  }

  // GET: api/Business/5
  [HttpGet("{id}")]
  public async Task<ActionResult<Business>> GetBusiness(int id)
  {
    Business business = await _db.Businesses.FindAsync(id);

    if (business == null)
    {
      return NotFound();
    }

    return business;
  }

  // POST api/business
  [HttpPost]
  public async Task<ActionResult<Business>> Post(Business business)
  {
    _db.Businesses.Add(business);
    await _db.SaveChangesAsync();
    return CreatedAtAction(nameof(GetBusiness), new { id = business.BusinessId }, business);
  }

  // PUT: api/Business/5
  [HttpPut("{id}")]
  public async Task<IActionResult> Put(int id, Business business)
  {
    if (id != business.BusinessId)
    {
      return BadRequest();
    }

    _db.Businesses.Update(business);

    try
    {
      await _db.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
      if (!BusinessExists(id))
      {
        return NotFound();
      }
      else
      {
        throw;
      }
    }

    return NoContent();
  }
  private bool BusinessExists(int id)
  {
    return _db.Businesses.Any(e => e.BusinessId == id);
  }

  // DELETE: api/Business/5
  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteBusiness(int id)
  {
    Business business = await _db.Businesses.FindAsync(id);
    if (business == null)
    {
      return NotFound();
    }

    _db.Businesses.Remove(business);
    await _db.SaveChangesAsync();

    return NoContent();
  }
}

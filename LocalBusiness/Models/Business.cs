using System.ComponentModel.DataAnnotations;

namespace LocalBusiness.Models;
public class Business
{
  public int BusinessId { get; set; }
  [Required]
  public string Name { get; set; }
  [Required]
  public string Description { get; set; }
  [Range(1, 5)]
  public int Review { get; set; }
}

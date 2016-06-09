using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotDolls.Models
{
  public class Geek
  {
    public int GeekId { get; set; }
    public string UserName { get; set; }
    public string EmailAddress { get; set; }
    public string Location { get; set; }
    public DateTime CreatedDate { get; set; }

    public string FigurineHREF { get; set; }

    // illustrates a many to one relationship
    public ICollection<Inventory> Figurines { get; set; }
  }
}

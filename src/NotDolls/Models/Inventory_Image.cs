using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotDolls.Models
{
  public class Inventory_Image
  {
    public int Inventory_ImageId { get; set; }
    public string Image { get; set; }
    public string MetaData { get; set; }
    public int InventoryId { get; set; }

    // illustrates a foreign key relationship
    private Inventory Inventory { get; set; }
  }
}

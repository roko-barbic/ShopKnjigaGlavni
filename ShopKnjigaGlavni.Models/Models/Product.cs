using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopKnjigaGlavni.Models.Models;

public class Product
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }
    public string ISBN { get; set; }
    [DisplayName("List price")]
    [Range(1D,1000D)]// [Range(1D,1000D)]
    public double ListPrice { get; set; }
    [Range(1D, 1000D)]
    public double Price { get; set; }
    [DisplayName("Price for 50+")]
    public double Price50 { get; set; }
    [DisplayName("Price for 100+")]
    public double Price100 { get; set; }
    //public DateTime YearOfPublish { get; set; }
    //public string Picture {  get; set; }
    //public Category Category { get; set; }
    //public int NumberOfPages { get; set; }

}

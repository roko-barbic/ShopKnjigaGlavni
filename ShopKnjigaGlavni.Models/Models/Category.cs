using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace ShopKnjigaGlavni.Models.Models; //napisi ; i on ce sam maknit {}, dode ti na isto

public class Category
{
    [Key]
    public int Id { get; set; }
    [MaxLength(30)]
    public string Name { get; set; }

    [DisplayName("Display order")]
    [Range(1, 30)]
    public int DisplayOrder { get; set; }

}

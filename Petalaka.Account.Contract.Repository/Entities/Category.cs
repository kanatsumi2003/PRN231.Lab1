using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Petalaka.Account.Contract.Repository.Base;
using Petalaka.Account.Contract.Repository.Base.Interface;

namespace Petalaka.Account.Contract.Repository.Entities;

public class Category : BaseEntity
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CategoryId { get; set; }
    [Required]
    [StringLength(40)]
    public string CategoryName { get; set; }
    public virtual ICollection<Product> Products { get; set; }
}
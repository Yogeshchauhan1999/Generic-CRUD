using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenericOps.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set;  }
        [Required]
        public string ProductName { get; set; }
        public double ProductPrice { get; set; } = 0.00;
        public DateTime AddedOn {  get; set; }=DateTime.Now;

        public int IsActive { get; set; } = 1;
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }  

    }
}

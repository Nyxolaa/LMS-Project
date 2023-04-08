using System.ComponentModel.DataAnnotations;

namespace LMS_Project_mvc.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}

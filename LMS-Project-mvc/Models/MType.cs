using System.ComponentModel.DataAnnotations;

namespace LMS_Project_mvc.Models
{
    public class MType
    {
        [Key]
        public int MTypeId { get; set; }
        public string TypeName { get; set; }

    }
}

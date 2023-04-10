using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace LMS_Project_mvc.Models
{
    public class Media
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string FileName { get; set; }
        public double FileSize { get; set; }
        public string FileSizeHuman { get; set; }
        public int Time { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
        public bool IsArchive { get; set; }

        public int CategoryId { get; set; } 
        public int MTypeId { get; set; } 

        public virtual Category? Category { get; set; }
        public virtual MType? MType { get; set; }


    }
}

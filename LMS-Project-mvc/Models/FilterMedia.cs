namespace LMS_Project_mvc.Models
{
    public class FilterMedia
    {
        public int CategoryId{ get; set; }
        public int MTypeId{ get; set; }
        public string Title{ get; set; }
        public string FileName{ get; set; }
        public bool IsArchive { get; set; }
    }
}

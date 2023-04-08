using Microsoft.EntityFrameworkCore;
using LMS_Project_mvc.Models;
using LMS_Project_mvc.Models.Config;

namespace LMS_Project_mvc.DAL
{
    public class MediaDB:DbContext
    {
        public MediaDB(DbContextOptions<MediaDB>options):base(options)
        {
            
        }

        public DbSet<Media> Medias { get; set; }
        public DbSet<Category> MediaCategories { get; set; }
        public DbSet<MType> MediaTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration<MType>(new MTypeCFG());
            builder.ApplyConfiguration<Category>(new CategoryCFG());

            base.OnModelCreating(builder);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS_Project_mvc.Models.Config
{
    public class MTypeCFG : IEntityTypeConfiguration<MType>
    {
        public void Configure(EntityTypeBuilder<MType> builder)
        {
            builder.HasData(new MType { MTypeId = 1, TypeName = "Video" });
            builder.HasData(new MType { MTypeId = 2, TypeName = "Podcast" });
            builder.HasData(new MType { MTypeId = 3, TypeName = "Scorm Paketi" });
        }
    }
}

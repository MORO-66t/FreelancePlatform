//using cloud.Domain.Entities;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace Cloud.Infrastructure.Data.Configurations;

//public class JobConfiguration : IEntityTypeConfiguration<Job>
//{
//    public void Configure(EntityTypeBuilder<Job> builder)
//    {
//        builder.Property(j => j.Title)
//            .HasMaxLength(200)
//            .IsRequired();

//        builder.Property(j => j.Budget)
//            .HasColumnType("decimal(18,2)");

//        builder.HasOne(j => j.Client)
//            .WithMany(c => c.Jobs)
//            .HasForeignKey(j => j.ClientId);
//    }
//}

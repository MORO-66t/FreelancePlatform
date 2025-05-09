//using cloud.Domain.Entities;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace Cloud.Infrastructure.Data.Configurations;

//public class OfferConfiguration : IEntityTypeConfiguration<Offer>
//{
//    public void Configure(EntityTypeBuilder<Offer> builder)
//    {
//        builder.Property(o => o.Proposal)
//            .HasMaxLength(1000);

//        builder.Property(o => o.BidAmount)
//            .HasColumnType("decimal(18,2)");

//        builder.HasOne(o => o.Job)
//            .WithMany(j => j.Offers)
//            .HasForeignKey(o => o.JobId);

//        //builder.HasOne(o => o.Freelancer)
//        //    .WithMany(f => f.Offers)
//        //    .HasForeignKey(o => o.FreelancerId);
//    }
//}

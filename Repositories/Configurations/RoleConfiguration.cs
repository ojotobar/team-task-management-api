using Entities.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Extrensions;

namespace Repositories.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = new Guid("27aec839-5fa7-4dab-8295-c5fb64dd0c64").ToString(),
                    Name = Roles.Member.GetDescription(),
                    NormalizedName = Roles.Member.GetDescription().ToUpper(),
                    ConcurrencyStamp = "0a756968-67bd-4e6e-9863-bd823cf36619"
                },
                new IdentityRole
                {
                    Id = new Guid("44e6e525-9fc7-46e7-99e3-9ce309c664ff").ToString(),
                    Name = Roles.TeamAdmin.GetDescription(),
                    NormalizedName = Roles.TeamAdmin.GetDescription().ToUpper(),
                    ConcurrencyStamp = "677e6ef6-a7eb-4143-91b2-37a858ae1f16"
                });
        }
    }

}

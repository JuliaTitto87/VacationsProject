using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vacations_DomainModel.Models.Identity;

namespace Vacations_DAL.DbEntityConfigurations.Identity
{
    internal class UserSettingsDBConfiguration : IEntityTypeConfiguration<UserSettings>
    {
        public void Configure(EntityTypeBuilder<UserSettings> builder)
        {
            builder.HasOne<User>()
                .WithOne(user => user.UserSettings)
                .HasForeignKey<UserSettings>(us => us.UserId);
        }
    }
}

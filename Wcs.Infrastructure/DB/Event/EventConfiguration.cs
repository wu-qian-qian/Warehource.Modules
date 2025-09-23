using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wcs.Infrastructure.Database;

namespace Wcs.Infrastructure.DB.Event
{
    internal class EventConfiguration : IEntityTypeConfiguration<Domain.Event.Event>
    {
        public void Configure(EntityTypeBuilder<Domain.Event.Event> builder)
        {
            builder.ToTable(Schemas.TableSchema + nameof(Domain.Event.Event));
            builder.Property(p => p.Errored).HasMaxLength(128).IsRequired(false);
            builder.Property(p => p.Content).HasMaxLength(256).IsRequired();
        }
    }
}
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TW.Training.Vote.Infrastructure.Programmes.PO;
using TW.Training.Vote.Infrastructure.Votings.PO;

namespace TW.Training.Vote.Infrastructure;

public class VoteDbContext : DbContext
{
    public VoteDbContext(DbContextOptions<VoteDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<ProgrammeItem>().ToTable("programme_items").HasKey(x => x.Id);
        builder.Entity<Voting>().ToTable("votes").HasKey(x => x.Id);
        
        builder.ApplyConfigurationsFromAssembly(typeof(VoteDbContext).Assembly);
    }

    public DbSet<Programme> Programmes { get; set; }
    public DbSet<ProgrammeItem> ProgrammeItems { get; set; }
    public DbSet<Voting> Votings { get; set; }
}

#region EntityTypeConfigurations

public class ProgrammeEntityTypeConfiguration : IEntityTypeConfiguration<Programme>
{
    public void Configure(EntityTypeBuilder<Programme> builder)
    {
        builder.ToTable("programmes").HasKey(x => x.Id);
        builder.HasMany(x => x.ProgrammeItems);
    }
}

#endregion
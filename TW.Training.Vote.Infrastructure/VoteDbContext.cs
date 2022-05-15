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

public class ProgrammeItemEntityTypeConfiguration : IEntityTypeConfiguration<ProgrammeItem>
{
    public void Configure(EntityTypeBuilder<ProgrammeItem> builder)
    {
        builder.ToTable("programme_items").HasKey(x => x.Id);
        builder.HasOne(x => x.Programme);
        builder.HasMany(x => x.Votings).WithOne(x => x.ProgrammeItem);
    }
}

public class VotingEntityTypeConfiguration : IEntityTypeConfiguration<Voting>
{
    public void Configure(EntityTypeBuilder<Voting> builder)
    {
        builder.ToTable("votings").HasKey(x => x.Id);
        builder.HasOne(x => x.ProgrammeItem).WithMany(x => x.Votings);
    }
}

#endregion
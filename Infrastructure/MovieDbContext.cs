using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

public class MovieDbContext : DbContext
{
    public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
    {
    }

    // DbSets pour chaque table
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Media> Media { get; set; }
    public DbSet<Episode> Episodes { get; set; }
    public DbSet<ViewMedia> ViewMedia { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuration des contraintes uniques qui ne peuvent pas être définies par des attributs

        // Contrainte unique sur le nom du genre
        modelBuilder.Entity<Genre>()
            .HasIndex(g => g.Name)
            .IsUnique();

        // Contrainte unique pour un épisode (media_id, saison, numero_episode)
        modelBuilder.Entity<Episode>()
            .HasIndex(e => new { e.MediaId, e.Season, e.EpisodeNumber })
            .IsUnique();

        // Configuration des relations pour éviter les cycles et les suppressions en cascade conflictuelles

        // Relation entre Media et Genre
        modelBuilder.Entity<Media>()
            .HasOne(m => m.Genre)
            .WithMany(g => g.Media)
            .HasForeignKey(m => m.GenreId)
            .OnDelete(DeleteBehavior.SetNull); // Correspond à ON DELETE SET NULL en SQL

        // Relation entre Episode et Media
        modelBuilder.Entity<Episode>()
            .HasOne(e => e.Media)
            .WithMany(m => m.Episodes)
            .HasForeignKey(e => e.MediaId)
            .OnDelete(DeleteBehavior.Cascade); // Correspond à ON DELETE CASCADE

        // Les relations pour View sont gérées par les attributs ForeignKey
        // et EF Core comprendra qu'elles peuvent être nulles.
    }
}
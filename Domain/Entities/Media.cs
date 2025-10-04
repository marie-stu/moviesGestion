using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Mappe vers la table "media"
[Table("media")]
public class Media
{
    [Key]
    [Column("media_id")]
    public int MediaId { get; set; }

    [Required]
    [Column("title")]
    [MaxLength(255)]
    public string Title { get; set; }

    [Column("release_year")]
    public int? ReleaseYear { get; set; }

    [Required]
    [Column("media_type")]
    [MaxLength(50)]
    public string MediaType { get; set; } // "movie" ou "tv_show"

    [Column("genre_id")]
    public int? GenreId { get; set; }

    // Propriétés de navigation
    [ForeignKey("GenreId")]
    public virtual Genre? Genre { get; set; }
    public virtual ICollection<Episode> Episodes { get; set; } = new List<Episode>();
    public virtual ICollection<ViewMedia> ViewMedia { get; set; } = new List<ViewMedia>();
}
// Mappe vers la table "episodes"
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("episodes")]
public class Episode
{
    [Key]
    [Column("episode_id")]
    public int EpisodeId { get; set; }

    [Column("media_id")]
    public int MediaId { get; set; }

    [Column("season")]
    public int Season { get; set; }

    [Column("episode_number")]
    public int EpisodeNumber { get; set; }

    [Column("episode_title")]
    [MaxLength(255)]
    public string? EpisodeTitle { get; set; }

    // Propriétés de navigation
    [ForeignKey("MediaId")]
    public virtual Media Media { get; set; }
    public virtual ICollection<ViewMedia> ViewMedia { get; set; } = new List<ViewMedia>();
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Mappe vers la table "views"
[Table("views")]
public class ViewMedia
{
    [Key]
    [Column("view_id")]
    public int ViewId { get; set; }

    [Column("media_id")]
    public int? MediaId { get; set; } // Nullable, pour un film

    [Column("episode_id")]
    public int? EpisodeId { get; set; } // Nullable, pour un épisode

    [Column("view_date")]
    public DateTime ViewDate { get; set; }

    // Propriétés de navigation
    [ForeignKey("MediaId")]
    public virtual Media? Media { get; set; }

    [ForeignKey("EpisodeId")]
    public virtual Episode? Episode { get; set; }
}

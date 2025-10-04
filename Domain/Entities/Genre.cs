using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// --- Entités (Classes qui mappent vos tables) ---

// Mappe vers la table "genres"
[Table("genres")]
public class Genre
{
    [Key]
    [Column("genre_id")]
    public int GenreId { get; set; }

    [Required]
    [Column("name")]
    [MaxLength(100)]
    public string Name { get; set; }

    // Propriété de navigation : Un genre peut avoir plusieurs médias
    public virtual ICollection<Media> Media { get; set; } = new List<Media>();
}
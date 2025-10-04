namespace moviesGestion.Dto
{
    public class ViewMediaDto
    {
        
        public int? MediaId { get; set; } // Nullable, pour un film

        public int? EpisodeId { get; set; } // Nullable, pour un épisode

        public DateTime ViewDate { get; set; }


    }
}

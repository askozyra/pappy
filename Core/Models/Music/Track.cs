namespace Core.Models.Music
{
    public class Track
    {
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Title { get; set; }
        public string Source { get; set; }
        public string Extension { get; set; }
        public TimeSpan Duration { get; set; }
    }
}

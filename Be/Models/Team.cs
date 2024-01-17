namespace Be.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Image {  get; set; }  

        public int PositionId { get; set; }
        public Position? Position { get; set; }

        public string? FaceLink { get; set; }
        public string? TwitLink { get; set;}
        public string? PlusLink {  get; set; } 
    }
}

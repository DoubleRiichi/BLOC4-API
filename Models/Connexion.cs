namespace BLOC4_API.Models
{
    public class Connexion
    {
        public int? Id { get; set; }
        public required string Password { get; set; }
        public required string? Token { get; set; }
    }
}

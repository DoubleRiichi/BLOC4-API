namespace BLOC4_API.Models
{
    public class Salaries
    {
        public int? Id { get; set; }
        public required string Nom { get; set; }
        public required string Prenom { get; set; }
        public required string Telephone_fixe { get; set; }
        public required string Telephone_mobile { get; set; }
        public required string Email { get; set; }
        public required int? Services_id { get; set; } 
    }
}

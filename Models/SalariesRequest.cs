namespace BLOC4_API.Models
{
    public class SalariesRequest
    {
        public required Salaries salaries { get; set; }
        public string? token { get; set; }
    }
}

namespace BLOC4_API
{
    using Microsoft.EntityFrameworkCore;
    using BLOC4_API.Models;

    public class BLOC4db : DbContext
    {
        public BLOC4db(DbContextOptions<BLOC4db> options) : base(options) { }

        public DbSet<Connexion> Connexion { get; set; }
        public DbSet<Salaries> Salaries { get; set; }
        public DbSet<Services> Services { get; set; }
        public DbSet<Sites> Sites { get; set; }
    }
}

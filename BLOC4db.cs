namespace BLOC4_API
{
    using Microsoft.EntityFrameworkCore;
    using BLOC4_API.Models;
    //Précise des informations sur la base de données utilisée, notamment ses tables. 
    //Utilisé par Entity Framework pour implémenter les fonctionalités ORM (object relational mapping)
    public class BLOC4db : DbContext
    {
        public BLOC4db(DbContextOptions<BLOC4db> options) : base(options) { }

        public DbSet<Connexion> Connexion { get; set; }
        public DbSet<Salaries> Salaries { get; set; }
        public DbSet<Services> Services { get; set; }
        public DbSet<Sites> Sites { get; set; }
    }
}

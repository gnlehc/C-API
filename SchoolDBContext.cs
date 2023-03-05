using BootcampAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace BootcampAPI
{
    public class SchoolDBContext : DbContext
    {
        public DbSet<MsGender> MsGender { get; set; }
        public DbSet<MsReligion> MsReligion { get; set; }
        public DbSet<MsGrade> MsGrade { get; set; }
        public DbSet<MsStudent> MsStudent { get; set; }
        public DbSet<MsSubject> MsSubject { get; set; }
        public DbSet<TrScore> TrScore { get; set; }
        public SchoolDBContext(DbContextOptions<SchoolDBContext> options) : base(options) 
        {
        }
    }
}

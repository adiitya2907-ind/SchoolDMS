using Microsoft.EntityFrameworkCore;
using SchoolDMS.API.Models.Entities;
using SchoolDMS.API.Models.Enums;

namespace SchoolDMS.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<School> Schools { get; set; } = null!;
        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<Visit> Visits { get; set; } = null!;
        public DbSet<Document> Documents { get; set; } = null!;
        public DbSet<ApprovalWorkflow> ApprovalWorkflows { get; set; } = null!;
        public DbSet<DocumentSearch> DocumentSearches { get; set; } = null!;
        public DbSet<AuditLog> AuditLogs { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Pre-seed roles
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = RoleEnum.Engineer, RoleName = "Engineer", Description = "Field Engineer" },
                new Role { RoleId = RoleEnum.OpsVerifier, RoleName = "OpsVerifier", Description = "Operations Team Verifier" },
                new Role { RoleId = RoleEnum.Vendor, RoleName = "Vendor", Description = "Equipment Vendor" },
                new Role { RoleId = RoleEnum.Admin, RoleName = "Admin", Description = "System Administrator" }
            );

            // Configure unique constraint for User Email
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Configure unique constraint for School UdiseCode
            modelBuilder.Entity<School>()
                .HasIndex(s => s.UdiseCode)
                .IsUnique();

            // Configure Delete Behavior
            modelBuilder.Entity<Visit>()
                .HasOne(v => v.School)
                .WithMany(s => s.Visits)
                .HasForeignKey(v => v.SchoolId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Visit>()
                .HasOne(v => v.Engineer)
                .WithMany(u => u.Visits)
                .HasForeignKey(v => v.EngineerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Visit>()
                .HasOne(v => v.Project)
                .WithMany(p => p.Visits)
                .HasForeignKey(v => v.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Document>()
                .HasOne(d => d.Visit)
                .WithMany(v => v.Documents)
                .HasForeignKey(d => d.VisitId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ApprovalWorkflow>()
                .HasOne(a => a.Visit)
                .WithMany(v => v.ApprovalWorkflows)
                .HasForeignKey(a => a.VisitId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ApprovalWorkflow>()
                .HasOne(a => a.Verifier)
                .WithMany(u => u.Approvals)
                .HasForeignKey(a => a.VerifierId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyInspection.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyInspection.Infrastructure.Data
{
    // We use IdentityDbContext which pre-configures tables for Users, Roles, etc.
    public class ApplicationDbContext : IdentityDbContext<User, Role, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Add DbSets for your other entities here
        //public DbSet<Customer> Customers { get; set; }
        //public DbSet<InspectionReport> InspectionReports { get; set; }
        // ... and so on for all your other tables
        // Add DbSets for all your custom entities
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerType> CustomerTypes { get; set; }
        public DbSet<InspectionReport> InspectionReports { get; set; }
        public DbSet<ReportDetails> ReportDetails { get; set; }
        public DbSet<MasterCriteria> MasterCriteria { get; set; }
        public DbSet<ReportCriteriaStatus> ReportCriteriaStatuses { get; set; }
        public DbSet<Checkpoint> Checkpoints { get; set; }
        public DbSet<ReportCheckpointAnswer> ReportCheckpointAnswers { get; set; }
        public DbSet<ReportQuantityItem> ReportQuantityItems { get; set; }
        public DbSet<ReportDefect> ReportDefects { get; set; }
        public DbSet<ReportMedia> ReportMediaFiles { get; set; }
        public DbSet<AQLLevel> AQLLevels { get; set; }
        public DbSet<AQLDetails> AQLDetails { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<ReportOverride> ReportOverrides { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); // This is required for Identity

            // Rename the default Identity tables to match our schema
            builder.Entity<User>().ToTable("Users");
            builder.Entity<Role>().ToTable("Roles");
            builder.Entity<IdentityUserRole<int>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<int>>().ToTable("UserTokens");

            // Configure any other custom relationships or constraints here

            // Here you can add any specific configurations (Fluent API)
            // For example, setting up a one-to-one relationship for ReportDetails
            builder.Entity<InspectionReport>()
                .HasOne(r => r.ReportDetails)
                .WithOne(d => d.InspectionReport)
                .HasForeignKey<ReportDetails>(d => d.ReportID);
        }
    }      
    
}

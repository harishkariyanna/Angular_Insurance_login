using Microsoft.EntityFrameworkCore;
using InsuranceManagement.Models;

namespace InsuranceManagement.Data
{
    public class InsuranceDbContext : DbContext
    {
        public InsuranceDbContext(DbContextOptions<InsuranceDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        public DbSet<Policy> Policies { get; set; }
        public DbSet<Coverage> Coverages { get; set; }
        public DbSet<Premium> Premiums { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<ClaimNote> ClaimNotes { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureUserRelationships(modelBuilder);
            ConfigurePolicyRelationships(modelBuilder);
            ConfigureClaimRelationships(modelBuilder);
            ConfigureIndexes(modelBuilder);
            SeedData(modelBuilder);
        }

        private void ConfigureUserRelationships(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.PermissionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RolePermission>()
                .HasIndex(rp => new { rp.RoleId, rp.PermissionId })
                .IsUnique();
        }

        private void ConfigurePolicyRelationships(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Policy>()
                .HasOne(p => p.Customer)
                .WithMany(u => u.Policies)
                .HasForeignKey(p => p.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Policy>()
                .HasOne(p => p.Agent)
                .WithMany()
                .HasForeignKey(p => p.AgentId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Coverage>()
                .HasOne(c => c.Policy)
                .WithMany(p => p.Coverages)
                .HasForeignKey(c => c.PolicyId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Premium>()
                .HasOne(pr => pr.Policy)
                .WithMany(p => p.Premiums)
                .HasForeignKey(pr => pr.PolicyId)
                .OnDelete(DeleteBehavior.Cascade);


        }

        private void ConfigureClaimRelationships(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Claim>()
                .HasOne(c => c.Policy)
                .WithMany(p => p.Claims)
                .HasForeignKey(c => c.PolicyId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Claim>()
                .HasOne(c => c.Customer)
                .WithMany(u => u.Claims)
                .HasForeignKey(c => c.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Claim>()
                .HasOne(c => c.AssignedAgent)
                .WithMany(u => u.AssignedClaims)
                .HasForeignKey(c => c.AssignedAgentId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<ClaimNote>()
                .HasOne(cn => cn.Claim)
                .WithMany(c => c.Notes)
                .HasForeignKey(cn => cn.ClaimId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ClaimNote>()
                .HasOne(cn => cn.User)
                .WithMany(u => u.ClaimNotes)
                .HasForeignKey(cn => cn.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private void ConfigureIndexes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Policy>()
                .HasIndex(p => p.PolicyNumber)
                .IsUnique();

            modelBuilder.Entity<Policy>()
                .HasIndex(p => new { p.CustomerId, p.Status });

            modelBuilder.Entity<Claim>()
                .HasIndex(c => c.ClaimNumber)
                .IsUnique();

            modelBuilder.Entity<Claim>()
                .HasIndex(c => new { c.Status, c.Priority });

            modelBuilder.Entity<AuditLog>()
                .HasIndex(a => new { a.EntityType, a.EntityId });

            modelBuilder.Entity<AuditLog>()
                .HasIndex(a => a.Timestamp);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin", Description = "System Administrator" },
                new Role { Id = 2, Name = "Agent", Description = "Insurance Agent" },
                new Role { Id = 3, Name = "Customer", Description = "Insurance Customer" },
                new Role { Id = 4, Name = "Manager", Description = "Insurance Manager" },
                new Role { Id = 5, Name = "Underwriter", Description = "Insurance Underwriter" }
            );

            var permissions = new[]
            {
                new Permission { Id = 1, Name = "ViewUsers", Resource = "User", Action = "View" },
                new Permission { Id = 2, Name = "CreateUsers", Resource = "User", Action = "Create" },
                new Permission { Id = 3, Name = "UpdateUsers", Resource = "User", Action = "Update" },
                new Permission { Id = 4, Name = "DeleteUsers", Resource = "User", Action = "Delete" },
                new Permission { Id = 5, Name = "ViewPolicies", Resource = "Policy", Action = "View" },
                new Permission { Id = 6, Name = "CreatePolicies", Resource = "Policy", Action = "Create" },
                new Permission { Id = 7, Name = "UpdatePolicies", Resource = "Policy", Action = "Update" },
                new Permission { Id = 8, Name = "DeletePolicies", Resource = "Policy", Action = "Delete" },
                new Permission { Id = 9, Name = "ViewClaims", Resource = "Claim", Action = "View" },
                new Permission { Id = 10, Name = "CreateClaims", Resource = "Claim", Action = "Create" },
                new Permission { Id = 11, Name = "UpdateClaims", Resource = "Claim", Action = "Update" },
                new Permission { Id = 12, Name = "ApproveClaims", Resource = "Claim", Action = "Approve" },
                new Permission { Id = 13, Name = "ViewReports", Resource = "Report", Action = "View" },
                new Permission { Id = 14, Name = "GenerateReports", Resource = "Report", Action = "Generate" }
            };

            modelBuilder.Entity<Permission>().HasData(permissions);

            // Seed default users with Indian names
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FirstName = "Rajesh", LastName = "Kumar", Email = "admin@insurance.com", PasswordHash = "Admin123!", RoleId = 1, PhoneNumber = "9876543210", IsActive = true, CreatedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new User { Id = 2, FirstName = "Priya", LastName = "Sharma", Email = "priya.agent@insurance.com", PasswordHash = "Agent123!", RoleId = 2, PhoneNumber = "9876543211", IsActive = true, CreatedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new User { Id = 3, FirstName = "Amit", LastName = "Patel", Email = "amit.manager@insurance.com", PasswordHash = "Manager123!", RoleId = 4, PhoneNumber = "9876543212", IsActive = true, CreatedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
            );

            // Seed addresses
            modelBuilder.Entity<Address>().HasData(
                new Address { Id = 1, Street = "123 MG Road", City = "Mumbai", State = "Maharashtra", ZipCode = "400001", Country = "India" },
                new Address { Id = 2, Street = "456 Brigade Road", City = "Bangalore", State = "Karnataka", ZipCode = "560001", Country = "India" },
                new Address { Id = 3, Street = "789 Anna Salai", City = "Chennai", State = "Tamil Nadu", ZipCode = "600001", Country = "India" }
            );



            // Remove policy seed data - will be created through application
            // modelBuilder.Entity<Policy>().HasData();
        }
    }
}
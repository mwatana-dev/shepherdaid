using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Rite.Software.Shepherd.DAL
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
                
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        //    Database.SetInitializer<ApplicationDbContext>(new DropCreateDatabaseIfModelChanges<ApplicationDbContext>());
        }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().ToTable("ApplicationUsers");
            modelBuilder.Entity<IdentityRole>().ToTable("ApplicationRoles");
            modelBuilder.Entity<IdentityUserRole>().ToTable("ApplicationUserRoles");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("AppliationUserClaims");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("ApplicationUserLogin");

            //modelBuilder.Entity<AppUserRole>()
            //    .HasKey(c => c.Id);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual DbSet<ApplicationGroup> ApplicationGroups { get; set; }
        public virtual DbSet<ApplicationGroupRole> ApplicationGroupRoles { get; set; }
        public virtual DbSet<Parish> Parishes { get; set; }
        public virtual DbSet<Diocese> Dioceses { get; set; }
        public virtual DbSet<Church> Churches { get; set; }
        public virtual DbSet<RankType> RankTypes { get; set; }
        public virtual DbSet<AppUser> AppUsers { get; set; }
        public virtual DbSet<AppRole> AppRoles { get; set; }
        public virtual DbSet<AppUserRole> AppUserRoles { get; set; }
    }

    #region "Entities"

    public class AppUser: ApplicationUser
    {
        public AppUser()
        {
            AppUserRoles = new HashSet<AppUserRole>();
        }

        [Required, Display(Name = "Application Group")]
        public int ApplicationGroupId { get; set; }

        [Required, Display(Name = "First Name"), StringLength(50)]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name"), StringLength(50)]
        public string MiddleName { get; set; }

        [Required, Display(Name = "Last Name"), StringLength(50)]
        public string LastName { get; set; }

        [Required, Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [Required, Display(Name = "Recorded By"), StringLength(50)]
        public string RecordedBy { get; set; }

        [Required, Display(Name = "Date Modified"), DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateRecorded { get; set; }

        [Required, Display(Name = "Last Modified By"), StringLength(50)]
        public string LastModifiedBy { get; set; }

        [Required, Display(Name = "Last Date Modified"), DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LastDateModified { get; set; }

        public ICollection<AppUserRole> AppUserRoles { get; set; }
        public virtual ApplicationGroup ApplicationGroup { get; set; }
    }

    public class AppRole : IdentityRole
    {
        public AppRole()
        {
            ApplicationGroupRoles = new HashSet<ApplicationGroupRole>();
            AppUserRoles = new HashSet<AppUserRole>();
        }

        [Display(Name="Rank Type")]
        public int RankTypeId { get; set; }

        public ICollection<ApplicationGroupRole> ApplicationGroupRoles { get; set; }
        public ICollection<AppUserRole> AppUserRoles { get; set; }

        public virtual RankType RankType { get; set; }
    }
    public class ApplicationGroup
    {
        public ApplicationGroup()
        {
            AppUsers = new HashSet<AppUser>();
            ApplicationGroupRoles = new HashSet<ApplicationGroupRole>();
        }

        [Key]
        public int Id { get; set; }

        [Required, Display(Name = "Parish")]
        public int ParishId { get; set; }
        
        [Required, Display(Name = "Group Name"), StringLength(50)]
        public string Name { get; set; }

        [Required, Display(Name = "Can Change")]
        public bool CanChange { get; set; }

        [Required, Display(Name = "Recorded By"), StringLength(50)]
        public string RecordedBy { get; set; }

        [Required, Display(Name = "Date Modified"), DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateRecorded { get; set; }

        [Required, Display(Name = "Last Modified By"), StringLength(50)]
        public string LastModifiedBy { get; set; }

        [Required, Display(Name = "Last Date Modified"), DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LastDateModified { get; set; }

        public virtual Parish Parish { get; set; }

        public virtual ICollection<AppUser> AppUsers { get; set; }
        public virtual ICollection<ApplicationGroupRole> ApplicationGroupRoles { get; set; }
    }
    public class ApplicationGroupRole
    {        
        [Key]
        public int Id { get; set; }

        [Required, Display(Name = "Application Group"), Index("IX_GroupAndRole", 1, IsUnique = true)]
        public int ApplicationGroupId { get; set; }

        [Required, Display(Name = "Application Role"), Index("IX_GroupAndRole", 2, IsUnique = true), StringLength(128)]
        public string AppRoleId { get; set; }
        
        [Required, Display(Name = "Recorded By"), StringLength(50)]
        public string RecordedBy { get; set; }

        [Required, Display(Name = "Date Modified"), DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateRecorded { get; set; }

        [Required, Display(Name = "Last Modified By"), StringLength(50)]
        public string LastModifiedBy { get; set; }

        [Required, Display(Name = "Last Date Modified"), DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LastDateModified { get; set; }

        public virtual ApplicationGroup ApplicationGroup { get; set; }
        public virtual AppRole AppRole { get; set; }
        
    }
    public class AppUserRole: IdentityUserRole
    {

        [Required, Display(Name = "Recorded By"), StringLength(50)]
        public string RecordedBy { get; set; }

        [Required, Display(Name = "Date Modified"), DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateRecorded { get; set; }

        [Required, Display(Name = "Last Modified By"), StringLength(50)]
        public string LastModifiedBy { get; set; }

        [Required, Display(Name = "Last Date Modified"), DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LastDateModified { get; set; }

        [ForeignKey("UserId")]
        public AppUser AppUser { get; set; }

        [ForeignKey("RoleId")]
        public AppRole AppRole { get; set; }
    }
    public class Parish
    {
        public Parish()
        {
            ApplicationGroups = new HashSet<ApplicationGroup>();
        }

        [Key]
        public int Id { get; set; }

        [Required, Display(Name = "Diocese")]
        public int DioceseId { get; set; }

        [Required, Display(Name = "Parish Name"), StringLength(50)]
        public string Name { get; set; }

        [Required, StringLength(250)]
        public string Address { get; set; }

        [Required, DataType(DataType.EmailAddress), StringLength(100)]
        public string Email { get; set; }

        [DataType(DataType.Url), StringLength(75)]
        public string Website { get; set; }


        [Required, Display(Name = "Phone #1"), StringLength(15)]
        public string Phone1 { get; set; }


        [Display(Name = "Phone #2"), StringLength(15)]
        public string Phone2 { get; set; }

        [Required, Display(Name = "Recorded By"), StringLength(50)]
        public string RecordedBy { get; set; }

        [Required, Display(Name = "Date Recorded"), DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateRecorded { get; set; }

        [Display(Name = "Last Modified By"), StringLength(50)]
        public string LastModifiedBy { get; set; }

        [Display(Name = "Last Date Modified"), DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LastDateModified { get; set; }

        public virtual ICollection<ApplicationGroup> ApplicationGroups { get; set; }

        public virtual Diocese Diocese { get; set; }
    }
    public class Diocese
    {
        public Diocese()
        {
            Parishes = new HashSet<Parish>();
        }

        [Key]
        public int Id { get; set; }

        [Required, Display(Name = "Chruch")]
        public int ChurchId { get; set; }

        [Required, Display(Name = "Diocese Name"), StringLength(50)]
        public string Name { get; set; }

        [Required, StringLength(250)]
        public string Address { get; set; }

        [Required, DataType(DataType.EmailAddress), StringLength(100)]
        public string Email { get; set; }

        [DataType(DataType.Url), StringLength(75)]
        public string Website { get; set; }


        [Required, Display(Name = "Phone #1"), StringLength(15)]
        public string Phone1 { get; set; }


        [Display(Name = "Phone #2"), StringLength(15)]
        public string Phone2 { get; set; }

        [Required, Display(Name = "Recorded By"), StringLength(50)]
        public string RecordedBy { get; set; }

        [Required, Display(Name = "Date Recorded"), DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateRecorded { get; set; }

        [Required, Display(Name = "Last Modified By"), StringLength(50)]
        public string LastModifiedBy { get; set; }

        [Display(Name = "Last Date Modified"), DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> LastDateModified { get; set; }

        public virtual ICollection<Parish> Parishes { get; set; }

        public virtual Church Church { get; set; }
    }
    public class Church
    {
        public Church()
        {
            Dioceses = new HashSet<Diocese>();
        }

        [Key]
        public int Id { get; set; }

        [Required, Display(Name = "Church Name"), StringLength(50)]
        public string Name { get; set; }

        [Required, StringLength(250)]
        public string Address { get; set; }

        [Required, DataType(DataType.EmailAddress), StringLength(100)]
        public string Email { get; set; }

        [DataType(DataType.Url), StringLength(75)]
        public string Website { get; set; }


        [Required, Display(Name = "Phone #1"), StringLength(15)]
        public string Phone1 { get; set; }


        [Display(Name = "Phone #2"), StringLength(15)]
        public string Phone2 { get; set; }

        [Required, Display(Name = "Recorded By"), StringLength(50)]
        public string RecordedBy { get; set; }

        [Required, Display(Name = "Date Recorded"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateRecorded { get; set; }

        [Display(Name = "Last Modified By"), StringLength(50)]
        public string LastModifiedBy { get; set; }

        [Display(Name = "Last Date Modified"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> LastDateModified { get; set; }

        public virtual ICollection<Diocese> Dioceses { get; set; }
    }
    
    public class RankType
    {
        public RankType()
        {
            AppRoles = new HashSet<AppRole>();
        }

        [Key]
        public int Id { get; set; }

        [Required, Display(Name = "Rank Level"), StringLength(50)]
        public string Rank { get; set; }

        [Required, Display(Name = "Recorded By"), StringLength(50)]
        public string RecordedBy { get; set; }

        [Required, Display(Name = "Date Recorded"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateRecorded { get; set; }

        public virtual ICollection<AppRole> AppRoles { get; set; }
    }
    
    #endregion "Entities"
}
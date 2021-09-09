using DotNetCore06ProjectConfig.Data.Entity;
using DotNetCore06ProjectConfig.Data.Entity.Auth;
using DotNetCore06ProjectConfig.Data.Entity.Master;
using DotNetCore06ProjectConfig.Data.Entity.MasterData;
using DotNetCore06ProjectConfig.Models;
using DotNetCore06ProjectConfig.Models.MasterData;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Infrastructure;

namespace DotNetCore06ProjectConfig.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor _httpContextAccessor) :
        base(options)
    {
        this._httpContextAccessor = _httpContextAccessor;
    }

    #region Auth
    public DbSet<AspNetCompanyRoles>? AspNetCompanyRoles { get; set; }
    public DbSet<NavModule>? NavModules { get; set; }
    public DbSet<NavParent>? NavParents { get; set; }
    public DbSet<NavBand>? NavBands { get; set; }
    public DbSet<NavItem>? NavItems { get; set; }
    public DbSet<UserAccessPage>? UserAccessPages { get; set; }
    public DbQuery<UserAccessPageSPModel>? UserAccessPageSPModels { get; set; }
    public DbSet<CompanyAccessPage>? CompanyAccessPages { get; set; }
    public DbSet<CompanyBasedUserAccessPage>? CompanyBasedUserAccessPages { get; set; }
    #endregion

    #region Master Data

    public DbSet<BusinessType>? BusinessTypes { get; set; }
    public DbSet<CompanyType>? CompanyTypes { get; set; }
    public DbSet<Company>? Companys { get; set; }
    public DbSet<AddressType>? AddressTypes { get; set; }
    public DbSet<Address>? Addresses { get; set; }
    public DbSet<Country>? Countries { get; set; }
    public DbSet<District>? Districts { get; set; }
    public DbSet<Division>? Divisions { get; set; }
    public DbSet<Thana>? Thanas { get; set; }
    public DbSet<PostOffice>? PostOffices { get; set; }
    public DbSet<PaymentMode>? PaymentModes { get; set; }
    public DbSet<Brand>? brands { get; set; }
    public DbSet<Color>? Colors { get; set; }
    public DbSet<Size>? Sizes { get; set; }

    public DbSet<Gender>? Genders { get; set; }
    public DbSet<Slider>? Sliders { get; set; }
    public DbSet<AddressCategory>? addressCategories { get; set; }
    public DbSet<Resource>? Resources { get; set; }
    public DbSet<CustomerComment>? CustomerComments { get; set; }

    #endregion


    #region Settings Configs
    public override int SaveChanges()
    {
        AddTimestamps();
        return base.SaveChanges();
    }

    public async Task<int> SaveChangesAsync()
    {
        AddTimestamps();
        return await base.SaveChangesAsync();
    }

    private void AddTimestamps()
    {

        var entities = ChangeTracker.Entries().Where(x => x.Entity is Base && (x.State == EntityState.Added || x.State == EntityState.Modified));

        var currentUsername = !string.IsNullOrEmpty(_httpContextAccessor?.HttpContext?.User?.Identity?.Name)
            ? _httpContextAccessor.HttpContext.User.Identity.Name
            : "Anonymous";

        foreach (var entity in entities)
        {
            if (entity.State == EntityState.Added)
            {
                ((Base)entity.Entity).CreatedAt = DateTime.Now.AddHours(-12);
                ((Base)entity.Entity).CreatedBy = currentUsername;
            }
            else
            {
                entity.Property("createdAt").IsModified = false;
                entity.Property("createdBy").IsModified = false;
                ((Base)entity.Entity).UpdatedAt = DateTime.Now.AddHours(-12);
                ((Base)entity.Entity).UpdatedBy = currentUsername;
            }

        }
    }
    #endregion

}
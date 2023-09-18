using AlexAstudilloERP.Domain.Entities.Common;
using AlexAstudilloERP.Domain.Entities.Integration;
using AlexAstudilloERP.Domain.Entities.Json;
using AlexAstudilloERP.Domain.Entities.Public;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text;
using File = AlexAstudilloERP.Domain.Entities.Public.File;

namespace AlexAstudilloERP.Infrastructure.Connections;

public partial class PostgreSQLContext : DbContext
{
    public PostgreSQLContext() { }

    public PostgreSQLContext(DbContextOptions<PostgreSQLContext> options) : base(options) { }

    public virtual DbSet<AuditDatum> AuditData { get; set; }

    public virtual DbSet<AuthProvider> AuthProviders { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<DataType> DataTypes { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EquivalenceTable> EquivalenceTables { get; set; }

    public virtual DbSet<Establishment> Establishments { get; set; }

    public virtual DbSet<EstablishmentType> EstablishmentTypes { get; set; }

    public virtual DbSet<File> Files { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<JobHistory> JobHistories { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Microservice> Microservices { get; set; }

    public virtual DbSet<Organization> Organizations { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<PersonDocumentType> PersonDocumentTypes { get; set; }

    public virtual DbSet<PoliticalDivision> PoliticalDivisions { get; set; }

    public virtual DbSet<PoliticalDivisionType> PoliticalDivisionTypes { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Table> Tables { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserMetadatum> UserMetadata { get; set; }

    /// <summary>
    /// Generate a custom unique code.
    /// </summary>
    /// <param name="length">The length of the code.</param>
    /// <returns>A string code.</returns>
    private static string GenerateCode(byte length = 20)
    {
        char[] _chars = new char[]
        {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
            'k', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
            'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd',
            'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'o',
            'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y',
            'z', '0', '1', '2', '3', '4', '5', '6', '7', '8',
            '9'
        };
        StringBuilder sb = new();
        Random random = new();
        for (int i = 0; i < length; i++) sb.Append(_chars[random.Next(0, _chars.Length)]);
        return sb.ToString();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<EntityEntry> entries = ChangeTracker.Entries().Where(entry =>
        {
            return entry.State == EntityState.Added || entry.State == EntityState.Modified;
        });
        foreach (EntityEntry entry in entries)
        {
            if (entry.Entity.GetType().GetProperty("UpdateDate") != null) Entry(entry.Entity).Property("UpdateDate").CurrentValue = DateTime.Now;
            if (entry.State == EntityState.Added)
            {
                if (entry.Entity.GetType().GetProperty("Code") != null && entry.Entity is not User) Entry(entry.Entity).Property("Code").CurrentValue = GenerateCode();
                if (entry.Entity.GetType().GetProperty("Active") != null) Entry(entry.Entity).Property("Active").CurrentValue = true;
                if (entry.Entity.GetType().GetProperty("CreationDate") != null) Entry(entry.Entity).Property("CreationDate").CurrentValue = DateTime.Now;
            }
            else if (entry.State == EntityState.Modified)
            {
                if (entry.Entity.GetType().GetProperty("CreationDate") != null) Entry(entry.Entity).Property("CreationDate").IsModified = false;
                if (entry.Entity.GetType().GetProperty("Code") != null) Entry(entry.Entity).Property("Code").IsModified = false;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresExtension("btree_gin")
            .HasPostgresExtension("btree_gist")
            .HasPostgresExtension("citext")
            .HasPostgresExtension("cube")
            .HasPostgresExtension("dblink")
            .HasPostgresExtension("dict_int")
            .HasPostgresExtension("dict_xsyn")
            .HasPostgresExtension("earthdistance")
            .HasPostgresExtension("fuzzystrmatch")
            .HasPostgresExtension("hstore")
            .HasPostgresExtension("intarray")
            .HasPostgresExtension("ltree")
            .HasPostgresExtension("pg_stat_statements")
            .HasPostgresExtension("pg_trgm")
            .HasPostgresExtension("pgcrypto")
            .HasPostgresExtension("pgrowlocks")
            .HasPostgresExtension("pgstattuple")
            .HasPostgresExtension("postgis")
            .HasPostgresExtension("postgis_raster")
            .HasPostgresExtension("tablefunc")
            .HasPostgresExtension("unaccent")
            .HasPostgresExtension("uuid-ossp")
            .HasPostgresExtension("xml2");

        modelBuilder.Entity<AuditDatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("audit_data_pkey");

            entity.ToTable("audit_data", "json");

            entity.HasIndex(e => e.Code, "audit_data_code_key").IsUnique();

            entity.HasIndex(e => e.TableId, "audit_data_table_id_idx");

            entity.Property(e => e.Id)
                .HasIdentityOptions(null, null, null, 2147483647L, true, null)
                .HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.LastModifiedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("last_modified_date");
            entity.Property(e => e.NewData)
                .HasColumnType("jsonb")
                .HasColumnName("new_data");
            entity.Property(e => e.OldData)
                .HasColumnType("jsonb")
                .HasColumnName("old_data");
            entity.Property(e => e.Operation)
                .HasMaxLength(1)
                .HasColumnName("operation");
            entity.Property(e => e.Origin)
                .HasMaxLength(1)
                .HasColumnName("origin");
            entity.Property(e => e.TableId).HasColumnName("table_id");
            entity.Property(e => e.UserCode)
                .HasMaxLength(128)
                .HasColumnName("user_code");

            entity.HasOne(d => d.Table).WithMany(p => p.AuditData)
                .HasForeignKey(d => d.TableId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_audit_data__table_id");
        });

        modelBuilder.Entity<AuthProvider>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("auth_providers_pkey");

            entity.ToTable("auth_providers");

            entity.HasIndex(e => e.Code, "auth_providers_code_key").IsUnique();

            entity.Property(e => e.Id)
                .HasIdentityOptions(null, null, null, null, true, null)
                .HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.CreationDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("creation_date");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_date");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("companies_pkey");

            entity.ToTable("companies");

            entity.HasIndex(e => e.Code, "companies_code_key").IsUnique();

            entity.HasIndex(e => e.OrganizationId, "companies_organization_id_idx");

            entity.HasIndex(e => e.PersonId, "companies_person_id_key").IsUnique();

            entity.Property(e => e.Id)
                .HasIdentityOptions(null, null, null, null, true, null)
                .HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.CreationDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("creation_date");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.OrganizationId).HasColumnName("organization_id");
            entity.Property(e => e.PersonId).HasColumnName("person_id");
            entity.Property(e => e.Tradename)
                .HasMaxLength(75)
                .HasColumnName("tradename");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_date");
            entity.Property(e => e.UserCode)
                .HasMaxLength(128)
                .HasColumnName("user_code");

            entity.HasOne(d => d.Organization).WithMany(p => p.Companies)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_companies__organization_id");

            entity.HasOne(d => d.Person).WithOne(p => p.Company)
                .HasForeignKey<Company>(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_companies__person_id");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("countries_pkey");

            entity.ToTable("countries");

            entity.HasIndex(e => e.Code, "countries_code_key").IsUnique();

            entity.HasIndex(e => e.RegionId, "countries_region_id_idx");

            entity.Property(e => e.Id)
                .HasIdentityOptions(null, null, null, null, true, null)
                .HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Code)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasComment("ISO 3166-1 alpha-2 codes are two-letter country codes defined in ISO 3166-1, part of the ISO 3166 standard[1] published by the International Organization for Standardization (ISO), to represent countries, dependent territories, and special areas of geographical interest.")
                .HasColumnName("code");
            entity.Property(e => e.CreationDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("creation_date");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.DialInCodes)
                .HasColumnType("jsonb")
                .HasColumnName("dial_in_codes");
            entity.Property(e => e.Name)
                .HasMaxLength(75)
                .HasColumnName("name");
            entity.Property(e => e.RegionId).HasColumnName("region_id");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_date");

            entity.HasOne(d => d.Region).WithMany(p => p.Countries)
                .HasForeignKey(d => d.RegionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_countries__region_id");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("customers_pkey");

            entity.ToTable("customers");

            entity.HasIndex(e => new { e.PersonId, e.CompanyId, e.Code }, "customers_person_id_company_id_code_key").IsUnique();

            entity.HasIndex(e => new { e.PersonId, e.CompanyId }, "customers_person_id_company_id_key").IsUnique();

            entity.Property(e => e.Id)
                .HasIdentityOptions(null, null, null, null, true, null)
                .HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Birthdate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("birthdate");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.CreationDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("creation_date");
            entity.Property(e => e.FirstName)
                .HasMaxLength(200)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(200)
                .HasColumnName("last_name");
            entity.Property(e => e.PersonId).HasColumnName("person_id");
            entity.Property(e => e.SocialReason)
                .HasMaxLength(200)
                .HasColumnName("social_reason");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_date");
            entity.Property(e => e.UserCode)
                .HasMaxLength(128)
                .HasColumnName("user_code");

            entity.HasOne(d => d.Company).WithMany(p => p.Customers)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_customers__company_id");

            entity.HasOne(d => d.Person).WithMany(p => p.Customers)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_customers__person_id");
        });

        modelBuilder.Entity<DataType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("data_types_pkey");

            entity.ToTable("data_types", "common", tb => tb.HasComment("Save all datatypes for fields"));

            entity.HasIndex(e => e.Code, "data_types_code_key").IsUnique();

            entity.Property(e => e.Id)
                .HasIdentityOptions(null, null, null, null, true, null)
                .HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.CreationDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("creation_date");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_date");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("departments_pkey");

            entity.ToTable("departments");

            entity.HasIndex(e => e.Code, "departments_code_key").IsUnique();

            entity.HasIndex(e => e.EstablishmentId, "departments_establishment_id_idx");

            entity.HasIndex(e => e.ManagerId, "departments_manager_id_idx");

            entity.Property(e => e.Id)
                .HasIdentityOptions(null, null, null, null, true, null)
                .HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.CreationDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("creation_date");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.EstablishmentId).HasColumnName("establishment_id");
            entity.Property(e => e.ManagerId).HasColumnName("manager_id");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_date");
            entity.Property(e => e.UserCode)
                .HasMaxLength(128)
                .HasColumnName("user_code");

            entity.HasOne(d => d.Establishment).WithMany(p => p.Departments)
                .HasForeignKey(d => d.EstablishmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_departments__establishment_id");

            entity.HasOne(d => d.Manager).WithMany(p => p.Departments)
                .HasForeignKey(d => d.ManagerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_departments__manager_id");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("employees_pkey");

            entity.ToTable("employees");

            entity.HasIndex(e => e.Code, "employees_code_key").IsUnique();

            entity.HasIndex(e => e.JobId, "employees_job_id_idx");

            entity.HasIndex(e => new { e.PersonId, e.CompanyId }, "employees_person_id_company_id_key").IsUnique();

            entity.Property(e => e.Id)
                .HasIdentityOptions(null, null, null, null, true, null)
                .HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Birthdate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("birthdate");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.CreationDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("creation_date");
            entity.Property(e => e.FirstName)
                .HasMaxLength(200)
                .HasColumnName("first_name");
            entity.Property(e => e.HireDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("hire_date");
            entity.Property(e => e.JobId).HasColumnName("job_id");
            entity.Property(e => e.LastName)
                .HasMaxLength(200)
                .HasColumnName("last_name");
            entity.Property(e => e.PersonId).HasColumnName("person_id");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_date");
            entity.Property(e => e.UserCode)
                .HasMaxLength(128)
                .HasColumnName("user_code");

            entity.HasOne(d => d.Company).WithMany(p => p.Employees)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_employees__company_id");

            entity.HasOne(d => d.Job).WithMany(p => p.Employees)
                .HasForeignKey(d => d.JobId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_employees__job_id");

            entity.HasOne(d => d.Person).WithMany(p => p.Employees)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_employees__person_id");
        });

        modelBuilder.Entity<EquivalenceTable>(entity =>
        {
            entity.HasKey(e => new { e.LocalValue, e.OrganizationId, e.MicroserviceId, e.TableId, e.DataTypeId }).HasName("equivalence_table_pkey");

            entity.ToTable("equivalence_table", "integration", tb => tb.HasComment("Save equivalence data for integrations"));

            entity.HasIndex(e => e.LocalDataTypeId, "equivalence_table_local_data_type_id_idx");

            entity.Property(e => e.LocalValue)
                .HasMaxLength(128)
                .HasColumnName("local_value");
            entity.Property(e => e.OrganizationId).HasColumnName("organization_id");
            entity.Property(e => e.MicroserviceId).HasColumnName("microservice_id");
            entity.Property(e => e.TableId).HasColumnName("table_id");
            entity.Property(e => e.DataTypeId).HasColumnName("data_type_id");
            entity.Property(e => e.LocalDataTypeId).HasColumnName("local_data_type_id");
            entity.Property(e => e.MicroserviceValue)
                .HasMaxLength(128)
                .HasColumnName("microservice_value");

            entity.HasOne(d => d.DataType).WithMany(p => p.EquivalenceTableDataTypes)
                .HasForeignKey(d => d.DataTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_equivalence_table__data_type_id");

            entity.HasOne(d => d.LocalDataType).WithMany(p => p.EquivalenceTableLocalDataTypes)
                .HasForeignKey(d => d.LocalDataTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_equivalence_table__local_data_type_id");

            entity.HasOne(d => d.Microservice).WithMany(p => p.EquivalenceTables)
                .HasForeignKey(d => d.MicroserviceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_equivalence_table__microservice_id");

            entity.HasOne(d => d.Organization).WithMany(p => p.EquivalenceTables)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_equivalence_table__organization_id");

            entity.HasOne(d => d.Table).WithMany(p => p.EquivalenceTables)
                .HasForeignKey(d => d.TableId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_equivalence_table__table_id");
        });

        modelBuilder.Entity<Establishment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("establishments_pkey");

            entity.ToTable("establishments");

            entity.HasIndex(e => e.Code, "establishments_code_key").IsUnique();

            entity.HasIndex(e => e.CompanyId, "establishments_company_id_idx");

            entity.HasIndex(e => e.EstablishmentTypeId, "establishments_establishment_type_id_idx");

            entity.HasIndex(e => e.LocationId, "establishments_location_id_idx");

            entity.Property(e => e.Id)
                .HasIdentityOptions(null, null, null, null, true, null)
                .HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.CreationDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("creation_date");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.EstablishmentTypeId).HasColumnName("establishment_type_id");
            entity.Property(e => e.LocationId).HasColumnName("location_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_date");
            entity.Property(e => e.UserCode)
                .HasMaxLength(128)
                .HasColumnName("user_code");

            entity.HasOne(d => d.Company).WithMany(p => p.Establishments)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_establishments__company_id");

            entity.HasOne(d => d.EstablishmentType).WithMany(p => p.Establishments)
                .HasForeignKey(d => d.EstablishmentTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_establishments__establishment_type_id");

            entity.HasOne(d => d.Location).WithMany(p => p.Establishments)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_establishments__location_id");
        });

        modelBuilder.Entity<EstablishmentType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("establishment_types_pkey");

            entity.ToTable("establishment_types");

            entity.HasIndex(e => e.Code, "establishment_types_code_key").IsUnique();

            entity.Property(e => e.Id)
                .HasIdentityOptions(null, null, null, null, true, null)
                .HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.CreationDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("creation_date");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_date");
        });

        modelBuilder.Entity<File>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("files_pkey");

            entity.ToTable("files");

            entity.HasIndex(e => e.Code, "files_code_key").IsUnique();

            entity.Property(e => e.Id)
                .HasIdentityOptions(null, null, null, null, true, null)
                .HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.CreationDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("creation_date");
            entity.Property(e => e.Extension)
                .HasMaxLength(10)
                .HasColumnName("extension");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_date");
            entity.Property(e => e.Url)
                .HasMaxLength(255)
                .HasColumnName("url");
            entity.Property(e => e.UserCode)
                .HasMaxLength(128)
                .HasColumnName("user_code");
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("genders_pkey");

            entity.ToTable("genders");

            entity.HasIndex(e => e.Code, "genders_code_key").IsUnique();

            entity.HasIndex(e => e.Name, "genders_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasIdentityOptions(null, null, null, null, true, null)
                .HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.CreationDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("creation_date");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_date");
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("jobs_pkey");

            entity.ToTable("jobs");

            entity.HasIndex(e => e.Code, "jobs_code_key").IsUnique();

            entity.HasIndex(e => e.CompanyId, "jobs_company_id_idx");

            entity.Property(e => e.Id)
                .HasIdentityOptions(null, null, null, null, true, null)
                .HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.CreationDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("creation_date");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.MaxSalary)
                .HasPrecision(19, 5)
                .HasColumnName("max_salary");
            entity.Property(e => e.MinSalary)
                .HasPrecision(19, 5)
                .HasColumnName("min_salary");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_date");
            entity.Property(e => e.UserCode)
                .HasMaxLength(128)
                .HasColumnName("user_code");

            entity.HasOne(d => d.Company).WithMany(p => p.Jobs)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_jobs__company_id");
        });

        modelBuilder.Entity<JobHistory>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.StartDate }).HasName("job_history_pkey");

            entity.ToTable("job_history");

            entity.HasIndex(e => e.DepartmentId, "job_history_department_id_idx");

            entity.HasIndex(e => e.JobId, "job_history_job_id_idx");

            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.StartDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("start_date");
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.EndDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("end_date");
            entity.Property(e => e.JobId).HasColumnName("job_id");

            entity.HasOne(d => d.Department).WithMany(p => p.JobHistories)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_job_history__department_id");

            entity.HasOne(d => d.Employee).WithMany(p => p.JobHistories)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_job_history__employee_id");

            entity.HasOne(d => d.Job).WithMany(p => p.JobHistories)
                .HasForeignKey(d => d.JobId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_job_history__job_id");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("locations_pkey");

            entity.ToTable("locations");

            entity.HasIndex(e => e.Code, "locations_code_key").IsUnique();

            entity.HasIndex(e => e.PoliticalDivisionId, "locations_political_division_id_idx");

            entity.Property(e => e.Id)
                .HasIdentityOptions(null, null, null, null, true, null)
                .HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.CreationDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("creation_date");
            entity.Property(e => e.HouseNumber)
                .HasMaxLength(15)
                .HasColumnName("house_number");
            entity.Property(e => e.Latitude).HasColumnName("latitude");
            entity.Property(e => e.Longitude).HasColumnName("longitude");
            entity.Property(e => e.MainStreet)
                .HasMaxLength(200)
                .HasColumnName("main_street");
            entity.Property(e => e.PoliticalDivisionId).HasColumnName("political_division_id");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(15)
                .HasColumnName("postal_code");
            entity.Property(e => e.SecondaryStreet)
                .HasMaxLength(200)
                .HasColumnName("secondary_street");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_date");
            entity.Property(e => e.UserCode)
                .HasMaxLength(128)
                .HasColumnName("user_code");

            entity.HasOne(d => d.PoliticalDivision).WithMany(p => p.Locations)
                .HasForeignKey(d => d.PoliticalDivisionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_locations__political_division_id");
        });

        modelBuilder.Entity<Microservice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("microservices_pkey");

            entity.ToTable("microservices", "integration");

            entity.HasIndex(e => e.Code, "microservices_code_key").IsUnique();

            entity.Property(e => e.Id)
                .HasIdentityOptions(null, null, null, null, true, null)
                .HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.CreationDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("creation_date");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_date");
        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("organizations_pkey");

            entity.ToTable("organizations");

            entity.HasIndex(e => e.Code, "organizations_code_key").IsUnique();

            entity.Property(e => e.Id)
                .HasIdentityOptions(null, null, null, null, true, null)
                .HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.CreationDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("creation_date");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(75)
                .HasColumnName("name");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_date");
            entity.Property(e => e.UserCode)
                .HasMaxLength(128)
                .HasColumnName("user_code");

            entity.HasMany(d => d.Users).WithMany(p => p.Organizations)
                .UsingEntity<Dictionary<string, object>>(
                    "OrganizationUser",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_organization_users__user_id"),
                    l => l.HasOne<Organization>().WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_organization_users__organization_id"),
                    j =>
                    {
                        j.HasKey("OrganizationId", "UserId").HasName("organization_users_pkey");
                        j.ToTable("organization_users");
                        j.IndexerProperty<short>("OrganizationId").HasColumnName("organization_id");
                        j.IndexerProperty<int>("UserId").HasColumnName("user_id");
                    });
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("permissions_pkey");

            entity.ToTable("permissions");

            entity.HasIndex(e => e.Code, "permissions_code_key").IsUnique();

            entity.Property(e => e.Id)
                .HasIdentityOptions(null, null, null, null, true, null)
                .HasColumnName("id");
            entity.Property(e => e.Action)
                .HasMaxLength(10)
                .HasColumnName("action");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Code)
                .HasMaxLength(100)
                .HasColumnName("code");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("people_pkey");

            entity.ToTable("people");

            entity.HasIndex(e => e.Code, "people_code_key").IsUnique();

            entity.HasIndex(e => e.GenderId, "people_gender_id_idx");

            entity.HasIndex(e => e.IdCard, "people_id_card_key").IsUnique();

            entity.HasIndex(e => e.PersonDocumentTypeId, "people_person_document_type_id_idx");

            entity.Property(e => e.Id)
                .HasIdentityOptions(null, null, null, null, true, null)
                .HasColumnName("id");
            entity.Property(e => e.Birthdate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("birthdate");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.FirstName)
                .HasMaxLength(200)
                .HasColumnName("first_name");
            entity.Property(e => e.GenderId).HasColumnName("gender_id");
            entity.Property(e => e.IdCard)
                .HasMaxLength(25)
                .HasColumnName("id_card");
            entity.Property(e => e.IdCardVerified).HasColumnName("id_card_verified");
            entity.Property(e => e.JuridicalPerson).HasColumnName("juridical_person");
            entity.Property(e => e.LastName)
                .HasMaxLength(200)
                .HasColumnName("last_name");
            entity.Property(e => e.PersonDocumentTypeId).HasColumnName("person_document_type_id");
            entity.Property(e => e.SocialReason)
                .HasMaxLength(255)
                .HasColumnName("social_reason");

            entity.HasOne(d => d.Gender).WithMany(p => p.People)
                .HasForeignKey(d => d.GenderId)
                .HasConstraintName("fk_people__gender_id");

            entity.HasOne(d => d.PersonDocumentType).WithMany(p => p.People)
                .HasForeignKey(d => d.PersonDocumentTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_people__person_document_type_id");
        });

        modelBuilder.Entity<PersonDocumentType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("person_document_types_pkey");

            entity.ToTable("person_document_types");

            entity.HasIndex(e => e.Code, "person_document_types_code_key").IsUnique();

            entity.HasIndex(e => new { e.CountryId, e.Name }, "person_document_types_country_id_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasIdentityOptions(null, null, null, null, true, null)
                .HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.CreationDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("creation_date");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_date");

            entity.HasOne(d => d.Country).WithMany(p => p.PersonDocumentTypes)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_person_document_types__country_id");
        });

        modelBuilder.Entity<PoliticalDivision>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("political_divisions_pkey");

            entity.ToTable("political_divisions");

            entity.HasIndex(e => e.Code, "political_divisions_code_key").IsUnique();

            entity.HasIndex(e => e.CountryId, "political_divisions_country_id_idx");

            entity.HasIndex(e => e.ParentId, "political_divisions_parent_id_idx");

            entity.HasIndex(e => e.PoliticalDivisionTypeId, "political_divisions_political_division_type_id_idx");

            entity.Property(e => e.Id)
                .HasIdentityOptions(null, null, null, null, true, null)
                .HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.CreationDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("creation_date");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");
            entity.Property(e => e.PoliticalDivisionTypeId).HasColumnName("political_division_type_id");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_date");

            entity.HasOne(d => d.Country).WithMany(p => p.PoliticalDivisions)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_political_divisions__country_id");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("fk_political_divisions__parent_id");

            entity.HasOne(d => d.PoliticalDivisionType).WithMany(p => p.PoliticalDivisions)
                .HasForeignKey(d => d.PoliticalDivisionTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_political_divisions__political_division_type_id");
        });

        modelBuilder.Entity<PoliticalDivisionType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("political_division_types_pkey");

            entity.ToTable("political_division_types");

            entity.HasIndex(e => e.Code, "political_division_types_code_key").IsUnique();

            entity.Property(e => e.Id)
                .HasIdentityOptions(null, null, null, null, true, null)
                .HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.CreationDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("creation_date");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_date");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("regions_pkey");

            entity.ToTable("regions");

            entity.HasIndex(e => e.Code, "regions_code_key").IsUnique();

            entity.Property(e => e.Id)
                .HasIdentityOptions(null, null, null, null, true, null)
                .HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.CreationDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("creation_date");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_date");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.HasIndex(e => e.Code, "roles_code_key").IsUnique();

            entity.HasIndex(e => new { e.CompanyId, e.Name }, "roles_company_id_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasIdentityOptions(null, null, null, null, true, null)
                .HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.CreationDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("creation_date");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Editable).HasColumnName("editable");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_date");
            entity.Property(e => e.UserCode)
                .HasMaxLength(128)
                .HasColumnName("user_code");

            entity.HasOne(d => d.Company).WithMany(p => p.Roles)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_roles__company_id");

            entity.HasMany(d => d.Permissions).WithMany(p => p.Roles)
                .UsingEntity<Dictionary<string, object>>(
                    "RolePermission",
                    r => r.HasOne<Permission>().WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_role_permissions__permission_id"),
                    l => l.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_role_permissions__role_id"),
                    j =>
                    {
                        j.HasKey("RoleId", "PermissionId").HasName("role_permissions_pkey");
                        j.ToTable("role_permissions");
                        j.IndexerProperty<int>("RoleId").HasColumnName("role_id");
                        j.IndexerProperty<short>("PermissionId").HasColumnName("permission_id");
                    });
        });

        modelBuilder.Entity<Table>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tables_pkey");

            entity.ToTable("tables", "common", tb => tb.HasComment("Save all available tables on the database"));

            entity.HasIndex(e => e.Code, "tables_code_key").IsUnique();

            entity.Property(e => e.Id)
                .HasIdentityOptions(null, null, null, null, true, null)
                .HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.CreationDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("creation_date");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_date");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Code, "users_code_key").IsUnique();

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.HasIndex(e => e.PersonId, "users_person_id_key").IsUnique();

            entity.HasIndex(e => e.PhoneNumber, "users_phone_number_key").IsUnique();

            entity.HasIndex(e => e.Username, "users_username_key").IsUnique();

            entity.Property(e => e.Id)
                .HasIdentityOptions(null, null, null, null, true, null)
                .HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(128)
                .HasColumnName("code");
            entity.Property(e => e.CustomClaims)
                .HasColumnType("jsonb")
                .HasColumnName("custom_claims");
            entity.Property(e => e.Disabled).HasColumnName("disabled");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(150)
                .HasColumnName("display_name");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.EmailVerified).HasColumnName("email_verified");
            entity.Property(e => e.Password)
                .HasMaxLength(128)
                .HasColumnName("password");
            entity.Property(e => e.PersonId).HasColumnName("person_id");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(30)
                .HasColumnName("phone_number");
            entity.Property(e => e.PhotoUrl)
                .HasMaxLength(255)
                .HasColumnName("photo_url");
            entity.Property(e => e.TenantId)
                .HasMaxLength(128)
                .HasColumnName("tenant_id");
            entity.Property(e => e.Username)
                .HasMaxLength(25)
                .HasColumnName("username");

            entity.HasOne(d => d.Person).WithOne(p => p.User)
                .HasForeignKey<User>(d => d.PersonId)
                .HasConstraintName("fk_users__person_id");

            entity.HasMany(d => d.AuthProviders).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserAuthProvider",
                    r => r.HasOne<AuthProvider>().WithMany()
                        .HasForeignKey("AuthProviderId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_user_auth_providers__provider_id"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_user_auth_providers__user_id"),
                    j =>
                    {
                        j.HasKey("UserId", "AuthProviderId").HasName("user_auth_providers_pkey");
                        j.ToTable("user_auth_providers");
                        j.IndexerProperty<int>("UserId").HasColumnName("user_id");
                        j.IndexerProperty<short>("AuthProviderId").HasColumnName("auth_provider_id");
                    });

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_user_roles__role_id"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_user_roles__user_id"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId").HasName("user_roles_pkey");
                        j.ToTable("user_roles");
                        j.IndexerProperty<int>("UserId").HasColumnName("user_id");
                        j.IndexerProperty<int>("RoleId").HasColumnName("role_id");
                    });
        });

        modelBuilder.Entity<UserMetadatum>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("user_metadata_pkey");

            entity.ToTable("user_metadata");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("user_id");
            entity.Property(e => e.CreationDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("creation_date");
            entity.Property(e => e.LastRefreshDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("last_refresh_date");
            entity.Property(e => e.LastSignInDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("last_sign_in_date");

            entity.HasOne(d => d.User).WithOne(p => p.UserMetadatum)
                .HasForeignKey<UserMetadatum>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_metadata__user_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

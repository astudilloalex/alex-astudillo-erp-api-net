using AlexAstudilloERP.Domain.Entities.Common;
using AlexAstudilloERP.Domain.Entities.Public;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.CodeDom.Compiler;
using System.Text;

namespace AlexAstudilloERP.Infrastructure.Connections;

public partial class PostgreSQLContext : DbContext
{
    public PostgreSQLContext() { }

    public PostgreSQLContext(DbContextOptions<PostgreSQLContext> options) : base(options) { }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<DatabaseTable> DatabaseTables { get; set; }

    public virtual DbSet<DialInCode> DialInCodes { get; set; }

    public virtual DbSet<Email> Emails { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Establishment> Establishments { get; set; }

    public virtual DbSet<EstablishmentType> EstablishmentTypes { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<JwtBlacklist> JwtBlacklists { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<PersonDocumentType> PersonDocumentTypes { get; set; }

    public virtual DbSet<Phone> Phones { get; set; }

    public virtual DbSet<PoliticalDivision> PoliticalDivisions { get; set; }

    public virtual DbSet<PoliticalDivisionType> PoliticalDivisionTypes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<User> Users { get; set; }

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
                if (entry.Entity.GetType().GetProperty("Code") != null) Entry(entry.Entity).Property("Code").CurrentValue = GenerateCode();
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

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("addresses_pkey");

            entity.ToTable("addresses");

            entity.HasIndex(e => e.Code, "addresses_code_key").IsUnique();

            entity.Property(e => e.Id)
                .HasIdentityOptions(null, null, null, null, true, null)
                .HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
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

            entity.HasOne(d => d.PoliticalDivision).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.PoliticalDivisionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("addresses_political_division_id_fkey");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("companies_pkey");

            entity.ToTable("companies");

            entity.HasIndex(e => e.Code, "companies_code_key").IsUnique();

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
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasColumnName("image_url");
            entity.Property(e => e.KeepAccount).HasColumnName("keep_account");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");
            entity.Property(e => e.PersonId).HasColumnName("person_id");
            entity.Property(e => e.SpecialTaxpayer).HasColumnName("special_taxpayer");
            entity.Property(e => e.SpecialTaxpayerNumber)
                .HasMaxLength(100)
                .HasColumnName("special_taxpayer_number");
            entity.Property(e => e.Tradename)
                .HasMaxLength(255)
                .HasColumnName("tradename");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("companies_parent_id_fkey");

            entity.HasOne(d => d.Person).WithOne(p => p.Company)
                .HasForeignKey<Company>(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("companies_person_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Companies)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("companies_user_id_fkey");

            entity.HasMany(d => d.Customers).WithMany(p => p.Companies)
                .UsingEntity<Dictionary<string, object>>(
                    "CompanyCustomer",
                    r => r.HasOne<Customer>().WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("company_customers_customer_id_fkey"),
                    l => l.HasOne<Company>().WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("company_customers_company_id_fkey"),
                    j =>
                    {
                        j.HasKey("CompanyId", "CustomerId").HasName("company_customers_pkey");
                        j.ToTable("company_customers");
                        j.IndexerProperty<int>("CompanyId").HasColumnName("company_id");
                        j.IndexerProperty<long>("CustomerId").HasColumnName("customer_id");
                    });

            entity.HasMany(d => d.Employees).WithMany(p => p.Companies)
                .UsingEntity<Dictionary<string, object>>(
                    "CompanyEmployee",
                    r => r.HasOne<Employee>().WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("company_employees_employee_id_fkey"),
                    l => l.HasOne<Company>().WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("company_employees_company_id_fkey"),
                    j =>
                    {
                        j.HasKey("CompanyId", "EmployeeId").HasName("company_employees_pkey");
                        j.ToTable("company_employees");
                        j.IndexerProperty<int>("CompanyId").HasColumnName("company_id");
                        j.IndexerProperty<long>("EmployeeId").HasColumnName("employee_id");
                    });

            entity.HasMany(d => d.Suppliers).WithMany(p => p.Companies)
                .UsingEntity<Dictionary<string, object>>(
                    "CompanySupplier",
                    r => r.HasOne<Supplier>().WithMany()
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("company_suppliers_supplier_id_fkey"),
                    l => l.HasOne<Company>().WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("company_suppliers_company_id_fkey"),
                    j =>
                    {
                        j.HasKey("CompanyId", "SupplierId").HasName("company_suppliers_pkey");
                        j.ToTable("company_suppliers");
                        j.IndexerProperty<int>("CompanyId").HasColumnName("company_id");
                        j.IndexerProperty<long>("SupplierId").HasColumnName("supplier_id");
                    });
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("countries_pkey");

            entity.ToTable("countries");

            entity.HasIndex(e => e.Code, "countries_code_key").IsUnique();

            entity.HasIndex(e => e.Name, "countries_name_key").IsUnique();

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
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_date");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("customers_pkey");

            entity.ToTable("customers");

            entity.HasIndex(e => e.Code, "customers_code_key").IsUnique();

            entity.Property(e => e.PersonId)
                .ValueGeneratedNever()
                .HasColumnName("person_id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.CreationDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("creation_date");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_date");

            entity.HasOne(d => d.Person).WithOne(p => p.Customer)
                .HasForeignKey<Customer>(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("customers_person_id_fkey");
        });

        modelBuilder.Entity<DatabaseTable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("database_tables_pkey");

            entity.ToTable("database_tables", "common");

            entity.HasIndex(e => e.Code, "database_tables_code_key").IsUnique();

            entity.HasIndex(e => e.Name, "database_tables_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasIdentityOptions(null, null, null, null, true, null)
                .HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<DialInCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("dial_in_codes_pkey");

            entity.ToTable("dial_in_codes");

            entity.HasIndex(e => e.Code, "dial_in_codes_code_key").IsUnique();

            entity.Property(e => e.Id)
                .HasIdentityOptions(null, null, null, null, true, null)
                .HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Code)
                .HasMaxLength(7)
                .HasColumnName("code");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.CreationDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("creation_date");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_date");

            entity.HasOne(d => d.Country).WithMany(p => p.DialInCodes)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_dial_in_codes__country_id");
        });

        modelBuilder.Entity<Email>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("emails_pkey");

            entity.ToTable("emails");

            entity.HasIndex(e => e.Code, "emails_code_key").IsUnique();

            entity.HasIndex(e => e.Mail, "emails_mail_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.Mail)
                .HasMaxLength(255)
                .HasColumnName("mail");
            entity.Property(e => e.Verified).HasColumnName("verified");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("employees_pkey");

            entity.ToTable("employees");

            entity.HasIndex(e => e.Code, "employees_code_key").IsUnique();

            entity.Property(e => e.PersonId)
                .ValueGeneratedNever()
                .HasColumnName("person_id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.CreationDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("creation_date");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_date");

            entity.HasOne(d => d.Person).WithOne(p => p.Employee)
                .HasForeignKey<Employee>(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employees_person_id_fkey");
        });

        modelBuilder.Entity<Establishment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("establishments_pkey");

            entity.ToTable("establishments");

            entity.HasIndex(e => e.Code, "establishments_code_key").IsUnique();

            entity.Property(e => e.Id)
                .HasIdentityOptions(null, null, null, null, true, null)
                .HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.AddressId).HasColumnName("address_id");
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
            entity.Property(e => e.Main).HasColumnName("main");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Address).WithMany(p => p.Establishments)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("establishments_address_id_fkey");

            entity.HasOne(d => d.Company).WithMany(p => p.Establishments)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("establishments_company_id_fkey");

            entity.HasOne(d => d.EstablishmentType).WithMany(p => p.Establishments)
                .HasForeignKey(d => d.EstablishmentTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("establishments_establishment_type_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Establishments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("establishments_user_id_fkey");
        });

        modelBuilder.Entity<EstablishmentType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("establishment_types_pkey");

            entity.ToTable("establishment_types");

            entity.HasIndex(e => e.Code, "establishment_type_code_key").IsUnique();

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
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_date");
        });

        modelBuilder.Entity<JwtBlacklist>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("jwt_blacklist_pkey");

            entity.ToTable("jwt_blacklist", "common");

            entity.HasIndex(e => e.Token, "jwt_blacklist_token_key").IsUnique();

            entity.Property(e => e.Id)
                .HasIdentityOptions(null, null, null, null, true, null)
                .HasColumnName("id");
            entity.Property(e => e.ExpiresAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("expires_at");
            entity.Property(e => e.Token).HasColumnName("token");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("permissions_pkey");

            entity.ToTable("permissions");

            entity.HasIndex(e => e.Code, "permissions_code_key").IsUnique();

            entity.HasIndex(e => e.Name, "permissions_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasIdentityOptions(null, null, null, null, true, null)
                .HasColumnName("id");
            entity.Property(e => e.Action)
                .HasMaxLength(10)
                .HasColumnName("action");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Code)
                .HasMaxLength(75)
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

            entity.HasIndex(e => e.IdCard, "people_id_card_key").IsUnique();

            entity.Property(e => e.Id)
                .HasIdentityOptions(null, null, null, null, true, null)
                .HasColumnName("id");
            entity.Property(e => e.Birthdate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("birthdate");
            entity.Property(e => e.DocumentTypeId).HasColumnName("document_type_id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("first_name");
            entity.Property(e => e.GenderId).HasColumnName("gender_id");
            entity.Property(e => e.IdCard)
                .HasMaxLength(25)
                .HasColumnName("id_card");
            entity.Property(e => e.JuridicalPerson).HasColumnName("juridical_person");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("last_name");
            entity.Property(e => e.SocialReason)
                .HasMaxLength(255)
                .HasColumnName("social_reason");

            entity.HasOne(d => d.DocumentType).WithMany(p => p.People)
                .HasForeignKey(d => d.DocumentTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("people_document_type_id_fkey");

            entity.HasOne(d => d.Gender).WithMany(p => p.People)
                .HasForeignKey(d => d.GenderId)
                .HasConstraintName("people_gender_id_fkey");

            entity.HasMany(d => d.Addresses).WithMany(p => p.People)
                .UsingEntity<Dictionary<string, object>>(
                    "PersonAddress",
                    r => r.HasOne<Address>().WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("person_addresses_address_id_fkey"),
                    l => l.HasOne<Person>().WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("person_addresses_person_id_fkey"),
                    j =>
                    {
                        j.HasKey("PersonId", "AddressId").HasName("person_addresses_pkey");
                        j.ToTable("person_addresses");
                        j.IndexerProperty<long>("PersonId").HasColumnName("person_id");
                        j.IndexerProperty<int>("AddressId").HasColumnName("address_id");
                    });

            entity.HasMany(d => d.Emails).WithMany(p => p.People)
                .UsingEntity<Dictionary<string, object>>(
                    "PersonEmail",
                    r => r.HasOne<Email>().WithMany()
                        .HasForeignKey("EmailId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("person_emails_email_id_fkey"),
                    l => l.HasOne<Person>().WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("person_emails_person_id_fkey"),
                    j =>
                    {
                        j.HasKey("PersonId", "EmailId").HasName("person_emails_pkey");
                        j.ToTable("person_emails");
                        j.IndexerProperty<long>("PersonId").HasColumnName("person_id");
                        j.IndexerProperty<int>("EmailId").HasColumnName("email_id");
                    });

            entity.HasMany(d => d.Phones).WithMany(p => p.People)
                .UsingEntity<Dictionary<string, object>>(
                    "PersonPhone",
                    r => r.HasOne<Phone>().WithMany()
                        .HasForeignKey("PhoneId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("person_phones_phone_id_fkey"),
                    l => l.HasOne<Person>().WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("person_phones_person_id_fkey"),
                    j =>
                    {
                        j.HasKey("PersonId", "PhoneId").HasName("person_phones_pkey");
                        j.ToTable("person_phones");
                        j.IndexerProperty<long>("PersonId").HasColumnName("person_id");
                        j.IndexerProperty<int>("PhoneId").HasColumnName("phone_id");
                    });
        });

        modelBuilder.Entity<PersonDocumentType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("person_document_types_pkey");

            entity.ToTable("person_document_types");

            entity.HasIndex(e => e.Code, "person_document_types_code_key").IsUnique();

            entity.HasIndex(e => e.Name, "person_document_types_name_key").IsUnique();

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

        modelBuilder.Entity<Phone>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("phones_pkey");

            entity.ToTable("phones");

            entity.HasIndex(e => e.Code, "phones_code_key").IsUnique();

            entity.HasIndex(e => e.PhoneNumber, "phones_phone_number_key").IsUnique();

            entity.Property(e => e.Id)
                .HasIdentityOptions(null, null, null, null, true, null)
                .HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.DialInCodeId).HasColumnName("dial_in_code_id");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(25)
                .HasColumnName("phone_number");
            entity.Property(e => e.Verified).HasColumnName("verified");

            entity.HasOne(d => d.DialInCode).WithMany(p => p.Phones)
                .HasForeignKey(d => d.DialInCodeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("phones_dial_in_code_id_fkey");
        });

        modelBuilder.Entity<PoliticalDivision>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("political_divisions_pkey");

            entity.ToTable("political_divisions");

            entity.HasIndex(e => e.Code, "political_divisions_code_key").IsUnique();

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
                .HasMaxLength(150)
                .HasColumnName("name");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");
            entity.Property(e => e.PoliticalDivisionTypeId).HasColumnName("political_division_type_id");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_date");

            entity.HasOne(d => d.Country).WithMany(p => p.PoliticalDivisions)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("political_divisions_country_id_fkey");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("political_divisions_parent_id_fkey");

            entity.HasOne(d => d.PoliticalDivisionType).WithMany(p => p.PoliticalDivisions)
                .HasForeignKey(d => d.PoliticalDivisionTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("political_divisions_political_division_type_id_fkey");
        });

        modelBuilder.Entity<PoliticalDivisionType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("political_division_types_pkey");

            entity.ToTable("political_division_types");

            entity.HasIndex(e => e.Code, "political_division_types_code_key").IsUnique();

            entity.HasIndex(e => e.Name, "political_division_types_name_key").IsUnique();

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
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Company).WithMany(p => p.Roles)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("roles_company_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Roles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("roles_user_id_fkey");

            entity.HasMany(d => d.Permissions).WithMany(p => p.Roles)
                .UsingEntity<Dictionary<string, object>>(
                    "RolePermission",
                    r => r.HasOne<Permission>().WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("role_permissions_permission_id_fkey"),
                    l => l.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("role_permissions_role_id_fkey"),
                    j =>
                    {
                        j.HasKey("RoleId", "PermissionId").HasName("role_permissions_pkey");
                        j.ToTable("role_permissions");
                        j.IndexerProperty<int>("RoleId").HasColumnName("role_id");
                        j.IndexerProperty<short>("PermissionId").HasColumnName("permission_id");
                    });
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("suppliers_pkey");

            entity.ToTable("suppliers");

            entity.HasIndex(e => e.Code, "suppliers_code_key").IsUnique();

            entity.Property(e => e.PersonId)
                .ValueGeneratedNever()
                .HasColumnName("person_id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.CreationDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("creation_date");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_date");

            entity.HasOne(d => d.Person).WithOne(p => p.Supplier)
                .HasForeignKey<Supplier>(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("suppliers_person_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Code, "users_code_key").IsUnique();

            entity.HasIndex(e => e.EmailId, "users_email_id_key").IsUnique();

            entity.HasIndex(e => e.Username, "users_username_key").IsUnique();

            entity.Property(e => e.PersonId)
                .ValueGeneratedNever()
                .HasColumnName("person_id");
            entity.Property(e => e.AccountNonExpired).HasColumnName("account_non_expired");
            entity.Property(e => e.AccountNonLocked).HasColumnName("account_non_locked");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.CreationDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("creation_date");
            entity.Property(e => e.CredentialsNonExpired).HasColumnName("credentials_non_expired");
            entity.Property(e => e.EmailId).HasColumnName("email_id");
            entity.Property(e => e.Enabled).HasColumnName("enabled");
            entity.Property(e => e.Password)
                .HasMaxLength(128)
                .HasColumnName("password");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_date");
            entity.Property(e => e.Username)
                .HasMaxLength(25)
                .HasColumnName("username");

            entity.HasOne(d => d.Email).WithOne(p => p.User)
                .HasForeignKey<User>(d => d.EmailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_email_id_fkey");

            entity.HasOne(d => d.Person).WithOne(p => p.User)
                .HasForeignKey<User>(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_person_id_fkey");

            entity.HasMany(d => d.EstablishmentsNavigation).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserEstablishment",
                    r => r.HasOne<Establishment>().WithMany()
                        .HasForeignKey("EstablishmentId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("user_establishments_establishment_id_fkey"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("user_establishments_user_id_fkey"),
                    j =>
                    {
                        j.HasKey("UserId", "EstablishmentId").HasName("user_establishments_pkey");
                        j.ToTable("user_establishments");
                        j.IndexerProperty<long>("UserId").HasColumnName("user_id");
                        j.IndexerProperty<int>("EstablishmentId").HasColumnName("establishment_id");
                    });

            entity.HasMany(d => d.RolesNavigation).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("user_roles_role_id_fkey"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("user_roles_user_id_fkey"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId").HasName("user_roles_pkey");
                        j.ToTable("user_roles");
                        j.IndexerProperty<long>("UserId").HasColumnName("user_id");
                        j.IndexerProperty<int>("RoleId").HasColumnName("role_id");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

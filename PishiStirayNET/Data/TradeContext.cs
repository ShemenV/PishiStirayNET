using Microsoft.EntityFrameworkCore;
using PishiStirayNET.Data.DbEntities;

namespace PishiStirayNET.Data;

public partial class TradeContext : DbContext
{
    public TradeContext()
    {
    }

    public TradeContext(DbContextOptions<TradeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Delivery> Deliveries { get; set; }

    public virtual DbSet<Issuepoint> Issuepoints { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Order1> Order1s { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<Orderproduct> Orderproducts { get; set; }

    public virtual DbSet<ProductDB> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Unit> Units { get; set; }

    public virtual DbSet<UserDB> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;user=root;password=1234;database=trade", ServerVersion.Parse("8.0.31-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Delivery>(entity =>
        {
            entity.HasKey(e => e.IdProvider).HasName("PRIMARY");

            entity.ToTable("delivery");

            entity.Property(e => e.IdProvider)
                .ValueGeneratedNever()
                .HasColumnName("Id_Provider");
            entity.Property(e => e.Name).HasMaxLength(65);
        });

        modelBuilder.Entity<Issuepoint>(entity =>
        {
            entity.HasKey(e => e.IdPunkta).HasName("PRIMARY");

            entity.ToTable("issuepoint");

            entity.Property(e => e.IdPunkta)
                .ValueGeneratedNever()
                .HasColumnName("id_Punkta");
            entity.Property(e => e.PunktCity)
                .HasColumnType("text")
                .HasColumnName("Punkt_City");
            entity.Property(e => e.PunktDom).HasColumnName("Punkt_Dom");
            entity.Property(e => e.PunktIndex).HasColumnName("Punkt_index");
            entity.Property(e => e.PunktStreet)
                .HasColumnType("text")
                .HasColumnName("Punkt_Street");
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.IdManafacturer).HasName("PRIMARY");

            entity.ToTable("manufacturer");

            entity.Property(e => e.IdManafacturer)
                .ValueGeneratedNever()
                .HasColumnName("Id_manafacturer");
            entity.Property(e => e.Name).HasMaxLength(65);
        });

        modelBuilder.Entity<Order1>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PRIMARY");

            entity.ToTable("order1");

            entity.HasIndex(e => e.OrderPickupPoint, "fk_point_idx");

            entity.HasIndex(e => e.OrderStatus, "fk_status_idx");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.CodePoluch).HasColumnName("Code_poluch");
            entity.Property(e => e.Fio)
                .HasMaxLength(90)
                .HasColumnName("FIO");
            entity.Property(e => e.OrderDeliveryDate).HasColumnType("datetime");
            entity.Property(e => e.OrderDeliveryDateEnd).HasColumnType("datetime");

            entity.HasOne(d => d.OrderPickupPointNavigation).WithMany(p => p.Order1s)
                .HasForeignKey(d => d.OrderPickupPoint)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_point");

            entity.HasOne(d => d.OrderStatusNavigation).WithMany(p => p.Order1s)
                .HasForeignKey(d => d.OrderStatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_status");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(e => e.IdOrderStatus).HasName("PRIMARY");

            entity.ToTable("order_status");

            entity.Property(e => e.IdOrderStatus)
                .ValueGeneratedNever()
                .HasColumnName("id_order_status");
            entity.Property(e => e.Name)
                .HasColumnType("text")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Orderproduct>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.ProductArticleNumber })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("orderproduct");

            entity.HasIndex(e => e.ProductArticleNumber, "ProductArticleNumber");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.ProductArticleNumber)
                .HasMaxLength(100)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");

            entity.HasOne(d => d.Order).WithMany(p => p.Orderproducts)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orderproduct_ibfk_1");

            entity.HasOne(d => d.ProductArticleNumberNavigation).WithMany(p => p.Orderproducts)
                .HasForeignKey(d => d.ProductArticleNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orderproduct_ibfk_2");
        });

        modelBuilder.Entity<ProductDB>(entity =>
        {
            entity.HasKey(e => e.ProductArticleNumber).HasName("PRIMARY");

            entity.ToTable("product");

            entity.HasIndex(e => e.ProductCategory, "fk_category_idx");

            entity.HasIndex(e => e.ProductManufacturer, "fk_mana_idx");

            entity.HasIndex(e => e.Delivery, "fk_provider_idx");

            entity.HasIndex(e => e.UnitOfMeasurement, "fk_unit_idx");

            entity.Property(e => e.ProductArticleNumber)
                .HasMaxLength(100)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ProductCost).HasPrecision(19, 4);
            entity.Property(e => e.ProductDescription).HasColumnType("text");
            entity.Property(e => e.ProductName).HasColumnType("text");
            entity.Property(e => e.ProductPhoto).HasColumnType("text");
            entity.Property(e => e.UnitOfMeasurement).HasColumnName("unit_of_measurement");

            entity.HasOne(d => d.DeliveryNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.Delivery)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_provider");

            entity.HasOne(d => d.ProductCategoryNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductCategory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_category");

            entity.HasOne(d => d.ProductManufacturerNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductManufacturer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_mana");

            entity.HasOne(d => d.UnitOfMeasurementNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.UnitOfMeasurement)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_unit");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => e.IdCategory).HasName("PRIMARY");

            entity.ToTable("product_category");

            entity.Property(e => e.IdCategory)
                .ValueGeneratedNever()
                .HasColumnName("Id_Category");
            entity.Property(e => e.NameCategory)
                .HasMaxLength(65)
                .HasColumnName("Name_Category");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PRIMARY");

            entity.ToTable("role");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.RoleName).HasMaxLength(100);
        });

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.HasKey(e => e.IdUnit).HasName("PRIMARY");

            entity.ToTable("unit");

            entity.Property(e => e.IdUnit)
                .ValueGeneratedNever()
                .HasColumnName("Id_Unit");
            entity.Property(e => e.Name).HasMaxLength(65);
        });

        modelBuilder.Entity<UserDB>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.UserRole, "UserRole");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.UserLogin).HasColumnType("text");
            entity.Property(e => e.UserName).HasMaxLength(100);
            entity.Property(e => e.UserPassword).HasColumnType("text");
            entity.Property(e => e.UserPatronymic).HasMaxLength(100);
            entity.Property(e => e.UserSurname).HasMaxLength(100);

            entity.HasOne(d => d.UserRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

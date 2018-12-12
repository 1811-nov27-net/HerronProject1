using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PizzaStoreApp.DataAccess
{
    public partial class PizzaStoreDBContext : DbContext
    {
        public PizzaStoreDBContext()
        {
        }

        public PizzaStoreDBContext(DbContextOptions<PizzaStoreDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<CustomerAddress> CustomerAddress { get; set; }
        public virtual DbSet<IngrediantList> IngrediantList { get; set; }
        public virtual DbSet<IngrediantsOnPizza> IngrediantsOnPizza { get; set; }
        public virtual DbSet<Invantory> Invantory { get; set; }
        public virtual DbSet<Pizza> Pizza { get; set; }
        public virtual DbSet<PizzaOrder> PizzaOrder { get; set; }
        public virtual DbSet<PizzasInOrder> PizzasInOrder { get; set; }
        public virtual DbSet<Store> Store { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(SecretString.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer", "PS");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.FavoriteStoreId).HasColumnName("FavoriteStoreID");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<CustomerAddress>(entity =>
            {
                entity.ToTable("CustomerAddress", "PS");

                entity.Property(e => e.CustomerAddressId).HasColumnName("CustomerAddressID");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Street2).HasMaxLength(100);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerAddress)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Address_Customer");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.CustomerAddress)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Address_Store");
            });

            modelBuilder.Entity<IngrediantList>(entity =>
            {
                entity.HasKey(e => e.IngrediantId)
                    .HasName("PS_Ingrediant_ID");

                entity.ToTable("IngrediantList", "PS");

                entity.Property(e => e.IngrediantId).HasColumnName("IngrediantID");

                entity.Property(e => e.IngrediantName).HasMaxLength(100);
            });

            modelBuilder.Entity<IngrediantsOnPizza>(entity =>
            {
                entity.HasKey(e => new { e.PizzaId, e.IngrediantId })
                    .HasName("PK_IoP");

                entity.ToTable("IngrediantsOnPizza", "PS");

                entity.Property(e => e.PizzaId).HasColumnName("PizzaID");

                entity.Property(e => e.IngrediantId).HasColumnName("IngrediantID");

                entity.HasOne(d => d.Ingrediant)
                    .WithMany(p => p.IngrediantsOnPizza)
                    .HasForeignKey(d => d.IngrediantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IoP_Ingrediants");

                entity.HasOne(d => d.Pizza)
                    .WithMany(p => p.IngrediantsOnPizza)
                    .HasForeignKey(d => d.PizzaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IoP_Pizza");
            });

            modelBuilder.Entity<Invantory>(entity =>
            {
                entity.HasKey(e => new { e.StoreId, e.IngrediantId });

                entity.ToTable("Invantory", "PS");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.IngrediantId).HasColumnName("IngrediantID");

                entity.HasOne(d => d.Ingrediant)
                    .WithMany(p => p.Invantory)
                    .HasForeignKey(d => d.IngrediantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ingrediant_Invantory");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Invantory)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Store_Invantory");
            });

            modelBuilder.Entity<Pizza>(entity =>
            {
                entity.ToTable("Pizza", "PS");

                entity.Property(e => e.PizzaId).HasColumnName("PizzaID");

                entity.Property(e => e.Cost).HasColumnType("money");
            });

            modelBuilder.Entity<PizzaOrder>(entity =>
            {
                entity.ToTable("PizzaOrder", "PS");

                entity.Property(e => e.PizzaOrderId).HasColumnName("PizzaOrderID");

                entity.Property(e => e.CustomerAddressId).HasColumnName("CustomerAddressID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.DatePlaced).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.TotalDue).HasColumnType("money");

                entity.HasOne(d => d.CustomerAddress)
                    .WithMany(p => p.PizzaOrder)
                    .HasForeignKey(d => d.CustomerAddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PO_CustAdd");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.PizzaOrder)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PO_Customer");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.PizzaOrder)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PO_Store");
            });

            modelBuilder.Entity<PizzasInOrder>(entity =>
            {
                entity.HasKey(e => new { e.PizzaId, e.PizzaOrderId })
                    .HasName("PK_PiO");

                entity.ToTable("PizzasInOrder", "PS");

                entity.Property(e => e.PizzaId).HasColumnName("PizzaID");

                entity.Property(e => e.PizzaOrderId).HasColumnName("PizzaOrderID");

                entity.Property(e => e.Quantity).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Pizza)
                    .WithMany(p => p.PizzasInOrder)
                    .HasForeignKey(d => d.PizzaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PiO_Pizza");

                entity.HasOne(d => d.PizzaOrder)
                    .WithMany(p => p.PizzasInOrder)
                    .HasForeignKey(d => d.PizzaOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PiO_Order");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.ToTable("Store", "PS");

                entity.HasIndex(e => e.StoreName)
                    .HasName("UQ__Store__9FD6996204A6D278")
                    .IsUnique();

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.StoreName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Street2).HasMaxLength(100);
            });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DiskInventory.Models
{
    public partial class disk_inventorytwContext : DbContext
    {
        public disk_inventorytwContext()
        {
        }

        public disk_inventorytwContext(DbContextOptions<disk_inventorytwContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<ArtistType> ArtistTypes { get; set; }
        public virtual DbSet<Borrower> Borrowers { get; set; }
        public virtual DbSet<Disk> Disks { get; set; }
        public virtual DbSet<DiskHasArtist> DiskHasArtists { get; set; }
        public virtual DbSet<DiskHasBorrower> DiskHasBorrowers { get; set; }
        public virtual DbSet<DiskType> DiskTypes { get; set; }
        public virtual DbSet<GenreType> GenreTypes { get; set; }
        public virtual DbSet<IndividualArtist> IndividualArtists { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=.\\SQLDEV01;Database=disk_inventorytw;Trusted_Connection=True;");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Artist>(entity =>
            {
                entity.ToTable("artist");

                entity.Property(e => e.ArtistId).HasColumnName("artistID");

                entity.Property(e => e.ArtistName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("artistName");

                entity.Property(e => e.ArtistTypeId).HasColumnName("artistTypeID");

                entity.HasOne(d => d.ArtistType)
                    .WithMany(p => p.Artists)
                    .HasForeignKey(d => d.ArtistTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__artist__artistTy__2E1BDC42");
            });

            modelBuilder.Entity<ArtistType>(entity =>
            {
                entity.ToTable("artistType");

                entity.Property(e => e.ArtistTypeId).HasColumnName("artistTypeID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("description")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Borrower>(entity =>
            {
                entity.ToTable("borrower");

                entity.Property(e => e.BorrowerId).HasColumnName("borrowerID");

                entity.Property(e => e.BorrowerFname)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("borrowerFName")
                    .IsFixedLength(true);

                entity.Property(e => e.BorrowerLname)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("borrowerLName")
                    .IsFixedLength(true);

                entity.Property(e => e.BorrowerPhoneNum)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("borrowerPhoneNum")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Disk>(entity =>
            {
                entity.HasKey(e => e.CdId)
                    .HasName("PK__disk__289C55A46B672D29");

                entity.ToTable("disk");

                entity.Property(e => e.CdId).HasColumnName("cdID");

                entity.Property(e => e.ArtistId).HasColumnName("artistID");

                entity.Property(e => e.CdName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("cdName")
                    .IsFixedLength(true);

                entity.Property(e => e.DiskTypeId).HasColumnName("diskTypeID");

                entity.Property(e => e.GenreId).HasColumnName("genreID");

                entity.Property(e => e.ReleaseDate)
                    .HasColumnType("date")
                    .HasColumnName("releaseDate");

                entity.Property(e => e.StatusId).HasColumnName("statusID");

                entity.HasOne(d => d.Artist)
                    .WithMany(p => p.Disks)
                    .HasForeignKey(d => d.ArtistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__disk__artistID__33D4B598");

                entity.HasOne(d => d.DiskType)
                    .WithMany(p => p.Disks)
                    .HasForeignKey(d => d.DiskTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__disk__diskTypeID__36B12243");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Disks)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__disk__genreID__34C8D9D1");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Disks)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__disk__statusID__35BCFE0A");
            });

            modelBuilder.Entity<DiskHasArtist>(entity =>
            {
                entity.HasKey(e => e.DiskArtist)
                    .HasName("PK__diskHasA__7A8126F25F0725A7");

                entity.ToTable("diskHasArtist");

                entity.Property(e => e.DiskArtist).HasColumnName("diskArtist");

                entity.Property(e => e.ArtistId).HasColumnName("artistID");

                entity.Property(e => e.CdId).HasColumnName("cdID");

                entity.HasOne(d => d.Artist)
                    .WithMany(p => p.DiskHasArtists)
                    .HasForeignKey(d => d.ArtistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__diskHasAr__artis__30F848ED");
            });

            modelBuilder.Entity<DiskHasBorrower>(entity =>
            {
                entity.HasKey(e => e.DiskBorrower)
                    .HasName("PK__diskHasB__11B77DBAD1EFB15C");

                entity.ToTable("diskHasBorrower");

                entity.Property(e => e.DiskBorrower).HasColumnName("diskBorrower");

                entity.Property(e => e.BorrowedDate)
                    .HasColumnType("date")
                    .HasColumnName("borrowedDate");

                entity.Property(e => e.BorrowerId).HasColumnName("borrowerID");

                entity.Property(e => e.CdId).HasColumnName("cdID");

                entity.Property(e => e.ReturnedDate)
                    .HasColumnType("date")
                    .HasColumnName("returnedDate");

                entity.HasOne(d => d.Borrower)
                    .WithMany(p => p.DiskHasBorrowers)
                    .HasForeignKey(d => d.BorrowerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__diskHasBo__borro__398D8EEE");

                entity.HasOne(d => d.Cd)
                    .WithMany(p => p.DiskHasBorrowers)
                    .HasForeignKey(d => d.CdId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__diskHasBor__cdID__3A81B327");
            });

            modelBuilder.Entity<DiskType>(entity =>
            {
                entity.ToTable("diskType");

                entity.Property(e => e.DiskTypeId).HasColumnName("diskTypeID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("description")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<GenreType>(entity =>
            {
                entity.HasKey(e => e.GenreId)
                    .HasName("PK__genreTyp__3C5476A20AE801C4");

                entity.ToTable("genreType");

                entity.Property(e => e.GenreId).HasColumnName("genreID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("description")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<IndividualArtist>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("IndividualArtist");

                entity.Property(e => e.ArtistId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("artistID");

                entity.Property(e => e.ArtistName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("artistName");

                entity.Property(e => e.First)
                    .HasMaxLength(60)
                    .HasColumnName("first");

                entity.Property(e => e.Last)
                    .HasMaxLength(60)
                    .HasColumnName("last");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("status");

                entity.Property(e => e.StatusId).HasColumnName("statusID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("description")
                    .IsFixedLength(true);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

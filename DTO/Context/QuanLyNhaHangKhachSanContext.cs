using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using DTO.Model;

namespace DTO.Context
{
    public partial class QuanLyNhaHangKhachSanContext : DbContext
    {
        public QuanLyNhaHangKhachSanContext()
        {
        }

        public QuanLyNhaHangKhachSanContext(DbContextOptions<QuanLyNhaHangKhachSanContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ban> Bans { get; set; } = null!;  
        public virtual DbSet<DsTheoLoaiPhongTam> DsTheoLoaiPhongTams { get; set; } = null!;
        public virtual DbSet<DichVu> DichVus { get; set; } = null!;
        public virtual DbSet<GoiMon> GoiMons { get; set; } = null!;
        public virtual DbSet<HangHoa> HangHoas { get; set; } = null!;
        public virtual DbSet<KhachHang> KhachHangs { get; set; } = null!;
        public virtual DbSet<Kho> Khos { get; set; } = null!;
        public virtual DbSet<LichSuDoi> LichSuDois { get; set; } = null!;
        public virtual DbSet<LoaiBan> LoaiBans { get; set; } = null!;
        public virtual DbSet<LoaiPhieu> LoaiPhieus { get; set; } = null!;
        public virtual DbSet<LoaiPhong> LoaiPhongs { get; set; } = null!;
        public virtual DbSet<NguoiDung> NguoiDungs { get; set; } = null!;
        public virtual DbSet<PhieuDat> PhieuDats { get; set; } = null!;
        public virtual DbSet<PhieuDatBanChiTiet> PhieuDatBanChiTiets { get; set; } = null!;
        public virtual DbSet<PhieuDatPhongChiTiet> PhieuDatPhongChiTiets { get; set; } = null!;
        public virtual DbSet<PhieuNhan> PhieuNhans { get; set; } = null!;
        public virtual DbSet<PhieuNhanBanChiTiet> PhieuNhanBanChiTiets { get; set; } = null!;
        public virtual DbSet<PhieuNhanPhongChiTiet> PhieuNhanPhongChiTiets { get; set; } = null!;
        public virtual DbSet<PhieuNhap> PhieuNhaps { get; set; } = null!;
        public virtual DbSet<PhieuNhapChiTiet> PhieuNhapChiTiets { get; set; } = null!;
        public virtual DbSet<PhieuThu> PhieuThus { get; set; } = null!;
        public virtual DbSet<PhieuXuat> PhieuXuats { get; set; } = null!;
        public virtual DbSet<PhieuXuatChiTiet> PhieuXuatChiTiets { get; set; } = null!;
        public virtual DbSet<Phong> Phongs { get; set; } = null!;
        public virtual DbSet<TrangThai> TrangThais { get; set; } = null!;
        public virtual DbSet<TrangThietBi> TrangThietBis { get; set; } = null!;

        public Task<dynamic> SaveChanges()
        {
            throw new NotImplementedException();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=QuanLyNhaHangKhachSan;MultipleActiveResultSets=true;User ID=sa;Password=123456;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DsTheoLoaiPhongTam>().HasNoKey();

            modelBuilder.Entity<Ban>(entity =>
            {
                entity.ToTable("Ban");

                entity.Property(e => e.BanId).HasColumnName("BanID");

                entity.Property(e => e.LoaiBanId).HasColumnName("LoaiBanID");

                entity.Property(e => e.TenBan).HasMaxLength(50);

                entity.Property(e => e.TrangThaiId).HasColumnName("TrangThaiID");

                entity.HasOne(d => d.LoaiBan)
                    .WithMany(p => p.Bans)
                    .HasForeignKey(d => d.LoaiBanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ban_LoaiBan");

                entity.HasOne(d => d.TrangThai)
                    .WithMany(p => p.Bans)
                    .HasForeignKey(d => d.TrangThaiId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ban_TrangThai");
            });

            modelBuilder.Entity<DichVu>(entity =>
            {
                entity.ToTable("DichVu");

                entity.Property(e => e.DichVuId).HasColumnName("DichVuID");

                entity.Property(e => e.BanId).HasColumnName("BanID");

                entity.Property(e => e.GhiChu).HasMaxLength(100);

                entity.Property(e => e.HangHoaId).HasColumnName("HangHoaID");

                entity.Property(e => e.PhieuNhanId).HasColumnName("PhieuNhanID");

                entity.Property(e => e.PhongId).HasColumnName("PhongID");

                entity.Property(e => e.ThoiGian).HasColumnType("datetime");

                entity.Property(e => e.TrangThai).HasMaxLength(50);

                entity.HasOne(d => d.Ban)
                    .WithMany(p => p.DichVus)
                    .HasForeignKey(d => d.BanId)
                    .HasConstraintName("FK_DichVu_Ban");

                entity.HasOne(d => d.HangHoa)
                    .WithMany(p => p.DichVus)
                    .HasForeignKey(d => d.HangHoaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DichVu_HangHoa");

                entity.HasOne(d => d.PhieuNhan)
                    .WithMany(p => p.DichVus)
                    .HasForeignKey(d => d.PhieuNhanId)
                    .HasConstraintName("FK_DichVu_PhieuNhan");

                entity.HasOne(d => d.Phong)
                    .WithMany(p => p.DichVus)
                    .HasForeignKey(d => d.PhongId)
                    .HasConstraintName("FK_DichVu_Phong");
            });

            modelBuilder.Entity<GoiMon>(entity =>
            {
                entity.ToTable("GoiMon");

                entity.Property(e => e.GoiMonId).HasColumnName("GoiMonID");

                entity.Property(e => e.BanId).HasColumnName("BanID");

                entity.Property(e => e.GhiChu).HasMaxLength(100);

                entity.Property(e => e.HangHoaId).HasColumnName("HangHoaID");

                entity.Property(e => e.PhieuNhanId).HasColumnName("PhieuNhanID");

                entity.Property(e => e.PhongId).HasColumnName("PhongID");

                entity.Property(e => e.ThoiGian).HasColumnType("datetime");

                entity.Property(e => e.TrangThai).HasMaxLength(50);

                entity.HasOne(d => d.Ban)
                    .WithMany(p => p.GoiMons)
                    .HasForeignKey(d => d.BanId)
                    .HasConstraintName("FK_GoiMon_Ban");

                entity.HasOne(d => d.HangHoa)
                    .WithMany(p => p.GoiMons)
                    .HasForeignKey(d => d.HangHoaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GoiMon_HangHoa");

                entity.HasOne(d => d.PhieuNhan)
                    .WithMany(p => p.GoiMons)
                    .HasForeignKey(d => d.PhieuNhanId)
                    .HasConstraintName("FK_GoiMon_PhieuNhan");

                entity.HasOne(d => d.Phong)
                    .WithMany(p => p.GoiMons)
                    .HasForeignKey(d => d.PhongId)
                    .HasConstraintName("FK_GoiMon_Phong");
            });

            modelBuilder.Entity<HangHoa>(entity =>
            {
                entity.ToTable("HangHoa");

                entity.HasIndex(e => e.MaHangHoa, "UK_HangHoa");

                entity.Property(e => e.HangHoaId).HasColumnName("HangHoaID");

                entity.Property(e => e.MaHangHoa)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NhomHangHoa).HasMaxLength(50);

                entity.Property(e => e.TenHangHoa).HasMaxLength(100);

                entity.Property(e => e.TrangThai).HasMaxLength(50);
            });

            modelBuilder.Entity<KhachHang>(entity =>
            {
                entity.ToTable("KhachHang");

                entity.Property(e => e.KhachHangId).HasColumnName("KhachHangID");

                entity.Property(e => e.Cccd)
                    .HasMaxLength(50)
                    .HasColumnName("CCCD");

                entity.Property(e => e.GioiTinh).HasMaxLength(50);

                entity.Property(e => e.LoaiKhachHang).HasMaxLength(50);

                entity.Property(e => e.NgaySinh).HasColumnType("date");

                entity.Property(e => e.NoiThuongTru).HasMaxLength(50);

                entity.Property(e => e.QueQuan).HasMaxLength(50);

                entity.Property(e => e.Sdt)
                    .HasMaxLength(50)
                    .HasColumnName("SDT");

                entity.Property(e => e.TenKhachHang).HasMaxLength(100);
            });

            modelBuilder.Entity<Kho>(entity =>
            {
                entity.ToTable("Kho");

                entity.Property(e => e.KhoId).HasColumnName("KhoID");

                entity.Property(e => e.TenKho).HasMaxLength(100);
            });

            modelBuilder.Entity<LichSuDoi>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("LichSuDoi");

                entity.Property(e => e.GhiChu).HasMaxLength(50);

                entity.Property(e => e.LichSuDoiId).HasColumnName("LichSuDoiID");

                entity.Property(e => e.LyDoDoi).HasMaxLength(50);

                entity.Property(e => e.PhieuNhanId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PhieuNhanID");

                entity.Property(e => e.ViTriBanDau).HasMaxLength(50);

                entity.Property(e => e.ViTriDoi).HasMaxLength(50);

                entity.HasOne(d => d.PhieuNhan)
                    .WithMany()
                    .HasForeignKey(d => d.PhieuNhanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LichSuDoi_PhieuNhan");
            });

            modelBuilder.Entity<LoaiBan>(entity =>
            {
                entity.ToTable("LoaiBan");

                entity.Property(e => e.LoaiBanId).HasColumnName("LoaiBanID");

                entity.Property(e => e.TenLoaiBan).HasMaxLength(50);
            });

            modelBuilder.Entity<LoaiPhieu>(entity =>
            {
                entity.ToTable("LoaiPhieu");

                entity.Property(e => e.LoaiPhieuId).HasColumnName("LoaiPhieuID");

                entity.Property(e => e.MaLoaiPhieu).HasMaxLength(50);

                entity.Property(e => e.TenLoaiPhieu).HasMaxLength(50);
            });

            modelBuilder.Entity<LoaiPhong>(entity =>
            {
                entity.ToTable("LoaiPhong");

                entity.Property(e => e.LoaiPhongId).HasColumnName("LoaiPhongID");

                entity.Property(e => e.MaLoaiPhong).HasMaxLength(50);

                entity.Property(e => e.TenLoaiPhong).HasMaxLength(50);
            });

            modelBuilder.Entity<NguoiDung>(entity =>
            {
                entity.ToTable("NguoiDung");

                entity.HasIndex(e => e.Cccd, "UK_NguoiDung_CCCD")
                    .IsUnique();

                entity.HasIndex(e => e.Sdt, "UK_NguoiDung_SDT")
                    .IsUnique();

                entity.Property(e => e.NguoiDungId).HasColumnName("NguoiDungID");

                entity.Property(e => e.Cccd)
                    .HasMaxLength(50)
                    .HasColumnName("CCCD");

                entity.Property(e => e.DiaChi).HasMaxLength(500);

                entity.Property(e => e.GioiTinh).HasMaxLength(50);

                entity.Property(e => e.LoaiTaiKhoan).HasMaxLength(50);

                entity.Property(e => e.MatKhau).HasMaxLength(50);

                entity.Property(e => e.Sdt)
                    .HasMaxLength(50)
                    .HasColumnName("SDT");

                entity.Property(e => e.TaiKhoan).HasMaxLength(50);

                entity.Property(e => e.TenNguoiDung).HasMaxLength(100);
            });

            modelBuilder.Entity<PhieuDat>(entity =>
            {
                entity.ToTable("PhieuDat");

                entity.Property(e => e.PhieuDatId).HasColumnName("PhieuDatID");

                entity.Property(e => e.GhiChu).HasMaxLength(500);

                entity.Property(e => e.KhachHangId).HasColumnName("KhachHangID");

                entity.Property(e => e.LoaiPhieuId).HasColumnName("LoaiPhieuID");

                entity.Property(e => e.NgayLap).HasColumnType("datetime");

                entity.Property(e => e.NguoiDungId).HasColumnName("NguoiDungID");

                entity.Property(e => e.SoChungtu)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ThoiGianNhanDuKien).HasColumnType("datetime");

                entity.Property(e => e.ThoiGianTraDuKien).HasColumnType("datetime");

                entity.Property(e => e.TrangThai).HasMaxLength(50);

                entity.HasOne(d => d.KhachHang)
                    .WithMany(p => p.PhieuDats)
                    .HasForeignKey(d => d.KhachHangId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PhieuDat_KhachHang");

                entity.HasOne(d => d.LoaiPhieu)
                    .WithMany(p => p.PhieuDats)
                    .HasForeignKey(d => d.LoaiPhieuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PhieuDat_LoaiPhieu");

                entity.HasOne(d => d.NguoiDung)
                    .WithMany(p => p.PhieuDats)
                    .HasForeignKey(d => d.NguoiDungId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PhieuDat_NguoiDung");
            });

            modelBuilder.Entity<PhieuDatBanChiTiet>(entity =>
            {
                entity.ToTable("PhieuDatBanChiTiet");

                entity.Property(e => e.PhieuDatBanChiTietId).HasColumnName("PhieuDatBanChiTietID");

                entity.Property(e => e.BanId).HasColumnName("BanID");

                entity.Property(e => e.PhieuDatId).HasColumnName("PhieuDatID");

                entity.HasOne(d => d.Ban)
                    .WithMany(p => p.PhieuDatBanChiTiets)
                    .HasForeignKey(d => d.BanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PhieuDatBanChiTiet_Ban");

                entity.HasOne(d => d.PhieuDat)
                    .WithMany(p => p.PhieuDatBanChiTiets)
                    .HasForeignKey(d => d.PhieuDatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PhieuDatBanChiTiet_PhieuDat");
            });

            modelBuilder.Entity<PhieuDatPhongChiTiet>(entity =>
            {
                entity.ToTable("PhieuDatPhongChiTiet");

                entity.Property(e => e.PhieuDatPhongChiTietId).HasColumnName("PhieuDatPhongChiTietID");

                entity.Property(e => e.PhieuDatId).HasColumnName("PhieuDatID");

                entity.Property(e => e.PhongId).HasColumnName("PhongID");

                entity.HasOne(d => d.PhieuDat)
                    .WithMany(p => p.PhieuDatPhongChiTiets)
                    .HasForeignKey(d => d.PhieuDatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PhieuDatPhongChiTiet_PhieuDat");

                entity.HasOne(d => d.Phong)
                    .WithMany(p => p.PhieuDatPhongChiTiets)
                    .HasForeignKey(d => d.PhongId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PhieuDatPhongChiTiet_Phong");
            });

            modelBuilder.Entity<PhieuNhan>(entity =>
            {
                entity.ToTable("PhieuNhan");

                entity.Property(e => e.PhieuNhanId).HasColumnName("PhieuNhanID");

                entity.Property(e => e.GhiChu).HasMaxLength(500);

                entity.Property(e => e.KhachHangId).HasColumnName("KhachHangID");

                entity.Property(e => e.LoaiPhieuId).HasColumnName("LoaiPhieuID");

                entity.Property(e => e.NgayLap).HasColumnType("datetime");

                entity.Property(e => e.NgayTra).HasColumnType("datetime");

                entity.Property(e => e.NguoiDungId).HasColumnName("NguoiDungID");

                entity.Property(e => e.SoChungTu)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TrangThai).HasMaxLength(50);

                entity.HasOne(d => d.KhachHang)
                    .WithMany(p => p.PhieuNhans)
                    .HasForeignKey(d => d.KhachHangId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PhieuNhan_KhachHang");

                entity.HasOne(d => d.LoaiPhieu)
                    .WithMany(p => p.PhieuNhans)
                    .HasForeignKey(d => d.LoaiPhieuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PhieuNhan_LoaiPhieu");

                entity.HasOne(d => d.NguoiDung)
                    .WithMany(p => p.PhieuNhans)
                    .HasForeignKey(d => d.NguoiDungId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PhieuNhan_NguoiDung");
            });

            modelBuilder.Entity<PhieuNhanBanChiTiet>(entity =>
            {
                entity.ToTable("PhieuNhanBanChiTiet");

                entity.Property(e => e.PhieuNhanBanChiTietId).HasColumnName("PhieuNhanBanChiTietID");

                entity.Property(e => e.BanId).HasColumnName("BanID");

                entity.Property(e => e.PhieuNhanId).HasColumnName("PhieuNhanID");

                entity.Property(e => e.ThoiGianNhanBan).HasColumnType("datetime");

                entity.Property(e => e.ThoiGianTraBan).HasColumnType("datetime");

                entity.HasOne(d => d.Ban)
                    .WithMany(p => p.PhieuNhanBanChiTiets)
                    .HasForeignKey(d => d.BanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PhieuNhanBanChiTiet_Ban");

                entity.HasOne(d => d.PhieuNhan)
                    .WithMany(p => p.PhieuNhanBanChiTiets)
                    .HasForeignKey(d => d.PhieuNhanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PhieuNhanBanChiTiet_PhieuNhan");
            });

            modelBuilder.Entity<PhieuNhanPhongChiTiet>(entity =>
            {
                entity.ToTable("PhieuNhanPhongChiTiet");

                entity.Property(e => e.PhieuNhanPhongChiTietId).HasColumnName("PhieuNhanPhongChiTietID");

                entity.Property(e => e.PhieuNhanId).HasColumnName("PhieuNhanID");

                entity.Property(e => e.PhongId).HasColumnName("PhongID");

                entity.Property(e => e.ThoiGianNhanPhong).HasColumnType("datetime");

                entity.Property(e => e.ThoiGianTraPhong).HasColumnType("datetime");

                entity.HasOne(d => d.PhieuNhan)
                    .WithMany(p => p.PhieuNhanPhongChiTiets)
                    .HasForeignKey(d => d.PhieuNhanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PhieuNhanPhongChiTiet_PhieuNhan");

                entity.HasOne(d => d.Phong)
                    .WithMany(p => p.PhieuNhanPhongChiTiets)
                    .HasForeignKey(d => d.PhongId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PhieuNhanPhongChiTiet_Phong");
            });

            modelBuilder.Entity<PhieuNhap>(entity =>
            {
                entity.ToTable("PhieuNhap");

                entity.Property(e => e.PhieuNhapId).HasColumnName("PhieuNhapID");

                entity.Property(e => e.GhiChu).HasMaxLength(500);

                entity.Property(e => e.KhachHangId).HasColumnName("KhachHangID");

                entity.Property(e => e.NgayLap).HasColumnType("datetime");

                entity.Property(e => e.NguoiDungId).HasColumnName("NguoiDungID");

                entity.Property(e => e.SoChungtu)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.KhachHang)
                    .WithMany(p => p.PhieuNhaps)
                    .HasForeignKey(d => d.KhachHangId)
                    .HasConstraintName("FK_PhieuNhap_KhachHang");

                entity.HasOne(d => d.NguoiDung)
                    .WithMany(p => p.PhieuNhaps)
                    .HasForeignKey(d => d.NguoiDungId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PhieuNhap_NguoiDung");
            });

            modelBuilder.Entity<PhieuNhapChiTiet>(entity =>
            {
                entity.ToTable("PhieuNhapChiTiet");

                entity.Property(e => e.PhieuNhapChiTietId).HasColumnName("PhieuNhapChiTietID");

                entity.Property(e => e.DonViTinh).HasMaxLength(50);

                entity.Property(e => e.GhiChu).HasMaxLength(500);

                entity.Property(e => e.HangHoaId).HasColumnName("HangHoaID");

                entity.Property(e => e.KhoId).HasColumnName("KhoID");

                entity.Property(e => e.PhieuNhapId).HasColumnName("PhieuNhapID");

                entity.Property(e => e.TenMatHang).HasMaxLength(100);

                entity.HasOne(d => d.HangHoa)
                    .WithMany(p => p.PhieuNhapChiTiets)
                    .HasForeignKey(d => d.HangHoaId)
                    .HasConstraintName("FK_PhieuNhapChiTiet_HangHoa");

                entity.HasOne(d => d.Kho)
                    .WithMany(p => p.PhieuNhapChiTiets)
                    .HasForeignKey(d => d.KhoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PhieuNhapChiTiet_Kho");

                entity.HasOne(d => d.PhieuNhap)
                    .WithMany(p => p.PhieuNhapChiTiets)
                    .HasForeignKey(d => d.PhieuNhapId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PhieuNhapChiTiet_PhieuNhap");
            });

            modelBuilder.Entity<PhieuThu>(entity =>
            {
                entity.ToTable("PhieuThu");

                entity.Property(e => e.PhieuThuId).HasColumnName("PhieuThuID");

                entity.Property(e => e.GhiChu).HasMaxLength(500);

                entity.Property(e => e.NgayLap).HasColumnType("datetime");

                entity.Property(e => e.PhieuNhanId).HasColumnName("PhieuNhanID");

                entity.Property(e => e.PhuongThucThanhToan).HasMaxLength(50);

                entity.Property(e => e.SoChungTu)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SoTk)
                    .HasMaxLength(100)
                    .HasColumnName("SoTK");

                entity.HasOne(d => d.PhieuNhan)
                    .WithMany(p => p.PhieuThus)
                    .HasForeignKey(d => d.PhieuNhanId)
                    .HasConstraintName("FK_PhieuThu_PhieuNhan");
            });

            modelBuilder.Entity<PhieuXuat>(entity =>
            {
                entity.ToTable("PhieuXuat");

                entity.Property(e => e.PhieuXuatId).HasColumnName("PhieuXuatID");

                entity.Property(e => e.GhiChu).HasMaxLength(500);

                entity.Property(e => e.KhachHangId).HasColumnName("KhachHangID");

                entity.Property(e => e.NgayLap).HasColumnType("datetime");

                entity.Property(e => e.NguoiDungId).HasColumnName("NguoiDungID");

                entity.Property(e => e.PhieuNhanId).HasColumnName("PhieuNhanID");

                entity.Property(e => e.SoChungTu)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.KhachHang)
                    .WithMany(p => p.PhieuXuats)
                    .HasForeignKey(d => d.KhachHangId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PhieuXuat_KhachHang");

                entity.HasOne(d => d.NguoiDung)
                    .WithMany(p => p.PhieuXuats)
                    .HasForeignKey(d => d.NguoiDungId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PhieuXuat_NguoiDung");

                entity.HasOne(d => d.PhieuNhan)
                    .WithMany(p => p.PhieuXuats)
                    .HasForeignKey(d => d.PhieuNhanId)
                    .HasConstraintName("FK_PhieuXuat_PhieuNhan");
            });

            modelBuilder.Entity<PhieuXuatChiTiet>(entity =>
            {
                entity.ToTable("PhieuXuatChiTiet");

                entity.Property(e => e.PhieuXuatChiTietId).HasColumnName("PhieuXuatChiTietID");

                entity.Property(e => e.DonViTinh).HasMaxLength(50);

                entity.Property(e => e.GhiChu).HasMaxLength(500);

                entity.Property(e => e.HangHoaId).HasColumnName("HangHoaID");

                entity.Property(e => e.PhieuXuatId).HasColumnName("PhieuXuatID");

                entity.HasOne(d => d.HangHoa)
                    .WithMany(p => p.PhieuXuatChiTiets)
                    .HasForeignKey(d => d.HangHoaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PhieuXuatChiTiet_HangHoa");

                entity.HasOne(d => d.PhieuXuat)
                    .WithMany(p => p.PhieuXuatChiTiets)
                    .HasForeignKey(d => d.PhieuXuatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PhieuXuatChiTiet_PhieuXuat");
            });

            modelBuilder.Entity<Phong>(entity =>
            {
                entity.ToTable("Phong");

                entity.Property(e => e.PhongId).HasColumnName("PhongID");

                entity.Property(e => e.LoaiPhongId).HasColumnName("LoaiPhongID");

                entity.Property(e => e.TrangThaiId).HasColumnName("TrangThaiID");

                entity.HasOne(d => d.LoaiPhong)
                    .WithMany(p => p.Phongs)
                    .HasForeignKey(d => d.LoaiPhongId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Phong_LoaiPhong");

                entity.HasOne(d => d.TrangThai)
                    .WithMany(p => p.Phongs)
                    .HasForeignKey(d => d.TrangThaiId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Phong_TrangThai");
            });

            modelBuilder.Entity<TrangThai>(entity =>
            {
                entity.ToTable("TrangThai");

                entity.HasIndex(e => e.MaTrangThai, "UK_TrangThaiPhong")
                    .IsUnique();

                entity.Property(e => e.TrangThaiId).HasColumnName("TrangThaiID");

                entity.Property(e => e.MaTrangThai)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TenTrangThai).HasMaxLength(50);
            });

            modelBuilder.Entity<TrangThietBi>(entity =>
            {
                entity.ToTable("TrangThietBi");

                entity.Property(e => e.TrangThietBiId).HasColumnName("TrangThietBiID");

                entity.Property(e => e.PhongId).HasColumnName("PhongID");

                entity.Property(e => e.TenTrangThietBi).HasMaxLength(50);

                entity.Property(e => e.TrangThaiId).HasColumnName("TrangThaiID");

                entity.HasOne(d => d.Phong)
                    .WithMany(p => p.TrangThietBis)
                    .HasForeignKey(d => d.PhongId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TrangThietBi_Phong");

                entity.HasOne(d => d.TrangThai)
                    .WithMany(p => p.TrangThietBis)
                    .HasForeignKey(d => d.TrangThaiId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TrangThietBi_TrangThai");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

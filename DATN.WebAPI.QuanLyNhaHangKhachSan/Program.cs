using DAO;
using DTO.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//abc//
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<QuanLyNhaHangKhachSanContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("connectionString")));


builder.Services.AddTransient<BanDAO, BanDAO>();
builder.Services.AddTransient<DichVuDAO, DichVuDAO>();
builder.Services.AddTransient<GoiMonDAO, GoiMonDAO>();
builder.Services.AddTransient<HangHoaDAO, HangHoaDAO>();
builder.Services.AddTransient<KhachHangDAO, KhachHangDAO>();
builder.Services.AddTransient<KhoDAO, KhoDAO>();
builder.Services.AddTransient<LichSuDoiDAO, LichSuDoiDAO>();
builder.Services.AddTransient<LoaiBanDAO, LoaiBanDAO>();
builder.Services.AddTransient<LoaiPhongDAO, LoaiPhongDAO>();
builder.Services.AddTransient<LoaiPhieuDAO, LoaiPhieuDAO>();
builder.Services.AddTransient<NguoiDungDAO, NguoiDungDAO>();
builder.Services.AddTransient<PhieuDatBanChiTietDAO, PhieuDatBanChiTietDAO>();
builder.Services.AddTransient<PhieuDatDAO, PhieuDatDAO>();
builder.Services.AddTransient<PhieuDatPhongChiTietDAO, PhieuDatPhongChiTietDAO>();
builder.Services.AddTransient<PhieuNhanBanChiTietDAO, PhieuNhanBanChiTietDAO>();
builder.Services.AddTransient<PhieuNhanDAO, PhieuNhanDAO>();
builder.Services.AddTransient<PhieuNhanPhongChiTietDAO, PhieuNhanPhongChiTietDAO>();
builder.Services.AddTransient<PhieuNhapChiTietDAO, PhieuNhapChiTietDAO>();
builder.Services.AddTransient<PhieuNhapDAO, PhieuNhapDAO>();
builder.Services.AddTransient<PhieuThuDAO, PhieuThuDAO>();
builder.Services.AddTransient<PhieuXuatDAO, PhieuXuatDAO>();
builder.Services.AddTransient<TrangThaiDAO, TrangThaiDAO>();
builder.Services.AddTransient<TrangThietBiDAO, TrangThietBiDAO>();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

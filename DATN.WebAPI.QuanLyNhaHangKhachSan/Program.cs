using DAO;
using DTO.Context;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddDbContext<QuanLyNhaHangKhachSanContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("connectionString")));
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Hay nhap token",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});
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
builder.Services.AddTransient<PhongDAO, PhongDAO>();
builder.Services.AddTransient<TrangThaiDAO, TrangThaiDAO>();
builder.Services.AddTransient<TrangThietBiDAO, TrangThietBiDAO>();
builder.Services.AddControllers();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction() || app.Environment.IsStaging())
{

    app.UseSwagger();
    if (app.Environment.IsDevelopment())
    {
        app.UseSwaggerUI();
    }
    else
    {
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.RoutePrefix = string.Empty;
        });
    }
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();









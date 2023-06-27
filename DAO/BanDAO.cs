using DTO.Context;
using DTO.Model;
using DTO.MultiTable;
using DTO.Public;
using DTO.publicDTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class BanDAO
    {

        private readonly QuanLyNhaHangKhachSanContext dbcontext;
        public BanDAO(QuanLyNhaHangKhachSanContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public async Task<ErrorMessageDTO> TimBan(int banid)
        {
            //return dbcontext.SanPhams.Where(p => p.SanPhamId == sanPhamID).FirstOrDefault();
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                Ban? item = dbcontext.Bans.Where(p => p.BanId == banid).FirstOrDefault();
                if (item == null)
                {
                    error.errorCode = Convert.ToInt32(ErrorCodeEnum.KhongTimThay).ToString();
                    error.message = ResponseDTO.GetValueError(ErrorCodeEnum.KhongTimThay);
                    error.flagThanhCong = false;
                    return await Task.FromResult(error);
                }

                error.data = item;
                error.flagThanhCong = true;
                return await Task.FromResult(error);

            }
            catch (Exception ex)
            {
                error.flagBiLoiEx = true;
                error.errorCode = Convert.ToInt32(ErrorCodeEnum.InternalServerError).ToString();
                error.message = ex.Message;
                return await Task.FromResult(error);
            }
        }

        public async Task<ErrorMessageDTO> LayDanhSachBan()
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                error.data = await dbcontext.Bans.ToListAsync();
                error.flagThanhCong = true;
                return await Task.FromResult(error);
            }
            catch (Exception ex)
            {
                error.flagBiLoiEx = true;
                error.errorCode = Convert.ToInt32(ErrorCodeEnum.InternalServerError).ToString();
                error.message = ex.Message;
                return await Task.FromResult(error);
            }
        }

        public async Task<ErrorMessageDTO> ThemBan(Ban obBan)
        {

            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                if (error.flagBiLoiEx)
                {
                    error.errorCode = Convert.ToInt32(ErrorCodeEnum.KhongTheThem).ToString();
                    error.message = ResponseDTO.GetValueError(ErrorCodeEnum.KhongTheThem);
                    return await Task.FromResult(error);
                }
                error.flagThanhCong = true;
                dbcontext.Bans.Add(obBan);
                dbcontext.SaveChanges();
                return await Task.FromResult(error);

            }
            catch (Exception)
            {
                error.flagBiLoiEx = true;
                error.errorCode = Convert.ToInt32(ErrorCodeEnum.InternalServerError).ToString();
                error.message = ResponseDTO.GetValueError(ErrorCodeEnum.InternalServerError).ToString();
                return await Task.FromResult(error);
            }

        }

        public async Task<ErrorMessageDTO> CapNhatBan(BanDTO ban)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            Ban? item = dbcontext.Bans.Where(p => p.BanId == ban.BanId).FirstOrDefault();
            try
            {
                if (error.flagBiLoiEx)
                {
                    error.errorCode = Convert.ToInt32(ErrorCodeEnum.KhongTheCapNhat).ToString();
                    error.message = ResponseDTO.GetValueError(ErrorCodeEnum.KhongTheCapNhat);
                    return await Task.FromResult(error);
                }
                error.flagThanhCong = true;
                item.TrangThaiId = ban.TrangThaiId;               
                await dbcontext.SaveChangesAsync();
                error.data = item;
                error.errorCode = Convert.ToInt32(ErrorCodeEnum.NoError).ToString();
                error.message = ResponseDTO.GetValueError(ErrorCodeEnum.CapNhatThanhCong);
                return await Task.FromResult(error);
            }
            catch (Exception)
            {

                error.flagBiLoiEx = true;
                error.errorCode = Convert.ToInt32(ErrorCodeEnum.InternalServerError).ToString();
                error.message = ResponseDTO.GetValueError(ErrorCodeEnum.InternalServerError).ToString();
                return await Task.FromResult(error);

            }


        }
        public async Task<ErrorMessageDTO> Xoa(int banid)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            Ban? item = dbcontext.Bans.Where(p => p.BanId == banid).FirstOrDefault();
            try
            {
                if (item == null)
                {
                    error.errorCode = Convert.ToInt32(ErrorCodeEnum.KhongTimThay).ToString();
                    error.message = ResponseDTO.GetValueError(ErrorCodeEnum.KhongTimThay);
                    return await Task.FromResult(error);
                }

                dbcontext.Remove(item);
                error.data = await dbcontext.SaveChangesAsync();
                error.flagThanhCong = true;
                error.errorCode = Convert.ToInt32(ErrorCodeEnum.XoaThanhCong).ToString();
                error.message = ResponseDTO.GetValueError(ErrorCodeEnum.XoaThanhCong);
                return await Task.FromResult(error);
            }
            catch (Exception)
            {

                error.flagBiLoiEx = true;
                error.errorCode = Convert.ToInt32(ErrorCodeEnum.InternalServerError).ToString();
                error.message = ResponseDTO.GetValueError(ErrorCodeEnum.InternalServerError).ToString();
                return await Task.FromResult(error);

            }


        }

        public async Task<ErrorMessageDTO> DoiBan(DoiBan doiBan)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                List<GoiMon>? goiMons = await dbcontext.GoiMons.Where(p => p.BanId == doiBan.viTriBanDau.BanId && p.TrangThai == "chưa thanh toán").ToListAsync();
                PhieuNhanBanChiTiet? phieuNhanBanChiTiet = await dbcontext.PhieuNhanBanChiTiets.Where(p => p.BanId == doiBan.viTriBanDau.BanId && p.TrangThai == 4).FirstOrDefaultAsync();
                PhieuNhan phieuNhan = await dbcontext.PhieuNhans.Where(p => p.PhieuNhanId == phieuNhanBanChiTiet.PhieuNhanId).FirstOrDefaultAsync();


                if (goiMons.Count() > 0)
                {
                    foreach (GoiMon goiMon in goiMons)
                    {
                        goiMon.BanId = doiBan.viTriDoi.BanId;
                        await dbcontext.SaveChangesAsync();
                    }
                }
                if (phieuNhanBanChiTiet != null)
                {
                    phieuNhanBanChiTiet.BanId = doiBan.viTriDoi.BanId;
                }

                Ban? viTriBanDau = await dbcontext.Bans.Where(p => p.BanId == doiBan.viTriBanDau.BanId).FirstOrDefaultAsync();
                viTriBanDau.TrangThaiId = 1;
                Ban? viTriDoi = await dbcontext.Bans.Where(p => p.BanId == doiBan.viTriDoi.BanId).FirstOrDefaultAsync();
                viTriDoi.TrangThaiId = 4;
                await dbcontext.SaveChangesAsync();

                LichSuDoiDTO lichSuDoiDTO = new LichSuDoiDTO();
                lichSuDoiDTO.ViTriBanDau = doiBan.viTriBanDau.TenBan;
                lichSuDoiDTO.ViTriDoi = doiBan.viTriDoi.TenBan;
                lichSuDoiDTO.LyDoDoi = doiBan.lyDoDoi;
                lichSuDoiDTO.GhiChu = doiBan.ghiChu;
                lichSuDoiDTO.PhieuNhanId = phieuNhan.PhieuNhanId;


                dbcontext.LichSuDois.Add(lichSuDoiDTO);
                error.data = await dbcontext.SaveChangesAsync();
                error.flagThanhCong = true;
                return await Task.FromResult(error);
            }
            catch (Exception ex)
            {
                error.flagBiLoiEx = true;
                error.errorCode = Convert.ToInt32(ErrorCodeEnum.InternalServerError).ToString();
                error.message = ex.Message;
                return await Task.FromResult(error);
            }
        }
    }
}

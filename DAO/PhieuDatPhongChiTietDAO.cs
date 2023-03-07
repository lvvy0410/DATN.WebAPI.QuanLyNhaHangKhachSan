using DTO.Context;
using DTO.Model;
using DTO.Public;
using DTO.publicDTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class PhieuDatPhongChiTietDAO
    {
        private readonly QuanLyNhaHangKhachSanContext dbcontext;

        public PhieuDatPhongChiTietDAO(QuanLyNhaHangKhachSanContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public async Task<ErrorMessageDTO> LayDanhSachPhieuDatPhongChiTiet()
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                error.data = await dbcontext.PhieuDatPhongChiTiets.ToListAsync();
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
        public async Task<ErrorMessageDTO> LayPhieuDatPhongChiTiet(int PhieuDatPhongChiTietID)
        {
            //return dbcontext.PhieuDatPhongChiTiets.Where(p => p.PhieuDatPhongChiTietID == PhieuDatPhongChiTietID).FirstOrDefault();
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                PhieuDatPhongChiTiet? item = await dbcontext.PhieuDatPhongChiTiets.Where(p => p.PhieuDatPhongChiTietId == PhieuDatPhongChiTietID).FirstOrDefaultAsync();
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
        public async Task<ErrorMessageDTO> ThemPhieuDatPhongChiTiet(PhieuDatPhongChiTietDTO obPhieuDatPhongChiTiet)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                try
                {
                    dbcontext.PhieuDatPhongChiTiets.Add(obPhieuDatPhongChiTiet);
                    error.data = await dbcontext.SaveChangesAsync();
                    error.flagThanhCong = true;
                    return await Task.FromResult(error);
                }
                catch
                {
                    error.errorCode = Convert.ToInt32(ErrorCodeEnum.KhongTheThem).ToString();
                    error.message = ResponseDTO.GetValueError(ErrorCodeEnum.KhongTheThem);
                    error.flagThanhCong = false;
                    return await Task.FromResult(error);
                }
            }
            catch (Exception ex)
            {
                error.errorCode = Convert.ToInt32(ErrorCodeEnum.InternalServerError).ToString();
                error.message = ResponseDTO.GetValueError(ErrorCodeEnum.InternalServerError);
                error.flagBiLoiEx = true;
                return await Task.FromResult(error);
            }
        }
        public async Task<ErrorMessageDTO> CapNhatPhieuDatPhongChiTiet(PhieuDatPhongChiTietDTO obPhieuDatPhongChiTiet)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                PhieuDatPhongChiTiet? obPhieuDatPhongChiTietId = dbcontext.PhieuDatPhongChiTiets.Where(p => p.PhieuDatPhongChiTietId == obPhieuDatPhongChiTiet.PhieuDatPhongChiTietId).FirstOrDefault();
                if (obPhieuDatPhongChiTietId == null)
                {
                    error.errorCode = Convert.ToInt32(ErrorCodeEnum.KhongTimThay).ToString();
                    error.message = ResponseDTO.GetValueError(ErrorCodeEnum.KhongTimThay);
                    error.flagThanhCong = false;
                    return await Task.FromResult(error);
                }
                obPhieuDatPhongChiTietId.SoNguoi = obPhieuDatPhongChiTiet.SoNguoi;
                error.data = await dbcontext.SaveChangesAsync();
                error.flagThanhCong = true;
                return await Task.FromResult(error);
            }
            catch (Exception ex)
            {
                error.errorCode = Convert.ToInt32(ErrorCodeEnum.InternalServerError).ToString();
                error.message = ResponseDTO.GetValueError(ErrorCodeEnum.InternalServerError);
                error.flagBiLoiEx = true;
                return await Task.FromResult(error);
            }
        }
        public async Task<ErrorMessageDTO> XoaPhieuDatPhongChiTiet(int PhieuDatPhongChiTietID)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                PhieuDatPhongChiTiet? item = await dbcontext.PhieuDatPhongChiTiets.Where(p => p.PhieuDatPhongChiTietId == PhieuDatPhongChiTietID).FirstOrDefaultAsync();
                if (item == null)
                {
                    error.errorCode = Convert.ToInt32(ErrorCodeEnum.NotFound).ToString();
                    error.message = ResponseDTO.GetValueError(ErrorCodeEnum.KhongTimThay);
                    error.flagThanhCong = false;
                    return await Task.FromResult(error);
                }
                dbcontext.PhieuDatPhongChiTiets.Remove(item);
                error.data = await dbcontext.SaveChangesAsync();
                error.errorCode = Convert.ToInt32(ErrorCodeEnum.XoaThanhCong).ToString();
                error.message = ErrorCodeEnum.XoaThanhCong.ToString();
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

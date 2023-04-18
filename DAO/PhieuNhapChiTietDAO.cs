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
    public class PhieuNhapChiTietDAO
    {
        private readonly QuanLyNhaHangKhachSanContext dbcontext;

        public PhieuNhapChiTietDAO(QuanLyNhaHangKhachSanContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        
        public async Task<ErrorMessageDTO> LayPhieuNhapChiTiet(int PhieuNhapChiTietID)
        {
            //return dbcontext.PhieuNhapChiTiets.Where(p => p.PhieuNhapChiTietID == PhieuNhapChiTietID).FirstOrDefault();
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                PhieuNhapChiTiet? item = await dbcontext.PhieuNhapChiTiets.Where(p => p.PhieuNhapChiTietId == PhieuNhapChiTietID).FirstOrDefaultAsync();
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
        public async Task<ErrorMessageDTO> ThemPhieuNhapChiTiet(PhieuNhapChiTietDTO obPhieuNhapChiTiet)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
               error.flagThanhCong = true;
                    dbcontext.PhieuNhapChiTiets.Add(obPhieuNhapChiTiet);
                    error.data = await dbcontext.SaveChangesAsync();
                   
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
        public async Task<ErrorMessageDTO> CapNhatPhieuNhapChiTiet(PhieuNhapChiTietDTO obPhieuNhapChiTiet)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            PhieuNhapChiTiet? obPhieuNhapChiTietId = dbcontext.PhieuNhapChiTiets.Where(p=>p.PhieuNhapChiTietId==obPhieuNhapChiTiet.PhieuNhapChiTietId).FirstOrDefault();
            try
            {
             
                if (obPhieuNhapChiTietId == null)
                {
                    error.errorCode = Convert.ToInt32(ErrorCodeEnum.KhongTimThay).ToString();
                    error.message = ResponseDTO.GetValueError(ErrorCodeEnum.KhongTimThay);
                    error.flagThanhCong = false;
                    return await Task.FromResult(error);
                }

                obPhieuNhapChiTietId.TenMatHang = obPhieuNhapChiTiet.TenMatHang;
                obPhieuNhapChiTietId.SoLuong = obPhieuNhapChiTiet.SoLuong;
                obPhieuNhapChiTietId.Gia = obPhieuNhapChiTiet.Gia;


               await dbcontext.SaveChangesAsync();
                error.data = obPhieuNhapChiTietId;
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
        public async Task<ErrorMessageDTO> XoaPhieuNhapChiTiet(int PhieuNhapChiTietID)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                PhieuNhapChiTiet? item = await dbcontext.PhieuNhapChiTiets.Where(p => p.PhieuNhapChiTietId == PhieuNhapChiTietID).FirstOrDefaultAsync();
                if (item == null)
                {
                    error.errorCode = Convert.ToInt32(ErrorCodeEnum.NotFound).ToString();
                    error.message = ResponseDTO.GetValueError(ErrorCodeEnum.KhongTimThay);
                    error.flagThanhCong = false;
                    return await Task.FromResult(error);
                }
                dbcontext.PhieuNhapChiTiets.Remove(item);
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

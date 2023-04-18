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
    public class PhieuNhapDAO
    {
        private readonly QuanLyNhaHangKhachSanContext dbcontext;

        public PhieuNhapDAO(QuanLyNhaHangKhachSanContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public async Task<ErrorMessageDTO> LayPhieuNhap(int PhieuNhapID)
        {
            //return dbcontext.PhieuNhaps.Where(p => p.PhieuNhapID == PhieuNhapID).FirstOrDefault();
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                PhieuNhap? item = await dbcontext.PhieuNhaps.Where(p => p.PhieuNhapId == PhieuNhapID).FirstOrDefaultAsync();
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
        public async Task<ErrorMessageDTO> LayDanhSachPhieuNhap()
        {
           ErrorMessageDTO error=new ErrorMessageDTO();
            try
            {
                error.data=await dbcontext.PhieuNhans.ToListAsync();
                error.flagThanhCong=true;
                return await Task.FromResult(error);
            }
            catch(Exception ex)
            {
                error.flagBiLoiEx = true;
                error.errorCode = Convert.ToInt32(ErrorCodeEnum.InternalServerError).ToString();
                error.message = ex.Message;
                return await Task.FromResult(error);
            }
        }
        public async Task<ErrorMessageDTO> ThemPhieuNhap(PhieuNhapDTO obPhieuNhap)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                
                 if(error.flagBiLoiEx) { 
                    error.errorCode = Convert.ToInt32(ErrorCodeEnum.KhongTheThem).ToString();
                    error.message = ResponseDTO.GetValueError(ErrorCodeEnum.KhongTheThem);
                    error.flagThanhCong = false;
                    return await Task.FromResult(error);
                }
                    dbcontext.PhieuNhaps.Add(obPhieuNhap);
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
        public async Task<ErrorMessageDTO> CapNhatPhieuNhap(PhieuNhapDTO obPhieuNhap)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            PhieuNhap? obPhieuNhapId = dbcontext.PhieuNhaps.Where(p=>p.PhieuNhapId==obPhieuNhap.PhieuNhapId).FirstOrDefault();
            try
            {

                if (obPhieuNhapId == null)
                {
                    error.errorCode = Convert.ToInt32(ErrorCodeEnum.KhongTimThay).ToString();
                    error.message = ResponseDTO.GetValueError(ErrorCodeEnum.KhongTimThay);
                    error.flagThanhCong = false;
                    return await Task.FromResult(error);
                }

                obPhieuNhapId.GhiChu = obPhieuNhap.GhiChu;
               


                await dbcontext.SaveChangesAsync();
                error.data = obPhieuNhapId;
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
        public async Task<ErrorMessageDTO> XoaPhieuNhap(int PhieuNhapID)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                PhieuNhap? item = await dbcontext.PhieuNhaps.Where(p => p.PhieuNhapId == PhieuNhapID).FirstOrDefaultAsync();
                if (item == null)
                {
                    error.errorCode = Convert.ToInt32(ErrorCodeEnum.NotFound).ToString();
                    error.message = ResponseDTO.GetValueError(ErrorCodeEnum.KhongTimThay);
                    error.flagThanhCong = false;
                    return await Task.FromResult(error);
                }
                dbcontext.PhieuNhaps.Remove(item);
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

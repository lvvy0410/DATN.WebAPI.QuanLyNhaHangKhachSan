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
    public class KhachHangDAO
    {
        private readonly QuanLyNhaHangKhachSanContext dbcontext;

        public KhachHangDAO(QuanLyNhaHangKhachSanContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public async Task<ErrorMessageDTO> LayDanhSachKhachHang()
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                error.data = await dbcontext.KhachHangs.ToListAsync();
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
        public async Task<ErrorMessageDTO> TimKhachHang(int KhachHangID)
        {
            //return dbcontext.KhachHangs.Where(p => p.KhachHangID == KhachHangID).FirstOrDefault();
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                KhachHang? item = await dbcontext.KhachHangs.Where(p => p.KhachHangId == KhachHangID).FirstOrDefaultAsync();
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
        public async Task<ErrorMessageDTO> ThemKhachHang(KhachHang obKhachHang)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                try
                {
                    dbcontext.KhachHangs.Add(obKhachHang);
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
        public async Task<ErrorMessageDTO> CapNhatKhachHang(KhachHang obKhachHang)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                KhachHang? obKhachHangId = dbcontext.KhachHangs.Where(p => p.KhachHangId == obKhachHang.KhachHangId).FirstOrDefault();
                if (obKhachHangId == null)
                {
                    error.errorCode = Convert.ToInt32(ErrorCodeEnum.KhongTimThay).ToString();
                    error.message = ResponseDTO.GetValueError(ErrorCodeEnum.KhongTimThay);
                    error.flagThanhCong = false;
                    return await Task.FromResult(error);
                }
                obKhachHangId.TenKhachHang = obKhachHang.TenKhachHang;
                obKhachHangId.Sdt = obKhachHang.Sdt;
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
        public async Task<ErrorMessageDTO> XoaKhachHang(int KhachHangID)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                KhachHang? item = await dbcontext.KhachHangs.Where(p => p.KhachHangId == KhachHangID).FirstOrDefaultAsync();
                if (item == null)
                {
                    error.errorCode = Convert.ToInt32(ErrorCodeEnum.NotFound).ToString();
                    error.message = ResponseDTO.GetValueError(ErrorCodeEnum.KhongTimThay);
                    error.flagThanhCong = false;
                    return await Task.FromResult(error);
                }
                dbcontext.KhachHangs.Remove(item);
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

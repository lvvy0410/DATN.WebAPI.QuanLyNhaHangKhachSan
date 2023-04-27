using DTO.Context;
using DTO.DieuKienLoc;
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
    public class PhieuNhanPhongChiTietDAO
    {
        private readonly QuanLyNhaHangKhachSanContext dbcontext;

        public PhieuNhanPhongChiTietDAO(QuanLyNhaHangKhachSanContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public async Task<ErrorMessageDTO> LayDanhSachPhieuNhanPhongChiTiet(DieuKienLocPhieuNhanPhongChiTiet oblay)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                error.data = await dbcontext.PhieuNhanPhongChiTiets.FromSqlRaw($"LayPhieuNhanChiTiet'{oblay.PhieuNhanPhongChiTietId}'," + $"'{oblay.PhieuNhanId}'," + $"'{oblay.PhongId}'").ToListAsync();

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
        public async Task<ErrorMessageDTO> LayPhieuNhanPhongChiTiet(int PhieuNhanPhongChiTietID)
        {
            //return dbcontext.PhieuNhanPhongChiTiets.Where(p => p.PhieuNhanPhongChiTietID == PhieuNhanPhongChiTietID).FirstOrDefault();
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                PhieuNhanPhongChiTiet? item = await dbcontext.PhieuNhanPhongChiTiets.Where(p => p.PhieuNhanPhongChiTietId == PhieuNhanPhongChiTietID).FirstOrDefaultAsync();
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
        public async Task<ErrorMessageDTO> ThemPhieuNhanPhongChiTiet(PhieuNhanPhongChiTietDTO obPhieuNhanPhongChiTiet)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                try
                {
                    dbcontext.PhieuNhanPhongChiTiets.Add(obPhieuNhanPhongChiTiet);
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
        public async Task<ErrorMessageDTO> CapNhatPhieuNhanPhongChiTiet(PhieuNhanPhongChiTietDTO obPhieuNhanPhongChiTiet)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                PhieuNhanPhongChiTiet? obPhieuNhanPhongChiTietId = dbcontext.PhieuNhanPhongChiTiets.Where(p => p.PhieuNhanPhongChiTietId == obPhieuNhanPhongChiTiet.PhieuNhanPhongChiTietId).FirstOrDefault();
                if (obPhieuNhanPhongChiTietId == null)
                {
                    error.errorCode = Convert.ToInt32(ErrorCodeEnum.KhongTimThay).ToString();
                    error.message = ResponseDTO.GetValueError(ErrorCodeEnum.KhongTimThay);
                    error.flagThanhCong = false;
                    return await Task.FromResult(error);
                }
                obPhieuNhanPhongChiTietId.SoNguoi = obPhieuNhanPhongChiTiet.SoNguoi;
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
        public async Task<ErrorMessageDTO> XoaPhieuNhanPhongChiTiet(int PhieuNhanPhongChiTietID)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                PhieuNhanPhongChiTiet? item = await dbcontext.PhieuNhanPhongChiTiets.Where(p => p.PhieuNhanPhongChiTietId == PhieuNhanPhongChiTietID).FirstOrDefaultAsync();
                if (item == null)
                {
                    error.errorCode = Convert.ToInt32(ErrorCodeEnum.NotFound).ToString();
                    error.message = ResponseDTO.GetValueError(ErrorCodeEnum.KhongTimThay);
                    error.flagThanhCong = false;
                    return await Task.FromResult(error);
                }
                dbcontext.PhieuNhanPhongChiTiets.Remove(item);
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

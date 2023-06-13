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
    public class PhieuNhanBanChiTietDAO
    {
        private readonly QuanLyNhaHangKhachSanContext dbcontext;

        public PhieuNhanBanChiTietDAO(QuanLyNhaHangKhachSanContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

     
        public async Task<ErrorMessageDTO> LayPhieuNhanBanChiTiet(DieuKienLocPhieuNhanBanChiTiet phieuNhan)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
        error.data = await dbcontext.PhieuNhanBanChiTiets.FromSqlRaw($"LayPhieuNhanBanChiTiet'{phieuNhan.PhieuNhanBanChiTietId}',"+ $"'{phieuNhan.PhieuNhanId}'," + $"'{phieuNhan.BanId}'").ToListAsync();
         
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
        public async Task<ErrorMessageDTO> CapNhatPhieuNhanBanChiTiet(PhieuNhanBanChiTietDTO ob)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                PhieuNhanBanChiTiet? obID = dbcontext.PhieuNhanBanChiTiets.Where(p => p.PhieuNhanBanChiTietId == ob.PhieuNhanBanChiTietId).FirstOrDefault();
                if (obID == null)
                {
                    error.errorCode = Convert.ToInt32(ErrorCodeEnum.KhongTimThay).ToString();
                    error.message = ResponseDTO.GetValueError(ErrorCodeEnum.KhongTimThay);
                    error.flagThanhCong = false;
                    return await Task.FromResult(error);
                }
                obID.ThoiGianTraBan = ob.ThoiGianTraBan;
                obID.TrangThai = ob.TrangThai;
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
    }
}

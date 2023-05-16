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
    public class PhieuXuatDAO
    {

        private readonly QuanLyNhaHangKhachSanContext dbcontext;

        public PhieuXuatDAO(QuanLyNhaHangKhachSanContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public async Task<ErrorMessageDTO> LayDanhSachPhieuXuat(DieuKienLocPhieuXuat? obPhieuXuat)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                error.data = await dbcontext.PhieuXuats.FromSqlRaw($"LayPhieuXuat '{obPhieuXuat.PhieuXuatId}', '{obPhieuXuat.SoChungTu}'," +$"'{obPhieuXuat.KhachHangId}', '{obPhieuXuat.Trangthai}'").ToListAsync();
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
       public async Task<ErrorMessageDTO> ThemPhieuXuat(PhieuXuatDTO phieuXuat)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                dbcontext.PhieuXuats.Add(phieuXuat);
                error.data = await dbcontext.SaveChangesAsync();
                error.flagThanhCong=true;
                return await Task.FromResult(error);
            }
            catch (Exception ex)
            {
                error.flagBiLoiEx = true;
                error.message=ResponseDTO.GetValueError(ErrorCodeEnum.InternalServerError).ToString();
                error.errorCode=Convert.ToInt32(ErrorCodeEnum.InternalServerError).ToString() ;
                return await Task.FromResult(error);
            }

        }


    }
}

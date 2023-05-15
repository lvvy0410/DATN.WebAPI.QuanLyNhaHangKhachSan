using DTO.Context;
using DTO.DieuKienLoc;
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
        public async Task<ErrorMessageDTO> ThemDV(DichVuDTO dichvu)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {

                if (error.flagBiLoiEx)
                {
                    error.errorCode = Convert.ToInt32(ErrorCodeEnum.KhongTheThem).ToString();
                    error.message = ResponseDTO.GetValueError(ErrorCodeEnum.KhongTheThem);
                    error.flagThanhCong = false;
                    return await Task.FromResult(error);
                }
                dbcontext.DichVus.Add(dichvu);
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

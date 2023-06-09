using DTO.Context;
using DTO.DieuKienLoc;
using DTO.Model;
using DTO.MultiTable;
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
       public async Task<ErrorMessageDTO> ThemPhieuXuat(XuatPhong xuatPhong)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                try
                {
                    dbcontext.PhieuXuats.Add(xuatPhong.phieuXuatDTO);
                    await dbcontext.SaveChangesAsync();

                    long phieuXuatId = xuatPhong.phieuXuatDTO.PhieuXuatId;

                    foreach (PhieuXuatChiTietDTO phieuXuatChiTietDTO in xuatPhong.phieuXuatChiTiets)
                    {
                        phieuXuatChiTietDTO.PhieuXuatId = phieuXuatId;
                        dbcontext.PhieuXuatChiTiets.Add(phieuXuatChiTietDTO);
                        await dbcontext.SaveChangesAsync();
                    }
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
                error.flagBiLoiEx = true;
                error.message=ResponseDTO.GetValueError(ErrorCodeEnum.InternalServerError).ToString();
                error.errorCode=Convert.ToInt32(ErrorCodeEnum.InternalServerError).ToString() ;
                return await Task.FromResult(error);
            }

        }
        public async Task<ErrorMessageDTO> CapNhatPX(PhieuXuatDTO dv)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            PhieuXuat? item = dbcontext.PhieuXuats.Where(p => p.PhieuXuatId == dv.PhieuXuatId).FirstOrDefault();
            try
            {
                if (error.flagBiLoiEx)
                {
                    error.errorCode = Convert.ToInt32(ErrorCodeEnum.KhongTheCapNhat).ToString();
                    error.message = ResponseDTO.GetValueError(ErrorCodeEnum.KhongTheCapNhat);
                    return await Task.FromResult(error);
                }
                error.flagThanhCong = true;
                item.TongThanhTien = dv.TongThanhTien;
                item.Trangthai=dv.Trangthai;
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


    }
}

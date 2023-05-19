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
    public class PhieuNhanDAO
    {
        private readonly QuanLyNhaHangKhachSanContext dbcontext;

        public PhieuNhanDAO(QuanLyNhaHangKhachSanContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public async Task<ErrorMessageDTO> LayDanhSachPhieuNhan(DieuKienLocPhieuNhan? obPhieuNhan)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                error.data = await dbcontext.PhieuNhans.FromSqlRaw($"LayDanhSachPhieuNhan '{obPhieuNhan.PhieuNhanId}', '{obPhieuNhan.SoChungTu}'," +
                    $"'{obPhieuNhan.LoaiPhieuId}', '{obPhieuNhan.KhachHangId}'").ToListAsync();
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
        public async Task<ErrorMessageDTO> LayPhieuNhan(int PhieuNhanID)
        {
            //return dbcontext.PhieuNhans.Where(p => p.PhieuNhanID == PhieuNhanID).FirstOrDefault();
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                PhieuNhan? item = await dbcontext.PhieuNhans.Where(p => p.PhieuNhanId == PhieuNhanID).FirstOrDefaultAsync();
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
        public async Task<ErrorMessageDTO> ThemPhieuNhan(NhanPhong nhanPhong)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                try
                {
                    dbcontext.PhieuNhans.Add(nhanPhong.phieuNhanDTO);
                    await dbcontext.SaveChangesAsync();

                    long phieuNhanId = nhanPhong.phieuNhanDTO.PhieuNhanId;

                    foreach (PhieuNhanPhongChiTietDTO phieuDatPhongChiTietDTO in nhanPhong.phieuNhanPhongChiTietDTOs)
                    {
                        phieuDatPhongChiTietDTO.PhieuNhanId = phieuNhanId;
                        dbcontext.PhieuNhanPhongChiTiets.Add(phieuDatPhongChiTietDTO);
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
                error.errorCode = Convert.ToInt32(ErrorCodeEnum.InternalServerError).ToString();
                error.message = ResponseDTO.GetValueError(ErrorCodeEnum.InternalServerError);
                error.flagBiLoiEx = true;
                return await Task.FromResult(error);
            }
        }
        public async Task<ErrorMessageDTO> CapNhatPhieuNhan(PhieuNhanDTO obPhieuNhan)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                PhieuNhan? obPhieuNhanId = dbcontext.PhieuNhans.Where(p => p.PhieuNhanId == obPhieuNhan.PhieuNhanId).FirstOrDefault();
                if (obPhieuNhanId == null)
                {
                    error.errorCode = Convert.ToInt32(ErrorCodeEnum.KhongTimThay).ToString();
                    error.message = ResponseDTO.GetValueError(ErrorCodeEnum.KhongTimThay);
                    error.flagThanhCong = false;
                    return await Task.FromResult(error);
                }
                obPhieuNhanId.SoChungTu = obPhieuNhan.SoChungTu;
                obPhieuNhanId.TrangThai = obPhieuNhan.TrangThai;
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
        public async Task<ErrorMessageDTO> XoaPhieuNhan(int PhieuNhanID)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                PhieuNhan? item = await dbcontext.PhieuNhans.Where(p => p.PhieuNhanId == PhieuNhanID).FirstOrDefaultAsync();
                if (item == null)
                {
                    error.errorCode = Convert.ToInt32(ErrorCodeEnum.NotFound).ToString();
                    error.message = ResponseDTO.GetValueError(ErrorCodeEnum.KhongTimThay);
                    error.flagThanhCong = false;
                    return await Task.FromResult(error);
                }
                dbcontext.PhieuNhans.Remove(item);
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

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
    public class PhieuDatDAO
    {
        private readonly QuanLyNhaHangKhachSanContext dbcontext;

        public PhieuDatDAO(QuanLyNhaHangKhachSanContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public async Task<ErrorMessageDTO> LayDanhSachPhieuDat(DieuKienLocPhieuDat? obPhieuDat)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                error.data = await dbcontext.PhieuDats.FromSqlRaw($"LayDanhSachPhieuDat '{obPhieuDat.PhieuDatId}', '{obPhieuDat.SoChungTu}'," +
                    $"'{obPhieuDat.loaiPhieu}', '{obPhieuDat.KhachHangId}', N'{obPhieuDat.TrangThai}'").ToListAsync();
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


        public async Task<ErrorMessageDTO> LayPhieuDat(int PhieuDatID)
        {
            //return dbcontext.PhieuDats.Where(p => p.PhieuDatID == PhieuDatID).FirstOrDefault();
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                PhieuDat? item = await dbcontext.PhieuDats.Where(p => p.PhieuDatId == PhieuDatID).FirstOrDefaultAsync();
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
        public async Task<ErrorMessageDTO> ThemPhieuDat(DatPhong datPhong)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                try
                {
                    //thêm khách hàng
                    dbcontext.KhachHangs.Add(datPhong.khachHang);
                    await dbcontext.SaveChangesAsync();

                    //lấy khách hàng id để thêm vào phiếu nhận
                    long khachHangId = datPhong.khachHang.KhachHangId;

                    //thêm phiếu đặt
                    long count = dbcontext.PhieuNhans.Count();
                    datPhong.phieuDatDTO.SoChungtu = "PN" + count + 1;
                    datPhong.phieuDatDTO.KhachHangId = khachHangId;
                    dbcontext.PhieuDats.Add(datPhong.phieuDatDTO);
                    await dbcontext.SaveChangesAsync();

                    long phieuDatId = datPhong.phieuDatDTO.PhieuDatId;

                    foreach(PhieuDatPhongChiTietDTO phieuDatPhongChiTietDAO in datPhong.phieuDatPhongChiTiets)
                    {
                        phieuDatPhongChiTietDAO.PhieuDatId = phieuDatId;
                        dbcontext.PhieuDatPhongChiTiets.Add(phieuDatPhongChiTietDAO);
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


        public async Task<ErrorMessageDTO> ThemPhieuDatBan(DatBan datBan)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                try
                {
                    //thêm khách hàng
                    dbcontext.KhachHangs.Add(datBan.khachHang);
                    await dbcontext.SaveChangesAsync();

                    //lấy khách hàng id để thêm vào phiếu nhận
                    long khachHangId = datBan.khachHang.KhachHangId;

                    //thêm phiếu đặt
                    long count = dbcontext.PhieuDats.Count();
                    datBan.phieuDatDTO.SoChungtu = "PN" + count + 1;
                    datBan.phieuDatDTO.KhachHangId = khachHangId;
                    dbcontext.PhieuDats.Add(datBan.phieuDatDTO);
                    await dbcontext.SaveChangesAsync();

                    long phieuDatId = datBan.phieuDatDTO.PhieuDatId;

                    foreach (PhieuDatBanChiTietDTO phieuDatBanChiTietDTO in datBan.phieuDatBanChiTiets)
                    {
                        phieuDatBanChiTietDTO.PhieuDatId = phieuDatId;
                        dbcontext.PhieuDatBanChiTiets.Add(phieuDatBanChiTietDTO);
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


        public async Task<ErrorMessageDTO> CapNhatPhieuDat(PhieuDatDTO obPhieuDat)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                PhieuDat? obPhieuDatId = dbcontext.PhieuDats.Where(p => p.PhieuDatId == obPhieuDat.PhieuDatId).FirstOrDefault();
                if (obPhieuDatId == null)
                {
                    error.errorCode = Convert.ToInt32(ErrorCodeEnum.KhongTimThay).ToString();
                    error.message = ResponseDTO.GetValueError(ErrorCodeEnum.KhongTimThay);
                    error.flagThanhCong = false;
                    return await Task.FromResult(error);
                }
                obPhieuDatId.SoChungtu = obPhieuDat.SoChungtu;
                obPhieuDatId.TrangThai = obPhieuDat.TrangThai;
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
        public async Task<ErrorMessageDTO> XoaPhieuDat(int PhieuDatID)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                PhieuDat? item = await dbcontext.PhieuDats.Where(p => p.PhieuDatId == PhieuDatID).FirstOrDefaultAsync();
                if (item == null)
                {
                    error.errorCode = Convert.ToInt32(ErrorCodeEnum.NotFound).ToString();
                    error.message = ResponseDTO.GetValueError(ErrorCodeEnum.KhongTimThay);
                    error.flagThanhCong = false;
                    return await Task.FromResult(error);
                }
                dbcontext.PhieuDats.Remove(item);
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

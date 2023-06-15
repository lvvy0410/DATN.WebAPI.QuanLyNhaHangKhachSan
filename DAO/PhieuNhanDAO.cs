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
                    $"'{obPhieuNhan.LoaiPhieuId}', '{obPhieuNhan.KhachHangId}', N'{obPhieuNhan.TrangThai}'").ToListAsync();
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
                    foreach (PhieuNhanPhongChiTietDTO phieu in nhanPhong.phieuNhanPhongChiTietDTOs)
                    {
                        Phong phong = await dbcontext.Phongs.Where(p=>p.PhongId==phieu.PhongId).FirstOrDefaultAsync();
                        phong.TrangThaiId = 4;
                        await dbcontext.SaveChangesAsync();
                    }


                    //thêm khách hàng
                    dbcontext.KhachHangs.Add(nhanPhong.khachHang);
                    await dbcontext.SaveChangesAsync();

                    //lấy khách hàng id để thêm vào phiếu nhận
                    long khachHangId = nhanPhong.khachHang.KhachHangId;

                    //thêm phiếu nhận
                    long count = dbcontext.PhieuNhans.Count();
                    nhanPhong.phieuNhanDTO.SoChungTu="PNP"+ count+1;
                    nhanPhong.phieuNhanDTO.KhachHangId = khachHangId;
                    dbcontext.PhieuNhans.Add(nhanPhong.phieuNhanDTO);
                    await dbcontext.SaveChangesAsync();

                    //lấy phiếu nhận id để cho vào phiếu nhận phòng chi tiết
                    long phieuNhanId = nhanPhong.phieuNhanDTO.PhieuNhanId;

                    //thêm phiếu nhận phòng chi tiết
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

        public async Task<ErrorMessageDTO> ThemPhieuNhanBan(NhanBan nhanBan)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                try
                {
                    foreach (PhieuNhanBanChiTietDTO phieu in nhanBan.phieuNhanBanChiTietDTOs)
                    {
                        Ban ban = await dbcontext.Bans.Where(p => p.BanId == phieu.BanId).FirstOrDefaultAsync();
                        ban.TrangThaiId = 4;
                        await dbcontext.SaveChangesAsync();
                    }


                    //thêm khách hàng
                    dbcontext.KhachHangs.Add(nhanBan.khachHang);
                    await dbcontext.SaveChangesAsync();

                    //lấy khách hàng id để thêm vào phiếu nhận
                    long khachHangId = nhanBan.khachHang.KhachHangId;

                    //thêm phiếu nhận
                    long count = dbcontext.PhieuNhans.Count();
                    nhanBan.phieuNhanDTO.SoChungTu = "PNB" + count + 1;
                    nhanBan.phieuNhanDTO.KhachHangId = khachHangId;
                    dbcontext.PhieuNhans.Add(nhanBan.phieuNhanDTO);
                    await dbcontext.SaveChangesAsync();

                    //lấy phiếu nhận id để cho vào phiếu nhận phòng chi tiết
                    long phieuNhanId = nhanBan.phieuNhanDTO.PhieuNhanId;

                    //thêm phiếu nhận phòng chi tiết
                    foreach (PhieuNhanBanChiTietDTO phieuNhanBanChiTietDTO in nhanBan.phieuNhanBanChiTietDTOs)
                    {
                        phieuNhanBanChiTietDTO.PhieuNhanId = phieuNhanId;
                        dbcontext.PhieuNhanBanChiTiets.Add(phieuNhanBanChiTietDTO);
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
                obPhieuNhanId.NgayTra = obPhieuNhan.NgayTra;
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

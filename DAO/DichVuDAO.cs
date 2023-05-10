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
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class DichVuDAO
    {

        private readonly QuanLyNhaHangKhachSanContext dbcontext;
        public DichVuDAO(QuanLyNhaHangKhachSanContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        

        public async Task<ErrorMessageDTO> LayDanhSachDichVu(DichVuDTO? dichVu)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                error.data = await dbcontext.DichVus.FromSqlRaw($"LayDanhSachDichVu '{dichVu.PhongId}', '{dichVu.BanId}'," +
                    $"'{dichVu.PhieuNhanId}', '{dichVu.TrangThai}'").ToListAsync();
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

        public async Task<ErrorMessageDTO> TimDV(int dv)
        {
            //return dbcontext.SanPhams.Where(p => p.SanPhamId == sanPhamID).FirstOrDefault();
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                DichVu? item = dbcontext.DichVus.Where(p => p.DichVuId == dv).FirstOrDefault();
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

        public async Task<ErrorMessageDTO> LayDanhSachDV()
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                error.data = await dbcontext.DichVus.ToListAsync();
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

        public async Task<ErrorMessageDTO> ThemDV(List<DichVuDTO> dichVus)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                try
                {
                    foreach (DichVuDTO dichVu in dichVus)
                    {
                        dbcontext.DichVus.Add(dichVu);
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

        public async Task<ErrorMessageDTO> CapNhatDV(List<DichVuDTO> dichVus)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                try
                {
                    foreach (DichVuDTO dichVu in dichVus)
                    {
                        DichVu? item = dbcontext.DichVus.Where(p => p.DichVuId == dichVu.DichVuId).FirstOrDefault();
                        item.SoLuong = dichVu.SoLuong;
                        item.DonGia = dichVu.DonGia;
                        item.ThanhTien = dichVu.ThanhTien;
                        item.GhiChu = dichVu.GhiChu;
                        item.TrangThai = dichVu.TrangThai;
                    }
                    error.data = await dbcontext.SaveChangesAsync();
                    error.flagThanhCong = true;
                    return await Task.FromResult(error);
                }
                catch
                {
                    error.errorCode = Convert.ToInt32(ErrorCodeEnum.KhongTheCapNhat).ToString();
                    error.message = ResponseDTO.GetValueError(ErrorCodeEnum.KhongTheCapNhat);
                    error.flagThanhCong = false;
                    return await Task.FromResult(error);
                }

            }
            catch (Exception)
            {

                error.flagBiLoiEx = true;
                error.errorCode = Convert.ToInt32(ErrorCodeEnum.InternalServerError).ToString();
                error.message = ResponseDTO.GetValueError(ErrorCodeEnum.InternalServerError).ToString();
                return await Task.FromResult(error);

            }


        }
        public async Task<ErrorMessageDTO> Xoa(int dv)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            DichVu? item = dbcontext.DichVus.Where(p => p.DichVuId == dv).FirstOrDefault();
            try
            {
                if (item == null)
                {
                    error.errorCode = Convert.ToInt32(ErrorCodeEnum.KhongTimThay).ToString();
                    error.message = ResponseDTO.GetValueError(ErrorCodeEnum.KhongTimThay);
                    return await Task.FromResult(error);
                }

                dbcontext.Remove(item);
                error.data = await dbcontext.SaveChangesAsync();
                error.flagThanhCong = true;
                error.errorCode = Convert.ToInt32(ErrorCodeEnum.XoaThanhCong).ToString();
                error.message = ResponseDTO.GetValueError(ErrorCodeEnum.XoaThanhCong);
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

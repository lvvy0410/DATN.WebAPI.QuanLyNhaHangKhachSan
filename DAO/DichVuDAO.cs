using DTO.Context;
using DTO.DieuKienLoc;
using DTO.Model;
using DTO.MultiTable;
using DTO.Public;
using DTO.publicDTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
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
                error.data = await dbcontext.DichVus.FromSqlRaw($"LayDanhSachDichVu  '{dichVu.PhongId}'," +
                    $"'{dichVu.PhieuNhanId}', N'{dichVu.TrangThai}'").ToListAsync();
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

        public async Task<ErrorMessageDTO> ThemDV(ListDichVu listDichVu)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                try
                {
                    foreach (DichVuDTO dichVu in listDichVu.dichVuDTOsThem)
                    {
                        if (dichVu.PhieuNhanId == 0)
                        {
                            dichVu.PhieuNhanId = null;
                        }
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

        public async Task<ErrorMessageDTO> CapNhatDV(ListDichVu listDichVu)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                try
                {
                    if (listDichVu.dichVuDTOsCapNhat.Count == 0)
                    {

                    }
                    else
                    {
                        if (listDichVu.dichVuDTOsCapNhat.Count == 1 && listDichVu.dichVuDTOsCapNhat[0].TrangThai == "xóa tất cả")
                        {
                            List<DichVu>? listDichVuXoaTatCa = await dbcontext.DichVus.Where(p => p.TrangThai == "chưa thanh toán" && p.PhongId == listDichVu.dichVuDTOsCapNhat[0].PhongId).ToListAsync();
                            for (int i = 0; i < listDichVuXoaTatCa.Count; i++)
                            {
                                DichVu? item = dbcontext.DichVus.Where(p => p.DichVuId == listDichVuXoaTatCa[i].DichVuId).FirstOrDefault();
                                item.TrangThai = "đã xóa";
                                await dbcontext.SaveChangesAsync();
                            }
                            listDichVu.dichVuDTOsCapNhat.Clear();
                        }
                        foreach (DichVuDTO dichVu in listDichVu.dichVuDTOsCapNhat)
                        {
                            DichVu? item = dbcontext.DichVus.Where(p => p.DichVuId == dichVu.DichVuId).FirstOrDefault();
                            if (item == null)
                            {
                                if (dichVu.PhieuNhanId == 0)
                                {
                                    dichVu.PhieuNhanId = null;
                                }
                                dbcontext.DichVus.Add(dichVu);
                                await dbcontext.SaveChangesAsync();
                            }
                            else
                            {
                                if (dichVu.PhieuNhanId == 0 || dichVu.PhieuNhanId == null)
                                {
                                    dichVu.PhieuNhanId = null;
                                }
                                item.SoLuong = dichVu.SoLuong;
                                item.DonGia = dichVu.DonGia;
                                item.ThanhTien = dichVu.ThanhTien;
                                item.GhiChu = dichVu.GhiChu;
                                item.TrangThai = dichVu.TrangThai;
                                await dbcontext.SaveChangesAsync();
                            }
                        }
                        List<DichVu>? listDichVuBanDau = await dbcontext.DichVus.Where(p => p.TrangThai == "chưa thanh toán" && p.PhongId == listDichVu.dichVuDTOsCapNhat[0].PhongId).ToListAsync();
                        if (listDichVuBanDau.Count > 0)
                        {
                            //var firstNotSecond = listDichVuBanDau.Except(listDichVu.dichVuDTOsCapNhat).ToList();
                            List<DichVu>? firstNotSecond = new List<DichVu>();
                            for (int i=0;i<listDichVuBanDau.Count;i++)
                            {
                                bool check=false;
                                for(int j = 0; j < listDichVu.dichVuDTOsCapNhat.Count; j++)
                                {
                                    if (listDichVuBanDau[i].HangHoaId == listDichVu.dichVuDTOsCapNhat[j].HangHoaId)
                                    {
                                        check= true;
                                    }
                                }
                                if (check == false)
                                {
                                    firstNotSecond.Add(listDichVuBanDau[i]as DichVu);
                                }
                            }
                            if (firstNotSecond.Count > 0)
                            {
                                for (int i = 0; i < firstNotSecond.Count; i++)
                                {
                                    DichVu? item = dbcontext.DichVus.Where(p => p.DichVuId == firstNotSecond[i].DichVuId).FirstOrDefault();
                                    item.TrangThai = "đã xóa";
                                    await dbcontext.SaveChangesAsync();
                                }
                            }
                        }
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
        public async Task<ErrorMessageDTO> Xoa(long dv)
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

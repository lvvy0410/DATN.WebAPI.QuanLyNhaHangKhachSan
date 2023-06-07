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
                        //xóa tất cả
                        //cách dùng : sau khi xóa tất cả dịch vụ trong list ở giao diện và bấm lưu sẽ tạo 1 dịch vụ ảo có trạng thái là xóa tất cả
                        //lưu ý : lúc đó list chỉ có 1 dịch vụ ảo và trạng thái xóa tất cả
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
                        //nếu list không có trạng thái xóa tất cả thì sẽ chạy xuống vòng lặp thêm và cập nhật
                        foreach (DichVuDTO dichVu in listDichVu.dichVuDTOsCapNhat)
                        {
                            //tìm dịch vụ đã có trong csdl chưa
                            DichVu? item = dbcontext.DichVus.Where(p => p.DichVuId == dichVu.DichVuId).FirstOrDefault();
                            
                            //nếu chưa thì là cần thêm mới
                            if (item == null)
                            {
                                if (dichVu.PhieuNhanId == 0)
                                {
                                    dichVu.PhieuNhanId = null;
                                }
                                dbcontext.DichVus.Add(dichVu);
                                await dbcontext.SaveChangesAsync();
                            }

                            //nếu có trong csdl rồi thì cần cập nhật
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

                        //xóa những dịch vụ cần xóa

                        //bước đầu tiên cần lấy tất cả dịch vụ của phòng cần xóa trong csdl
                        List<DichVu>? listDichVuBanDau = await dbcontext.DichVus.Where(p => p.TrangThai == "chưa thanh toán" && p.PhongId == listDichVu.dichVuDTOsCapNhat[0].PhongId).ToListAsync();

                        
                        if (listDichVuBanDau.Count > 0)
                        {
                            //đầu tiên tạo 1 list dịch vụ để chứa những dịch vụ cần xóa
                            //var firstNotSecond = listDichVuBanDau.Except(listDichVu.dichVuDTOsCapNhat).ToList();
                            List<DichVu>? firstNotSecond = new List<DichVu>();
                            for (int i=0;i<listDichVuBanDau.Count;i++)
                            {
                                //tạo biến bool để biết cái nào là cái cần xóa
                                bool check=false;

                                //lúc này ta sẽ kiểm tra những dịch vụ nào của phòng này nhưng mà lại không có trong list dữ liệu truyền vào thì là dịch vụ cần xóa
                                //bởi vì cái cần xóa đã bị xóa bởi người dùng trong giao diện trước khi truyền dữ liệu
                                //nên lúc này dịch vụ nào có trong csdl mà không có trong list truyền vào là dịch vụ cần xóa
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

                            //nếu có dịch vụ cần xóa thì set trạng thái của dịch vụ thành đã xóa
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

        public async Task<ErrorMessageDTO> LayDVTheoPN(DichVuDTO? dichVu)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                error.data = await dbcontext.DichVus.FromSqlRaw($"LayDVTheoPN  '{dichVu.PhieuNhanId}'," +
                     $"'{dichVu.PhongId}'").ToListAsync();
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

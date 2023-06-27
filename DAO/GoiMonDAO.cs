using DTO.Context;
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
    public class GoiMonDAO
    {

        private readonly QuanLyNhaHangKhachSanContext dbcontext;
        public GoiMonDAO(QuanLyNhaHangKhachSanContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public async Task<ErrorMessageDTO> TimGM(int gm)
        {
            //return dbcontext.SanPhams.Where(p => p.SanPhamId == sanPhamID).FirstOrDefault();
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                GoiMon? item = dbcontext.GoiMons.Where(p => p.GoiMonId == gm).FirstOrDefault();
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

        public async Task<ErrorMessageDTO> LayDanhSachGoiMon(GoiMonDTO? GoiMon)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                error.data = await dbcontext.GoiMons.FromSqlRaw($"LayDanhSachGoiMon  '{GoiMon.BanId}'," +
                    $"'{GoiMon.PhieuNhanId}', N'{GoiMon.TrangThai}'").ToListAsync();
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

        public async Task<ErrorMessageDTO> LayDanhSachGM()
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                error.data = await dbcontext.GoiMons.ToListAsync();
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

        public async Task<ErrorMessageDTO> ThemGM(GoiMonDTO gm)
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
                dbcontext.GoiMons.Add(gm);
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

        public async Task<ErrorMessageDTO> CapNhatGM(ListGoiMon listGoiMon)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {

                //xóa tất cả
                //cách dùng : sau khi xóa tất cả dịch vụ trong list ở giao diện và bấm lưu sẽ tạo 1 dịch vụ ảo có trạng thái là xóa tất cả
                //lưu ý : lúc đó list chỉ có 1 dịch vụ ảo và trạng thái xóa tất cả
                if (listGoiMon.goiMonDTOsCapNhat.Count == 1 && listGoiMon.goiMonDTOsCapNhat[0].TrangThai == "xóa tất cả")
                {
                    List<GoiMon>? listGoiMonXoaTatCa = await dbcontext.GoiMons.Where(p => p.TrangThai == "chưa thanh toán" && p.BanId == listGoiMon.goiMonDTOsCapNhat[0].BanId).ToListAsync();
                    for (int i = 0; i < listGoiMonXoaTatCa.Count; i++)
                    {
                        GoiMon? item = dbcontext.GoiMons.Where(p => p.GoiMonId == listGoiMonXoaTatCa[i].GoiMonId).FirstOrDefault();
                        item.TrangThai = "đã xóa";
                        await dbcontext.SaveChangesAsync();
                    }
                    listGoiMon.goiMonDTOsCapNhat.Clear();
                }
                //nếu list không có trạng thái xóa tất cả thì sẽ chạy xuống vòng lặp thêm và cập nhật
                foreach (GoiMonDTO goiMon in listGoiMon.goiMonDTOsCapNhat)
                {
                    //tìm dịch vụ đã có trong csdl chưa
                    GoiMon? item = dbcontext.GoiMons.Where(p => p.GoiMonId == goiMon.GoiMonId).FirstOrDefault();

                    //nếu chưa thì là cần thêm mới
                    if (item == null)
                    {
                        if (goiMon.PhieuNhanId == 0)
                        {
                            goiMon.PhieuNhanId = null;
                        }
                        dbcontext.GoiMons.Add(goiMon);
                        await dbcontext.SaveChangesAsync();
                    }
                    else
                    {//nếu có trong csdl rồi thì cần cập nhật
                        if (goiMon.PhieuNhanId == 0 || goiMon.PhieuNhanId == null)
                        {
                            goiMon.PhieuNhanId = null;
                        }
                        item.SoLuong = goiMon.SoLuong;
                        item.DonGia = goiMon.DonGia;
                        item.ThanhTien = goiMon.ThanhTien;
                        item.GhiChu = goiMon.GhiChu;
                        item.TrangThai = goiMon.TrangThai;
                        await dbcontext.SaveChangesAsync();
                    }
                }

                //xóa những dịch vụ cần xóa

                //bước đầu tiên cần lấy tất cả dịch vụ của phòng cần xóa trong csdl
                List<GoiMon>? listGoiMonBanDau = await dbcontext.GoiMons.Where(p => p.TrangThai == "chưa thanh toán" && p.BanId == listGoiMon.goiMonDTOsCapNhat[0].BanId).ToListAsync();


                if (listGoiMonBanDau.Count > 0)
                {
                    //đầu tiên tạo 1 list dịch vụ để chứa những dịch vụ cần xóa
                    //var firstNotSecond = listDichVuBanDau.Except(listDichVu.dichVuDTOsCapNhat).ToList();
                    List<GoiMon>? firstNotSecond = new List<GoiMon>();
                    for (int i = 0; i < listGoiMonBanDau.Count; i++)
                    {
                        //tạo biến bool để biết cái nào là cái cần xóa
                        bool check = false;

                        //lúc này ta sẽ kiểm tra những dịch vụ nào của phòng này nhưng mà lại không có trong list dữ liệu truyền vào thì là dịch vụ cần xóa
                        //bởi vì cái cần xóa đã bị xóa bởi người dùng trong giao diện trước khi truyền dữ liệu
                        //nên lúc này dịch vụ nào có trong csdl mà không có trong list truyền vào là dịch vụ cần xóa
                        for (int j = 0; j < listGoiMon.goiMonDTOsCapNhat.Count; j++)
                        {
                            if (listGoiMonBanDau[i].HangHoaId == listGoiMon.goiMonDTOsCapNhat[j].HangHoaId)
                            {
                                check = true;
                            }
                        }
                        if (check == false)
                        {
                            firstNotSecond.Add(listGoiMonBanDau[i] as GoiMon);
                        }
                    }

                    //nếu có dịch vụ cần xóa thì set trạng thái của dịch vụ thành đã xóa
                    if (firstNotSecond.Count > 0)
                    {
                        for (int i = 0; i < firstNotSecond.Count; i++)
                        {
                            GoiMon? item = dbcontext.GoiMons.Where(p => p.GoiMonId == firstNotSecond[i].GoiMonId).FirstOrDefault();
                            item.TrangThai = "đã xóa";
                            await dbcontext.SaveChangesAsync();
                        }
                    }
                }





                //if (error.flagBiLoiEx)
                //{
                //    error.errorCode = Convert.ToInt32(ErrorCodeEnum.KhongTheCapNhat).ToString();
                //    error.message = ResponseDTO.GetValueError(ErrorCodeEnum.KhongTheCapNhat);
                //    return await Task.FromResult(error);
                //}
                //error.flagThanhCong = true;
                //item.BanId = gm.BanId;
                //item.HangHoaId = gm.HangHoaId;
                //item.PhieuNhanId = gm.PhieuNhanId;
                //item.SoLuong = gm.SoLuong;
                //item.DonGia = gm.DonGia;
                //item.ThanhTien = gm.ThanhTien;
                //item.GhiChu = gm.GhiChu;
                //item.TrangThai = gm.TrangThai;
                //;
                //await dbcontext.SaveChangesAsync();
                //error.data = item;

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
        public async Task<ErrorMessageDTO> Xoa(int gm)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            GoiMon? item = dbcontext.GoiMons.Where(p => p.GoiMonId == gm).FirstOrDefault();
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

        public async Task<ErrorMessageDTO> CapNhatGoiMon(GoiMonDTO gm)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            GoiMon? item = dbcontext.GoiMons.Where(p => p.GoiMonId == gm.GoiMonId).FirstOrDefault();
            try
            {
                if (error.flagBiLoiEx)
                {
                    error.errorCode = Convert.ToInt32(ErrorCodeEnum.KhongTheCapNhat).ToString();
                    error.message = ResponseDTO.GetValueError(ErrorCodeEnum.KhongTheCapNhat);
                    return await Task.FromResult(error);
                }
                error.flagThanhCong = true;
                item.PhieuNhanId=gm.PhieuNhanId;
                item.TrangThai = gm.TrangThai;
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


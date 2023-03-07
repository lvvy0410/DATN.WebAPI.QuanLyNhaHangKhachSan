using DTO.Context;
using DTO.Model;
using DTO.Public;
using DTO.publicDTO;
using Microsoft.EntityFrameworkCore;
using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class LoaiPhongDAO
    {

        private readonly QuanLyNhaHangKhachSanContext dbcontext;
        public LoaiPhongDAO(QuanLyNhaHangKhachSanContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public async Task<ErrorMessageDTO> TimLoaiPhong(int loaiPhong)
        {
            //return dbcontext.SanPhams.Where(p => p.SanPhamId == sanPhamID).FirstOrDefault();
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                LoaiPhong? item = dbcontext.LoaiPhongs.Where(p => p.LoaiPhongId == loaiPhong).FirstOrDefault();
               
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

        public async Task<ErrorMessageDTO> LayDanhSachLoaiPhong()
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                error.data = await dbcontext.LoaiPhongs.ToListAsync();
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

        public async Task<ErrorMessageDTO> ThemLoaiPhong(LoaiPhong loaiPhong)
        {

            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
               
                error.flagThanhCong = true;
                dbcontext.LoaiPhongs.Add(loaiPhong);
                dbcontext.SaveChanges();
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

        public async Task<ErrorMessageDTO> CapNhatLoaiPhong(LoaiPhong loaiPhong)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            LoaiPhong? item = dbcontext.LoaiPhongs.Where(p => p.LoaiPhongId == loaiPhong.LoaiPhongId).FirstOrDefault();
            try
            {
               
                error.flagThanhCong = true;
                item.MaLoaiPhong = loaiPhong.MaLoaiPhong;
                item.TenLoaiPhong = loaiPhong.TenLoaiPhong;
                item.SoNguoiToiDa = loaiPhong.SoNguoiToiDa;
                item.DonGia = loaiPhong.DonGia;
                await dbcontext.SaveChangesAsync();               

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
        public async Task<ErrorMessageDTO> Xoa(int loaiPhong)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            LoaiPhong? item = dbcontext.LoaiPhongs.Where(p => p.LoaiPhongId == loaiPhong).FirstOrDefault();
            try
            {
               

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
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
    public class NguoiDungDAO
    {

        private readonly QuanLyNhaHangKhachSanContext dbcontext;
        public NguoiDungDAO(QuanLyNhaHangKhachSanContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public async Task<ErrorMessageDTO> TimNguoiDung(int nguoiDung)
        {
            //return dbcontext.SanPhams.Where(p => p.SanPhamId == sanPhamID).FirstOrDefault();
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                NguoiDung? item = dbcontext.NguoiDungs.Where(p => p.NguoiDungId == nguoiDung).FirstOrDefault();
             

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

        public async Task<ErrorMessageDTO> LayDanhSachNguoiDung()
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                error.data = await dbcontext.NguoiDungs.ToListAsync();
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

        public async Task<ErrorMessageDTO> ThemNguoiDung(NguoiDung nguoiDung)
        {

            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
               
                error.flagThanhCong = true;
                dbcontext.NguoiDungs.Add(nguoiDung);
                error.data = await dbcontext.SaveChangesAsync();
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

        public async Task<ErrorMessageDTO> CapNhatNguoiDung(NguoiDung nguoiDung)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            NguoiDung? item = dbcontext.NguoiDungs.Where(p => p.NguoiDungId == nguoiDung.NguoiDungId).FirstOrDefault();
            try
            {
              
                error.flagThanhCong = true;          
                item.Sdt = nguoiDung.Sdt;  
                item.Cccd= nguoiDung.Cccd;
                item.DiaChi = nguoiDung.DiaChi;                           
                item.MatKhau = nguoiDung.MatKhau;                           
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
        public async Task<ErrorMessageDTO> Xoa(int nguoiDung)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            NguoiDung? item = dbcontext.NguoiDungs.Where(p => p.NguoiDungId == nguoiDung).FirstOrDefault();
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
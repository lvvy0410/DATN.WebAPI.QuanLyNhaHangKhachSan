using DTO.Context;
using DTO.Model;
using DTO.Public;
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
    public class TrangThietBiDAO
    {

        private readonly QuanLyNhaHangKhachSanContext dbcontext;
        public TrangThietBiDAO(QuanLyNhaHangKhachSanContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public async Task<ErrorMessageDTO> TimTrangThietBi(int trangThietBi)
        {
            //return dbcontext.SanPhams.Where(p => p.SanPhamId == sanPhamID).FirstOrDefault();
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                TrangThietBi? item = dbcontext.TrangThietBis.Where(p => p.TrangThietBiId == trangThietBi).FirstOrDefault();
               

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

        public async Task<ErrorMessageDTO> LayDanhSachTrangThietBi()
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                error.data = await dbcontext.TrangThietBis.ToListAsync();
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

        public async Task<ErrorMessageDTO> ThemTrangThietBi(TrangThietBi trangThietBi)
        {

            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
               
                error.flagThanhCong = true;
                dbcontext.TrangThietBis.Add(trangThietBi);
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

        public async Task<ErrorMessageDTO> CapNhatTrangThietBi(TrangThietBi trangThietBi)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            TrangThietBi? item = dbcontext.TrangThietBis.Where(p => p.TrangThietBiId == trangThietBi.TrangThietBiId).FirstOrDefault();
            try
            {
               
                error.flagThanhCong = true;
                item.TenTrangThietBi = trangThietBi.TenTrangThietBi;
                item.PhongId = trangThietBi.PhongId;
                item.TrangThaiId = trangThietBi.TrangThaiId;
                item.SoLuong = trangThietBi.SoLuong;


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
        public async Task<ErrorMessageDTO> Xoa(int trangThietBi)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            TrangThietBi? item = dbcontext.TrangThietBis.Where(p => p.TrangThietBiId == trangThietBi).FirstOrDefault();
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
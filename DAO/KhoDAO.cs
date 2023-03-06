

using DTO.Context;
using DTO.Model;
using DTO.Public;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class KhoDAO
    {

        private readonly QuanLyNhaHangKhachSanContext dbcontext;
        public KhoDAO(QuanLyNhaHangKhachSanContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public async Task<ErrorMessageDTO> TimKho(int kho)
        {
            //return dbcontext.SanPhams.Where(p => p.SanPhamId == sanPhamID).FirstOrDefault();
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                Kho? item = dbcontext.Khos.Where(p => p.KhoId == kho).FirstOrDefault();
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

        public async Task<ErrorMessageDTO> LayDanhSachKho()
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                error.data = await dbcontext.Khos.ToListAsync();
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

        public async Task<ErrorMessageDTO> ThemKho(Kho kho)
        {

            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                if (error.flagBiLoiEx)
                {
                    error.errorCode = Convert.ToInt32(ErrorCodeEnum.KhongTheThem).ToString();
                    error.message = ResponseDTO.GetValueError(ErrorCodeEnum.KhongTheThem);
                    return await Task.FromResult(error);
                }
                error.flagThanhCong = true;
                dbcontext.Khos.Add(kho);
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

        public async Task<ErrorMessageDTO> CapNhatKho(Kho kho)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            Kho? item = dbcontext.Khos.Where(p => p.KhoId == kho.KhoId).FirstOrDefault();
            try
            {
                if (error.flagBiLoiEx)
                {
                    error.errorCode = Convert.ToInt32(ErrorCodeEnum.KhongTheCapNhat).ToString();
                    error.message = ResponseDTO.GetValueError(ErrorCodeEnum.KhongTheCapNhat);
                    return await Task.FromResult(error);
                }
                error.flagThanhCong = true;
                item.TenKho = kho.TenKho;


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
        public async Task<ErrorMessageDTO> Xoa(int kho)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            Kho? item = dbcontext.Khos.Where(p => p.KhoId == kho).FirstOrDefault();
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

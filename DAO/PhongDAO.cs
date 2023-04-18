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
    public class PhongDAO
    {

        private readonly QuanLyNhaHangKhachSanContext dbcontext;
        public PhongDAO(QuanLyNhaHangKhachSanContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public async Task<ErrorMessageDTO> TimPhong(int phong)
        {
            //return dbcontext.SanPhams.Where(p => p.SanPhamId == sanPhamID).FirstOrDefault();
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                Phong? item = dbcontext.Phongs.Where(p => p.PhongId == phong).FirstOrDefault();


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

        public async Task<ErrorMessageDTO> LayDanhSachPhong()
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                error.data = await dbcontext.Phongs.ToListAsync();
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

        public async Task<ErrorMessageDTO> ThemPhong(PhongDTO phong)
        {

            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {

                error.flagThanhCong = true;
                dbcontext.Phongs.Add(phong);
               await dbcontext.SaveChangesAsync();
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

        public async Task<ErrorMessageDTO> CapNhatPhong(PhongDTO phong)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            Phong? item = dbcontext.Phongs.Where(p => p.PhongId == phong.PhongId).FirstOrDefault();
            try
            {

                error.flagThanhCong = true;
            
                item.TrangThaiId = phong.TrangThaiId;
              


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
        public async Task<ErrorMessageDTO> Xoa(int phong)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            Phong? item = dbcontext.Phongs.Where(p => p.PhongId == phong).FirstOrDefault();
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
        public async Task<ErrorMessageDTO> LayDanhSachPhongProc()
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                error.data = await dbcontext.Phongs.FromSqlRaw("dsphong").ToListAsync();

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

        public async Task<ErrorMessageDTO> LayDanhSachPhongThuong1G(int id)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {

                error.data = await dbcontext.Phongs.Where(p => p.LoaiPhongId == id).ToListAsync();
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
        //    public async Task<ErrorMessageDTO> LayDanhSachPhongThuong2G()
        //    {
        //        ErrorMessageDTO error = new ErrorMessageDTO();
        //        try
        //        {

        //            error.data = await dbcontext.Phongs.Where(p => p.LoaiPhongId == 2).ToListAsync();
        //            error.flagThanhCong = true;
        //            return await Task.FromResult(error);
        //        }
        //        catch (Exception ex)
        //        {
        //            error.flagBiLoiEx = true;
        //            error.errorCode = Convert.ToInt32(ErrorCodeEnum.InternalServerError).ToString();
        //            error.message = ex.Message;
        //            return await Task.FromResult(error);
        //        }
        //    }
        //    public async Task<ErrorMessageDTO> LayDanhSachPhongVip1G()
        //    {
        //        ErrorMessageDTO error = new ErrorMessageDTO();
        //        try
        //        {

        //            error.data = await dbcontext.Phongs.Where(p => p.LoaiPhongId == 3).ToListAsync();
        //            error.flagThanhCong = true;
        //            return await Task.FromResult(error);
        //        }
        //        catch (Exception ex)
        //        {
        //            error.flagBiLoiEx = true;
        //            error.errorCode = Convert.ToInt32(ErrorCodeEnum.InternalServerError).ToString();
        //            error.message = ex.Message;
        //            return await Task.FromResult(error);
        //        }
        //    }
        //    public async Task<ErrorMessageDTO> LayDanhSachPhongVip2G()
        //    {
        //        ErrorMessageDTO error = new ErrorMessageDTO();
        //        try
        //        {

        //            error.data = await dbcontext.Phongs.Where(p => p.LoaiPhongId == 4).ToListAsync();
        //            error.flagThanhCong = true;
        //            return await Task.FromResult(error);
        //        }
        //        catch (Exception ex)
        //        {
        //            error.flagBiLoiEx = true;
        //            error.errorCode = Convert.ToInt32(ErrorCodeEnum.InternalServerError).ToString();
        //            error.message = ex.Message;
        //            return await Task.FromResult(error);
        //        }
        //    }
        //}
    }
}
using DTO.Context;
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
    public class PhieuThuDAO
    {
        private readonly QuanLyNhaHangKhachSanContext dbcontext;
        public PhieuThuDAO(QuanLyNhaHangKhachSanContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public async Task<ErrorMessageDTO> LayDanhSachPhieuThu()
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                error.data=await dbcontext.PhieuThus.ToListAsync();
               error.flagThanhCong= true;
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
        public async  Task<ErrorMessageDTO> ThemPhieuThu(PhieuThuDTO pt )
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                error.flagThanhCong= true;
                dbcontext.PhieuThus.Add(pt);
                await dbcontext.SaveChangesAsync();
                return await Task.FromResult(error);
                
            }
            catch(Exception ex)
            {
                error.flagBiLoiEx = true;
                error.errorCode = Convert.ToInt32(ErrorCodeEnum.InternalServerError).ToString();
                error.message = ex.Message;
                return await Task.FromResult(error);
            }

        }
    }
}

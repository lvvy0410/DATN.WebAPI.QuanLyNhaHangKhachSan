using DTO.Context;
using DTO.DieuKienLoc;
using DTO.Model;
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
    public class PhieuXuatChiTietDAO
    {
        private readonly QuanLyNhaHangKhachSanContext dbcontext;

        public PhieuXuatChiTietDAO(QuanLyNhaHangKhachSanContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public async Task<ErrorMessageDTO> LayPhieuXuatChiTiet(DieuKienLocPhieuXuatChiTiet? oblay)
        {
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                error.data = await dbcontext.PhieuXuatChiTiets.FromSqlRaw($"LayPhieuXuatChiTiet'{oblay.PhieuXuatChiTietId}','{oblay.PhieuXuatId}'" ).ToListAsync();

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


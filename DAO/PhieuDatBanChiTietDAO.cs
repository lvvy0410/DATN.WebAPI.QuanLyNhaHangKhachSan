using DTO.Context;
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
    public class PhieuDatBanChiTietDAO
    {
        private readonly QuanLyNhaHangKhachSanContext dbcontext;

        public PhieuDatBanChiTietDAO(QuanLyNhaHangKhachSanContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public async Task<ErrorMessageDTO> LayPhieuDatBanChiTiet(PhieuDatDTO phieuDat)
        {
            //return dbcontext.PhieuDatPhongChiTiets.Where(p => p.PhieuDatPhongChiTietID == PhieuDatPhongChiTietID).FirstOrDefault();
            ErrorMessageDTO error = new ErrorMessageDTO();
            try
            {
                List<PhieuDatBanChiTiet> item = await dbcontext.PhieuDatBanChiTiets.Where(p => p.PhieuDatId == phieuDat.PhieuDatId).ToListAsync();
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
    }
}

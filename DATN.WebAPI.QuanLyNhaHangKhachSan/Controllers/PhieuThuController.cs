using DAO;
using DTO.Context;
using DTO.Model;
using DTO.Public;
using DTO.publicDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace DATN.WebAPI.QuanLyNhaHangKhachSan.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuThuController : ControllerBase
    {
        private readonly QuanLyNhaHangKhachSanContext dbcontext;
        private readonly PhieuThuDAO phieuThuDAO;
        
        public PhieuThuController(QuanLyNhaHangKhachSanContext dbcontext, PhieuThuDAO phieuThuDAO)
        {
            this.dbcontext = dbcontext;
            this.phieuThuDAO = phieuThuDAO;
        }
        [HttpPost]
        [Route("danhsach_phieuthu")]
        public async Task<ActionResult<ResponseDTO>> DanhSachPT()
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                ErrorMessageDTO error = await phieuThuDAO.LayDanhSachPhieuThu();
                if(error.flagBiLoiEx|| !error.flagThanhCong)
                {
                    responseDTO.errorCode=error.errorCode;
                    responseDTO.message=error.message;
                    return Ok(responseDTO);
                }
                responseDTO.statusCode = HttpStatusCode.OK;
                responseDTO.message=HttpStatusCode.OK.ToString();
                responseDTO.data = error.data;
                return Ok(responseDTO);
            }
            catch (Exception ex)
            {

                responseDTO.statusCode = HttpStatusCode.BadRequest;
                responseDTO.errorCode = Convert.ToInt32(ErrorCodeEnum.BadRequest).ToString();
                responseDTO.message = ex.Message;
                return BadRequest(responseDTO);
            }
        }
        [HttpPost]
        [Route("them_phieuthu")]
        public async Task<ActionResult<PhieuThu>> ThemPhieuThu(PhieuThuDTO pt)
        {
           ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                ErrorMessageDTO error = await phieuThuDAO.ThemPhieuThu(pt);
                    if(error.flagBiLoiEx)
                {
                    responseDTO.errorCode=Convert.ToInt32(ErrorCodeEnum.KhongTheThem).ToString();
                    responseDTO.message = ResponseDTO.GetValueError(ErrorCodeEnum.KhongTheThem);
                    return Ok(responseDTO);
                }
                    responseDTO.statusCode = HttpStatusCode.OK;
                responseDTO.errorCode = Convert.ToInt32(ErrorCodeEnum.ThemThanhCong).ToString();
                responseDTO.message = ResponseDTO.GetValueError(ErrorCodeEnum.ThemThanhCong);
                return Ok(responseDTO);
            }catch(Exception ex)
            {

                responseDTO.statusCode = HttpStatusCode.BadRequest;
                responseDTO.errorCode = Convert.ToInt32(ErrorCodeEnum.BadRequest).ToString();
                responseDTO.message = ex.Message;
                return BadRequest(responseDTO);
            }
        }
      
    }
}

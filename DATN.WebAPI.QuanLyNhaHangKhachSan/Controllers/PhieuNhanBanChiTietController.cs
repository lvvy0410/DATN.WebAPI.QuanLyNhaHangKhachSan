using DAO;
using DTO.Context;
using DTO.DieuKienLoc;
using DTO.Public;
using DTO.publicDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DATN.WebAPI.QuanLyNhaHangKhachSan.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/PhieuNhanBanChiTiet")]
    public class PhieuNhanBanChiTietController : Controller
    {
        private readonly QuanLyNhaHangKhachSanContext dbcontext;
        private readonly PhieuNhanBanChiTietDAO phieuNhanBanChiTietDAO;
        public PhieuNhanBanChiTietController(QuanLyNhaHangKhachSanContext dbcontext, PhieuNhanBanChiTietDAO phieuNhanBanChiTietDAO)
        {
            this.dbcontext = dbcontext;
            this.phieuNhanBanChiTietDAO = phieuNhanBanChiTietDAO;
        }
        [HttpPost]
        [Route("danhsach-phieunhanbanCT")]
        public async Task<ActionResult<ResponseDTO>> LayDsPhieuNhanBan(DieuKienLocPhieuNhanBanChiTiet ban)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                ErrorMessageDTO error= await phieuNhanBanChiTietDAO.LayPhieuNhanBanChiTiet(ban);
                if(error.flagBiLoiEx||!error.flagThanhCong)
                {
                    responseDTO.statusCode=HttpStatusCode.OK;
                    responseDTO.errorCode = error.errorCode;
                    responseDTO.message=error.message;
                    return Ok(responseDTO);
                }
                responseDTO.statusCode=HttpStatusCode.OK;
            responseDTO.data=error.data;
                responseDTO.message=HttpStatusCode.OK.ToString();

                return Ok(responseDTO);


            }
            catch(Exception ex)
            {
                responseDTO.statusCode = HttpStatusCode.BadRequest;
                responseDTO.errorCode = Convert.ToInt32(ErrorCodeEnum.BadRequest).ToString();
                responseDTO.message = ex.Message;
                return BadRequest(responseDTO);
            }
        }

        [HttpPost]
        [Route("capnhat-PhieuNhanBanChiTiet")]
        public async Task<ActionResult<ResponseDTO>> CapNhatPhieuNhanBanChiTiet(PhieuNhanBanChiTietDTO ob)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                ErrorMessageDTO error = await phieuNhanBanChiTietDAO.CapNhatPhieuNhanBanChiTiet(ob);
                if (error.flagBiLoiEx)
                {
                    responseDTO.statusCode = HttpStatusCode.OK;
                    responseDTO.errorCode = Convert.ToInt32(ErrorCodeEnum.NotFound).ToString();
                    responseDTO.message = ResponseDTO.GetValueError(ErrorCodeEnum.KhongTheCapNhat);
                    return Ok(responseDTO);
                }
                if (error.flagThanhCong == false)
                {
                    responseDTO.statusCode = HttpStatusCode.OK;
                    responseDTO.errorCode = error.errorCode;
                    responseDTO.message = error.message;
                    return Ok(responseDTO);
                }

                responseDTO.statusCode = HttpStatusCode.OK;
                responseDTO.message = HttpStatusCode.OK.ToString();
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
    }
}

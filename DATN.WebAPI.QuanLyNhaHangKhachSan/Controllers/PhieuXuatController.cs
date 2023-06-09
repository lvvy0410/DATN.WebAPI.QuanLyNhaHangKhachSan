using DAO;
using DTO.Context;
using DTO.DieuKienLoc;
using DTO.Model;
using DTO.MultiTable;
using DTO.Public;
using DTO.publicDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DATN.WebAPI.QuanLyNhaHangKhachSan.Controllers
{
    [Authorize]
    [Route("api/PhieuXuat")]
    [ApiController]
    public class PhieuXuatController : Controller
    {


        private readonly QuanLyNhaHangKhachSanContext dbcontext;
        private readonly PhieuXuatDAO phieuXuatDAO;

        public PhieuXuatController(QuanLyNhaHangKhachSanContext dbcontext, PhieuXuatDAO phieuXuatDAO)
        {
            this.phieuXuatDAO = phieuXuatDAO;
            this.dbcontext = dbcontext;
        }
        [HttpPost]
        [Route("danhsach-PhieuXuat")]
        public async Task<ActionResult<ResponseDTO>> LayDanhSachPhieuXuat(DieuKienLocPhieuXuat? obPhieuXuat)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                ErrorMessageDTO error = await phieuXuatDAO.LayDanhSachPhieuXuat(obPhieuXuat);
                if (error.flagBiLoiEx || !error.flagThanhCong)
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
        [HttpPost]
        [Route("themphieuxuat")]
        public async Task<ActionResult<ResponseDTO>> ThemPhieuXuat(XuatPhong phieuXuat)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                ErrorMessageDTO error = await phieuXuatDAO.ThemPhieuXuat(phieuXuat);
                if (error.flagBiLoiEx != !error.flagThanhCong)
                {
                    responseDTO.statusCode = HttpStatusCode.OK;
                    responseDTO.errorCode = Convert.ToInt32(ErrorCodeEnum.KhongTheThem).ToString();
                    responseDTO.message = ResponseDTO.GetValueError(ErrorCodeEnum.KhongTheThem);
                    return Ok(responseDTO);
                }

                responseDTO.statusCode = HttpStatusCode.OK;
                responseDTO.errorCode = Convert.ToInt32(ErrorCodeEnum.ThemThanhCong).ToString();
                responseDTO.message = ResponseDTO.GetValueError(ErrorCodeEnum.ThemThanhCong);
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
        [Route("CapNhat-PX")]
        public async Task<ActionResult<PhieuXuat>> CapNhatPX(PhieuXuatDTO dv)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                ErrorMessageDTO error = await phieuXuatDAO.CapNhatPX(dv);
                if (error.flagBiLoiEx || !error.flagThanhCong)
                {

                    responseDTO.errorCode = Convert.ToInt32(ErrorCodeEnum.KhongTheCapNhat).ToString();
                    responseDTO.message = ResponseDTO.GetValueError(ErrorCodeEnum.KhongTimThay);
                    return Ok(responseDTO);

                }
                if (error.data == null)
                {

                    responseDTO.errorCode = Convert.ToInt32(ErrorCodeEnum.KhongTimThay).ToString();
                    responseDTO.message = ResponseDTO.GetValueError(ErrorCodeEnum.KhongTimThay);
                    return Ok(responseDTO);
                }
                responseDTO.statusCode = HttpStatusCode.OK;
                responseDTO.message = ResponseDTO.GetValueError(ErrorCodeEnum.CapNhatThanhCong);
                responseDTO.errorCode = Convert.ToInt32(ErrorCodeEnum.CapNhatThanhCong).ToString();
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

using DAO;
using DTO.Context;
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
    [Route("api/PhieuDatPhongChiTiet")]
    [ApiController]
    public class PhieuDatPhongChiTietController : Controller
    {


        private readonly QuanLyNhaHangKhachSanContext dbcontext;
        private readonly PhieuDatPhongChiTietDAO PhieuDatPhongChiTietDAO;

        public PhieuDatPhongChiTietController(QuanLyNhaHangKhachSanContext dbcontext, PhieuDatPhongChiTietDAO PhieuDatPhongChiTietDAO)
        {
            this.PhieuDatPhongChiTietDAO = PhieuDatPhongChiTietDAO;
            this.dbcontext = dbcontext;
        }
        [HttpPost]
        [Route("danhsach-PhieuDatPhongChiTiet")]
        public async Task<ActionResult<ResponseDTO>> LayDanhSachPhieuDatPhongChiTiet()
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                ErrorMessageDTO error = await PhieuDatPhongChiTietDAO.LayDanhSachPhieuDatPhongChiTiet();
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
        [Route("layPhieuDatPhongChiTiet")]
        public async Task<ActionResult<ResponseDTO>> LayPhieuDatPhongChiTiet(PhieuDatDTO phieuDat)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                ErrorMessageDTO error = await PhieuDatPhongChiTietDAO.LayPhieuDatPhongChiTiet(phieuDat);
                if (error.flagBiLoiEx || !error.flagThanhCong)
                {
                    responseDTO.statusCode = HttpStatusCode.OK;
                    responseDTO.errorCode = error.errorCode;
                    responseDTO.message = error.message;
                    return Ok(responseDTO);
                }
                if (error.data == null)
                {
                    responseDTO.statusCode = HttpStatusCode.OK;
                    responseDTO.errorCode = Convert.ToInt32(ErrorCodeEnum.KhongTimThay).ToString();
                    responseDTO.message = ResponseDTO.GetValueError(ErrorCodeEnum.KhongTimThay);
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
        [Route("them-PhieuDatPhongChiTiet")]
        public async Task<ActionResult<ResponseDTO>> ThemPhieuDatPhongChiTiet(PhieuDatPhongChiTietDTO obPhieuDatPhongChiTiet)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                ErrorMessageDTO error = await PhieuDatPhongChiTietDAO.ThemPhieuDatPhongChiTiet(obPhieuDatPhongChiTiet);
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
        [Route("capnhat-PhieuDatPhongChiTiet")]
        public async Task<ActionResult<ResponseDTO>> CapNhatPhieuDatPhongChiTiet(PhieuDatPhongChiTietDTO obPhieuDatPhongChiTiet)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                ErrorMessageDTO error = await PhieuDatPhongChiTietDAO.CapNhatPhieuDatPhongChiTiet(obPhieuDatPhongChiTiet);
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
        [HttpDelete]
        [Route("xoa-PhieuDatPhongChiTiet")]
        public async Task<ActionResult<ResponseDTO>> XoaSanPham(int PhieuDatPhongChiTietID)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                ErrorMessageDTO error = await PhieuDatPhongChiTietDAO.XoaPhieuDatPhongChiTiet(PhieuDatPhongChiTietID);
                if (error.flagBiLoiEx)
                {
                    responseDTO.statusCode = HttpStatusCode.OK;
                    responseDTO.errorCode = Convert.ToInt32(ErrorCodeEnum.NotFound).ToString();
                    responseDTO.message = ResponseDTO.GetValueError(ErrorCodeEnum.KhongTheXoa);
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
                responseDTO.errorCode = error.errorCode;
                responseDTO.message = error.message;
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

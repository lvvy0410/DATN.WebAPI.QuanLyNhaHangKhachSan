using DAO;
using DTO.Context;
using DTO.Model;
using DTO.publicDTO;
using DTO.Public;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DATN.WebAPI.QuanLyNhaHangKhachSan.Controllers
{
    [Route("api/PhieuDat")]
    [ApiController]
    public class PhieuDatController : Controller
    {


        private readonly QuanLyNhaHangKhachSanContext dbcontext;
        private readonly PhieuDatDAO PhieuDatDAO;

        public PhieuDatController(QuanLyNhaHangKhachSanContext dbcontext, PhieuDatDAO PhieuDatDAO)
        {
            this.PhieuDatDAO = PhieuDatDAO;
            this.dbcontext = dbcontext;
        }
        [HttpPost]
        [Route("danhsach-PhieuDat")]
        public async Task<ActionResult<ResponseDTO>> LayDanhSachPhieuDat()
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                ErrorMessageDTO error = await PhieuDatDAO.LayDanhSachPhieuDat();
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
        [HttpGet]
        [Route("{PhieuDatID}")]
        public async Task<ActionResult<ResponseDTO>> LayPhieuDat(Int32 PhieuDatID)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                ErrorMessageDTO error = await PhieuDatDAO.LayPhieuDat(PhieuDatID);
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
        [Route("them-PhieuDat")]
        public async Task<ActionResult<ResponseDTO>> ThemPhieuDat(PhieuDatDTO obPhieuDat)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                ErrorMessageDTO error = await PhieuDatDAO.ThemPhieuDat(obPhieuDat);
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
        [Route("capnhat-PhieuDat")]
        public async Task<ActionResult<ResponseDTO>> CapNhatPhieuDat(PhieuDatDTO obPhieuDat)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                ErrorMessageDTO error = await PhieuDatDAO.CapNhatPhieuDat(obPhieuDat);
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
        [Route("xoa-PhieuDat")]
        public async Task<ActionResult<ResponseDTO>> XoaSanPham(int PhieuDatID)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                ErrorMessageDTO error = await PhieuDatDAO.XoaPhieuDat(PhieuDatID);
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

using DAO;
using DTO.Context;
using DTO.Model;
using DTO.Public;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DATN.WebAPI.QuanLyNhaHangKhachSan.Controllers
{
    [Route("api/khachhang")]
    [ApiController]
    public class KhachHangController : Controller
    {
        
        
            private readonly QuanLyNhaHangKhachSanContext dbcontext;
            private readonly KhachHangDAO khachHangDAO;

            public KhachHangController(QuanLyNhaHangKhachSanContext dbcontext, KhachHangDAO khachHangDAO)
            {
                this.khachHangDAO = khachHangDAO;
                this.dbcontext = dbcontext;
            }
            [HttpPost]
            [Route("danhsach-khachhang")]
            public async Task<ActionResult<ResponseDTO>> LayDanhSachKhachHang()
            {
                ResponseDTO responseDTO = new ResponseDTO();
                try
                {
                    ErrorMessageDTO error = await khachHangDAO.LayDanhSachKhachHang();
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
            [Route("{khachHangID}")]
            public async Task<ActionResult<ResponseDTO>> LayKhachHang(Int32 khachHangID)
            {
                ResponseDTO responseDTO = new ResponseDTO();
                try
                {
                    ErrorMessageDTO error = await khachHangDAO.LayKhachHang(khachHangID);
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
            [Route("them-khachhang")]
            public async Task<ActionResult<ResponseDTO>> ThemKhachHang(KhachHang obkhachHang)
            {
                ResponseDTO responseDTO = new ResponseDTO();
                try
                {
                    ErrorMessageDTO error = await khachHangDAO.ThemKhachHang(obkhachHang);
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
            [Route("capnhat-khachhang")]
            public async Task<ActionResult<ResponseDTO>> CapNhatkhachHang(KhachHang obkhachHang)
            {
                ResponseDTO responseDTO = new ResponseDTO();
                try
                {
                    ErrorMessageDTO error = await khachHangDAO.CapNhatKhachHang(obkhachHang);
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
            [Route("xoa-khachhang")]
            public async Task<ActionResult<ResponseDTO>> XoaSanPham(int KhachHangID)
            {
                ResponseDTO responseDTO = new ResponseDTO();
                try
                {
                    ErrorMessageDTO error = await khachHangDAO.XoaKhachHang(KhachHangID);
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

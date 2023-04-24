﻿using DAO;
using DTO.Context;
using DTO.DieuKienLoc;
using DTO.Public;
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
    }
}

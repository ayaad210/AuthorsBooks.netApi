using AuthorsAngularTask.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorsAngularTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {


        private readonly IHostEnvironment _env;
        public FilesController(IHostEnvironment env)
        {
                 
            _env = env;

        }

        [HttpPost]
        [Route("save/{old?}")]
        public async Task<ActionResult<ApiResponse<string>>> saveImageAsync([FromRoute] string old)
        {


            #region save image
            ApiResponse<string> resp = new ApiResponse<string>();
                
            string photofileName = await Task.Factory.StartNew(() =>
            {
                try
                {
                    var httprequest = Request.Form;//من هناك هبعت الصورة فشكل فورم داتا
                    var postedFile = httprequest.Files[0];
                    try
                    {
                        if (old != postedFile.FileName && old != "default.jpg")//تاكيد ملوش لازمة
                        {
                            //مسح القديم
                            FileInfo f = new FileInfo(_env.ContentRootPath + "/photos/" + old);
                            f.Delete();
                        }
                        else
                        {
                            return old;
                        }

                    }
                    catch (Exception)
                    {
                    }
                    string newfileName = Path.GetFileName( postedFile.FileName)+(DateTime.Now.Second + DateTime.Now.Millisecond).ToString() +Path.GetExtension(postedFile.FileName) ;
                    var physicalpath = _env.ContentRootPath + "/photos/" + newfileName;
                    using (var stream = new FileStream(physicalpath, FileMode.Create))
                    {
                        postedFile.CopyTo(stream);
                    }

                    return newfileName;
                }
                catch (Exception)
                {
                    
                    return "default.jpg";

                }

            });


            resp.MesasageId = 1;
            resp.Mesasage = "Success";
            resp.Data = new List<string> { photofileName  };

            return Ok(resp);
            #endregion
        }
    }
}

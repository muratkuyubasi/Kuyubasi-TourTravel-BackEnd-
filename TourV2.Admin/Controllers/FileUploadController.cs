using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourV2.Helper;

namespace TourV2.Admin.Controllers
{
    /// <summary>
    /// Action
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class FileUploadController : BaseController
    {
        private IWebHostEnvironment _webHostEnvironment;

        /// <summary>
        /// File
        /// </summary>
        /// <param name="mediator"></param>
        public FileUploadController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        /// <summary>
        /// Upload File
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>

        [HttpPost]
        [Produces("application/json", "application/xml")]
        public async Task<IActionResult> Index([FromForm] FileUploadDTO fileUploadDTO)
        {
            fileUploadDTO.RootPath = _webHostEnvironment.WebRootPath;
            return Ok(FileUpload.UploadDocument(fileUploadDTO));
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RollsApi.Controllers
{
    [ApiController]
    [TypeFilter(typeof(TokenCheck))]
    public class DesignationController : Controller
    {
        private readonly IDesignationRepo _designationRepo;

        public DesignationController(IDesignationRepo designationRepo)
        {
            _designationRepo = designationRepo;
        }

        [HttpGet]
        [Route("designation/all")]
        public async Task<JsonResult> GetDesignation()
        {
            var status = 401;
            var title = "Not Found";
            IList<Designation> dataObj = await _designationRepo.GetDesignationsAsync();
            if (dataObj.Count > 0)
            {
                status = 200;
                title = "OK";
            }

            var jsonObj = new
            {
                status = status,
                title = title,
                dataObj = dataObj
            };
            return Json(jsonObj);
        }

        [HttpPost]
        [Route("designation/add")]
        public async Task<JsonResult> DesignationAdd([FromBody] DesignationAddEditVM dataObj)
        {
            var status = 402;
            var title = "Add Error";
            long result = await _designationRepo.DesignationAddAsync(dataObj);
            if (result > 0)
            {
                status = 200;
                title = "OK";
            }

            var jsonObj = new
            {
                status = status,
                title = title,
                dataObj = result
            };
            return Json(jsonObj);
        }

        [HttpPost]
        [Route("designation/name/exists")]
        public async Task<JsonResult> DesignationNameExists([FromBody] IsExistsVM requestObj)
        {
            var status = 401;
            var title = "Not Found";
            Designation dataObj = await _designationRepo.DesignationNameExistsAsync(requestObj);

            if (dataObj != null)
            {
                status = 200;
                title = "OK";
            }

            var jsonObj = new
            {
                status = status,
                title = title,
                dataObj = dataObj
            };
            return Json(jsonObj);
        }

        [HttpPost]
        [Route("designation/delete")]
        public async Task<JsonResult> DesignationDelete([FromBody] DesignationDeleteVM dataObj)
        {
            var status = 402;
            var title = "Delete Error";
            long result = await _designationRepo.DesignationDeleteAsync(dataObj);
            if (result > 0)
            {
                status = 200;
                title = "OK";
            }

            var jsonObj = new
            {
                status = status,
                title = title,
                dataObj = result
            };
            return Json(jsonObj);
        }

        [HttpGet]
        [Route("designation/deleted/all")]
        public async Task<JsonResult> GetDeletedDesignations()
        {
            var status = 401;
            var title = "Not Found";
            IList<Designation> dataObj = await _designationRepo.GetDeletedDesignationsAsync();
            if (dataObj.Count > 0)
            {
                status = 200;
                title = "OK";
            }

            var jsonObj = new
            {
                status = status,
                title = title,
                dataObj = dataObj
            };
            return Json(jsonObj);
        }

        [HttpPost]
        [Route("designation/restore")]
        public async Task<JsonResult> RestoreAsync([FromBody] DesignationDeleteVM dataObj)
        {
            var status = 402;
            var title = "Restore Error";
            long result = await _designationRepo.DesignationRestoreAsync(dataObj);
            if (result > 0)
            {
                status = 200;
                title = "OK";
            }

            var jsonObj = new
            {
                status = status,
                title = title,
                dataObj = result
            };
            return Json(jsonObj);
        }

        [HttpPost]
        [Route("designation/permanent/delete")]
        public async Task<JsonResult> DesignationPermanentDeleteAsync([FromBody] DesignationDeleteVM dataObj)
        {
            var status = 402;
            var title = "Permanent Delete Error";
            long result = await _designationRepo.DesignationPermanentDeleteAsync(dataObj);
            if (result > 0)
            {
                status = 200;
                title = "OK";
            }

            var jsonObj = new
            {
                status = status,
                title = title,
                dataObj = result
            };
            return Json(jsonObj);
        }

        [HttpGet]
        [Route("designation/dropdown")]
        public async Task<JsonResult> DesignationDropDown()
        {
            var status = 401;
            var title = "Not Found";
            IList<DesignationDropDown> dataObj = await _designationRepo.DesignationDropDownAsync();
            if (dataObj.Count > 0)
            {
                status = 200;
                title = "OK";
            }

            var jsonObj = new
            {
                status = status,
                title = title,
                dataObj = dataObj
            };
            return Json(jsonObj);
        }
    }
}

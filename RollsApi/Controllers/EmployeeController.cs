using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RollsApi.Controllers
{
    
    [ApiController]
    [TypeFilter(typeof(TokenCheck))]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepo _employeeRepo;

        public EmployeeController(IEmployeeRepo employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }

        [HttpPost]
        [Route("employee/add")]
        public async Task<JsonResult> EmployeeAdd([FromBody] EmployeeAddEditVM dataObj)
        {
            var status = 402;
            var title = "Add Error";

            long result = await _employeeRepo.EmployeeAddAsync(dataObj);

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
        [Route("employee/exists")]
        public async Task<JsonResult> EmployeeNameExists([FromBody] DoesEmployeeExist requestObj)
        {
            var status = 401;
            var title = "Not Found";

            Employee dataObj = await _employeeRepo.EmployeeExistsAsync(requestObj);

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

        [HttpGet]
        [Route("employee/all")]
        public async Task<JsonResult> GetEmployees()
        {
            var status = 401;
            var title = "Not Found";

            IList<EmployeeGet> dataObj = await _employeeRepo.GetEmployeesAsync();

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
        [Route("employee/delete")]
        public async Task<JsonResult> employeeDelete([FromBody] EmployeeDeleteVM dataObj)
        {
            var status = 402;
            var title = "Delete Error";
            long result = await _employeeRepo.EmployeeDeleteAsync(dataObj);
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
        [Route("employee/deleted/all")]
        public async Task<JsonResult> GetDeletedEmployees()
        {
            var status = 401;
            var title = "Not Found";

            IList<EmployeeGet> dataObj = await _employeeRepo.GetDeletedEmployeesAsync();

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
        [Route("employee/restore")]
        public async Task<JsonResult> RestoreAsync([FromBody] EmployeeDeleteVM dataObj)
        {
            var status = 402;
            var title = "Restore Error";
            long result = await _employeeRepo.EmployeeRestoreAsync(dataObj);
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
        [Route("employee/permanent/delete")]
        public async Task<JsonResult> DesignationPermanentDeleteAsync([FromBody] EmployeeDeleteVM dataObj)
        {
            var status = 402;
            var title = "Permanent Delete Error";
            long result = await _employeeRepo.EmployeePermanentDeleteAsync(dataObj);
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

    }
}

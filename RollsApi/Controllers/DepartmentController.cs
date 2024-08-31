using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RollsApi.Controllers
{
    [ApiController]
    [TypeFilter(typeof(TokenCheck))]

    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepo _departmentRepo;

        public DepartmentController(IDepartmentRepo departmentRepo)
        {
            _departmentRepo = departmentRepo;
        }

        [HttpGet]
        [Route("department/all")]
        public async Task<JsonResult> GetDepartmentsAsync()
        {
            var status = 401;
            var title = "Not Found";
            IList<Department> dataObj = await _departmentRepo.GetDepartmentsAsync();
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
        [Route("department/name/exists")]
        public async Task<JsonResult> DepartmentNameExistsAsync([FromBody] IsExistsVM requestObj)
        {
            var status = 401;
            var title = "Not Found";
            Department dataObj = await _departmentRepo.DepartmentNameExistsAsync(requestObj);

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
        [Route("department/add")]
        public async Task<JsonResult> DepartmentAddAsync([FromBody] DepartmentAddEditVM dataObj)
        {
            var status = 402;
            var title = "Add Error";
            long result = await _departmentRepo.DepartmentAddAsync(dataObj);
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

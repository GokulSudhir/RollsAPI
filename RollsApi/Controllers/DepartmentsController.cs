using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RollsApi.Controllers
{
    [ApiController]
    [TypeFilter(typeof(TokenCheck))]

    public class DepartmentsController : Controller
    {
        private readonly IDepartmentsRepo _departmentsRepo;

        public DepartmentsController(IDepartmentsRepo departmentsRepo)
        {
            _departmentsRepo = departmentsRepo;
        }

        [HttpGet]
        [Route("departments/all")]
        public async Task<JsonResult> GetDepartmentsAsync()
        {
            var status = 401;
            var title = "Not Found";
            IList<Departments> dataObj = await _departmentsRepo.GetDepartmentsAsync();
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
        [Route("departments/name/exists")]
        public async Task<JsonResult> DepartmentNameExistsAsync([FromBody] IsExistsVM requestObj)
        {
            var status = 401;
            var title = "Not Found";
            Departments dataObj = await _departmentsRepo.DepartmentNameExistsAsync(requestObj);

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
        [Route("departments/add")]
        public async Task<JsonResult> DepartmentAddAsync([FromBody] DepartmentsAddEditVM dataObj)
        {
            var status = 402;
            var title = "Add Error";
            long result = await _departmentsRepo.DepartmentAddAsync(dataObj);
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
        [Route("departments/delete")]
        public async Task<JsonResult> DepartmentDeleteAsync([FromBody] DepartmentDeleteVM dataObj)
        {
            var status = 402;
            var title = "Delete Error";
            long result = await _departmentsRepo.DepartmentDeleteAsync(dataObj);
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
        [Route("departments/deleted/all")]
        public async Task<JsonResult> GetDeletedDepartments()
        {
            var status = 401;
            var title = "Not Found";
            IList<Departments> dataObj = await _departmentsRepo.GetDeletedBanks();
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
        [Route("departments/restore")]
        public async Task<JsonResult> RestoreAsync([FromBody] DepartmentDeleteVM dataObj)
        {
            var status = 402;
            var title = "Restore Error";
            long result = await _departmentsRepo.DepartmentRestoreAsync(dataObj);
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
        [Route("department/permanent/delete")]
        public async Task<JsonResult> BankPermanentDeleteAsync([FromBody] DepartmentDeleteVM dataObj)
		{
			var status = 402;
			var title = "Permanent Delete Error";
			long result = await _departmentsRepo.DepartmentPermanentDeleteAsync(dataObj);
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

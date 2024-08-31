using Microsoft.AspNetCore.Mvc;

namespace RollsApi.Controllers
{
	[ApiController]
	[TypeFilter(typeof(TokenCheck))]
	public class BankController : Controller
	{
		private readonly IBankRepo _bankRepo;

		public BankController(IBankRepo bankRepo)
		{
			_bankRepo = bankRepo;
		}

		[HttpGet]
		[Route("bank/all")]
		public async Task<JsonResult> GetBanksAsync()
		{
			var status = 401;
			var title = "Not Found";
			IList<Bank> dataObj = await _bankRepo.GetBanks();
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
		[Route("bank/add")]
		public async Task<JsonResult> BankAddAsync([FromBody] BankAddEditVM dataObj)
		{
			var status = 402;
			var title = "Add Error";
			long result = await _bankRepo.BankAddAsync(dataObj);
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
		[Route("bank/name/exists")]
		public async Task<JsonResult> BankNameExistsAsync([FromBody] IsExistsVM requestObj)
		{
			var status = 401;
			var title = "Not Found";
			Bank dataObj = await _bankRepo.BankNameExistsAsync(requestObj);

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
		[Route("bank/delete")]
		public async Task<JsonResult> BankDeleteAsync([FromBody] IdVM dataObj)
		{
			var status = 402;
			var title = "Delete Error";
			long result = await _bankRepo.BankDeleteAsync(dataObj);
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
		[Route("bank/deleted/all")]
		public async Task<JsonResult> GetDeletedBanksAsync()
		{
			var status = 401;
			var title = "Not Found";
			IList<Bank> dataObj = await _bankRepo.GetDeletedBanks();
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
		[Route("bank/restore")]
		public async Task<JsonResult> BankRestoreAsync([FromBody] IdVM dataObj)
		{
			var status = 402;
			var title = "Restore Error";
			long result = await _bankRepo.BankRestoreAsync(dataObj);
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
		[Route("bank/permanent/delete")]
		public async Task<JsonResult> BankPermanentDeleteAsync([FromBody] IdVM dataObj)
		{
			var status = 402;
			var title = "Permanent Delete Error";
			long result = await _bankRepo.BankPermanentDeleteAsync(dataObj);
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

using Microsoft.AspNetCore.Mvc;

namespace RollsApi.Controllers
{
  [ApiController]
  [TypeFilter(typeof(TokenCheck))]
  public class CountryController : Controller
  {
    private readonly ICountryRepo _countryRepo;

    public CountryController(ICountryRepo countryRepo)
    {
      _countryRepo = countryRepo;
    }

    [HttpGet]
    [Route("country/all")]
    public async Task<JsonResult> GetCountriesAsync()
    {
      var status = 401;
      var title = "Not Found";
      IList<Country> dataObj = await _countryRepo.GetCountries();
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
    [Route("country/name/exists")]
    public async Task<JsonResult> CountryNameExistsAsync([FromBody] IsExistsVM requestObj)
    {
      var status = 401;
      var title = "Not Found";
      Country dataObj = await _countryRepo.CountryNameExistsAsync(requestObj);
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
  }
}

using RollsApi.ViewModels;

namespace RollsApi.Interfaces
{
  public interface ICountryRepo
  {
    public Task<Country> CountryNameExistsAsync(IsExistsVM dataObj);

    public Task<IList<Country>> GetCountries();
  }
}

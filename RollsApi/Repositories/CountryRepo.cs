
namespace RollsApi.Repositories
{
    public class CountryRepo : ICountryRepo
    {
        private readonly DapperContext _dapperContext;
        public CountryRepo(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<Country> CountryNameExistsAsync(IsExistsVM dataObj)
        {
            Country data = new Country();

            var q1 = "select * from countries where lower(country_name) = @a and country_id <> @b";
            var q2 = "select * from countries where lower(country_name) = @a";

            //var q1 = "select * from " + dataObj.table_name + " where " + dataObj.name_field + " = @a and " + dataObj.id_field + " <> @b";
            //var q2 = "select * from " + dataObj.table_name + " where "+ dataObj.name_field + " = @a";

            using (var connection = _dapperContext.CreateConnection())
            {
                connection.Open();
                try
                {
                    if (dataObj.id > 0)
                    {
                        var r1 = await connection.QueryAsync<Country>(q1, new
                        {
                            a = dataObj.str1.ToLower().Trim(),
                            b = dataObj.id
                        });
                        data = r1.SingleOrDefault();
                    }
                    else
                    {
                        var r2 = await connection.QueryAsync<Country>(q2, new
                        {
                            a = dataObj.str1.ToLower().Trim()
                        });

                        data = r2.SingleOrDefault();
                    }
                }
                catch (Exception err)
                {
                    var error = err.GetBaseException().Message;
                }
            }
            return data;
        }


        public async Task<IList<Country>> GetCountries()
        {
            IList<Country> dataObj = new List<Country>();

            var q = "select * from countries where record_status = 'ACTIVE'";

            try
            {
                using (var connection = _dapperContext.CreateConnection())
                {
                    connection.Open();
                    try
                    {
                        var r = await connection.QueryAsync<Country>(q);
                        dataObj = r.ToList();
                    }
                    catch (Exception err)
                    {
                        Log.Error(err, $"Get Countries : {err.GetBaseException().Message}");
                    }
                }
            }
            catch (Exception err)
            {
                Log.Error(err, $"Db Connection : {err.GetBaseException().Message}");
            }

            return dataObj;
        }

    }
}



using RollsApi.Models;

namespace RollsApi.Repositories
{
    public class DesignationRepo : IDesignationRepo
    {
        private readonly DapperContext _dapperContext;

        public DesignationRepo(DapperContext context)
        {
            _dapperContext = context;
        }

        public async Task<IList<Designation>> GetDesignationsAsync()
        {
            IList<Designation> dataObj = new List<Designation>();

            var query = "select * from designations where record_status = 'ACTIVE'";

            try
            {
                using (var connection = _dapperContext.CreateConnection())
                {
                    connection.Open();
                    try
                    {
                        var r = await connection.QueryAsync<Designation>(query);
                        dataObj = r.ToList();
                    }
                    catch (Exception err)
                    {
                        Log.Error(err, $"Get Designations : {err.GetBaseException().Message}");
                    }
                }
            }
            catch (Exception err)
            {
                Log.Error(err, $"Db Connection : {err.GetBaseException().Message}");
            }

            return dataObj;
        }

        public async Task<long> DesignationAddAsync(DesignationAddEditVM dataObj)
        {
            long data = 0;

            //add
            StringBuilder q = new StringBuilder();
            q.Append("insert into designations (designation_name, designation_category, record_status) ");
            q.Append("values(@a,@b,@c) ");
            q.Append(" returning designation_id");

            var p = new DynamicParameters();

            p.Add(name: "a", value: dataObj.designation_name, direction: System.Data.ParameterDirection.Input);
            p.Add(name: "b", value: dataObj.designation_category, direction: System.Data.ParameterDirection.Input);
            p.Add(name: "c", value: dataObj.record_status, direction: System.Data.ParameterDirection.Input);

            //edit
            StringBuilder q2 = new StringBuilder();
            q2.Append("update designations set designation_name = @d, designation_category = @e where designation_id = @f");

            var p2 = new DynamicParameters();
            p2.Add(name: "d", value: dataObj.designation_name, direction: System.Data.ParameterDirection.Input);
            p2.Add(name: "e", value: dataObj.designation_category, direction: System.Data.ParameterDirection.Input);
            p2.Add(name: "f", value: dataObj.designation_id, direction: System.Data.ParameterDirection.Input);

            try
            {
                using (var connection = _dapperContext.CreateConnection())
                {
                    connection.Open();
                    var transaction = connection.BeginTransaction();
                    try
                    {
                        if (dataObj.designation_id is null)
                        {
                            long r = await connection.QuerySingleAsync<long>(q.ToString(), p);
                            data = r;
                        }
                        else
                        {
                            var r = await connection.ExecuteAsync(q2.ToString(), p2);
                            data = r;
                        }

                        transaction.Commit();
                    }
                    catch (Exception err)
                    {
                        transaction.Rollback();
                        Log.Error(err, $"Designation Entry Error:{err.GetBaseException().Message}");
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            catch (Exception err)
            {
                Log.Error(err, $"Db Conn Error:{err.GetBaseException().Message}");
            }

            return data;
        }

        public async Task<Designation> DesignationNameExistsAsync(IsExistsVM dataObj)
        {
            Designation data = new Designation();

            var q1 = "select * from designations where designation_name = @a and designation_id <> @c";
            var q2 = "select * from designations where designation_name = @a";

            using (var connection = _dapperContext.CreateConnection())
            {
                connection.Open();
                try
                {
                    if (dataObj.id > 0)
                    {
                        var r1 = await connection.QueryAsync<Designation>(q1, new
                        {
                            a = dataObj.str1.Trim(),
                            b = dataObj.str2.Trim(),
                            c = dataObj.id
                        });

                        data = r1.SingleOrDefault();
                    }
                    else
                    {
                        var r2 = await connection.QueryAsync<Designation>(q2, new
                        {
                            a = dataObj.str1.Trim(),
                            b = dataObj.str2.Trim()
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

        public async Task<long> DesignationDeleteAsync(DesignationDeleteVM dataObj)
        {
            long data = 0;
            //delete
            StringBuilder q = new StringBuilder();
            q.Append("update designations set record_status = 'DELETED' where designation_id = @a");

            var p = new DynamicParameters();

            p.Add(name: "a", value: dataObj.designation_id, direction: System.Data.ParameterDirection.Input);

            try
            {
                using (var connection = _dapperContext.CreateConnection())
                {
                    connection.Open();
                    var transaction = connection.BeginTransaction();
                    try
                    {

                        var r = await connection.ExecuteAsync(q.ToString(), p);
                        data = r;

                        transaction.Commit();
                    }
                    catch (Exception err)
                    {
                        transaction.Rollback();
                        Log.Error(err, $"Designation Delete Error:{err.GetBaseException().Message}");
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            catch (Exception err)
            {
                Log.Error(err, $"Db Conn Error:{err.GetBaseException().Message}");
            }

            return data;
        }

        public async Task<IList<Designation>> GetDeletedDesignationsAsync()
        {
            IList<Designation> dataObj = new List<Designation>();

            var q = "select * from designations where record_status = 'DELETED'";

            try
            {
                using (var connection = _dapperContext.CreateConnection())
                {
                    connection.Open();
                    try
                    {
                        var r = await connection.QueryAsync<Designation>(q);
                        dataObj = r.ToList();
                    }
                    catch (Exception err)
                    {
                        Log.Error(err, $"Cannot get deleted Designations : {err.GetBaseException().Message}");
                    }
                }
            }
            catch (Exception err)
            {
                Log.Error(err, $"Db Connection : {err.GetBaseException().Message}");
            }

            return dataObj;
        }

        public async Task<long> DesignationRestoreAsync(DesignationDeleteVM dataObj)
        {
            long data = 0;
            //restore
            StringBuilder q = new StringBuilder();
            q.Append("update designations set record_status = 'ACTIVE' where designation_id = @a");

            var p = new DynamicParameters();

            p.Add(name: "a", value: dataObj.designation_id, direction: System.Data.ParameterDirection.Input);

            try
            {
                using (var connection = _dapperContext.CreateConnection())
                {
                    connection.Open();
                    var transaction = connection.BeginTransaction();
                    try
                    {
                        var r = await connection.ExecuteAsync(q.ToString(), p);
                        data = r;

                        transaction.Commit();
                    }
                    catch (Exception err)
                    {
                        transaction.Rollback();
                        Log.Error(err, $"Designation Restore Error:{err.GetBaseException().Message}");
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            catch (Exception err)
            {
                Log.Error(err, $"Db Conn Error:{err.GetBaseException().Message}");
            }

            return data;
        }

        public async Task<long> DesignationPermanentDeleteAsync(DesignationDeleteVM dataObj)
        {
            long data = 0;

            StringBuilder q = new StringBuilder();
            q.Append("delete from designations  where designation_id = @a");

            var p = new DynamicParameters();

            p.Add(name: "a", value: dataObj.designation_id, direction: System.Data.ParameterDirection.Input);

            try
            {
                using (var connection = _dapperContext.CreateConnection())
                {
                    connection.Open();
                    var transaction = connection.BeginTransaction();
                    try
                    {

                        var r = await connection.ExecuteAsync(q.ToString(), p);
                        data = r;

                        transaction.Commit();
                    }
                    catch (Exception err)
                    {
                        transaction.Rollback();
                        Log.Error(err, $"Designation permanent delete Error:{err.GetBaseException().Message}");
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            catch (Exception err)
            {
                Log.Error(err, $"Db Conn Error:{err.GetBaseException().Message}");
            }

            return data;
        }

        public async Task<IList<DesignationDropDown>> DesignationDropDownAsync()
        {
            IList<DesignationDropDown> dataObj = new List<DesignationDropDown>();

            var query = "select designation_id, designation_name from designations where record_status = 'ACTIVE'";

            try
            {
                using (var connection = _dapperContext.CreateConnection())
                {
                    connection.Open();
                    try
                    {
                        var r = await connection.QueryAsync<DesignationDropDown>(query);
                        dataObj = r.ToList();
                    }
                    catch (Exception err)
                    {
                        Log.Error(err, $"Get Designations : {err.GetBaseException().Message}");
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

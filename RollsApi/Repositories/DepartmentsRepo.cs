
using RollsApi.Data;

namespace RollsApi.Repositories
{
    public class DepartmentsRepo : IDepartmentsRepo
    {
        private readonly DapperContext _dapperContext;

        public DepartmentsRepo(DapperContext context)
        {
            _dapperContext = context;
        }

        public async Task<long> DepartmentAddAsync(DepartmentsAddEditVM dataObj)
        {
            long data = 0;

            //add
            StringBuilder q = new StringBuilder();
            q.Append("insert into departmentss(department_name, department_classification, record_status) ");
            q.Append("values(@a,@b,@c) ");
            q.Append(" returning department_id");

            var p = new DynamicParameters();

            p.Add(name: "a", value: dataObj.department_name, direction: System.Data.ParameterDirection.Input);
            p.Add(name: "b", value: dataObj.department_classification, direction: System.Data.ParameterDirection.Input);
            p.Add(name: "c", value: dataObj.record_status, direction: System.Data.ParameterDirection.Input);

            //edit
            StringBuilder q2 = new StringBuilder();
            q2.Append("update departmentss set department_name = @d, department_classification = @e where department_id = @f");

            var p2 = new DynamicParameters();
            p2.Add(name: "d", value: dataObj.department_name, direction: System.Data.ParameterDirection.Input);
            p2.Add(name: "e", value: dataObj.department_classification, direction: System.Data.ParameterDirection.Input);
            p2.Add(name: "f", value: dataObj.department_id, direction: System.Data.ParameterDirection.Input);

            try
            {
                using (var connection = _dapperContext.CreateConnection())
                {
                    connection.Open();
                    var transaction = connection.BeginTransaction();
                    try
                    {
                        if (dataObj.department_id is null)
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
                        Log.Error(err, $"Banks Entry Error:{err.GetBaseException().Message}");
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

        public async Task<long> DepartmentDeleteAsync(DepartmentDeleteVM dataObj)
        {
            long data = 0;
            //delete
            StringBuilder q = new StringBuilder();
            q.Append("update departmentss set record_status = 'DELETED' where department_id = @a");

            var p = new DynamicParameters();

            p.Add(name: "a", value: dataObj.department_id, direction: System.Data.ParameterDirection.Input);

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
                        Log.Error(err, $"Department Deleted Error:{err.GetBaseException().Message}");
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

        public async Task<Departments> DepartmentNameExistsAsync(IsExistsVM dataObj)
        {
            Departments data = new Departments();

            var q1 = "select * from departmentss where department_name = @a and department_id <> @c";
            var q2 = "select * from departmentss where department_name = @a";

            using (var connection = _dapperContext.CreateConnection())
            {
                connection.Open();
                try
                {
                    if (dataObj.id > 0)
                    {
                        var r1 = await connection.QueryAsync<Departments>(q1, new
                        {
                            a = dataObj.str1.Trim(),
                            b = dataObj.str2.Trim(),
                            c = dataObj.id
                        });

                        data = r1.SingleOrDefault();
                    }
                    else
                    {
                        var r2 = await connection.QueryAsync<Departments>(q2, new
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

        public async Task<long> DepartmentPermanentDeleteAsync(DepartmentDeleteVM dataObj)
        {
            long data = 0;
            //restore
            StringBuilder q = new StringBuilder();
            q.Append("delete from departmentss  where department_id = @a");

            var p = new DynamicParameters();

            p.Add(name: "a", value: dataObj.department_id, direction: System.Data.ParameterDirection.Input);

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
                        Log.Error(err, $"Department permanent delete Error:{err.GetBaseException().Message}");
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

        public async Task<long> DepartmentRestoreAsync(DepartmentDeleteVM dataObj)
        {
            long data = 0;
            //restore
            StringBuilder q = new StringBuilder();
            q.Append("update departmentss set record_status = 'ACTIVE' where department_id = @a");

            var p = new DynamicParameters();

            p.Add(name: "a", value: dataObj.department_id, direction: System.Data.ParameterDirection.Input);

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
                        Log.Error(err, $"Department Restore Error:{err.GetBaseException().Message}");
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

        public async Task<IList<Departments>> GetDeletedBanks()
        {
            IList<Departments> dataObj = new List<Departments>();

            var q = "select * from departmentss where record_status = 'DELETED'";

            try
            {
                using (var connection = _dapperContext.CreateConnection())
                {
                    connection.Open();
                    try
                    {
                        var r = await connection.QueryAsync<Departments>(q);
                        dataObj = r.ToList();
                    }
                    catch (Exception err)
                    {
                        Log.Error(err, $"Get deleted Departments : {err.GetBaseException().Message}");
                    }
                }
            }
            catch (Exception err)
            {
                Log.Error(err, $"Db Connection : {err.GetBaseException().Message}");
            }

            return dataObj;
        }

        public async Task<IList<Departments>> GetDepartmentsAsync()
        {
            IList<Departments> dataObj = new List<Departments>();

            var query = "select * from departmentss where record_status = 'ACTIVE'";

            try
            {
                using (var connection = _dapperContext.CreateConnection())
                {
                    connection.Open();
                    try
                    {
                        var r = await connection.QueryAsync<Departments>(query);
                        dataObj = r.ToList();
                    }
                    catch (Exception err)
                    {
                        Log.Error(err, $"Get Departments : {err.GetBaseException().Message}");
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

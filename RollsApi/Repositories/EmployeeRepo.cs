

namespace RollsApi.Repositories
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly DapperContext _dapperContext;

        public EmployeeRepo(DapperContext context)
        {
            _dapperContext = context;
        }

        public async Task<long> EmployeeAddAsync(EmployeeAddEditVM dataObj)
        {
            long data = 0;

            //add
            StringBuilder q = new StringBuilder();
            q.Append("insert into employees (first_name, middle_name, last_name, email_id, mobile, record_status, department_id, designation_id) ");
            q.Append("values(@a,@b,@c,@d,@e,@f,@g,@h) ");
            q.Append(" returning employee_id");

            var p = new DynamicParameters();

            p.Add(name: "a", value: dataObj.first_name, direction: System.Data.ParameterDirection.Input);
            p.Add(name: "b", value: dataObj.middle_name, direction: System.Data.ParameterDirection.Input);
            p.Add(name: "c", value: dataObj.last_name, direction: System.Data.ParameterDirection.Input);
            p.Add(name: "d", value: dataObj.email_id, direction: System.Data.ParameterDirection.Input);
            p.Add(name: "e", value: dataObj.mobile, direction: System.Data.ParameterDirection.Input);
            p.Add(name: "f", value: dataObj.record_status, direction: System.Data.ParameterDirection.Input);
            p.Add(name: "g", value: dataObj.department_id, direction: System.Data.ParameterDirection.Input);
            p.Add(name: "h", value: dataObj.designation_id, direction: System.Data.ParameterDirection.Input);

            //edit
            StringBuilder q2 = new StringBuilder();
            q2.Append("update employees set first_name = @i, middle_name = @j, last_name = @k, email_id = @l, mobile = @m, department_id = @n, designation_id = @o where mobile = @m");

            var p2 = new DynamicParameters();
            p2.Add(name: "i", value: dataObj.first_name, direction: System.Data.ParameterDirection.Input);
            p2.Add(name: "j", value: dataObj.middle_name, direction: System.Data.ParameterDirection.Input);
            p2.Add(name: "k", value: dataObj.last_name, direction: System.Data.ParameterDirection.Input);
            p2.Add(name: "l", value: dataObj.email_id, direction: System.Data.ParameterDirection.Input);
            p2.Add(name: "m", value: dataObj.mobile, direction: System.Data.ParameterDirection.Input);
            p2.Add(name: "n", value: dataObj.department_id, direction: System.Data.ParameterDirection.Input);
            p2.Add(name: "o", value: dataObj.designation_id, direction: System.Data.ParameterDirection.Input);

            try
            {
                using (var connection = _dapperContext.CreateConnection())
                {
                    connection.Open();
                    var transaction = connection.BeginTransaction();
                    try
                    {
                        if (dataObj.employee_id is null)
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
                        Log.Error(err, $"Employee Entry Error:{err.GetBaseException().Message}");
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

        public async Task<Employee> EmployeeExistsAsync(DoesEmployeeExist dataObj)
        {
            Employee data = new Employee();

            var q1 = "select * from employees where mobile = @a and employee_id <> @c";
            var q2 = "select * from employees where mobile = @a";

            using (var connection = _dapperContext.CreateConnection())
            {
                connection.Open();
                try
                {
                    if (dataObj.id > 0)
                    {
                        var r1 = await connection.QueryAsync<Employee>(q1, new
                        {
                            a = dataObj.mobile.Trim(),
                            c = dataObj.id
                        });

                        data = r1.SingleOrDefault();
                    }
                    else
                    {
                        var r2 = await connection.QueryAsync<Employee>(q2, new
                        {
                            a = dataObj.mobile.Trim()
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

        public async Task<IList<EmployeeGet>> GetEmployeesAsync()
        {
            IList<EmployeeGet> dataObj = new List<EmployeeGet>();

            var query = @"SELECT e.*, dp.department_name, ds.designation_name
                          FROM employees e
                          JOIN departmentss dp ON e.department_id = dp.department_id
                          JOIN designations ds ON e.designation_id = ds.designation_id
                          WHERE e.record_status = 'ACTIVE'";

            try
            {
                using (var connection = _dapperContext.CreateConnection())
                {
                    connection.Open();
                    try
                    {
                        var r = await connection.QueryAsync<EmployeeGet>(query);
                        dataObj = r.ToList();
                    }
                    catch (Exception err)
                    {
                        Log.Error(err, $"Get Employees : {err.GetBaseException().Message}");
                    }
                }
            }
            catch (Exception err)
            {
                Log.Error(err, $"Db Connection : {err.GetBaseException().Message}");
            }

            return dataObj;
        }

        public async Task<long> EmployeeDeleteAsync(EmployeeDeleteVM dataObj)
        {
            long data = 0;

            StringBuilder q = new StringBuilder();

            q.Append("update employees set record_status = 'DELETED' where employee_id = @a");

            var p = new DynamicParameters();

            p.Add(name: "a", value: dataObj.employee_id, direction: System.Data.ParameterDirection.Input);

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
                        Log.Error(err, $"Employee Delete Error:{err.GetBaseException().Message}");
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

        public async Task<IList<EmployeeGet>> GetDeletedEmployeesAsync()
        {
            IList<EmployeeGet> dataObj = new List<EmployeeGet>();

            var query = @"SELECT e.*, dp.department_name, ds.designation_name
                          FROM employees e
                          JOIN departmentss dp ON e.department_id = dp.department_id
                          JOIN designations ds ON e.designation_id = ds.designation_id
                          WHERE e.record_status = 'DELETED'";


            try
            {
                using (var connection = _dapperContext.CreateConnection())
                {
                    connection.Open();
                    try
                    {
                        var r = await connection.QueryAsync<EmployeeGet>(query);
                        dataObj = r.ToList();
                    }
                    catch (Exception err)
                    {
                        Log.Error(err, $"Cannot get deleted Employees : {err.GetBaseException().Message}");
                    }
                }
            }
            catch (Exception err)
            {
                Log.Error(err, $"Db Connection : {err.GetBaseException().Message}");
            }

            return dataObj;
        }

        public async Task<long> EmployeeRestoreAsync(EmployeeDeleteVM dataObj)
        {
            long data = 0;
            StringBuilder q = new StringBuilder();

            q.Append("update employees set record_status = 'ACTIVE' where employee_id = @a");

            var p = new DynamicParameters();

            p.Add(name: "a", value: dataObj.employee_id, direction: System.Data.ParameterDirection.Input);

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
                        Log.Error(err, $"Employee Restore Error:{err.GetBaseException().Message}");
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

        public async Task<long> EmployeePermanentDeleteAsync(EmployeeDeleteVM dataObj)
        {
            long data = 0;

            StringBuilder q = new StringBuilder();

            q.Append("delete from employees where employee_id = @a");

            var p = new DynamicParameters();

            p.Add(name: "a", value: dataObj.employee_id, direction: System.Data.ParameterDirection.Input);

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
                        Log.Error(err, $"Employee permanent delete Error:{err.GetBaseException().Message}");
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
    }
}

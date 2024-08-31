
using RollsApi.Data;
using RollsApi.Models;

namespace RollsApi.Repositories
{
    public class DepartmentRepo : IDepartmentRepo
    {
        private readonly DapperContext _dapperContext;

        public DepartmentRepo(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<long> DepartmentAddAsync(DepartmentAddEditVM dataObj)
        {
            long data = 0;

            //add
            StringBuilder q = new StringBuilder();
            q.Append("insert into departments(department_name, department_code, record_status) ");
            q.Append("values(@a,@b,@c) ");
            q.Append(" returning department_id");

            var p = new DynamicParameters();
            /*p.Add(name: "a", value: dataObj.bank_name.Trim().ToUpper(), direction: System.Data.ParameterDirection.Input);
            p.Add(name: "b", value: dataObj.record_status.Trim().ToUpper(), direction: System.Data.ParameterDirection.Input);*/

            p.Add(name: "a", value: dataObj.department_name, direction: System.Data.ParameterDirection.Input);
            p.Add(name: "b", value: dataObj.department_code, direction: System.Data.ParameterDirection.Input);
            p.Add(name: "c", value: dataObj.record_status, direction: System.Data.ParameterDirection.Input);

            //edit
            StringBuilder q2 = new StringBuilder();
            q2.Append("update departments set department_name = @d, department_code = @e where department_id = @f");

            var p2 = new DynamicParameters();
            p2.Add(name: "d", value: dataObj.department_name, direction: System.Data.ParameterDirection.Input);
            p2.Add(name: "e", value: dataObj.department_code, direction: System.Data.ParameterDirection.Input);
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

        public async Task<Department> DepartmentNameExistsAsync(IsExistsVM dataObj)
        {
            Department data = new Department();

            var q1 = "select * from departments where (department_name = @a or department_code = @b) and department_id <> @c";
            var q2 = "select * from departments where department_name = @a or department_code = @b";

            using (var connection = _dapperContext.CreateConnection())
            {
                connection.Open();
                try
                {
                    if (dataObj.id > 0)
                    {
                        var r1 = await connection.QueryAsync<Department>(q1, new
                        {
                            a = dataObj.str1.Trim(),
                            b = dataObj.str2.Trim(),
                            c = dataObj.id
                        });

                        data = r1.SingleOrDefault();
                    }
                    else
                    {
                        var r2 = await connection.QueryAsync<Department>(q2, new
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

        public async Task<IList<Department>> GetDepartmentsAsync()
        {
            IList<Department> dataObj = new List<Department>();

            var query = "select * from departments where record_status = 'ACTIVE'";

            try
            {
                using (var connection = _dapperContext.CreateConnection())
                {
                    connection.Open();
                    try
                    {
                        var r = await connection.QueryAsync<Department>(query);
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

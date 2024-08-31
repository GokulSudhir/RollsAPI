using RollsApi.Data;

namespace RollsApi.Repositories
{
	public class BankRepo : IBankRepo
	{
		private readonly DapperContext _dapperContext;
		public BankRepo(DapperContext dapperContext)
		{
			_dapperContext = dapperContext;
		}
		public async Task<IList<Bank>> GetBanks()
		{
			IList<Bank> dataObj = new List<Bank>();

			var q = "select * from banks where record_status = 'ACTIVE'";

			try
			{
				using (var connection = _dapperContext.CreateConnection())
				{
					connection.Open();
					try
					{
						var r = await connection.QueryAsync<Bank>(q);
						dataObj = r.ToList();
					}
					catch (Exception err)
					{
						Log.Error(err, $"Get Banks : {err.GetBaseException().Message}");
					}
				}
			}
			catch (Exception err)
			{
				Log.Error(err, $"Db Connection : {err.GetBaseException().Message}");
			}

			return dataObj;
		}

		public async Task<long> BankAddAsync(BankAddEditVM dataObj)
		{
			long data = 0;

			//add
			StringBuilder q = new StringBuilder();
			q.Append("insert into banks(bank_name, record_status) ");
			q.Append("values(@a,@b) ");
			q.Append(" returning bank_id");

			var p = new DynamicParameters();
			p.Add(name: "a", value: dataObj.bank_name.Trim().ToUpper(), direction: System.Data.ParameterDirection.Input);
			p.Add(name: "b", value: dataObj.record_status.Trim().ToUpper(), direction: System.Data.ParameterDirection.Input);

			//edit
			StringBuilder q2 = new StringBuilder();
			q2.Append("update banks set bank_name = @c where bank_id = @d");

			var p2 = new DynamicParameters();
			p2.Add(name: "c", value: dataObj.bank_name.Trim().ToUpper(), direction: System.Data.ParameterDirection.Input);
			p2.Add(name: "d", value: dataObj.bank_id, direction: System.Data.ParameterDirection.Input);

			try
			{
				using (var connection = _dapperContext.CreateConnection())
				{
					connection.Open();
					var transaction = connection.BeginTransaction();
					try
					{
						if (dataObj.bank_id is null)
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

		public async Task<Bank> BankNameExistsAsync(IsExistsVM dataObj)
		{
			Bank data = new Bank();

			var q1 = "select * from banks where bank_name = @a and bank_id <> @b";
			var q2 = "select * from banks where bank_name = @a";

			using (var connection = _dapperContext.CreateConnection())
			{
				connection.Open();
				try
				{
					if (dataObj.id > 0)
					{
						var r1 = await connection.QueryAsync<Bank>(q1, new
						{
							a = dataObj.str1.Trim(),
							b = dataObj.id
						});

						data = r1.SingleOrDefault();
					}
					else
					{
						var r2 = await connection.QueryAsync<Bank>(q2, new
						{
							a = dataObj.str1.Trim()
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

		public async Task<long> BankDeleteAsync(IdVM dataObj)
		{
			long data = 0;
			//delete
			StringBuilder q = new StringBuilder();
			q.Append("update banks set record_status = 'DELETED' where bank_id = @a");

			var p = new DynamicParameters();

			p.Add(name: "a", value: dataObj.bank_id, direction: System.Data.ParameterDirection.Input);

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
						Log.Error(err, $"Banks Deleted Error:{err.GetBaseException().Message}");
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

		public async Task<IList<Bank>> GetDeletedBanks()
		{
			IList<Bank> dataObj = new List<Bank>();

			var q = "select * from banks where record_status = 'DELETED'";

			try
			{
				using (var connection = _dapperContext.CreateConnection())
				{
					connection.Open();
					try
					{
						var r = await connection.QueryAsync<Bank>(q);
						dataObj = r.ToList();
					}
					catch (Exception err)
					{
						Log.Error(err, $"Get deleted Banks : {err.GetBaseException().Message}");
					}
				}
			}
			catch (Exception err)
			{
				Log.Error(err, $"Db Connection : {err.GetBaseException().Message}");
			}

			return dataObj;
		}


		public async Task<long> BankRestoreAsync(IdVM dataObj)
		{
			long data = 0;
			//restore
			StringBuilder q = new StringBuilder();
			q.Append("update banks set record_status = 'ACTIVE' where bank_id = @a");

			var p = new DynamicParameters();

			p.Add(name: "a", value: dataObj.bank_id, direction: System.Data.ParameterDirection.Input);

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
						Log.Error(err, $"Banks Restore Error:{err.GetBaseException().Message}");
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

		public async Task<long> BankPermanentDeleteAsync(IdVM dataObj)
		{
			long data = 0;
			//restore
			StringBuilder q = new StringBuilder();
			q.Append("delete from banks  where bank_id = @a");

			var p = new DynamicParameters();

			p.Add(name: "a", value: dataObj.bank_id, direction: System.Data.ParameterDirection.Input);

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
						Log.Error(err, $"Banks permanent delete Error:{err.GetBaseException().Message}");
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

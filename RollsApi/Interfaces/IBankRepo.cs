namespace RollsApi.Interfaces
{
	public interface IBankRepo
	{
		public Task<IList<Bank>> GetBanks();
		public Task<long> BankAddAsync(BankAddEditVM dataObj);
		public Task<Bank> BankNameExistsAsync(IsExistsVM dataObj);

		public Task<long> BankDeleteAsync(IdVM dataObj);
		public Task<IList<Bank>> GetDeletedBanks();
		public Task<long> BankRestoreAsync(IdVM dataObj);
		public Task<long> BankPermanentDeleteAsync(IdVM dataObj);
	}
}

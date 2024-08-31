namespace RollsApi.Interfaces
{
    public interface IDepartmentsRepo
    {
        public Task<IList<Departments>> GetDepartmentsAsync();
        public Task<long> DepartmentAddAsync(DepartmentsAddEditVM dataObj);
        public Task<Departments> DepartmentNameExistsAsync(IsExistsVM dataObj);
        public Task<long> DepartmentDeleteAsync(DepartmentDeleteVM dataObj);
        public Task<IList<Departments>> GetDeletedBanks();
        public Task<long> DepartmentRestoreAsync(DepartmentDeleteVM dataObj);
        public Task<long> DepartmentPermanentDeleteAsync(DepartmentDeleteVM dataObj);
    }
}

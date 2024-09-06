namespace RollsApi.Interfaces
{
    public interface IEmployeeRepo
    {
        public Task<long> EmployeeAddAsync(EmployeeAddEditVM dataObj);
        public Task<Employee> EmployeeExistsAsync(DoesEmployeeExist dataObj);
        public Task<IList<EmployeeGet>> GetEmployeesAsync();
        public Task<long> EmployeeDeleteAsync(EmployeeDeleteVM dataObj);
        public Task<IList<EmployeeGet>> GetDeletedEmployeesAsync();
        public Task<long> EmployeeRestoreAsync(EmployeeDeleteVM dataObj);
        public Task<long> EmployeePermanentDeleteAsync(EmployeeDeleteVM dataObj);
    }
}

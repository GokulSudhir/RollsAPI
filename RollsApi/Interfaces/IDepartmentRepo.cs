namespace RollsApi.Interfaces
{
    public interface IDepartmentRepo
    {
        public Task<IList<Department>> GetDepartmentsAsync();
        public Task<long> DepartmentAddAsync(DepartmentAddEditVM dataObj);
        public Task<Department> DepartmentNameExistsAsync(IsExistsVM dataObj);
    }
}

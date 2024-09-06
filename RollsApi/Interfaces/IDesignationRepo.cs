namespace RollsApi.Interfaces
{
    public interface IDesignationRepo
    {
        public Task<IList<Designation>> GetDesignationsAsync();
        public Task<long> DesignationAddAsync(DesignationAddEditVM dataObj);
        public Task<Designation> DesignationNameExistsAsync(IsExistsVM dataObj);
        public Task<long> DesignationDeleteAsync(DesignationDeleteVM dataObj);
        public Task<IList<Designation>> GetDeletedDesignationsAsync();
        public Task<long> DesignationRestoreAsync(DesignationDeleteVM dataObj);
        public Task<long> DesignationPermanentDeleteAsync(DesignationDeleteVM dataObj);
        public Task<IList<DesignationDropDown>> DesignationDropDownAsync();
    }
}

namespace RollsApi.ViewModels
{
    public class EmployeeAddEditVM
    {
        public long? employee_id { get; set; }

        public string first_name { get; set; }

        public string? first_name_edit { get; set; }

        public string middle_name { get; set; }

        public string? middle_name_edit { get; set; }

        public string last_name { get; set; }

        public string? last_name_edit { get; set; }

        public string email_id { get; set; }

        public string? email_id_edit { get; set; }

        public string mobile { get; set; }

        public string? mobile_edit { get; set; }

        public long department_id { get; set; }

        public long? department_id_edit { get; set; }

        public long designation_id { get; set; }

        public long? designation_id_edit { get; set; }

        public IList<string>? errors { get; set; }

        public UserClaimVM? user_claims { get; set; }

        public string? record_status { get; set; }

        public string? message { get; set; }
    }
}

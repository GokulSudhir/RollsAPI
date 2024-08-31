using System.ComponentModel;

namespace RollsApi.ViewModels
{
    public class DepartmentsAddEditVM
    {
        public long? department_id { get; set; }

        [DisplayName("Department Name")]
        public string department_name { get; set; }

        [DisplayName("Department Classification")]
        public string department_classification { get; set; }
        public IList<string>? errors { get; set; }

        public UserClaimVM? user_claims { get; set; }

        public string? record_status { get; set; }

        public string? message { get; set; }
    }
}

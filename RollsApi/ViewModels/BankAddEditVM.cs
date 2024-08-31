using System.ComponentModel;

namespace RollsApi.ViewModels
{
  public class BankAddEditVM
  {
    public long? bank_id { get; set; }

    [DisplayName("Bank Name")]
    public string bank_name { get; set; }
    public IList<string>? errors { get; set; }

    public UserClaimVM? user_claims { get; set; }

    public string? record_status { get; set; }

    public string? message { get; set; }
  }
}

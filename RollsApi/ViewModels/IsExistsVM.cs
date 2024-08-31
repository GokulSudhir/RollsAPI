namespace RollsApi.ViewModels
{
  public class IsExistsVM
  {
    public long? id { get; set; }
    public long? id2 { get; set; }
    public string? str1 { get; set; }
    public string? str2 { get; set; }

    public string? search_string { get; set; }
    public string? table_name { get; set; }
    public string? id_field { get; set; }
    public string? name_field { get; set; }
  }
}

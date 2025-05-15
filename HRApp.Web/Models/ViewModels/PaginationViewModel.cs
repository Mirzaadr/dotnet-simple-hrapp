namespace HRApp.Web.Models.ViewModels;

public class PaginationViewModel
{
  public int CurrentPage { get; set; }
  public int TotalPages { get; set; }
  public string? SearchTerm { get; set; }
}
